using System;

namespace DrivingLicenceScanner.Model
{
    internal interface IExtractor
    {
        DateTime DoB { get; set; }

        int Height { get; set; }

        string Sex { get; set; }

        string ZipCode { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        string EyeColor { get; set; }

        string State { get; set; }

        string City { get; set; }

        string Street { get; set; }

        DateTime LicenceExpireDate { get; set; }

        DateTime LicenceIssueDate { get; set; }

        string LicenceNumber { get; set; }
        string MiddleName { get; set; }
    }
}