using Cobalt.MvM.DB;
using Cobalt.MvM.Items;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Cobalt.ToolWindow
{
    /// <summary>
    /// ItemSelect.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ItemSelect : Window
    {
        ItemsDB db;
        public ItemSelect(ItemsDB data)
        {
            db = data;

            InitializeComponent();
            RoutedEventHandler handler = new RoutedEventHandler(this.Button_Click_Items);
            Button t_Button = null; Image t_Img = null;
            foreach(TFItem item in db.querryAllItem())
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
            Console.WriteLine(db.FindItemById((int)clicked.Content));
            this.Hide();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
