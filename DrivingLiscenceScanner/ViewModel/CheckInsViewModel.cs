using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DrivingLicenceScanner.Entities;
using DrivingLicenceScanner.Infrastructure;
using DrivingLicenceScanner.Model;

namespace DrivingLicenceScanner.ViewModel
{
    public class CheckInsViewModel : ViewModelBase
    {
        #region Fields

        private CheckIn _checkIn;
        private ObservableCollection<CheckIn> _checkIns;

        #endregion

        #region Properties

        public ObservableCollection<CheckIn> CheckIns
        {
            get { return _checkIns; }
            set
            {
                if (Equals(value, _checkIns)) return;
                _checkIns = value;
                OnPropertyChanged();
            }
        }

        public CheckIn CheckIn
        {
            get { return _checkIn; }
            set
            {
                if (Equals(value, _checkIn)) return;
                _checkIn = value;
                OnPropertyChanged();
            }
        }

        #region Commands

        public RelayCommand LoadCheckInsCommand { get; set; }

        #endregion

        #endregion

        #region Methods

        public CheckInsViewModel()
        {
            LoadCheckInsCommand = new RelayCommand(LoadCheckIns);
        }

        private async void LoadCheckIns()
        {
            BusyState = BusyState.Busy;
            await
                Task.Run(
                    async () =>
                    {
                        CheckIns =
                            new ObservableCollection<CheckIn>(
                                await
                                    Context.CheckIns.Where(
                                        e =>
                                            e.Customer.Licence.Number ==
                                            ViewModelLocator.ScanViewModel.Customer.Licence.Number).ToListAsync());
                    });
            BusyState = BusyState.Free;
        }

        #endregion
    }
}