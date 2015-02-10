using System.Threading.Tasks;
using DrivingLicenceScanner.Infrastructure;
using DrivingLicenceScanner.Model;

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
                        BusyState = BusyState.Busy;
                        await Task.Run(() =>
                        {
                            Navigator.SwitchView(ViewModelLocator.CheckInsViewMode);
                            ViewModelLocator.CheckInsViewMode.LoadCheckInsCommand.Execute(null);
                        });
                        BusyState = BusyState.Free;
                    }, () =>
                        ViewModelLocator.ScanViewModel.Customer != null ||
                        ViewModelLocator.DetailsViewModel.Customer != null);

            SwitchToScanViewCommand =
                new RelayCommand(
                    async () =>
                    {
                        BusyMessage = "";
                        BusyState = BusyState.Busy;
                        await Task.Run(() => Navigator.SwitchView(ViewModelLocator.ScanViewModel));
                        BusyState = BusyState.Free;
                    });

            SwitchToSettingsViewCommand =
                new RelayCommand(
                    async () =>
                    {
                        BusyMessage = "";
                        BusyState = BusyState.Busy;
                        await Task.Run(() => Navigator.SwitchView(ViewModelLocator.SettingsViewModel));
                        BusyState = BusyState.Free;
                    });

            SwitchToCustomersViewCommand =
                new RelayCommand(
                    async () =>
                    {
                        BusyMessage = "Loading Customers...";
                        BusyState = BusyState.Busy;
                        await Task.Run(() => Navigator.SwitchView(ViewModelLocator.CustomersViewModel));
                        ViewModelLocator.CustomersViewModel.LoadCustomersCommand.Execute(null);
                        BusyState = BusyState.Free;
                    });

            SwitchToDetailsViewCommand =
                new RelayCommand(
                    async () =>
                    {
                        BusyMessage = "";
                        BusyState = BusyState.Busy;
                        await Task.Run(() => Navigator.SwitchView(ViewModelLocator.DetailsViewModel));
                        BusyState = BusyState.Free;
                    },
                    () => ViewModelLocator.ScanViewModel.Customer != null);
        }

        #endregion
    }
}