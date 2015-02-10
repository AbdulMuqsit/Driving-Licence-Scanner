using System.Threading.Tasks;
using DrivingLicenceScanner.Infrastructure;

namespace DrivingLicenceScanner.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields

        private ViewModelBase _currentChildViewModel;

        #endregion

        #region Properties

        public ViewModelBase CurrentChildViewModel
        {
            get { return _currentChildViewModel; }
            set
            {
                if (Equals(value, _currentChildViewModel)) return;
                _currentChildViewModel = value;

                OnPropertyChanged("CurrentChildViewModel");
            }
        }

        #region Commands

        public RelayCommand SwitchToScanViewCommand { get; private set; }
        public RelayCommand SwitchToCustomersViewCommand { get; private set; }
        public RelayCommand SwitchToDetailsViewCommand { get; private set; }
        public RelayCommand SwitchToCheckInsViewCommand { get; private set; }
        public RelayCommand SwitchToSettingsViewCommand { get; private set; }

        #endregion

        #endregion

        #region Methods

        public MainViewModel()
        {
            CurrentChildViewModel = new ScanViewModel();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            SwitchToCheckInsViewCommand =
                new RelayCommand(
                    async () =>
                    {
                        BusyMessage = "Loading Check Ins...";
                        BusyState = true;
                        await Task.Run(() =>
                        {
                            Navigator.SwitchView(ViewModelLocator.CheckInsViewMode);
                            ViewModelLocator.CheckInsViewMode.LoadCheckInsCommand.Execute(null);
                        });
                    }, () =>
                        ViewModelLocator.ScanViewModel.Customer != null ||
                        ViewModelLocator.DetailsViewModel.Customer != null);

            SwitchToScanViewCommand =
                new RelayCommand(
                    async () =>
                    {
                        BusyMessage = "";
                        BusyState = true;
                        await Task.Run(() => Navigator.SwitchView(ViewModelLocator.ScanViewModel));
                        BusyState = false;
                    });

            SwitchToSettingsViewCommand =
                new RelayCommand(
                    async () =>
                    {
                        BusyMessage = "";
                        BusyState = true;
                        await Task.Run(() => Navigator.SwitchView(ViewModelLocator.SettingsViewModel));
                        BusyState = false;
                    });

            SwitchToCustomersViewCommand =
                new RelayCommand(
                    async () =>
                    {
                        BusyMessage = "Loading Customers...";
                        BusyState = true;
                        await Task.Run(() => Navigator.SwitchView(ViewModelLocator.CustomersViewModel));
                        ViewModelLocator.CustomersViewModel.LoadCustomersCommand.Execute(null);
                    });

            SwitchToDetailsViewCommand =
                new RelayCommand(
                    async () =>
                    {
                        BusyMessage = "";
                        BusyState = true;
                        await Task.Run(() => Navigator.SwitchView(ViewModelLocator.DetailsViewModel));
                        BusyState = false;
                    },
                    () => ViewModelLocator.ScanViewModel.Customer != null);
        }

        #endregion
    }
}