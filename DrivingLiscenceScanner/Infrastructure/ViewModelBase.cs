using System.Windows;
using DrivingLicenceScanner.Entities.Infrastructure;

namespace DrivingLicenceScanner.Infrastructure
{
    public class ViewModelBase : ObjectBase
    {
        private static ViewModelLocator _viewModelLocator;

        public static ViewModelLocator ViewModelLocator
        {
            get {
                return _viewModelLocator ??
                       (_viewModelLocator =
                           (ViewModelLocator) Application.Current.Resources["ViewModelLocator"]);
            }
          
        }
    }
}