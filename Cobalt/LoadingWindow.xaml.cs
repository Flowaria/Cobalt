using Cobalt.FileIO.CFG;
using Cobalt.FileIO.DL;
using Cobalt.Forms;
using Cobalt.Parser;
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
        private SchemaParser p_Schema;
        private TemplateParser p_Template;

        private MainWindow mainWindow;


        public LoadingWindow()
        {  
            InitializeComponent();

            p_Schema = new SchemaParser();
            p_Template = new TemplateParser();

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
            XmlConfig.loadConfig();
            MapConfig.loadConfig("");
            IconDownloader icodl = new IconDownloader();
            icodl.Progress = eBar;
            icodl.TextBox = eLabel;
            await p_Schema.parse(); //스캐마 파싱
            await icodl.download(); //아이콘 다운로드
            //await p_Template.parse(); //템플릿 파싱
            //await initP.initTemplate(p_Template);
            mainWindow.Show();
            Close();
        }
    }
}
