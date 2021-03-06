﻿using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Threading.Tasks;
using DrivingLicenceScanner.Entities;
using DrivingLicenceScanner.Infrastructure;

namespace DrivingLicenceScanner.ViewModel
{
    public class CustomersViewModel : ViewModelBase
    {
        #region Fields

        private ObservableCollection<Customer> _customers;

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
    }
}