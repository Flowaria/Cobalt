using Cobalt.FileIO.DL;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace Cobalt.FileIO.CFG
{
    public static class XmlConfig
    {
        public static string API_KEY;
        public static string API_LANG;
        

        public static void loadConfig()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Properties.Settings.Default.PATH_CFG + "config.xml");

            XmlElement rNode = doc.DocumentElement;
            XmlNodeList NodeApi = rNode.GetElementsByTagName("API");
            XmlNodeList NodeMessage = rNode.GetElementsByTagName("MESSAGE");

            foreach (XmlNode node in NodeApi)
            {

                string Key = node["Key"].InnerText;

                if (Key.Equals("EDITPLEASE") || !Downloader.CheckValidURL("http://api.steampowered.com/IEconItems_440/GetSchemaURL/v1/?key=" + Key))
                {
                    MessageBox.Show("Key 값 : " + Key + "이 이상합니다.\nconfig.xml 파일을 다시 확인해주세요.", "닫는중...",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                    Environment.Exit(-1);
                }

                API_KEY = Key;

                API_LANG = node["BaseLang"].InnerText;
            }
        }

        public static async Task saveConfig()
        {

        }
    }
}
