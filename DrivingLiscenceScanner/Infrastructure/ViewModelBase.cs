using System;
using System.Windows;
using DrivingLicenceScanner.Entities.Infrastructure;
using DrivingLicenceScanner.EntityFramework;

namespace DrivingLicenceScanner.Infrastructure
{
    public class ViewModelBase : ObjectBase
    {
        private static ViewModelLocator _viewModelLocator;


        public DrivingLicenceScannerDbContext Context { get { return new DrivingLicenceScannerDbContext(); } }

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