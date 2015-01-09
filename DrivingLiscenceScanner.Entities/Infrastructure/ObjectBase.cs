using System.ComponentModel;
using DrivingLiscenceScanner.Entities.Annotations;

namespace DrivingLicenceScanner.Entities.Infrastructure
{
    public class ObjectBase:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}