using Cobalt.Extension;
using Cobalt.FileIO.CFG;
using Cobalt.FileIO.DL;
using Cobalt.UserControls;
using Cobalt.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Xml;
using TF2.Info;
using TF2.Items;

namespace Cobalt
{
    /// <summary>
    /// LoadingWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainSplashScreen : Window
    {
        public MainSplashScreen()
        {
            InitializeComponent();
            eLabel.Content = Translation.Get("loading_maPsetting");
            showRandomSplash(6);
        }

        public void showRandomSplash(int max)
        {
            int r = new Random().Next(0, max);
            eImage.Source = new BitmapImage(new Uri(String.Format("/Resources/Splash/{0}.png", r), UriKind.Relative));
        }

        private async void Window_ContentRendered(object sender, EventArgs e)
        {
            //선언
            var mCfg = new MapConfig();

            //컨픽
            eLabel.Content = Translation.Get("loading_mapsetting");
            foreach (string file in Directory.GetFiles(Properties.Settings.Default.PATH_CFG_MAP))
            {
                await mCfg.loadConfigAsync(file);
            }

            //Check Schema Version (redownload)
            eLabel.Content = Translation.Get("version_schema");
            var result = await ItemsInfo.FetchSchema("resource/schema/");

            //Read Schema
            eLabel.Content = Translation.Get("loading_schema");
            ItemsInfo.ItemsHandler += NodeToTFItem;
            ItemsInfo.AttributesHandler += NodeToTFAttribute;
            await ItemsInfo.ReadSchema();
            
            //Items Image
            eLabel.Content = Translation.Get("checksum_itemimage");
            var items = ItemsInfo.Items;
            var dl_new = new List<string>();
            await Task.Factory.StartNew(delegate
            {
                foreach (var item in items)
                {
                    var path = Path.Combine("resource/backpack-image", item.ImageName);
                    var url = item.ImageURL;
                    if (File.Exists(path))
                    {
                        if (result == FetchSchemaResult.SUCCESS)
                        {
                            MD5 md5 = MD5.Create();
                            string hash = Encoding.Default.GetString(md5.ComputeHash(File.ReadAllBytes(path)));
                            using (System.Net.WebClient wc = new System.Net.WebClient())
                            {
                                wc.Encoding = Encoding.UTF8;
                                string net_hash = Encoding.Default.GetString(md5.ComputeHash(wc.DownloadData(url)));
                                if(!net_hash.Equals(hash))
                                {
                                    dl_new.Add(url);
                                }
                            }
                        }
                    }
                    else
                    {
                        dl_new.Add(url);
                    }
                }

            });

            //download new icons
            if (dl_new.Count > 0)
            {
                eLabel.Content = Translation.Get("download_itemimage");

                var dl = new IconDownloader();
                dl.Progress = eBar;
                dl.TextBox = eLabel;
                await dl.download(dl_new, "resource/backpack-image/");
            }

            //

            //메인 윈도우 가동
            var mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
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
                TFItem item = new TFItem();
                item.ClassName = node["item_class"].InnerText;
                item.DefinitionID = int.Parse(node["defindex"].InnerText);
                item.Name = node["name"].InnerText;
                item.DisplayName = node["item_name"].InnerText;
                item.Slot = TFEnumConvert.StringToSlot(node["item_slot"].InnerText);
                item.ImageURL = node["image_url"].InnerText;

                string[] splited = item.ImageURL.Split('/');
                var sp = splited[splited.Length - 1].Split('.');
                item.ImageName = String.Join(".", sp[0], sp[2]);

                if (node["used_by_classes"] != null)
                {
                    foreach (XmlNode node2 in node.SelectNodes("used_by_classes"))
                    {
                        TFClass cls = TFEnumConvert.StringToTFClass(node2.InnerText);
                        if (cls != TFClass.None)
                        {
                            item.UsedByClass(cls);
                        }
                    }
                }
                item.ReadOnly = true;
                return item;
            }
            return null;
        }

        private TFAttribute NodeToTFAttribute(XmlNode node)
        {
            if (node["name"] != null
                && node["defindex"] != null
                && node["attribute_class"] != null
                && node["description_string"] != null
                && node["description_format"] != null
                && node["effect_type"] != null
            )
            {
                TFAttribute attr = new TFAttribute();
                attr.Name = node["name"].InnerText;
                attr.DefinitionID = int.Parse(node["defindex"].InnerText);
                attr.AttributeClass = node["attribute_class"].InnerText;
                attr.Description = node["description_string"].InnerText.Replace("%s1", "{0}") ;
                attr.DescriptionFormat = TFEnumConvert.StringToDescriptionFormat(node["description_format"].InnerText);
                attr.EffectType = TFEnumConvert.StringToEffectType(node["effect_type"].InnerText);
                attr.ReadOnly = true;
                return attr;
            }
            return null;
        }
    }
}
