using DrivingLicenceScanner.Entities;
using DrivingLicenceScanner.Infrastructure;

namespace DrivingLicenceScanner.ViewModel
{
    public class DetailsViewModel : ViewModelBase
    {
        #region Fields

        private Customer _customer;

        #endregion

        #region Properties

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

        #endregion
    }
}