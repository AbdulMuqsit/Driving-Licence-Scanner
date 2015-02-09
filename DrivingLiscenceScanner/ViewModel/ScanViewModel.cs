using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DrivingLicenceScanner.Entities;
using DrivingLicenceScanner.Infrastructure;
using DrivingLicenceScanner.Model;

namespace DrivingLicenceScanner.ViewModel
{
    public class ScanViewModel : ViewModelBase
    {
        #region Fields

        private string _scanText;
        private Customer _customer;
        private string _errorMessage;
        private ObservableCollection<CustomerLegalStatus> _customerLegalStatuses;

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

        public Customer Customer
        {
            get { return _customer; }
            set
            {
                if (Equals(value, _customer)) return;
                _customer = value;
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

        #region Commands
        public RelayCommand ScanCommand { get; private set; }
        public RelayCommand ClearCommand { get; private set; }
        #endregion
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
        }

        private void InitializeCommands()
        {
            ScanCommand = new RelayCommand(Scan, () => !String.IsNullOrWhiteSpace(ScanText));
            ClearCommand = new RelayCommand(Clear, () => Customer != null);
        }


        #region HelperMethods
        private async void Scan()
        {
            Customer = null;
            ErrorMessage = String.Empty;
            await Task.Run(async () =>
            {
                try
                {

                    Customer = new Customer
                    {
                        Licence = new Licence(),

                        DoB = DateTime.Parse(Regex.Match(ScanText, Patterns.DoBPattern).Value
                            .Replace(Patterns.DoBInitToken, String.Empty)
                            .Replace(Patterns.DoBExitToken, String.Empty).Insert(2, "-").Insert(5, "-")),

                        Height = Int32.Parse(Regex.Match(ScanText, Patterns.HeightPattern).Value
                            .Replace(Patterns.HeightInitToken, String.Empty)
                            .Replace(Patterns.HeightExitToken, String.Empty)),
                        Sex = Regex.Match(ScanText, Patterns.SexPattern).Value
                            .Replace(Patterns.SexInitToken, String.Empty)
                            .Replace(Patterns.SexExitToken, String.Empty) == "1" ? "Male" : "Female",


                        ZipCode = Regex.Match(ScanText, Patterns.ZipCodePattern).Value
                            .Replace(Patterns.ZipCodeInitToken, String.Empty)
                            .Replace(Patterns.ZipCodeExitToken, String.Empty)
                    };
                    var firstName = Regex.Match(ScanText, Patterns.FirstNamePattern).Value
                        .Replace(Patterns.FirstNameInitToken, String.Empty)
                        .Replace(Patterns.FirstNameExitToken, String.Empty);
                    Customer.FirstName = String.Concat(firstName.Substring(0, 1), firstName.Substring(1).ToLower());


                    var lastName = Regex.Match(ScanText, Patterns.LastNamePattern).Value
                        .Replace(Patterns.LastNameInitToken, String.Empty)
                        .Replace(Patterns.LastNameExitToken, String.Empty);
                    Customer.LastName = String.Concat(lastName.Substring(0, 1), lastName.Substring(1).ToLower());


                    var eyeColor = Regex.Match(ScanText, Patterns.EyeColorPattern).Value
                        .Replace(Patterns.EyeColorInitToken, String.Empty)
                        .Replace(Patterns.EyeColorExitToken, String.Empty);
                    Customer.EyeColor = String.Concat(eyeColor.Substring(0, 1), eyeColor.Substring(1).ToLower());

                    var state = Regex.Match(ScanText, Patterns.StatePattern).Value
                        .Replace(Patterns.StateInitToken, String.Empty)
                        .Replace(Patterns.StateExitToken, String.Empty);
                    Customer.State = String.Concat(state.Substring(0, 1), state.Substring(1).ToLower());

                    var city = Regex.Match(ScanText, Patterns.CityPattern).Value
                        .Replace(Patterns.CityInitToken, String.Empty)
                        .Replace(Patterns.CityExitToken, String.Empty);
                    Customer.City = String.Concat(city.Substring(0, 1), city.Substring(1).ToLower());

                    var street = Regex.Match(ScanText, Patterns.StreetPattern).Value
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
                    ViewModelLocator.DetailsViewModel.Customer = Customer;
                    using (var context = Context)
                    {


                        //if customer alrady exists, add a new checkin
                        var customer = await context.Customers.FirstOrDefaultAsync(cust => cust.Licence.Number == Customer.Licence.Number);
                        if ((customer != null))
                        {
                            customer.CheckIns.Add(new CheckIn() { Time = DateTime.Now });
                        }
                        else
                        {
                            context.Customers.Add(Customer);
                            Customer.CheckIns = new Collection<CheckIn> { new CheckIn() { Time = DateTime.Now } };
                        }
                        await context.SaveChangesAsync();
                    }
                    OnPropertyChanged("Age");

                }
                catch (InvalidOperationException)
                {
                    ErrorMessage = "Application encountered an error while saving new check in.";
                }

                catch (Exception)
                {
                    ErrorMessage = "Invalid Data, Please Scan again.";
                }

                //checking legal status of customer
                var legalAges = await Context.LegalAges.ToListAsync();

                var legal = new ObservableCollection<CustomerLegalStatus>();
                foreach (var legality in legalAges)
                {
                    legal.Add(new CustomerLegalStatus() { Allowed = Age >= legality.Age, Name = legality.Name });
                }
                CustomerLegalStatuses = legal;
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
