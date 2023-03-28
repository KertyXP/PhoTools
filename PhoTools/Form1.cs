using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAPICodePack.Taskbar;


namespace PhoTools
{
    public partial class Form1 : Form
    {
        int nGaugeRealValue = 0;

        photo.exif.Parser oParser = new photo.exif.Parser();

        public Form1()
        {
            InitializeComponent();
            this.tb_grouper_from.Text = Properties.Settings.Default.pathFrom;
            this.tb_grouper_to.Text = Properties.Settings.Default.pathTo;
            this.tb_rename_date.Text = Properties.Settings.Default.pathTo2;
        }

        private void bt_grouper_from_Click(object sender, EventArgs e)
        {
            var oDialog = new CommonOpenFileDialog();
            oDialog.IsFolderPicker = true;

            var oResult = oDialog.ShowDialog();
            if (oResult == CommonFileDialogResult.Ok)
            {
                var oFolder = oDialog.FileName;
                this.tb_grouper_from.Text = oFolder;
                Properties.Settings.Default.pathFrom = this.tb_grouper_from.Text;
                Properties.Settings.Default.Save();

            }
        }

        private void bt_grouper_to_Click(object sender, EventArgs e)
        {
            var oDialog = new CommonOpenFileDialog();
            oDialog.IsFolderPicker = true;

            var oResult = oDialog.ShowDialog();
            if (oResult == CommonFileDialogResult.Ok)
            {
                var oFolder = oDialog.FileName;
                this.tb_grouper_to.Text = oFolder;

                Properties.Settings.Default.pathTo = this.tb_grouper_to.Text;
                Properties.Settings.Default.Save();

            }
        }

        private void InokeOnMainThread(Action o)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => { o.Invoke(); }));
            }
            else
            {
                o.Invoke();
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            this.Enabled = false;


            await Task.Run(() =>
            {

                string sFrom = this.tb_grouper_from.Text;
                string sTo = this.tb_grouper_to.Text;

                if (Directory.Exists(sFrom) == false)
                {
                    MessageBox.Show("Invalid Path (from)");
                }
                if (string.IsNullOrEmpty(sTo))
                {
                    MessageBox.Show("'To' cannot be empty");
                }
                try
                {
                    Directory.CreateDirectory(sTo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(" could not create directory in 'To' (" + ex.Message + ")");
                }

                var aFiles = Directory.GetFiles(sFrom, "*.*", SearchOption.AllDirectories);
                int nCount = aFiles.Count();

                SetGauge(0, nCount * 2);



                Dictionary<string, DateTime> aDic = new Dictionary<string, DateTime>();
                foreach (var oFile in aFiles)
                {
                    
                    var sExtension = Path.GetExtension(oFile).ToLower();
                    var sFileName = Path.GetFileNameWithoutExtension(oFile).ToLower();

                    if (aDic.ContainsKey(sFileName) == false)
                    {
                        if (sExtension == ".jpg")
                        {
                            var oExif = oParser.Parse(oFile);
                            var sDT = oExif.FirstOrDefault(x => x.Title == "ExifDTOrig").Value.ToString().Replace("\0", "");
                            try
                            {
                                DateTime oDT = DateTime.ParseExact(sDT, "yyyy:MM:dd HH:mm:ss", CultureInfo.InvariantCulture); // 2019:01:20 13:18:51
                                aDic.Add(sFileName, oDT);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }

                    SetGauge(nGaugeRealValue + 1);
                }

                foreach (var sFile in aFiles)
                {
                    var sExtension = Path.GetExtension(sFile).ToLower();
                    var sFileName = Path.GetFileNameWithoutExtension(sFile).ToLower().Replace(".nef", "");

                    string sDirectory = string.Empty;

                    if (aDic.ContainsKey(sFileName) == false)
                    {
                        sDirectory = $"{sTo}\\Unclassified";
                    }
                    else
                    {
                        string sToWithDate = sTo + "\\" + aDic[sFileName].ToString("yyyy-MM-dd");

                        if (sExtension == ".nef" || sExtension == ".dop")
                        {
                            sDirectory = $"{sToWithDate}\\nef";
                        }
                        else if (sExtension == ".jpg" || sExtension == ".jpeg")
                        {

                            var oExif = oParser.Parse(sFile);

                            var nWidth = oExif.FirstOrDefault(x => x.Title == "ExifPixXDim").Value.ToString().ToInt();
                            var nHeight = oExif.FirstOrDefault(x => x.Title == "ExifPixYDim").Value.ToString().ToInt();
                            if (nWidth * nHeight == 0)
                            {
                                sDirectory = $"{sToWithDate}\\Unclassified";
                            }
                            else if (nWidth * nHeight < 4000000)
                            {
                                sDirectory = $"{sToWithDate}\\jpg small";
                            }
                            else
                            {
                                sDirectory = $"{sToWithDate}\\jpg full";
                            }
                        }

                    }

                    if (string.IsNullOrEmpty(sDirectory) == false)
                    {
                        Directory.CreateDirectory(sDirectory);
                        string sFullPathTo = $"{sDirectory}\\{Path.GetFileName(sFile)}";
                        if (File.Exists(sFullPathTo))
                        {
                            File.Delete(sFullPathTo);
                        }
                        if (this.cb_copy.Checked)
                        {
                            File.Copy(sFile, sFullPathTo);
                        }
                        else
                        {
                            File.Move(sFile, $"{sDirectory}\\{Path.GetFileName(sFile)}");
                        }
                    }

                    SetGauge(nGaugeRealValue + 1);
                }

            });
            this.Enabled = true;
            SetGauge(0, 100);
        }

        private void SetGauge(int nValue, int nMax = 0)
        {
            nGaugeRealValue = nValue;

            if (nGaugeRealValue - this.progressBar1.Value < 10 && nValue > 0)
                return;


            nGaugeRealValue = nValue;

            InokeOnMainThread(() =>
            {
                if (nMax > 0)
                {
                    this.progressBar1.Maximum = nMax;
                }

                this.progressBar1.Value = nValue;
                TaskbarManager.Instance.SetProgressValue(this.progressBar1.Value, this.progressBar1.Maximum, Handle);
            });
        }


        private void cb_copy_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_copy.Text = this.cb_copy.Checked ? "Copy" : "Move";
        }

        private void button5_Click(object sender, EventArgs e)
        {

            var oDialog = new CommonOpenFileDialog();
            oDialog.IsFolderPicker = true;

            var oResult = oDialog.ShowDialog();
            if (oResult == CommonFileDialogResult.Ok)
            {
                var oFolder = oDialog.FileName;
                this.tb_rename_date.Text = oFolder;

                Properties.Settings.Default.pathTo2 = this.tb_rename_date.Text;
                Properties.Settings.Default.Save();

            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {

            this.Enabled = false;


            await Task.Run(() =>
            {

                string sFrom = this.tb_rename_date.Text;

                if (Directory.Exists(sFrom) == false)
                {
                    MessageBox.Show("Invalid Path (from)");
                    return;
                }

                var aFiles = Directory.GetFiles(sFrom, "*.*", SearchOption.AllDirectories);
                int nCount = aFiles.Count();

                SetGauge(0, nCount * 2);


                Dictionary<string, List<string>> aDicDateCount = new Dictionary<string, List<string>>();

                foreach (var oFile in aFiles)
                {

                    var sExtension = Path.GetExtension(oFile).ToLower();
                    var sFileName = Path.GetFileNameWithoutExtension(oFile).ToLower();

                    if (sExtension == ".jpg" || sExtension == ".jpeg")
                    {
                        if (aDicDateCount.Any(dic => dic.Value.Any(d2 => d2 == sFileName) == true) == false) // file name already processed
                        {
                            var oExif = oParser.Parse(oFile);
                            var oExifDT = oExif.FirstOrDefault(x => x.Title == "ExifDTOrig");
                            if (oExifDT != null)
                            {

                                var sDTExif = oExifDT.Value.ToString().Replace("\0", "");
                                try
                                {
                                    DateTime oDT = DateTime.ParseExact(sDTExif, "yyyy:MM:dd HH:mm:ss", CultureInfo.InvariantCulture); // 2019:01:20 13:18:51
                                    string sDT = oDT.ToString("yyyy-MM-dd HH-mm-ss");

                                    if (aDicDateCount.ContainsKey(sDT) == true) // same date for different files
                                    {
                                        aDicDateCount[sDT].Add(sFileName);
                                    }
                                    else
                                    {
                                        aDicDateCount.Add(sDT, new List<string>() { sFileName });
                                    }
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                        }
                        else
                        {

                        }
                    }

                    SetGauge(nGaugeRealValue + 1, 0);
                }


                foreach (var sFile in aFiles)
                {
                    var sExtension = Path.GetExtension(sFile).ToLower();
                    if (sExtension == ".db" || sExtension == ".mts" || sExtension == ".mp4" || sExtension == ".mov" || sExtension == ".nomedia" || sExtension == ".pdf")
                        continue;
                    if (sExtension != ".jpg" && sExtension != ".jpeg" && sExtension != ".nef" && sExtension != ".dop")
                        continue;
                    var sDirectory = Path.GetDirectoryName(sFile);
                    var sFileName = Path.GetFileNameWithoutExtension(sFile).ToLower().Replace(".nef", "");
                    var o = aDicDateCount.FirstOrDefault(d => d.Value.Any(v => v == sFileName));

                    if (o.Value == null)
                        continue;

                    if (o.Value == null && (sExtension == ".nef" || sExtension == ".dop"))
                        continue; // unprocessed nef
                    if (o.Value == null && sExtension == ".nef")
                        continue; // unprocessed nef
                    var sNewName = o.Key;
                    if (o.Value.Count > 10)
                        sNewName += "_" + o.Value.IndexOf(sFileName).ToString("2d");
                    if (o.Value.Count > 1)
                        sNewName += "_" + o.Value.IndexOf(sFileName).ToString("d");
                    sNewName += sExtension;

                    File.Move(sFile, $"{sDirectory}\\{sNewName}");

                    SetGauge(nGaugeRealValue + 1);


                }
            });
            this.Enabled = true;
            SetGauge(0, 100);
        }

        private void tb_rename_date_TextChanged(object sender, EventArgs e)
        {

        }

        private void tb_grouper_to_TextChanged(object sender, EventArgs e)
        {

        }

        private async void bt_jpg_small_Click(object sender, EventArgs e)
        {

            this.Enabled = false;


            await Task.Run(() =>
            {

                string sFrom = this.tb_rename_date.Text;

                if (Directory.Exists(sFrom) == false)
                {
                    MessageBox.Show("Invalid Path (from)");
                    return;
                }

                string sDirFull = Directory.Exists(sFrom + "\\jpg full") ? sFrom + "\\jpg full" : sFrom + "\\jpeg full";
                string sDirSmall = Directory.Exists(sFrom + "\\jpg small") ? sFrom + "\\jpg small" : sFrom + "\\jpeg small";

                var aFilesFull = Directory.GetFiles(sDirFull, "*.*", SearchOption.AllDirectories);
                var aFilesSmall = Directory.GetFiles(sDirSmall, "*.*", SearchOption.AllDirectories).Select(s => Path.GetFileName(s).ToLower());
                int nCount = aFilesFull.Count();

                SetGauge(0, nCount * 2);


                List<string> aFilesJPGFullToResize = new List<string>();

                foreach (var oFile in aFilesFull)
                {
                    var sExtension = Path.GetExtension(oFile).ToLower();
                    var sFileName = Path.GetFileName(oFile).ToLower();

                    if (sExtension == ".jpg" || sExtension == ".jpeg")
                    {
                        if(aFilesSmall.Any(f => f == sFileName) == false)
                        {
                            aFilesJPGFullToResize.Add(oFile);
                        }
                    }

                    SetGauge(nGaugeRealValue + 1, 0);
                }

                var myImageCodecInfo = GetEncoderInfo("image/jpeg");

                System.Drawing.Imaging.Encoder myEncoder;
                EncoderParameter myEncoderParameter;
                EncoderParameters myEncoderParameters;
                myEncoder = System.Drawing.Imaging.Encoder.Quality;
                myEncoderParameters = new EncoderParameters(1);

                myEncoderParameter = new EncoderParameter(myEncoder, 75L);
                myEncoderParameters.Param[0] = myEncoderParameter;

                foreach (var sFileToResize in aFilesJPGFullToResize)
                {
                    using (var img = Image.FromFile(sFileToResize))
                    {
                        //var oExif = oParser.Parse(sFileToResize);


                        var nWidth = img.Width;
                        var nHeight = img.Height;
                        double dRatio = 1;
                        if (nWidth > nHeight)
                        {
                            dRatio = (double)1920 / nWidth;
                        }
                        else
                        {
                            dRatio = (double)1920 / nHeight;
                        }
                        int nNewWidth = (int)(nWidth * dRatio);
                        int nNewHeight = (int)(nHeight * dRatio);

                        using (var oNewIMG = new Bitmap(nNewWidth, nNewHeight))
                        {
                            using (var graph = Graphics.FromImage(oNewIMG))
                            {

                                foreach (var id in img.PropertyIdList)
                                    oNewIMG.SetPropertyItem(img.GetPropertyItem(id));
                                oNewIMG.SetResolution(nNewWidth, nNewHeight);

                                graph.DrawImage(img, 0, 0, nNewWidth, nNewHeight);

                                string sTo = sFrom + "\\jpg small\\" + Path.GetFileName(sFileToResize);
                                oNewIMG.Save(sTo, myImageCodecInfo, myEncoderParameters);

                            }
                        }
                    }
                }


                //foreach (var sFile in aFiles)
                //{
                //    var sExtension = Path.GetExtension(sFile).ToLower();
                //    if (sExtension == ".db" || sExtension == ".mts" || sExtension == ".mp4" || sExtension == ".mov" || sExtension == ".nomedia" || sExtension == ".pdf")
                //        continue;
                //    if (sExtension != ".jpg" && sExtension != ".nef" && sExtension != ".dop")
                //        continue;
                //    var sDirectory = Path.GetDirectoryName(sFile);
                //    var sFileName = Path.GetFileNameWithoutExtension(sFile).ToLower().Replace(".nef", "");
                //    var o = aDicDateCount.FirstOrDefault(d => d.Value.Any(v => v == sFileName));

                //    if (o.Value == null && (sExtension == ".nef" || sExtension == ".dop"))
                //        continue; // unprocessed nef
                //    if (o.Value == null && sExtension == ".nef")
                //        continue; // unprocessed nef
                //    var sNewName = o.Key;
                //    if (o.Value.Count > 10)
                //        sNewName += "_" + o.Value.IndexOf(sFileName).ToString("2d");
                //    if (o.Value.Count > 1)
                //        sNewName += "_" + o.Value.IndexOf(sFileName).ToString("d");
                //    sNewName += sExtension;

                //    File.Move(sFile, $"{sDirectory}\\{sNewName}");

                //    SetGauge(nGaugeRealValue + 1);


                //}
            });
            this.Enabled = true;
            SetGauge(0, 100);
        }


        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

    }
}
