using Cobalt.FileIO.CFG;
using Flowaria.Translation;
using System;
using System.IO;
using System.Windows;
using System.Xml;

namespace Cobalt
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Console.WriteLine(System.Threading.Thread.CurrentThread.CurrentUICulture);
            if(!Translation.LoadDefaultFile("translation/default.xml"))
            {
                MessageBox.Show("기본 번역 파일이 존재하지 않습니다.", Translation.Get("msg_error_title"), MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(-1);
            }
            if (!File.Exists("config/config.xml"))
            {
                File.WriteAllText("config/config.xml", Cobalt.Properties.Resources.config);
            }

            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load("config/config.xml");
                var node = doc.DocumentElement;
                if(node["UiLang"] != null)
                {
                    string path = String.Format("translation/{0}.xml", node["UiLang"].InnerText);
                    if(!Translation.LoadTranslationFile(path))
                    {
                        MessageBox.Show(Translation.Get("msg_ui_translation_call_fail"), Translation.Get("msg_warn_title"), MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch
            {
                MessageBox.Show(Translation.Get("msg_config_call_fail"), Translation.Get("msg_error_title"), MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(-1);
            }
        } 
    }
}
