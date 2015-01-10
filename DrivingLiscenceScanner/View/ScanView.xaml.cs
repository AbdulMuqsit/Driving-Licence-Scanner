using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DrivingLicenceScanner.View
{
    /// <summary>
    /// Interaction logic for ScanView.xaml
    /// </summary>
    public partial class ScanView : UserControl
    {
        public ScanView()
        {
            InitializeComponent();
            this.Focus();
            var window = Window.GetWindow(this);
            Application.Current.MainWindow.KeyDown += ScanView_KeyDown;
            this.Unloaded += (sender, args) => { Application.Current.MainWindow.KeyDown -= ScanView_KeyDown; };
           
        }
        void ScanView_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Box.Focusable = true;
            Keyboard.Focus(Box);
           
        }

    }
}
