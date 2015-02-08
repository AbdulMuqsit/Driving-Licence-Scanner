using System.ComponentModel;
using System.Windows;
using DrivingLicenceScanner.ViewModel;

namespace DrivingLicenceScanner.Infrastructure
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel { get; set; }
        public ScanViewModel ScanViewModel { get; set; }
        public CustomersViewModel CustomersViewModel { get; set; }
        public DetailsViewModel DetailsViewModel { get; set; }
        public CheckInsViewModel CheckInsViewMode { get; set; }
        public SettingsViewModel SettingsViewModel { get; set; }

        public ViewModelLocator()
        {
            if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                MainViewModel = new MainViewModel();
                ScanViewModel = new ScanViewModel();
                CustomersViewModel = new CustomersViewModel(); 
                DetailsViewModel = new DetailsViewModel();
                CheckInsViewMode = new CheckInsViewModel();
                SettingsViewModel= new SettingsViewModel();
            }
        }

    }
}
