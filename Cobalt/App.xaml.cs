using Cobalt.FileIO.CFG;
using Flowaria.Translation;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
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
            TextElement.FontFamilyProperty.OverrideMetadata(
                typeof(TextElement),
                new FrameworkPropertyMetadata(
                new FontFamily("TF2 Build")));
                TextBlock.FontFamilyProperty.OverrideMetadata(
                typeof(TextBlock),
                new FrameworkPropertyMetadata(
            new FontFamily("TF2 Build")));
            InitResource();
            InitConfig();
        }

        public void InitResource()
        {
            Console.WriteLine("res");
            var files = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames();
            foreach(var file in files)
            {

                if(file.StartsWith("Cobalt.Resources.Export."))
                {
                    string basedir = file.Replace("Cobalt.Resources.Export.", "");
                    string[] splited = basedir.Split('.');
                    string dir = Path.Combine(String.Join("/", splited.Take(splited.Length - 2)), String.Join(".", splited[splited.Length - 2], splited[splited.Length - 1]));
                    if (!File.Exists(dir))
                    {
                        using (var stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(file))
                        {
                            using (var fileStream = new FileStream(dir, FileMode.Create))
                            {
                                stream.CopyTo(fileStream);
                            }
                        }
                    }
                }
            }
            Console.WriteLine("====");
        }

        public void InitConfig()
        {
            Translation.LoadDefaultFile("translation/default.xml");
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load("config/config.xml");
                var node = doc.DocumentElement;
                if (node["UiLang"] != null)
                {
                    string path = String.Format("translation/{0}.xml", node["UiLang"].InnerText);
                    if (!Translation.LoadTranslationFile(path))
                    {
                        MessageBox.Show(Translation.Get("msg_ui_translation_call_fail").Replace("\n", Environment.NewLine), Translation.Get("msg_warn_title"), MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                if (node["ApiKey"] != null)
                {

                }
                else
                {
                    MessageBox.Show(Translation.Get("msg_config_apikey_fail"), Translation.Get("msg_error_title"), MessageBoxButton.OK, MessageBoxImage.Error);
                    Environment.Exit(-1);
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
