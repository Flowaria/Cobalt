using Cobalt.FileIO;
using Cobalt.FileIO.CFG;
using Cobalt.FileIO.DL;
using Cobalt.Parser;
using Cobalt.Population;
using Cobalt.Windows;
using Flowaria.Translation;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Cobalt
{
    /// <summary>
    /// LoadingWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainSplashScreen : Window
    {
        private MainWindow mainWindow;


        public MainSplashScreen()
        {
            InitializeComponent();
            Translation.LoadDefaultFile("translation/ko.xml");
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
            var Schema = new SchemaParser();
            var Template = new TemplateParser();
            var icodl = new IconDownloader();
            var fff = new PopProjectFile();
            var mCfg = new MapConfig();

            //컨픽
            MainConfig.loadConfig();

            foreach (string file in Directory.GetFiles(Properties.Settings.Default.PATH_CFG_MAP))
            {
                await mCfg.loadConfigAsync(file);
            }
            //fff.Import(FileFunction.RelativeURL("population/mvm_mannworks_expert1.pop"));

            eLabel.Content = Translation.Get("loading_schema");
            
            //다운로더 컨텐츠 대상 설정
            icodl.Progress = eBar;
            icodl.TextBox = eLabel;
            await Schema.readFromURL(String.Format(Properties.Settings.Default.Format_Schema,
                Properties.Settings.Default.API_KEY,
                Properties.Settings.Default.API_LANG)); //스캐마 파싱

            await icodl.download(); //아이콘 다운로드
            //await p_Template.parse(); //템플릿 파싱
            //await initP.initTemplate(p_Template);

            //메인 윈도우 가동
            mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
