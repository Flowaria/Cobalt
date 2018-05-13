using Cobalt.Extension;
using Cobalt.FileIO.CFG;
using Cobalt.FileIO.DL;
using Cobalt.Parser;
using Cobalt.Windows;
using System;
using System.IO;
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
        }

        public void showRandomSplash(int max)
        {
            int r = new Random().Next(0, max);
            eImage.Source = new BitmapImage(new Uri(String.Format("/Resources/Splash/{0}.png", r), UriKind.Relative));
        }

        private async void Window_ContentRendered(object sender, EventArgs e)
        {
            //선언
            var Template = new TemplateParser();
            var icodl = new IconDownloader();
            var mCfg = new MapConfig();

            //컨픽
            eLabel.Content = Translation.Get("loading_mapsetting");
            foreach (string file in Directory.GetFiles(Properties.Settings.Default.PATH_CFG_MAP))
            {
                await mCfg.loadConfigAsync(file);
            }
            //fff.Import(FileFunction.RelativeURL("population/mvm_mannworks_expert1.pop"));

            
            eLabel.Content = Translation.Get("version_schema");
            await ItemsInfo.FetchSchema("resource/schema");

            //다운로더 컨텐츠 대상 설정
            icodl.Progress = eBar;
            icodl.TextBox = eLabel;

            eLabel.Content = Translation.Get("loading_schema");
            //await ItemsInfo.ReadSchema();

            eLabel.Content = Translation.Get("download_itemimage");
            //await ItemsInfo.InitItemImage("resource/items/");

            //메인 윈도우 가동
            var mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
