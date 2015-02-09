using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrivingLicenceScanner.Entities;
using DrivingLicenceScanner.Infrastructure;

namespace DrivingLicenceScanner.ViewModel
{
    public class CheckInsViewModel : ViewModelBase
    {
        private ObservableCollection<CheckIn> _checkIns;
        private CheckIn _checkIn;

        public ObservableCollection<CheckIn> CheckIns
        {
            get
            {

                return _checkIns;
            }
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

        public RelayCommand LoadCheckInsCommand { get; set; }

        public CheckInsViewModel()
        {
            LoadCheckInsCommand = new RelayCommand(LoadCheckIns);
        }

        private async void LoadCheckIns()
        {
            await Task.Run(async () =>
            {
               
                CheckIns = new ObservableCollection<CheckIn>(await Context.CheckIns.Where(e => e.Customer.Licence.Number == ViewModelLocator.ScanViewModel.Customer.Licence.Number).ToListAsync());
            });
        }
    }
}
