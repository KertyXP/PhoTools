using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoTools
{
    public static class extensions
    {
        public static int ToInt(this string s, int nDefaultValue = 0)
        {
            int result;
            if (int.TryParse(s, out result))
            {
                return result;
            }
            else
            {
                return nDefaultValue;
            }

        }
    }
}
