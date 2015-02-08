using System;
using System.ComponentModel;
using System.Diagnostics;
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
        private FrameworkElement _root;

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

        public override void OnApplyTemplate()
        {
            // base.OnApplyTemplate();
            // _root = (FrameworkElement)GetTemplateChild("RootElement");
            // TxtBoxScan = (TextBox)GetTemplateChild("TxtBoxScan");
            //var went = VisualStateManager.GoToElementState(_root, "BeforeScan", false);
            // Debug.Write(went);
        }

        void ScanView_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (TxtBoxScan != null)
            {
                TxtBoxScan.Focusable = true;
                Keyboard.Focus(TxtBoxScan);
            }
        }

        public TextBox TxtBoxScan { get; set; }
    }
}
