﻿using System.Windows;
using DrivingLicenceScanner.Entities.Infrastructure;
using DrivingLicenceScanner.EntityFramework;

namespace DrivingLicenceScanner.Infrastructure
{
    /*
     *  B22sdaf00007234
        7/24/1968
        3/23/2012
        7/31/2015
        well
        nice
        MIDDLE NAME
        1
        073 in
        HZL
        438 LONNA
        BRICK
        NJ
        07202-3941
     */
    public class ViewModelBase : ObjectBase
    {
        #region Fields

        private static ViewModelLocator _viewModelLocator;
        private string _busyMessage;
        private bool _busyState;

        #endregion

        #region Properties

        public string BusyMessage
        {
            get { return _busyMessage; }
            set
            {
                if (value == _busyMessage) return;
                _busyMessage = value;
                OnPropertyChanged();
            }
        }

        public bool BusyState
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
            get { return new DrivingLicenceScannerDbContext(); }
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

        #endregion
    }
}