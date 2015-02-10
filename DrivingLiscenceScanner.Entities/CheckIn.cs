using System;
using DrivingLicenceScanner.Entities.Infrastructure;

namespace DrivingLicenceScanner.Entities
{
    public class CheckIn : ObjectBase
    {
        private Customer _customer;
        private int _id;
        private DateTime _time;

        public int Id
        {
            get { return _id; }
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        public DateTime Time
        {
            get { return _time; }
            set
            {
                if (value == _time) return;
                _time = value;
                OnPropertyChanged("Time");
            }
        }

        public virtual Customer Customer
        {
            get { return _customer; }
            set
            {
                if (Equals(value, _customer)) return;
                _customer = value;
                OnPropertyChanged("Customer");
            }
        }
    }
}