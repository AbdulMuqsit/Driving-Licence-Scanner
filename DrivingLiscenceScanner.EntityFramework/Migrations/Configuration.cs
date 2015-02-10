using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using DrivingLicenceScanner.Entities;

namespace DrivingLicenceScanner.EntityFramework.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DrivingLicenceScannerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DrivingLicenceScannerDbContext context)
        {
            var rand = new Random();
            var customers = new List<Customer>();
            var checkIns = new List<CheckIn>();
            var legalAges = new List<LegalAge>();

            legalAges.Add(new LegalAge {Age = 17, Name = "Cigarettes"});
            legalAges.Add(new LegalAge {Age = 20, Name = "Alcohol"});
            legalAges.Add(new LegalAge {Age = 21, Name = "Lottery"});

            context.LegalAges.AddRange(legalAges);
        }
    }
}