using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DrivingLicenceScanner.Entities;
using DrivingLicenceScanner.Infrastructure;

namespace DrivingLicenceScanner.ViewModel
{
    public class CustomersViewModel : ViewModelBase
    {
        #region Fields

        private ObservableCollection<Customer> _customers;
        private string _searchText;
        private string _filter;

        #endregion

        #region Properties

        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
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

        public RelayCommand LoadCustomersCommand { get; set; }

        #endregion

        #region Methods

        public CustomersViewModel()
        {
            LoadCustomersCommand = new RelayCommand(LoadCustomers);
            SearchCommand = new RelayCommand(Search);
        }

        public RelayCommand SearchCommand { get; set; }

        private async void Search()
        {
            BusyMessage = "Loading Customers...";
            await
                Task.Run(
                    async () =>
                    {

                        Customers = new ObservableCollection<Customer>(await Context.Customers.Where(e => e.FirstName.Contains(SearchText) || e.LastName.Contains(SearchText) || e.MiddleName.Contains(SearchText) || e.Licence.Number.Contains(SearchText) || e.Street.Contains(SearchText) || e.Street.Contains(SearchText) || e.State.Contains(SearchText) || e.ZipCode.Contains(SearchText)).ToListAsync());

                    });
            ViewModelLocator.MainViewModel.BusyState = false;
        }

        public string Filter
        {
            get { return _filter; }
            set
            {
                if (value == _filter) return;
                _filter = value;
                OnPropertyChanged();
            }
        }

        private async void LoadCustomers()
        {
            BusyMessage = "Loading Customers...";
            await
                Task.Run(
                    async () =>
                    {
                        Customers = new ObservableCollection<Customer>(await Context.Customers.ToListAsync());
                    });
            ViewModelLocator.MainViewModel.BusyState = false;
        }

        #endregion

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (value == _searchText) return;
                _searchText = value;
                SearchCommand.Execute(null);
                OnPropertyChanged();
            }
        }
    }
}