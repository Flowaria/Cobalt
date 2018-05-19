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
            //var icon = new TFBotIcon("resource/image/loch_rotate_darker.png");
            //icon.IsGiant = true;
            //icon.IsCrit = true;
            //AddChild(icon);
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
            await ItemsInfo.ReadSchema();
            
            //Items Image
            eLabel.Content = Translation.Get("checksum_itemimage");
            var items = ItemsInfo.Items;
            var dl_new = new List<string>();
            await Task.Factory.StartNew(delegate
            {
                foreach (var item in items)
                {
                    var path = Path.Combine("resource/backpack-image", item.GetImageName());
                    var url = item.GetImageURL();
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

            if (dl_new.Count > 0)
            {
                eLabel.Content = Translation.Get("download_itemimage");

                var dl = new IconDownloader();
                dl.Progress = eBar;
                dl.TextBox = eLabel;
                await dl.download(dl_new, "resource/backpack-image/");
            }

            //메인 윈도우 가동
            var mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
