using DrivingLicenceScanner.Entities.Infrastructure;

namespace DrivingLicenceScanner.Model
{
    public class CustomerLegalStatus : ObjectBase
    {
        private bool _allowed;
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public bool Allowed
        {
            get { return _allowed; }
            set
            {
                if (value.Equals(_allowed)) return;
                _allowed = value;
                OnPropertyChanged();
            }
        }
    }
}