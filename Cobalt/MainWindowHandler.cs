using System.Windows.Controls;
using System.Windows.Input;

namespace Cobalt
{
    public partial class MainWindow
    {
        //웨이브 버튼 창 슬라이더를 다시 돌려놓기
        private void waveSlider_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var slider = sender as Slider; slider.Value = 1.0;
        }
    }
}
