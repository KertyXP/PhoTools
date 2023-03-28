namespace PhoTools
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.button4 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_rename_date = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.cb_copy = new System.Windows.Forms.CheckBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_grouper_to = new System.Windows.Forms.TextBox();
            this.bt_grouper_to = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_grouper_from = new System.Windows.Forms.TextBox();
            this.bt_grouper_from = new System.Windows.Forms.Button();
            this.bt_jpg_small = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(75, 184);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(151, 23);
            this.button4.TabIndex = 25;
            this.button4.Text = "Rename By Date";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "From";
            // 
            // tb_rename_date
            // 
            this.tb_rename_date.Location = new System.Drawing.Point(74, 158);
            this.tb_rename_date.Name = "tb_rename_date";
            this.tb_rename_date.Size = new System.Drawing.Size(262, 20);
            this.tb_rename_date.TabIndex = 23;
            this.tb_rename_date.TextChanged += new System.EventHandler(this.tb_rename_date_TextChanged);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(342, 157);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(57, 23);
            this.button5.TabIndex = 22;
            this.button5.Text = "...";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // cb_copy
            // 
            this.cb_copy.AutoSize = true;
            this.cb_copy.Checked = true;
            this.cb_copy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_copy.Location = new System.Drawing.Point(9, 64);
            this.cb_copy.Name = "cb_copy";
            this.cb_copy.Size = new System.Drawing.Size(50, 17);
            this.cb_copy.TabIndex = 21;
            this.cb_copy.Text = "Copy";
            this.cb_copy.UseVisualStyleBackColor = true;
            this.cb_copy.Click += new System.EventHandler(this.cb_copy_CheckedChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(75, 64);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(262, 23);
            this.progressBar1.TabIndex = 20;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(343, 64);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(57, 23);
            this.button3.TabIndex = 19;
            this.button3.Text = "Proceed";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "To";
            // 
            // tb_grouper_to
            // 
            this.tb_grouper_to.Location = new System.Drawing.Point(75, 38);
            this.tb_grouper_to.Name = "tb_grouper_to";
            this.tb_grouper_to.Size = new System.Drawing.Size(262, 20);
            this.tb_grouper_to.TabIndex = 17;
            this.tb_grouper_to.TextChanged += new System.EventHandler(this.tb_grouper_to_TextChanged);
            // 
            // bt_grouper_to
            // 
            this.bt_grouper_to.Location = new System.Drawing.Point(343, 37);
            this.bt_grouper_to.Name = "bt_grouper_to";
            this.bt_grouper_to.Size = new System.Drawing.Size(57, 23);
            this.bt_grouper_to.TabIndex = 16;
            this.bt_grouper_to.Text = "...";
            this.bt_grouper_to.UseVisualStyleBackColor = true;
            this.bt_grouper_to.Click += new System.EventHandler(this.bt_grouper_to_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "From";
            // 
            // tb_grouper_from
            // 
            this.tb_grouper_from.Location = new System.Drawing.Point(75, 12);
            this.tb_grouper_from.Name = "tb_grouper_from";
            this.tb_grouper_from.Size = new System.Drawing.Size(262, 20);
            this.tb_grouper_from.TabIndex = 14;
            // 
            // bt_grouper_from
            // 
            this.bt_grouper_from.Location = new System.Drawing.Point(343, 11);
            this.bt_grouper_from.Name = "bt_grouper_from";
            this.bt_grouper_from.Size = new System.Drawing.Size(57, 23);
            this.bt_grouper_from.TabIndex = 13;
            this.bt_grouper_from.Text = "...";
            this.bt_grouper_from.UseVisualStyleBackColor = true;
            this.bt_grouper_from.Click += new System.EventHandler(this.bt_grouper_from_Click);
            // 
            // bt_jpg_small
            // 
            this.bt_jpg_small.Location = new System.Drawing.Point(248, 184);
            this.bt_jpg_small.Name = "bt_jpg_small";
            this.bt_jpg_small.Size = new System.Drawing.Size(151, 23);
            this.bt_jpg_small.TabIndex = 26;
            this.bt_jpg_small.Text = "Resize to small";
            this.bt_jpg_small.UseVisualStyleBackColor = true;
            this.bt_jpg_small.Click += new System.EventHandler(this.bt_jpg_small_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.bt_jpg_small);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_rename_date);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.cb_copy);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_grouper_to);
            this.Controls.Add(this.bt_grouper_to);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_grouper_from);
            this.Controls.Add(this.bt_grouper_from);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_rename_date;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.CheckBox cb_copy;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_grouper_to;
        private System.Windows.Forms.Button bt_grouper_to;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_grouper_from;
        private System.Windows.Forms.Button bt_grouper_from;
        private System.Windows.Forms.Button bt_jpg_small;
    }
}

