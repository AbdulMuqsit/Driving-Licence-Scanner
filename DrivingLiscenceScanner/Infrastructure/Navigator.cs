namespace DrivingLicenceScanner.Infrastructure
{
    public static class Navigator
    {
        private delegate void ToSwitchView(ViewModelBase child, ViewModelBase parent);

        private static event ToSwitchView OnSwitchView;

        static Navigator()
        {
            OnSwitchView += Navigator_OnSwitchView;
        }

        static void Navigator_OnSwitchView(ViewModelBase child, ViewModelBase parent)
        {
            var propertyInfo = parent.GetType().GetProperty("CurrentChildViewModel");
            if (propertyInfo != null) propertyInfo.SetValue(parent, child, null);
        }

        public static void SwitchView(ViewModelBase child, ViewModelBase parent = null)
        {
            var toSwitchView = OnSwitchView;
            if (parent == null) parent = ViewModelBase.ViewModelLocator.MainViewModel;
            if (toSwitchView != null) toSwitchView(child, parent);
        }
    }
}
