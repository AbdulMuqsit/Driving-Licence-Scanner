using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DrivingLicenceScanner.Entities;
using DrivingLicenceScanner.Entities.Infrastructure;
using DrivingLicenceScanner.EntityFramework;
using DrivingLicenceScanner.Model;

namespace DrivingLicenceScanner.Infrastructure
{
    public class ViewModelBase : ObjectBase
    {
        private static ViewModelLocator _viewModelLocator;
        private BusyState _busyState;

        public BusyState BusyState
        {
            get { return _busyState; }
            set
            {
                if (value == _busyState) return;
                _busyState = value;
                OnPropertyChanged();
            }
        }

        public DrivingLicenceScannerDbContext Context
        {
            get
            {
                return new DrivingLicenceScannerDbContext();
            }
        }

        public static ViewModelLocator ViewModelLocator
        {
            get
            {
                return _viewModelLocator ??
                       (_viewModelLocator =
                           (ViewModelLocator)Application.Current.Resources["ViewModelLocator"]);
            }

        }

        public ViewModelBase()
        {


        }


    }
}