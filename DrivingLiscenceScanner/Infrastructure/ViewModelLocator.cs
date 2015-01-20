using DrivingLicenceScanner.ViewModel;

namespace DrivingLicenceScanner.Infrastructure
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel { get; set; }
        public ScanViewModel ScanViewModel { get; set; }
        public CustomersViewModel CustomersViewModel { get; set; }

        public ViewModelLocator()
        {
            MainViewModel = new MainViewModel();
            ScanViewModel = new ScanViewModel();
            CustomersViewModel = new CustomersViewModel();
        }

    }
}
