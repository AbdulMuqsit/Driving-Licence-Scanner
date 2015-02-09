using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Threading.Tasks;
using DrivingLicenceScanner.Entities;
using DrivingLicenceScanner.Infrastructure;

namespace DrivingLicenceScanner.ViewModel
{
    public class CustomersViewModel : ViewModelBase
    {
        private ObservableCollection<Customer> _customers;

        #region Properties

        public ObservableCollection<Customer> Customers
        {
            get
            {
                
                return _customers;
            }
            set
            {
                if (Equals(value, _customers)) return;
                _customers = value;
                OnPropertyChanged();
            }
        }


        public DetailsViewModel CurrentChildViewModel
        {
            get { return ViewModelLocator.DetailsViewModel; }

        }

        public CustomersViewModel()
        {
            LoadCustomersCommand = new RelayCommand(LoadCustomers);
        }

        public RelayCommand LoadCustomersCommand { get; set; }

        private async void LoadCustomers()
        {
            await Task.Run(async () =>
            {
                Customers = new ObservableCollection<Customer>(await Context.Customers.ToListAsync());

            });
        }

        #endregion
    }
}
