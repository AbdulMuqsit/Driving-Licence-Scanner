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
                Application.Current.MainWindow.MouseDown += MainWindow_MouseDown;
                this.Unloaded += (sender, args) => { Application.Current.MainWindow.KeyDown -= ScanView_KeyDown; };
            }

        }

      

        void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!(sender == TxtBoxScan))
            {
                Keyboard.DefaultRestoreFocusMode = RestoreFocusMode.Auto;
                Keyboard.ClearFocus();
            }
            else
            {
                TxtBoxScan.Clear();
            }
        }
        
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _root = (FrameworkElement)GetTemplateChild("RootElement");
            TxtBoxScan = (TextBox)GetTemplateChild("TxtBoxScan");
            var went = VisualStateManager.GoToElementState(_root, "BeforeState", false);
            Debug.Write(went);
        }

        void ScanView_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (TxtBoxScan != null && !TxtBoxScan.IsFocused)
            {
                TxtBoxScan.Text = String.Empty;
                TxtBoxScan.Focusable = true;
                Keyboard.Focus(TxtBoxScan);
            }
        }

        public TextBox TxtBoxScan { get; set; }
    }
}
