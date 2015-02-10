using System;
using System.Collections.Generic;
using DrivingLicenceScanner.Entities.Infrastructure;

namespace DrivingLicenceScanner.Entities
{
    public class Customer : ObjectBase
    {
        #region Fields

        private ICollection<CheckIn> _checkIns;
        private string _city;
        private DateTime _doB;
        private string _eyeColor;
        private string _firstName;
        private string _hairColor;
        private int _height;
        private int _id;
        private string _lastName;
        private Licence _licence;
        private int _licenceId;
        private string _middleName;
        private string _sex;
        private string _state;
        private string _street;
        private int _weight;
        private string _zipCode;

        #endregion

        #region Properties

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

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (value == _firstName) return;
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (value == _lastName) return;
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        public string MiddleName
        {
            get { return _middleName; }
            set
            {
                if (value == _middleName) return;
                _middleName = value;
                OnPropertyChanged("MiddleName");
            }
        }

        public DateTime DoB
        {
            get { return _doB; }
            set
            {
                if (value.Equals(_doB)) return;
                _doB = value;
                OnPropertyChanged("DoB");
            }
        }

        public string Sex
        {
            get { return _sex; }
            set
            {
                if (value == _sex) return;
                _sex = value;
                OnPropertyChanged("Sex");
            }
        }

        public int Height
        {
            get { return _height; }
            set
            {
                if (value == _height) return;
                _height = value;
                OnPropertyChanged("Height");
            }
        }

        public int Weight
        {
            get { return _weight; }
            set
            {
                if (value == _weight) return;
                _weight = value;
                OnPropertyChanged("Weight");
            }
        }

        public string HairColor
        {
            get { return _hairColor; }
            set
            {
                if (value == _hairColor) return;
                _hairColor = value;
                OnPropertyChanged("HairColor");
            }
        }

        public string EyeColor
        {
            get { return _eyeColor; }
            set
            {
                if (value == _eyeColor) return;
                _eyeColor = value;
                OnPropertyChanged("EyeColor");
            }
        }

        public string Street
        {
            get { return _street; }
            set
            {
                if (value == _street) return;
                _street = value;
                OnPropertyChanged("Street");
            }
        }

        public string City
        {
            get { return _city; }
            set
            {
                if (value == _city) return;
                _city = value;
                OnPropertyChanged("City");
            }
        }

        public string State
        {
            get { return _state; }
            set
            {
                if (value == _state) return;
                _state = value;
                OnPropertyChanged("State");
            }
        }

        public string ZipCode
        {
            get { return _zipCode; }
            set
            {
                if (value == _zipCode) return;
                _zipCode = value;
                OnPropertyChanged("ZipCode");
            }
        }

        public int LicenceId
        {
            get { return _licenceId; }
            set
            {
                if (value == _licenceId) return;
                _licenceId = value;
                OnPropertyChanged("LicenceId");
            }
        }

        public virtual Licence Licence
        {
            get { return _licence; }
            set
            {
                if (Equals(value, _licence)) return;
                _licence = value;
                OnPropertyChanged("Licence");
            }
        }

        public virtual ICollection<CheckIn> CheckIns
        {
            get { return _checkIns; }
            set
            {
                if (Equals(value, _checkIns)) return;
                _checkIns = value;
                OnPropertyChanged("CheckIns");
            }
        }

        #endregion
    }
}