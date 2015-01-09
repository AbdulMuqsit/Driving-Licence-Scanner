using DrivingLicenceScanner.Infrastructure;

namespace DrivingLicenceScanner.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields

        private ViewModelBase _currentViewModel;

        #endregion

        #region Properties

        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                if (Equals(value, _currentViewModel)) return;
                _currentViewModel = value;
                OnPropertyChanged("CurrentViewModel");
            }
        }

        #endregion

        #region Methods

        public MainViewModel()
        {
            CurrentViewModel = new ScanViewModel();
        }

        #endregion

    }
}
