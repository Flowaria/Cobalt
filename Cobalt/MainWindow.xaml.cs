using Cobalt.Forms;
using Cobalt.MvM.DB;
using Cobalt.ToolWindow;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cobalt
{
    public partial class MainWindow : Window
    {
        public Boolean projectOpened = false;
        private ItemsDB DBItems;
        private TemplateDB DBTemplates;
        private ItemSelect w_ItemSel;
        private LoadingWindow w_Loading;


        public MainWindow()
        {
            //if (File.Exists(Properties.Settings.Default.PATH_CFG+"main.json"))
            
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

        }

        private void ButtonClick_Close(object sender, RoutedEventArgs e)
        {
            //if(!projectOpened)
               // ProjectFunction.newProject();
            //else
            //show dialog
            w_ItemSel.Show();
        }

        //웨이브 버튼 창 슬라이더를 다시 돌려놓기
        private void waveSlider_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var slider = sender as Slider;slider.Value = 1.0;
        }
    }
}
