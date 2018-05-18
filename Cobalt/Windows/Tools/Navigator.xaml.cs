using Cobalt.UserControls;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Cobalt.Windows.Tools
{
    /// <summary>
    /// Navigator.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Navigator : Window
    {
        public Navigator()
        {
            InitializeComponent();
        }

        public void AddIcon(TFBotIcon icon)
        {
            IconPanel.Children.Add(icon);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage logo = new BitmapImage();
            logo.BeginInit(); logo.UriSource = new Uri(@"/Image/Icon/demo_pirate.png", UriKind.Relative); logo.EndInit();
            TFBotIcon icon = new TFBotIcon(logo);
            icon.IsGiant = true;
            icon.IsCrit = true;
            AddIcon(icon);
        }
    }
}
