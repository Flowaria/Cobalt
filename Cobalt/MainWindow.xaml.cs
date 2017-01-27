using Cobalt.Parser;
using Cobalt.Src;
using Cobalt.Src.Parser;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;

namespace Cobalt
{
    public partial class MainWindow : Window
    {
        public Boolean projectOpened = false;
        private SchemaParser p_Schema;
        private TemplateParser p_Template;
        private LoadingWindow w_Loading;

        public MainWindow()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            this.Hide();

            p_Schema = new SchemaParser();
            p_Template = new TemplateParser();
            w_Loading = new LoadingWindow();

            //리소스 불러오기  
            initConfig();
            initResource();   
        }

        public async Task initResource()
        {
            InitParser initP = new InitParser(w_Loading.eBar, w_Loading.eLabel);
            w_Loading.Show();
            await p_Schema.parse(); //스캐마 파싱
            await initP.initSchema(p_Schema);
            await p_Template.parse(); //템플릿 파싱
            await initP.initTemplate(p_Template);
            w_Loading.Close();
            this.Show();
        }

        public void initConfig()
        {
            Properties.Settings Settings = Properties.Settings.Default;

            //콘피그 파일이 없으면 만들어라
            if (!File.Exists(Settings.PATH_CFG + "config.xml"))
            {
                //디렉토리 생성
                if (!Directory.Exists(Settings.PATH_CFG))
                    Directory.CreateDirectory(Settings.PATH_CFG);

                //파일 추출
                File.WriteAllText(Settings.PATH_CFG + "config.xml", Properties.Resources.config);

                MessageBox.Show("최초 실행입니다. config.xml 파일을 수정해주세요", "닫는중...",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                Environment.Exit(-1);
            }

            //파일 로드
            XmlDocument doc = new XmlDocument();
            doc.Load(Properties.Settings.Default.PATH_CFG + "config.xml");

            XmlElement rNode = doc.DocumentElement;
            XmlNodeList NodeApi = rNode.GetElementsByTagName("API");
            XmlNodeList NodeMessage = rNode.GetElementsByTagName("MESSAGE");

            foreach (XmlNode node in NodeApi)
            {

                string Key = node["Key"].InnerText;

                if (Key.Equals("EDITPLEASE") || !Downloader.CheckValidURL("http://api.steampowered.com/IEconItems_440/GetSchemaURL/v1/?key=" + Key))
                {
                    MessageBox.Show("Key 값 : " + Key + "이 이상합니다.\nconfig.xml 파일을 다시 확인해주세요.", "닫는중...",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                    Environment.Exit(-1);
                }

                Settings.API_KEY = Key;

                Settings.API_LANG = node["BaseLang"].InnerText;
            }

            foreach (XmlNode node in NodeMessage)
            {
                Settings.LOAD_ITEM = node["ItemSchemaLoad"].InnerText;
                Settings.Load_Item_Image = node["ItemImageDownload"].InnerText;
                Settings.LOAD_TEMPLATE = node["TemplateLoad"].InnerText;
            }

            //Properties.Settings.Default.Save();
        }

        //웨이브 버튼 창 슬라이더를 다시 돌려놓기
        private void waveSlider_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var slider = sender as Slider;slider.Value = 1.0;
        }
    }
}
