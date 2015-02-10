using System;
using DrivingLicenceScanner.Entities.Infrastructure;

namespace DrivingLicenceScanner.Entities
{
    public class Licence : ObjectBase
    {
        private string _classCode;
        private Customer _customer;
        private DateTime _expiryDate;
        private int _id;
        private DateTime _issueDate;
        private string _number;

        #region Fields

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

        public DateTime IssueDate
        {
            get { return _issueDate; }
            set
            {
                if (value.Equals(_issueDate)) return;
                _issueDate = value;
                OnPropertyChanged("IssueDate");
            }
        }

        public DateTime ExpiryDate
        {
            get { return _expiryDate; }
            set
            {
                if (value.Equals(_expiryDate)) return;
                _expiryDate = value;
                OnPropertyChanged("ExpiryDate");
            }
        }

        public string Number
        {
            get { return _number; }
            set
            {
                if (value == _number) return;
                _number = value;
                OnPropertyChanged("Number");
            }
        }

        public string ClassCode
        {
            get { return _classCode; }
            set
            {
                if (value == _classCode) return;
                _classCode = value;
                OnPropertyChanged("ClassCode");
            }
        }

        public virtual Customer Customer
        {
            get { return _customer; }
            set
            {
                if (Equals(value, _customer)) return;
                _customer = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}