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

namespace Cobalt.Windows.MainTab
{
    /// <summary>
    /// TabWaveSchedule.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TabWaveSchedule : UserControl
    {
        public TabWaveSchedule()
        {
            InitializeComponent();
        }

        public void AddWave(int pos = -1)
        {
            if(pos == -1)
            {
                var tab = new TabItem();
                tab.Header = tabControl_Waves.Items.Count + 1;
                tab.Style = Resources["TabItem_Waves"] as Style;
                tab.Content = new TabWave();
                this.tabControl_Waves.Items.Add(tab);
            }
        }
        public void RemoveWave(int pos)
        {
            
        }
    }
}
