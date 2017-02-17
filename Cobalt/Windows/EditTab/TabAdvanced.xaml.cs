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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cobalt.Windows.EditTab
{
    /// <summary>
    /// TabAdvanced.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TabAdvanced : UserControl
    {
        private TreeViewItem tree;
        private TextBox box;
        private bool tabEditing = false;
        public TabAdvanced()
        {
            box = new TextBox();
            box.LostFocus += (o, e) =>
            {
                tabEdited();
            };
            box.KeyDown += (o, e) =>
            {
                if (e.Key == Key.Return)
                {
                    tabEdited();
                }
            };
            InitializeComponent();
        }

        private void tabEdited()
        {
            if (box.Text.Length < 1)
            {
                int index = MainTree.Items.IndexOf(tree);
                Console.WriteLine(index);
                MainTree.Items.Remove(index);
            }
            tree.Header = box.Text;
            tabEditing = false;
        }

        private void treeView_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(!tabEditing)
            {
                tree = e.Source as TreeViewItem;
                box.Text = tree.Header.ToString();
                tree.Header = box;
                tabEditing = true;
            }
        }

        private void treeView_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void treeView_MouseMove(object sender, MouseEventArgs e)
        {

        }
        private void treeView_DragOver(object sender, DragEventArgs e)
        {

        }
        private void treeView_Drop(object sender, DragEventArgs e)
        {

        }
    }
}
