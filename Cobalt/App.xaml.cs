using Cobalt.Extension;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml;
using TF2.Info;
using TF2.Items;
using Valve.KeyValue;

namespace Cobalt
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            InitResource();
            InitConfig();
            ItemsInfo.ItemsHandler += NodeToTFItem;
        }

        public void InitResource()
        {
            if (!Directory.Exists("resource/backpack-image"))
                Directory.CreateDirectory("resource/backpack-image");
            if (!Directory.Exists("resource/schema"))
                Directory.CreateDirectory("resource/schema");

            //Export Included Resource
            foreach (var file in System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames())
            {
                if(file.StartsWith("Cobalt.Resources.Export."))
                {
                    string basedir = file.Replace("Cobalt.Resources.Export.", "");
                    string[] splited = basedir.Split('.');
                    string dir = Path.Combine(String.Join("/", splited.Take(splited.Length - 2)), String.Join(".", splited[splited.Length - 2], splited[splited.Length - 1]));
                    if (!File.Exists(dir))
                    {
                        if(!Directory.Exists(Path.GetDirectoryName(dir)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(dir));
                        }

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

        private TFItem NodeToTFItem(XmlNode node)
        {
            if (node["item_class"] != null
                && node["defindex"] != null
                && node["name"] != null
                && node["item_slot"] != null
                && !node["item_slot"].InnerText.Equals("action")
                && node["image_url"] != null
                && node["image_url"].InnerText.StartsWith("http://media.steampowered.com")
                && node["item_name"] != null
                && node["item_class"] != null
                && !node["item_class"].InnerText.Equals("no_entity")
                )
            {
                TFItem item = null;
                if (node["used_by_classes"] != null)
                {
                    item = new TFItem(node["item_class"].InnerText, int.Parse(node["defindex"].InnerText), node["name"].InnerText, TFEnumConvert.StringToSlot(node["item_slot"].InnerText), node["image_url"].InnerText, node["item_name"].InnerText, false);
                    foreach (XmlNode node2 in node.SelectNodes("used_by_classes"))
                    {
                        TFClass cls = TFEnumConvert.StringToTFClass(node2.InnerText);
                        if (cls != TFClass.None)
                        {
                            item.UsedByClassToggle(cls);
                        }
                    }
                }
                else
                {
                    item = new TFItem(node["item_class"].InnerText, int.Parse(node["defindex"].InnerText), node["name"].InnerText, TFEnumConvert.StringToSlot(node["item_slot"].InnerText), node["image_url"].InnerText, node["item_name"].InnerText, true);
                }
                return item;
            }
            return null;
        }
    }
}
