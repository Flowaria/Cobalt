using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace Cobalt.UserControls
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
                if (value) { Rectangle_Bg.Fill = Icons.bgGiants; }
                else { Rectangle_Bg.Fill = Icons.bgNormal; }
                m_IsGiant = value;
            }
        }

        public bool IsCrit
        {
            get
            {
                return m_IsCrit;
            }
            set
            {
                if (value) { Rectangle_Bg.Stroke = Icons.stCrit; }
                else { Rectangle_Bg.Stroke = Icons.stNormal; }
                m_IsCrit = value;
            }
        }

        
        public TFBotIcon(BitmapImage icon)
        {
            InitializeComponent();

            Image_Icon.Source = icon;

            Rectangle_Bg.Fill = Icons.bgNormal;
            Rectangle_Bg.Stroke = Icons.stNormal;
        }
        public TFBotIcon(string path)
        {
            InitializeComponent();

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + path))
            {
                Uri uri = new Uri(AppDomain.CurrentDomain.BaseDirectory + path, UriKind.RelativeOrAbsolute);
                Image_Icon.Source = BitmapFrame.Create(uri);
            }
        }
    }
    public static class Icons
    {
        public readonly static SolidColorBrush
            bgNormal = new SolidColorBrush(),
            bgGiants = new SolidColorBrush(),
            stNormal = new SolidColorBrush(),
            stCrit = new SolidColorBrush();

        static Icons()
        {
            bgNormal.Color = Color.FromRgb(235, 228, 202); //#EBE4CA
            bgGiants.Color = Color.FromRgb(193, 21, 0); //#FF2424
            stNormal.Color = Color.FromArgb(0, 0, 0, 0); //Black
            stCrit.Color = Color.FromRgb(54, 138, 255); //#368AFF

            ColorAnimation stCritAnimation = new ColorAnimation();
            stCritAnimation.From = Color.FromRgb(54, 138, 255);
            stCritAnimation.To = Color.FromRgb(0, 180, 219);
            stCritAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            stCritAnimation.RepeatBehavior = RepeatBehavior.Forever;
            stCrit.BeginAnimation(SolidColorBrush.ColorProperty, stCritAnimation);
        }
    }
}
