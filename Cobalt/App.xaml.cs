using Cobalt.Extension;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml;
using TF2.Items;

namespace Cobalt
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            InitResource();
            InitConfig();
        }

        public void InitResource()
        {
            Directory.CreateDirectory("Resource/Schema");
            Directory.CreateDirectory("Resource/Backpack-Image");
            Directory.CreateDirectory("Resource/Boticons/Image");
            Directory.CreateDirectory("Resource/Boticons/Plugin");

            //Export Included Resource
            foreach (var file in System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames())
            {
                if(file.StartsWith("Cobalt.Resources.Export."))
                {
                    string basedir = file.Replace("Cobalt.Resources.Export.", "");
                    string[] splited = basedir.Split('.');
                    string dir = Path.Combine(String.Join("/", splited.Take(splited.Length - 2)), String.Join(".", splited[splited.Length - 2], splited[splited.Length - 1]));

                    if (!Directory.Exists(Path.GetDirectoryName(dir)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(dir));
                    }

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
        }

        public void InitConfig()
        {
            try
            {
                Translation.LoadDefaultFile("translation/default.xml");
                XmlDocument doc = new XmlDocument();
            
                doc.Load("config/config.xml");
                var node = doc.DocumentElement;

                //Ui 언어
                if (node["UiLang"] != null)
                {
                    string path = String.Format("translation/{0}.xml", node["UiLang"].InnerText);
                    if (!Translation.LoadTranslationFile(path))
                    {
                        MessageBox.Show(Translation.Get("msg_ui_translation_call_fail").Replace("\n", Environment.NewLine), Translation.Get("msg_warn_title"), MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }

                //Api키, 스캐마 언어
                if (node["ApiKey"] != null && node["ApiKey"].InnerText != null && node["ItemLang"] != null)
                {
                    if(!ItemsInfo.Register(node["ApiKey"].InnerText, node["ItemLang"].InnerText))
                    {
                        MessageBox.Show(Translation.Get("msg_config_apikey_fail"), Translation.Get("msg_error_title"), MessageBoxButton.OK, MessageBoxImage.Error);
                        Environment.Exit(-1);
                    }
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
