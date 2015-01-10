using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using DrivingLicenceScanner.Entities;
using DrivingLicenceScanner.Infrastructure;
using DrivingLicenceScanner.Model;

namespace DrivingLicenceScanner.ViewModel
{
    public class ScanViewModel : ViewModelBase
    {
        #region Fields

        private string _scanText;
        private ObservableCollection<Customer> _customers;
        private Customer _customer;
        private string _errorMessage;

        #endregion

        #region Properties

        public Customer Customer
        {
            get { return _customer; }
            set
            {
                if (Equals(value, _customer)) return;
                _customer = value;
                OnPropertyChanged("Customer");
            }
        }

        public string ScanText
        {
            get { return _scanText; }
            set
            {
                if (value == _scanText) return;
                _scanText = value;
                OnPropertyChanged("ScanText");
            }
        }

        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set
            {
                if (Equals(value, _customers)) return;
                _customers = value;
                OnPropertyChanged("Customers");
            }
        }

        public RelayCommand ScanCommand { get; set; }
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                if (value == _errorMessage) return;
                _errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }

        #endregion

        #region Methods

        public ScanViewModel()
        {
            InitializeObjects();
            InitializeCommands();
        }

        private void InitializeObjects()
        {
            _customer = new Customer { Licence = new Licence() };
            _customers = new ObservableCollection<Customer>();
        }

        private void InitializeCommands()
        {
            ScanCommand = new RelayCommand(() =>
            {

                try
                {
                    Customer.FirstName = Regex.Match(ScanText, Patterns.FirstNamePattern).Value
                                                .Replace(Patterns.FirstNameInitToken, String.Empty)
                                                .Replace(Patterns.FirstNameExitToken, String.Empty);

                    Customer.LastName = Regex.Match(ScanText, Patterns.LastNamePattern).Value
                                        .Replace(Patterns.LastNameInitToken, String.Empty)
                                        .Replace(Patterns.LastNameExitToken, String.Empty);

                    Customer.DoB = DateTime.Parse(Regex.Match(ScanText, Patterns.DoBPattern).Value
                                        .Replace(Patterns.DoBInitToken, String.Empty)
                                        .Replace(Patterns.DoBExitToken, String.Empty).Insert(2, "-").Insert(5, "-"));

                    Customer.EyeColor = Regex.Match(ScanText, Patterns.EyeColorPattern).Value
                                        .Replace(Patterns.EyeColorInitToken, String.Empty)
                                        .Replace(Patterns.EyeColorExitToken, String.Empty);

                    Customer.Height = Int32.Parse(Regex.Match(ScanText, Patterns.HeightPattern).Value
                                        .Replace(Patterns.HeightInitToken, String.Empty)
                                        .Replace(Patterns.HeightExitToken, String.Empty));

                    Customer.Sex = Regex.Match(ScanText, Patterns.SexPattern).Value
                                        .Replace(Patterns.SexInitToken, String.Empty)
                                        .Replace(Patterns.SexExitToken, String.Empty);

                    Customer.State = Regex.Match(ScanText, Patterns.StatePattern).Value
                                        .Replace(Patterns.StateInitToken, String.Empty)
                                        .Replace(Patterns.StateExitToken, String.Empty);

                    Customer.City = Regex.Match(ScanText, Patterns.CityPattern).Value
                                        .Replace(Patterns.CityInitToken, String.Empty)
                                        .Replace(Patterns.CityExitToken, String.Empty);

                    Customer.Street = Regex.Match(ScanText, Patterns.StreetPattern).Value
                                        .Replace(Patterns.StreetInitToken, String.Empty)
                                        .Replace(Patterns.StreetExitToken, String.Empty);

                    Customer.ZipCode = Regex.Match(ScanText, Patterns.ZipCodePattern).Value
                                        .Replace(Patterns.ZipCodeInitToken, String.Empty)
                                        .Replace(Patterns.ZipCodeExitToken, String.Empty);

                    Customer.Licence.ExpiryDate = DateTime.Parse(Regex.Match(ScanText, Patterns.LicenceExpireDatePattern).Value
                                        .Replace(Patterns.LicenceExpireDateInitToken, String.Empty)
                                        .Replace(Patterns.LicenceExpireDateExitToken, String.Empty).Insert(2, "-").Insert(5, "-"));

                    Customer.Licence.IssueDate = DateTime.Parse(Regex.Match(ScanText, Patterns.LicenceIssueDatePattern).Value
                                        .Replace(Patterns.LicenceIssueDateInitToken, String.Empty)
                                        .Replace(Patterns.LicenceIssueDateExitToken, String.Empty).Insert(2, "-").Insert(5, "-"));

                    Customer.Licence.Number = Regex.Match(ScanText, Patterns.LastNamePattern).Value
                                        .Replace(Patterns.LastNameInitToken, String.Empty)
                                        .Replace(Patterns.LastNameExitToken, String.Empty);

                    Customers.Add(Customer);
                }
                catch (Exception)
                {

                    ErrorMessage = "Invalid Data, Please Scan again.";
                }
            }, () => !String.IsNullOrWhiteSpace(ScanText));
        }

       

        #endregion
    }
}
