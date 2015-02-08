namespace DrivingLicenceScanner.Infrastructure
{
    public static class Navigator
    {
        public static void SwitchView(ViewModelBase child, ViewModelBase parent = null)
        {
            if (parent == null) parent = ViewModelBase.ViewModelLocator.MainViewModel;
            var propertyInfo = parent.GetType().GetProperty("CurrentChildViewModel");
            if (propertyInfo != null) propertyInfo.SetValue(parent, child, null);
        }
    }
}
