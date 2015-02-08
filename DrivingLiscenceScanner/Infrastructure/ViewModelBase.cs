using System;
using System.Windows;
using DrivingLicenceScanner.Entities.Infrastructure;
using DrivingLicenceScanner.EntityFramework;

namespace DrivingLicenceScanner.Infrastructure
{
    public class ViewModelBase : ObjectBase
    {
        private static ViewModelLocator _viewModelLocator;

        public ViewModelBase()
        {
           
            Context = new DrivingLicenceScannerDbContext();
        }

        public DrivingLicenceScannerDbContext Context { get; private set; }

        public static ViewModelLocator ViewModelLocator
        {
            get
            {
                return _viewModelLocator ??
                       (_viewModelLocator =
                           (ViewModelLocator)Application.Current.Resources["ViewModelLocator"]);
            }

        }
    }
}