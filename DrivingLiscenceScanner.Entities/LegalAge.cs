using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrivingLicenceScanner.Entities.Infrastructure;

namespace DrivingLicenceScanner.Entities
{
    class LegalAge:ObjectBase
    {
        private int _cigerates;
        private int _liquor;
        private int _lottery;

        public int Cigarette
        {
            get { return _cigerates; }
            set
            {
                if (value == _cigerates) return;
                _cigerates = value;
                OnPropertyChanged("Cigerates");
            }
        }

        public int Liquor
        {
            get { return _liquor; }
            set
            {
                if (value == _liquor) return;
                _liquor = value;
                OnPropertyChanged("Liquor");
            }
        }

        public int Lottery
        {
            get { return _lottery; }
            set
            {
                if (value == _lottery) return;
                _lottery = value;
                OnPropertyChanged("Lottery");
            }
        }
    }
}
