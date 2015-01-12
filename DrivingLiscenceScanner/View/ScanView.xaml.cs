using System;
using System.ComponentModel;
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
            if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                this.Focus();
                var window = Window.GetWindow(this);
                Application.Current.MainWindow.KeyDown += ScanView_KeyDown;
                this.Unloaded += (sender, args) => { Application.Current.MainWindow.KeyDown -= ScanView_KeyDown; };
            }

        }
        void ScanView_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //TxtBoxScan.Focusable = true;
            //Keyboard.Focus(TxtBoxScan);
           
        }

    }
}
