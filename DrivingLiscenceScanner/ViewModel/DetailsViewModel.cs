using DrivingLicenceScanner.Entities;
using DrivingLicenceScanner.Infrastructure;

namespace DrivingLicenceScanner.ViewModel
{
    public class DetailsViewModel : ViewModelBase
    {
        private Customer _customer;

        public Customer Customer
        {
            get { return _customer; }
            set
            {
                if (Equals(value, _customer)) return;
                _customer = value;
                OnPropertyChanged();
            }
        }
    }
}