using Cobalt.FileIO.CFG;
using Cobalt.FileIO.DL;
using Cobalt.Parser;
using Cobalt.Windows;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Cobalt
{
    /// <summary>
    /// LoadingWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoadingWindow : Window
    {
        private MainWindow mainWindow;


        public LoadingWindow()
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
            MapConfig.loadConfig("");

            //선언
            SchemaParser Schema = new SchemaParser();
            TemplateParser Template = new TemplateParser();
            IconDownloader icodl = new IconDownloader();

            //다운로더 컨텐츠 대상 설정
            icodl.Progress = eBar;
            icodl.TextBox = eLabel;
            await Schema.parse(); //스캐마 파싱
            await icodl.download(); //아이콘 다운로드
            //await p_Template.parse(); //템플릿 파싱
            //await initP.initTemplate(p_Template);
            mainWindow.Show();
            Close();
        }
    }
}
