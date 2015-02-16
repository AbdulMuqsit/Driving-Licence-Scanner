using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Entity;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DrivingLicenceScanner.Entities;
using DrivingLicenceScanner.EntityFramework;
using DrivingLicenceScanner.Infrastructure;
using DrivingLicenceScanner.Model;

namespace DrivingLicenceScanner.ViewModel
{
    public class ScanViewModel : ViewModelBase
    {
        #region Fields

        private Customer _customer;
        private ObservableCollection<CustomerLegalStatus> _customerLegalStatuses;
        private string _errorMessage;
        private string _scanText;
        private DetailsViewModel _currentChildViewModel;
        private DateTime _timeStamp;

        public int Age
        {
            get
            {
                if (Customer != null)
                {
                    DateTime today = DateTime.Today;
                    int cusotmerAge = today.Year - Customer.DoB.Year;
                    if (Customer.DoB > today.AddYears(-cusotmerAge)) cusotmerAge--;
                    return cusotmerAge;
                }
                return 0;
            }
        }

        #endregion

        #region Properties

        public DateTime TimeStamp
        {
            get { return _timeStamp; }
            set
            {
                if (value.Equals(_timeStamp)) return;
                _timeStamp = value;
                OnPropertyChanged();
            }
        }

        public Customer Customer
        {
            get { return _customer; }
            set
            {
                if (Equals(value, _customer)) return;
                _customer = value;
                ViewModelLocator.DetailsViewModel.Customer = Customer;
                OnPropertyChanged("Customer");
                OnPropertyChanged("Age");
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

        #region Collections

        public ObservableCollection<CustomerLegalStatus> CustomerLegalStatuses
        {
            get { return _customerLegalStatuses; }
            set
            {
                if (Equals(value, _customerLegalStatuses)) return;
                _customerLegalStatuses = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        public RelayCommand ScanCommand { get; private set; }
        public RelayCommand ClearCommand { get; private set; }

        #endregion

        #endregion

        #region Methods

        public ScanViewModel()
        {
            InitializeObjects();
            InitializeCommands();
        }

        private void InitializeObjects()
        {
            // _customer = new Customer { Licence = new Licence() };
            CustomerLegalStatuses = new ObservableCollection<CustomerLegalStatus>();
            CurrentChildViewModel = new DetailsViewModel();
        }

        private void InitializeCommands()
        {
            ScanCommand = new RelayCommand(Scan, () => !String.IsNullOrWhiteSpace(ScanText));
            ClearCommand = new RelayCommand(Clear, () => Customer != null);
        }

        public DetailsViewModel CurrentChildViewModel
        {
            get { return _currentChildViewModel; }
            set
            {
                if (Equals(value, _currentChildViewModel)) return;
                _currentChildViewModel = value;
                OnPropertyChanged();
            }
        }

        #region HelperMethods

        private async void Scan()
        {
            Customer = null;
            ErrorMessage = String.Empty;
            Customer = new Customer();

            BusyMessage = "Scanning...";
            BusyState = true;
            await Task.Run(async () =>
            {
                try
                {
                    Customer.Licence = new Licence();

                    Customer.DoB = DateTime.Parse(Regex.Match(ScanText, Patterns.DoBPattern).Value
                        .Replace(Patterns.DoBInitToken, String.Empty)
                        .Replace(Patterns.DoBExitToken, String.Empty).Insert(2, "-").Insert(5, "-"));

                    Customer.Height = Int32.Parse(Regex.Match(ScanText, Patterns.HeightPattern).Value
                        .Replace(Patterns.HeightInitToken, String.Empty)
                        .Replace(Patterns.HeightExitToken, String.Empty));
                    Customer.Sex = Regex.Match(ScanText, Patterns.SexPattern).Value
                        .Replace(Patterns.SexInitToken, String.Empty)
                        .Replace(Patterns.SexExitToken, String.Empty) == "1"
                        ? "Male"
                        : "Female";


                    Customer.ZipCode = Regex.Match(ScanText, Patterns.ZipCodePattern).Value
                        .Replace(Patterns.ZipCodeInitToken, String.Empty)
                        .Replace(Patterns.ZipCodeExitToken, String.Empty);

                    string firstName = Regex.Match(ScanText, Patterns.FirstNamePattern).Value
                        .Replace(Patterns.FirstNameInitToken, String.Empty)
                        .Replace(Patterns.FirstNameExitToken, String.Empty);
                    Customer.FirstName = String.Concat(firstName.Substring(0, 1), firstName.Substring(1).ToLower());


                    string lastName = Regex.Match(ScanText, Patterns.LastNamePattern).Value
                        .Replace(Patterns.LastNameInitToken, String.Empty)
                        .Replace(Patterns.LastNameExitToken, String.Empty);
                    Customer.LastName = String.Concat(lastName.Substring(0, 1), lastName.Substring(1).ToLower());


                    string eyeColor = Regex.Match(ScanText, Patterns.EyeColorPattern).Value
                        .Replace(Patterns.EyeColorInitToken, String.Empty)
                        .Replace(Patterns.EyeColorExitToken, String.Empty);
                    Customer.EyeColor = String.Concat(eyeColor.Substring(0, 1), eyeColor.Substring(1).ToLower());

                    string state = Regex.Match(ScanText, Patterns.StatePattern).Value
                        .Replace(Patterns.StateInitToken, String.Empty)
                        .Replace(Patterns.StateExitToken, String.Empty);
                    Customer.State = String.Concat(state.Substring(0, 1), state.Substring(1).ToLower());

                    string city = Regex.Match(ScanText, Patterns.CityPattern).Value
                        .Replace(Patterns.CityInitToken, String.Empty)
                        .Replace(Patterns.CityExitToken, String.Empty);
                    Customer.City = String.Concat(city.Substring(0, 1), city.Substring(1).ToLower());

                    string street = Regex.Match(ScanText, Patterns.StreetPattern).Value
                        .Replace(Patterns.StreetInitToken, String.Empty)
                        .Replace(Patterns.StreetExitToken, String.Empty);
                    Customer.Street = String.Concat(street.Substring(0, 1), street.Substring(1).ToLower());

                    Customer.Licence.ExpiryDate =
                        DateTime.Parse(Regex.Match(ScanText, Patterns.LicenceExpireDatePattern).Value
                            .Replace(Patterns.LicenceExpireDateInitToken, String.Empty)
                            .Replace(Patterns.LicenceExpireDateExitToken, String.Empty).Insert(2, "-").Insert(5, "-"));

                    Customer.Licence.IssueDate =
                        DateTime.Parse(Regex.Match(ScanText, Patterns.LicenceIssueDatePattern).Value
                            .Replace(Patterns.LicenceIssueDateInitToken, String.Empty)
                            .Replace(Patterns.LicenceIssueDateExitToken, String.Empty).Insert(2, "-").Insert(5, "-"));

                    Customer.Licence.Number = Regex.Match(ScanText, Patterns.LicenceNumberPattern).Value
                        .Replace(Patterns.LicenceNumberInitToken, String.Empty)
                        .Replace(Patterns.LicenceNumberExitToken, String.Empty);

                    using (DrivingLicenceScannerDbContext context = Context)
                    {
                        //if customer alrady exists, add a new checkin
                        Customer customer =
                            await
                                context.Customers.FirstOrDefaultAsync(
                                    cust => cust.Licence.Number == Customer.Licence.Number);
                        if ((customer != null))
                        {
                            customer.CheckIns.Add(new CheckIn { Time = DateTime.Now });
                        }
                        else
                        {
                            context.Customers.Add(Customer);
                            Customer.CheckIns = new Collection<CheckIn> { new CheckIn { Time = DateTime.Now } };
                        }
                        await context.SaveChangesAsync();
                    }
                    OnPropertyChanged("Age");
                    TimeStamp = DateTime.Now;
                }
                catch (DbException)
                {
                    ErrorMessage = "Application encountered an error while saving new check in.";
                    BusyState = false;
                }

                catch (Exception)
                {
                    if (ScanText != "@")
                    {
                        ErrorMessage = "Invalid Data, Please Scan again.";
                    }
                    BusyState = false;
                }
                finally
                {
                    ScanText = String.Empty;
                }


                //checking legal status of customer
                List<LegalAge> legalAges = await Context.LegalAges.ToListAsync();

                var legal = new ObservableCollection<CustomerLegalStatus>();
                foreach (LegalAge legality in legalAges)
                {
                    legal.Add(new CustomerLegalStatus { Allowed = Age >= legality.Age, Name = legality.Name });
                }
                CustomerLegalStatuses = legal;
                BusyState = false;
            });
        }

        private void Clear()
        {
            Customer = null;
            ScanText = String.Empty;
        }

        #endregion

        #endregion
    }
}