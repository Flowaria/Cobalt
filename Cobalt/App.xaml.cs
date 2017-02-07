using System;
using System.Windows;

namespace Cobalt
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Console.WriteLine(System.Threading.Thread.CurrentThread.CurrentUICulture);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            Console.WriteLine(System.Threading.Thread.CurrentThread.CurrentUICulture);
        } 
    }
}
