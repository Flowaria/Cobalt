using Cobalt.Config;
using Cobalt.Forms.Tools;
using Cobalt.Parser;
using Cobalt.Src;
using Cobalt.Src.Parser;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace Cobalt
{
    public partial class MainWindow : Window
    {
        public Boolean projectOpened = false;
        private SchemaParser p_Schema;
        private TemplateParser p_Template;
        private LoadingWindow w_Loading;
        private Navigator nav;

        public MainWindow()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            this.Hide();

            p_Schema = new SchemaParser();
            p_Template = new TemplateParser();
            w_Loading = new LoadingWindow();
            nav = new Navigator();

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
            //await p_Template.parse(); //템플릿 파싱
            //await initP.initTemplate(p_Template);
            w_Loading.Close();
            this.Show();
            nav.Show();
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
            XmlConfig.loadConfig();

            //Properties.Settings.Default.Save();
        }
    }
}
