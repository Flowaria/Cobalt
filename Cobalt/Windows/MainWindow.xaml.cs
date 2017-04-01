using Cobalt.Windows.MainTab;
using Cobalt.Windows.Tools;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cobalt.Windows
{
    public partial class MainWindow : Window
    {
        public Boolean projectOpened = false;
        public static PropertiesWindow f_Setting;
        public static TabSetting f_tabSetting;
        public static TabWaveSchedule f_tabWave;
        public static TabMission f_tabMission;
        public static TabTemplate f_tabTemplate;

        public MainWindow()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            //윈도우 초기화, 탭 초기화
            f_Setting = new PropertiesWindow();
            f_tabSetting = new TabSetting(); tab_setting.Content = f_tabSetting;
            f_tabWave = new TabWaveSchedule(); tab_waves.Content = f_tabWave;
            f_tabMission = new TabMission(); tab_mission.Content = f_tabMission;
            f_tabTemplate = new TabTemplate(); tab_template.Content = f_tabTemplate;

            for(int i = 0;i<8;i++)
            f_tabWave.AddWave();
        }

        //웨이브 버튼 창 슬라이더를 다시 돌려놓기
        private void waveSlider_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var slider = sender as Slider; slider.Value = 1.0;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to save changes?", "Confirmation", MessageBoxButton.YesNoCancel);
            if(result == MessageBoxResult.Yes)
            {

            }
            else if (result == MessageBoxResult.No)
            {

            }
            else if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            f_Setting.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //nav.Show();
        }

        private void MenuFile_Properties_Click(object sender, RoutedEventArgs e)
        {
            f_Setting.Show();
        }
    }
}
