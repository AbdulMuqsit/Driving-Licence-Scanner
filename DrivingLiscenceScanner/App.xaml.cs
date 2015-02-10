using System;
using System.Windows;

namespace DrivingLiscenceScanner
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory",
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            AppDomain.CurrentDomain.UnhandledException += (s, k) => MessageBox.Show("Something went wrong!\nMessage: " + ((Exception)(k.ExceptionObject)).Message);

        }
    }
}