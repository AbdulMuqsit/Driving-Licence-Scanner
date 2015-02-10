using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DrivingLicenceScanner.View
{
    /// <summary>
    ///     Interaction logic for ScanView.xaml
    /// </summary>
    public partial class ScanView : UserControl
    {
        private FrameworkElement _root;

        public ScanView()
        {
            InitializeComponent();
            if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                Focus();
                Window window = Window.GetWindow(this);
                Application.Current.MainWindow.KeyDown += ScanView_KeyDown;
                Unloaded += (sender, args) => { Application.Current.MainWindow.KeyDown -= ScanView_KeyDown; };
            }
        }

        public TextBox TxtBoxScan { get; set; }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _root = (FrameworkElement) GetTemplateChild("RootElement");
            TxtBoxScan = (TextBox) GetTemplateChild("TxtBoxScan");
            bool went = VisualStateManager.GoToElementState(_root, "BeforeState", false);
            Debug.Write(went);
        }

        private void ScanView_KeyDown(object sender, KeyEventArgs e)
        {
            if (TxtBoxScan != null && !TxtBoxScan.IsFocused)
            {
                TxtBoxScan.Text = String.Empty;
                TxtBoxScan.Focusable = true;
                Keyboard.Focus(TxtBoxScan);
            }
        }
    }
}