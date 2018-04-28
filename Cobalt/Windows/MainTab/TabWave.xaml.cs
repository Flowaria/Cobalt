using Cobalt.Data;
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
    /// TabWave.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TabWave : UserControl
    {
        public TabWave()
        {
            InitializeComponent();

            TFMap.MapChange += c_MapChanged;
        }

        void c_MapChanged(object sender, MapChangeEventArgs e)
        {
            Console.WriteLine(e.Current.MapName);

            clearWaveRelayCombo();


        }

        void clearWaveRelayCombo()
        {
            while (ComboWaveStarted.Items.Count > 3)
            {
                ComboWaveStarted.Items.RemoveAt(3);
            }

            while (ComboWaveDone.Items.Count > 3)
            {
                ComboWaveDone.Items.RemoveAt(3);
            }

            while (ComboWaveInit.Items.Count > 3)
            {
                ComboWaveInit.Items.RemoveAt(3);
            }
        }
    }
}
