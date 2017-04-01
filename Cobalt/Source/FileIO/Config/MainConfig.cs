using Cobalt.FileIO.DL;
using Cobalt.Properties;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Xml;

namespace Cobalt.FileIO.CFG
{
    public static class MainConfig
    {
        private static XmlDocument doc = new XmlDocument();
        static MainConfig()
        {
            //콘픽파일 만들기
            if (FileFunction.ExportString(Resources.config, Settings.Default.PATH_CFG, "config.xml") == FileFunction.Status.Success)
            {
                MessageBox.Show("최초 실행입니다. config.xml 파일을 수정해주세요", "닫는중...",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                Environment.Exit(-1);
            }

            //XML 불러오기
            try
            {
                Console.WriteLine(FileFunction.RelativeURL(Settings.Default.PATH_CFG + "config.xml"));
                doc.Load(Settings.Default.PATH_CFG+"config.xml");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                MessageBox.Show("config.xml 파일을 읽는중 문제가 발생하였습니다.\n파일을 다시 확인해주세요.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(-1);
            }
        }

        public static void loadConfig()
        {
            try
            {
                XmlElement rNode = doc.DocumentElement;
                string key = rNode.SelectSingleNode("Key").InnerText;

                switch (key)
                {
                    //Case : null or blank
                    case null:
                    case "":
                        MessageBox.Show("config.xml 파일에서 키값을 불러올수 없습니다...\n파일을 다시 확인해주세요", "닫는중...",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        Environment.Exit(-1);
                        break;
                    //Case : is not edited
                    case "EDITPLEASE":
                        MessageBox.Show("config.xml 파일을 열어 내용을 수정해주세요", "닫는중...",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        Environment.Exit(-1);
                        break;

                    default:
                        if(!Downloader.CheckValidURL("http://api.steampowered.com/IEconItems_440/GetSchemaURL/v1/?key=" + key))
                        {
                            MessageBox.Show("Key 값 : " + key + "이 이상합니다.\nconfig.xml 파일을 다시 확인해주세요.", "닫는중...",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                            Environment.Exit(-1);
                        }
                        else
                        {
                            Settings.Default.API_KEY = key;
                        }
                        break;
                }
            }
            catch
            {
                //TODO : ERRROR HANDLER
            }
        }
    }
}
