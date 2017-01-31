using Cobalt.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cobalt.Forms.Tools
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
            icon.IsCrit = false;
            AddIcon(icon);
        }
    }
}
