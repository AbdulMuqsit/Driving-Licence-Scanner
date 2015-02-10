using System.Windows;
using System.Windows.Controls;

namespace DrivingLicenceScanner.View
{
    /// <summary>
    ///     Interaction logic for BusyAnimation.xaml
    /// </summary>
    public partial class BusyAnimation : UserControl
    {
        public static readonly DependencyProperty BusyProperty = DependencyProperty.Register(
            "Busy", typeof (bool), typeof (BusyAnimation), new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty BusyMessageProperty = DependencyProperty.Register(
            "BusyMessage", typeof (string), typeof (BusyAnimation), new PropertyMetadata(default(string)));

        public BusyAnimation()
        {
            InitializeComponent();
        }

        public bool Busy
        {
            get { return (bool) GetValue(BusyProperty); }
            set { SetValue(BusyProperty, value); }
        }

        public string BusyMessage
        {
            get { return (string) GetValue(BusyMessageProperty); }
            set { SetValue(BusyMessageProperty, value); }
        }
    }
}