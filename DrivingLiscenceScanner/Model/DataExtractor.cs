using System;
using System.Collections.Generic;
using System.Linq;

namespace DrivingLicenceScanner.Model
{
    internal class DataExtractor : IExtractor
    {
        public DataExtractor(string scanText)
        {

            List<string> values = scanText.Split('\n').Select(value => value.Trim(' ', '\r', '\n')).ToList();

            LicenceNumber = values[0];
            DoB = DateTime.Parse(values[1]);

            LicenceIssueDate = DateTime.Parse(values[2]);

            LicenceExpireDate = DateTime.Parse(values[3]);

            LastName = String.Concat(values[4].Substring(0, 1), values[4].Substring(1).ToLower());

            FirstName = String.Concat(values[5].Substring(0, 1), values[5].Substring(1).ToLower());

            MiddleName = String.Concat(values[6].Substring(0, 1), values[6].Substring(1).ToLower());

            Sex = values[7] == "1"
                ? "Male"
                : "Female";

            Height = Int32.Parse(values[8].Replace(" in", ""));

            EyeColor = String.Concat(values[9].Substring(0, 1), values[9].Substring(1).ToLower());

            Street = String.Concat(values[10].Substring(0, 1), values[10].Substring(1).ToLower());

            City = String.Concat(values[11].Substring(0, 1), values[11].Substring(1).ToLower());

            State = String.Concat(values[12].Substring(0, 1), values[12].Substring(1).ToLower());

            ZipCode = values[13];
        }

        public DateTime DoB { get; set; }
        public int Height { get; set; }
        public string Sex { get; set; }
        public string ZipCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EyeColor { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public DateTime LicenceExpireDate { get; set; }
        public DateTime LicenceIssueDate { get; set; }
        public string LicenceNumber { get; set; }
        public string MiddleName { get; set; }
    }
}