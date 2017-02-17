using Cobalt.FileIO.DL;
using Cobalt.Properties;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;

namespace Cobalt.FileIO.CFG
{
    public static class MainConfig
    {
        static MainConfig()
        {
            if (FileFunction.ExportString(Resources.config, Settings.Default.PATH_CFG, "config.ini") == FileFunction.Status.Success)
            {
                MessageBox.Show("최초 실행입니다. config.xml 파일을 수정해주세요", "닫는중...",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                Environment.Exit(-1);
            }
        }

        public static void loadConfig()
        {
            string url = FileFunction.RelativeURL(Properties.Settings.Default.PATH_CFG + "config.ini");
            Console.WriteLine(url);
            iniUtil ini = new iniUtil(url);

            //Key
            string key = ini.Get("API", "Key");
            Console.WriteLine(key);
            if (key.Equals("EDITPLEASE") || !Downloader.CheckValidURL("http://api.steampowered.com/IEconItems_440/GetSchemaURL/v1/?key=" + key))
            {
                MessageBox.Show("Key 값 : " + key + "이 이상합니다.\nconfig.xml 파일을 다시 확인해주세요.", "닫는중...",
                MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(-1);
            }
            Settings.Default.API_KEY = key;
        }
    }
}
