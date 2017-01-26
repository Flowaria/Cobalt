using Cobalt.Forms;
using Cobalt.MvM.DB;
using Cobalt.ToolWindow;
using System;
using System.IO;
using System.Net;
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
        private ItemsDB DBItems;
        private TemplateDB DBTemplates;
        private LoadingWindow w_Loading;

        public MainWindow()
        {
            //콘피그 파일이 없으면 만들어라
            if (!File.Exists(Properties.Settings.Default.PATH_CFG+"config.xml"))
            {
                //디렉토리 생성
                if (!Directory.Exists(Properties.Settings.Default.PATH_CFG))
                    Directory.CreateDirectory(Properties.Settings.Default.PATH_CFG);

                //파일 추출
                File.WriteAllText(Properties.Settings.Default.PATH_CFG + "config.xml",
                    Properties.Resources.config);

                MessageBox.Show("최초 실행입니다. config.xml 파일을 수정해주세요", "닫는중...",
                    MessageBoxButton.OK, MessageBoxImage.Warning);

                Application.Current.Shutdown();
            }
                
            readConfig();
            
            InitializeComponent();
            //작업표시줄 넘지 않게 설정
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            Hide();

            //로딩화면 띄우기
            w_Loading = new LoadingWindow();
            w_Loading.Show();

            //리소스 불러오기
            initResource();
        }

        public async Task initResource()
        {
            DBItems = new ItemsDB();
            await DBItems.initDB();
            await w_Loading.initItemsResource(DBItems);

            DBTemplates = new TemplateDB();
            await w_Loading.initTemplateResource(DBTemplates);
            w_Loading.Close();
            Show();
        }

        public void readConfig()
        {
            //파일 로드
            XmlDocument doc = new XmlDocument();
            doc.Load(Properties.Settings.Default.PATH_CFG + "config.xml");

            XmlElement rNode = doc.DocumentElement;
            XmlNodeList NodeApi = rNode.GetElementsByTagName("API");
            XmlNodeList NodeMessage = rNode.GetElementsByTagName("MESSAGE");


            foreach (XmlNode node in NodeApi)
            {
                string Key = node["Key"].InnerText;

                if(Key.Equals("EDITPLEASE") || !CheckValidKey(Key))
                {
                    MessageBox.Show("Key 값 : "+Key+"이 이상합니다.\nconfig.xml 파일을 다시 확인해주세요.", "닫는중...",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                    Application.Current.Shutdown();
                }

                Properties.Settings.Default.API_KEY = Key;

                Properties.Settings.Default.API_LANG = node["BaseLang"].InnerText;
            }

            foreach (XmlNode node in NodeMessage)
            {
                Properties.Settings.Default.LOAD_ITEM = node["ItemSchemaLoad"].InnerText;
                Properties.Settings.Default.LOAD_ITEM_IMAGE = node["ItemImageDownload"].InnerText;
                Properties.Settings.Default.LOAD_TEMPLATE = node["TemplateLoad"].InnerText;
            }
        }

        private bool CheckValidKey(string key)
        {
            try
            {
                HttpWebRequest querry = (HttpWebRequest) WebRequest.Create("http://api.steampowered.com/IEconItems_440/GetSchemaURL/v1/?key=" + key);
                HttpWebResponse response = (HttpWebResponse) querry.GetResponse();
                response.Close();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                return false;
            }
        }

        //웨이브 버튼 창 슬라이더를 다시 돌려놓기
        private void waveSlider_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var slider = sender as Slider;slider.Value = 1.0;
        }
    }
}
