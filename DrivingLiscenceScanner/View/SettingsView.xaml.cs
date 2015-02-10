using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace DrivingLicenceScanner.View
{
    /// <summary>
    ///     Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        private FrameworkElement _root;

        public SettingsView()
        {
            InitializeComponent();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _root = (FrameworkElement) GetTemplateChild("RootElement");

            bool went = VisualStateManager.GoToElementState(_root, "ViewState", false);
            Debug.Write(went);
        }
    }
}