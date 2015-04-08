using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Entity;
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

        private DetailsViewModel _currentChildViewModel;
        private Customer _customer;
        private ObservableCollection<CustomerLegalStatus> _customerLegalStatuses;
        private string _errorMessage;
        private string _scanText;
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
                    IExtractor extractor = new DataExtractor(ScanText);

                    Customer.Licence = new Licence();

                    Customer.DoB = extractor.DoB;
                    Customer.Height = extractor.Height;
                    Customer.Sex = extractor.Sex;
                    Customer.MiddleName = extractor.MiddleName;

                    Customer.ZipCode = extractor.ZipCode;

                    Customer.FirstName = extractor.FirstName;


                    Customer.LastName = extractor.LastName;
                    Customer.EyeColor = extractor.EyeColor;
                    Customer.State = extractor.State;
                    Customer.City = extractor.City;
                    Customer.Street = extractor.Street;
                    Customer.Licence.ExpiryDate = extractor.LicenceExpireDate;

                    Customer.Licence.IssueDate = extractor.LicenceIssueDate;
                    Customer.Licence.Number = extractor.LicenceNumber;


                    using (DrivingLicenceScannerDbContext context = Context)
                    {
                        //if customer alrady exists, add a new checkin
                        Customer customer =
                            await
                                context.Customers.FirstOrDefaultAsync(
                                    cust => cust.Licence.Number == Customer.Licence.Number);
                        if ((customer != null))
                        {
                            customer.CheckIns.Add(new CheckIn {Time = DateTime.Now});
                        }
                        else
                        {
                            context.Customers.Add(Customer);
                            Customer.CheckIns = new Collection<CheckIn> {new CheckIn {Time = DateTime.Now}};
                        }
                        //await context.SaveChangesAsync();
                    }
                    OnPropertyChanged("Age");
                    TimeStamp = DateTime.Now;
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
                    legal.Add(new CustomerLegalStatus {Allowed = Age >= legality.Age, Name = legality.Name});
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

        #endregion
    }
}