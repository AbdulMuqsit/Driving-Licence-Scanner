namespace DrivingLicenceScanner.Entities
{
    public static class Patterns
    {
        private const string AnyPattern = "(.*?)";

        public const string FirstNameInitToken = "DAC";
        public const string FirstNameExitToken = "DDF";

        public const string LastNameInitToken = "DCS";
        public const string LastNameExitToken = "DDE";

        public const string LicenceNumberInitToken = "DAQ";
        public const string LicenceNumberExitToken = "DCS";

        public const string DoBInitToken = "DBB";
        public const string DoBExitToken = "DBA";

        public const string LicenceIssueDateInitToken = "DBD";
        public const string LicenceIssueDateExitToken = "DBB";

        public const string LicenceExpireDateInitToken = "DBA";
        public const string LicenceExpireDateExitToken = "DBC";

        public const string SexInitToken = "DBC";
        public const string SexExitToken = "DAU";

        public const string HeightInitToken = "DAU";
        public const string HeightExitToken = " in";

        public const string EyeColorInitToken = "DAY";
        public const string EyeColorExitToken = "DAG";

        public const string StreetInitToken = "DAG";
        public const string StreetExitToken = "DAI";

        public const string CityInitToken = "DAI";
        public const string CityExitToken = "DAG";

        public const string StateInitToken = "DAJ";
        public const string StateExitToken = "DAK";

        public const string ZipCodeInitToken = "DAK";
        public const string ZipCodeExitToken = "DCF";

        public const string FirstNamePattern = FirstNameInitToken + AnyPattern + FirstNameExitToken;
        public const string LastNamePattern = LastNameInitToken + AnyPattern + LastNameExitToken;
        public const string LicenceNumberPattern = LicenceNumberInitToken + AnyPattern + LicenceNumberExitToken;
        public const string DoBPattern = DoBInitToken + AnyPattern + DoBExitToken;
        public const string LicenceIssueDatePattern = LicenceIssueDateInitToken + AnyPattern + LicenceIssueDateExitToken;
        public const string LicenceExpireDatePattern = LicenceExpireDateInitToken + AnyPattern + LicenceExpireDateExitToken;
        public const string SexPattern = SexInitToken + AnyPattern + SexExitToken;
        public const string HeightPattern = HeightInitToken + AnyPattern + HeightExitToken;
        public const string EyeColorPattern = EyeColorInitToken + AnyPattern + EyeColorExitToken;
        public const string CityPattern = CityInitToken + AnyPattern + CityExitToken;
        public const string StatePattern = StateInitToken + AnyPattern + StateExitToken;
        public const string ZipCodePattern = ZipCodeInitToken + AnyPattern + ZipCodeExitToken;
        public const string StreetPattern = StreetInitToken + AnyPattern + StreetExitToken;

    }
}