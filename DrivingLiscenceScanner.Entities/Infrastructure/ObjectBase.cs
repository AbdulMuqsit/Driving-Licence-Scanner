using System.ComponentModel;
using System.Runtime.CompilerServices;
using DrivingLicenceScanner.Entities.Annotations;

namespace DrivingLicenceScanner.Entities.Infrastructure
{
    public class ObjectBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}