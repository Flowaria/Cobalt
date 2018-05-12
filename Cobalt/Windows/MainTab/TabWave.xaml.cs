using System;
using System.Windows.Controls;

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

            //TFMap.MapChange += c_MapChanged;
        }

        /*
        void c_MapChanged(object sender, MapChangeEventArgs e)
        {
            Console.WriteLine(e.Current.MapName);

            clearWaveRelayCombo();


        }
        */

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
