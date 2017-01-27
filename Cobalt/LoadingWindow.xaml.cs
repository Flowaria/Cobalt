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
        public LoadingWindow()
        {  
            InitializeComponent();
            eLabelVersion.Content = String.Format("Cobalt v{0}", Properties.Settings.Default.VERSION_STRING);
            eLabel.Content = Properties.Settings.Default.LOAD_ITEM;
            int r = new Random().Next(0,6);
            eImage.Source = new BitmapImage(new Uri(String.Format("/Resources/Splash/{0}.png", r), UriKind.Relative));       
        }
    }
}
