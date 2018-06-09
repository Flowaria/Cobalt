using Cobalt.Extension;
using Cobalt.FileIO.CFG;
using Cobalt.FileIO.DL;
using Cobalt.UserControls;
using Cobalt.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Xml;
using Valve.TF2.Items;

namespace Cobalt
{
    /// <summary>
    /// LoadingWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SetupScreen : Window
    {
        public SetupScreen()
        {
            InitializeComponent();
        }

        private async void Window_ContentRendered(object sender, EventArgs e)
        {
            //스플래시 이미지 띄우기
            int r = new Random().Next(0, 6);
            eImage.Source = new BitmapImage(new Uri(String.Format("/Resources/Splash/{0}.png", r), UriKind.Relative));

            //컨픽
            /*eLabel.Content = Translation.Get("loading_mapsetting");
            foreach (string file in Directory.GetFiles(Properties.Settings.Default.PATH_CFG_MAP))
            {
                await mCfg.loadConfigAsync(file);
            }*/

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
            foreach (var item in items)
            {
                var path = Path.Combine("resource/backpack-image", item.ImageName);
                var url = item.ImageURL;
                if (File.Exists(path))
                {
                    if (result == FetchSchemaResult.SUCCESS)
                    {
                        using (var wc = new WebClient())
                        {
                            wc.Encoding = Encoding.UTF8;
                            if (!File.ReadAllBytes(path).SequenceEqual(await wc.DownloadDataTaskAsync(url)))
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
    }
}
