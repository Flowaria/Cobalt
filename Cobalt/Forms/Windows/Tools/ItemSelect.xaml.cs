using Cobalt.TFItems;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Cobalt.Forms
{
    public partial class ItemSelect : Window
    {
        public ItemSelect(List<TFItem> items)
        {
            InitializeComponent();
            RoutedEventHandler handler = new RoutedEventHandler(this.Button_Click_Items);
            Button t_Button = null; Image t_Img = null;
            foreach(TFItem item in items)
            {
                t_Button = new Button();
                t_Button.Content = item.DefId;
                t_Button.Width = 96; t_Button.Height = 96;
                t_Button.Click += handler;

                t_Img = new Image();
                //t_Img.Source = Properties.Settings.Default.PATH_IMG + item.getImgUrl();
                if(t_Button != null)
                    this.Panel_Image.Children.Add(t_Button);
            }
            
        }

        private void Button_Click_Items(object sender, EventArgs e)
        {
            Button clicked = (Button)sender;
            this.Hide();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
