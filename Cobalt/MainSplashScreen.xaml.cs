using Cobalt.FileIO;
using Cobalt.FileIO.CFG;
using Cobalt.FileIO.DL;
using Cobalt.Parser;
using Cobalt.Population;
using Cobalt.Windows;
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

            mainWindow = new MainWindow();
            mainWindow.Hide();

            showRandomSplash(6);
        }

        public void showRandomSplash(int max)
        {
            eLabelVersion.Content = String.Format("Cobalt v{0}", Properties.Settings.Default.VERSION_STRING);
            eLabel.Content = Properties.Settings.Default.LOAD_ITEM;
            int r = new Random().Next(0, max);
            eImage.Source = new BitmapImage(new Uri(String.Format("/Resources/Splash/{0}.png", r), UriKind.Relative));
        }

        private async void Window_ContentRendered(object sender, EventArgs e)
        {
            //컨픽
            MainConfig.loadConfig();
            
            foreach (string file in Directory.GetFiles(Properties.Settings.Default.PATH_CFG_MAP))
            {
                MapConfig.loadConfig(file);
            }
            

            //선언
            var Schema = new SchemaParser();
            var Template = new TemplateParser();
            var icodl = new IconDownloader();
            var fff = new PopProjectFile();

            fff.Import(FileFunction.RelativeURL("population/mvm_mannworks_expert1.pop"));

            //다운로더 컨텐츠 대상 설정
            icodl.Progress = eBar;
            icodl.TextBox = eLabel;
            await Schema.readFromURL(String.Format(Properties.Settings.Default.Format_Schema,
                Properties.Settings.Default.API_KEY,
                Properties.Settings.Default.API_LANG)); //스캐마 파싱

            await icodl.download(); //아이콘 다운로드
            //await p_Template.parse(); //템플릿 파싱
            //await initP.initTemplate(p_Template);
            mainWindow.Show();
            Close();
        }
    }
}
