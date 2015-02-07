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

        #endregion

        #region Methods

        public MainViewModel()
        {
            CurrentChildViewModel = new ScanViewModel();
        }

        #endregion

    }
}
