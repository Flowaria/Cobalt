using Cobalt.Forms.Tools;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cobalt.Forms
{
    public partial class MainWindow : Window
    {
        public Boolean projectOpened = false;
        private Navigator nav;

        public MainWindow()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            nav = new Navigator();
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
            nav.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nav.Show();
        }
    }
}
