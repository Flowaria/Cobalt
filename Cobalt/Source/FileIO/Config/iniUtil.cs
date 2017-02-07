using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Cobalt.FileIO.CFG
{
    public class iniUtil
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        private string path;

        public iniUtil(string filepath)
        {
            path = filepath;
        }

        public string Get(string section, string key, string def = null)
        {
            StringBuilder temp = new StringBuilder();
            GetPrivateProfileString(section, key, def, temp, 128, path);
            return temp.ToString();
        }

        public void Set(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, path);
        }
    }
}
