using System.Windows;
using DrivingLiscenceScanner.Entities.Infrastructure;

namespace DrivingLiscenceScanner.Infrastructure
{
    public class ViewModelBase : ObjectBase
    {
        private static ViewModelLocator _viewModelLocator;

        public static ViewModelLocator ViewModelLocator
        {
            get {
                return _viewModelLocator ??
                       (_viewModelLocator =
                           (ViewModelLocator) Application.Current.Resources.FindName("ViewModelLocator"));
            }
            set
            {
                _viewModelLocator = value;
            }
        }
    }
}