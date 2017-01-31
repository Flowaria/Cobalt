using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Cobalt.Controls
{
    /// <summary>
    /// TFBotIcon.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TFBotIcon : UserControl
    {
        private bool m_IsGiant = false, m_IsCrit = false;
        public bool IsGiant
        {
            get
            {
                return m_IsGiant;
            }
            set
            {
                if (value)
                {
                    Rectangle_Bg.Fill = bgGiants;
                }
                else
                {
                    Rectangle_Bg.Fill = bgNormal;
                }
                m_IsGiant = value;
            }
        }

        public bool IsCrit
        {
            get
            {
                return m_IsGiant;
            }
            set
            {
                if (value)
                {
                    Rectangle_Bg.Stroke = stCrit;
                }
                else
                {
                    Rectangle_Bg.Stroke = stNormal;
                }
                m_IsGiant = value;
            }
        }

        private SolidColorBrush
            bgNormal = new SolidColorBrush(),
            bgGiants = new SolidColorBrush(),
            stNormal = new SolidColorBrush(),
            stCrit = new SolidColorBrush();
        public TFBotIcon(BitmapImage icon)
        {
            InitializeComponent();

            bgNormal.Color = Color.FromRgb(235, 228, 202); //#EBE4CA
            bgGiants.Color = Color.FromRgb(193, 21, 0); //#FF2424
            stNormal.Color = Color.FromArgb(0, 0, 0, 0); //Black
            stCrit.Color = Color.FromRgb(54, 138, 255); //#368AFF
           
            Image_Icon.Source = icon;

            Rectangle_Bg.Fill = bgNormal;
            Rectangle_Bg.Stroke = stCrit;
        }
    }
}
