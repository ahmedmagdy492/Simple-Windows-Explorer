using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileControl
{
    public static class StringManipulation
    {
        public static string ShortenString(string str)
        {
            return str.Length > 9 ? str.Substring(0, 9) + "." : str;
        }
    }
}
