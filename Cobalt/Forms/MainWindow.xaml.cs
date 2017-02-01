using Cobalt.Forms.Tools;
using System;
using System.Windows;

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
            nav.Show();
        }
    }
}
