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

        public RelayCommand SwitchToScanViewCommand { get; private set; }
        public RelayCommand SwitchToCustomersViewCommand { get; private set; }
        public RelayCommand SwitchToDetailsViewCommand { get; private set; }
        public RelayCommand SwitchToCheckInsViewCommand { get; private set; }
        public RelayCommand SwitchToSettingsViewCommand { get; private set; }

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
                    async () => await Task.Run(() => Navigator.SwitchView(ViewModelLocator.CheckInsViewMode)));
            SwitchToScanViewCommand =
                new RelayCommand(
                    async () => await Task.Run(() => Navigator.SwitchView(ViewModelLocator.CheckInsViewMode)));
            SwitchToSettingsViewCommand =
                new RelayCommand(
                    async () => await Task.Run(() => Navigator.SwitchView(ViewModelLocator.SettingsViewModel)));
            SwitchToCustomersViewCommand =
                new RelayCommand(
                    async () => { await Task.Run(() => Navigator.SwitchView(ViewModelLocator.CustomersViewModel)); });
            SwitchToDetailsViewCommand =
                new RelayCommand(
                    async () => await Task.Run(() => Navigator.SwitchView(ViewModelLocator.DetailsViewModel)));
        }

        #endregion
    }
}