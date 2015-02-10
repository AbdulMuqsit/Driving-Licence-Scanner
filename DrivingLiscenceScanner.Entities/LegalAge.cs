using DrivingLicenceScanner.Entities.Infrastructure;

namespace DrivingLicenceScanner.Entities
{
    public class LegalAge : ObjectBase
    {
        private int _age;
        private int _id;
        private string _name;

        public int Id
        {
            get { return _id; }
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged();
            }
        }

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

        public int Age
        {
            get { return _age; }
            set
            {
                if (value == _age) return;
                _age = value;
                OnPropertyChanged();
            }
        }
    }
}