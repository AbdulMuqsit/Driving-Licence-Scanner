using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DrivingLicenceScanner.Model;

namespace DrivingLicenceScanner.View
{
    /// <summary>
    /// Interaction logic for BusyAnimation.xaml
    /// </summary>
    public partial class BusyAnimation : UserControl
    {
        public static readonly DependencyProperty BusyProperty = DependencyProperty.Register(
            "Busy", typeof(bool), typeof(BusyAnimation), new PropertyMetadata(default(bool)));

        public bool Busy
        {
            get { return (bool)GetValue(BusyProperty); }
            set { SetValue(BusyProperty, value); }
        }

        public static readonly DependencyProperty BusyMessageProperty = DependencyProperty.Register(
            "BusyMessage", typeof(string), typeof(BusyAnimation), new PropertyMetadata(default(string)));

        public string BusyMessage
        {
            get { return (string)GetValue(BusyMessageProperty); }
            set { SetValue(BusyMessageProperty, value); }
        }
        public BusyAnimation()
        {
            InitializeComponent();
        }
    }
}
