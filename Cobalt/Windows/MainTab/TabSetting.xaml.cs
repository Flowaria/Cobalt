using Cobalt.Windows.Element;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cobalt.Windows.MainTab
{
    /// <summary>
    /// TabSetting.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TabSetting : UserControl
    {
        public TabSetting()
        {
            InitializeComponent();

            //맵 선택 이벤트 후크
            //TFMap.MapChange += c_MapChanged;

            //맵파일
            comboMaps.Items.Clear();
            /*
            foreach (TFMap map in TFMap.GetMaps())
            {
                var item = new ComboBoxItem();
                item.Content = map.MapName;
                comboMaps.Items.Add(item);
            }
            */
        }
        /*

        void RefreshRadioList(ListBox list, TFMap map, string fieldname, string group)
        {
            list.Items.Clear();
            List<string> field = typeof(TFMap).GetField(fieldname).GetValue(map) as List<string>;
            foreach (var i in field)
            {
                Console.WriteLine(i);
                var radio = new TextRadioButton(i);
                radio.GroupName = group;
                list.Items.Add(radio);
            }
        }

        void c_MapChanged(object sender, MapChangeEventArgs e)
        {
            Console.WriteLine(e.Current.MapName);
            RefreshRadioList(WaveBeginRelay, e.Current, "RelayWaveStarted", "d");
            RefreshRadioList(WaveEndRelay, e.Current, "RelayWaveDone", "dd");
            RefreshRadioList(WaveInitRelay, e.Current, "RelayWaveInit", "ddd");
        }
        */

        //HEAVY
        private void comboMaps_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count > 0)
            {
                string select = (e.AddedItems[0] as ComboBoxItem).Content as string;
                if (!String.IsNullOrEmpty(select))
                {
                    //TFMap.SetCurrentMap(select);
                }
            }
        }

        private void TextBox_PreviewTextInput_NumberOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumber(e.Text);
        }

        private void TextBox_Pasting_NumberOnly(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsNumber(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }
        private static bool IsNumber(string text)
        {
            Regex regex = new Regex("[^0-9.-]+");
            return !regex.IsMatch(text);
        }

        private void CheckBox_Checked_RespawnTime(object sender, RoutedEventArgs e)
        {
            var element = e.Source as CheckBox;
            if (element.IsChecked == true)
            {
                //turn on elemment
                Respawn_Time.IsEnabled = true;
                Respawn_Force.IsEnabled = true;
            }
            else
            {
                //turn off element
                Respawn_Time.IsEnabled = false;
                Respawn_Force.IsEnabled = false;
            }
        }
    }
}
