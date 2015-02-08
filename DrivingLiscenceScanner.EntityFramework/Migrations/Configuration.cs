using System.Collections.Generic;
using System.Collections.ObjectModel;
using DrivingLicenceScanner.Entities;

namespace DrivingLicenceScanner.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DrivingLicenceScanner.EntityFramework.DrivingLicenceScannerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DrivingLicenceScanner.EntityFramework.DrivingLicenceScannerDbContext context)
        {
            Random rand = new Random();
            var customers = new List<Customer>();
            var checkIns = new List<CheckIn>();
            var legalAges = new List<LegalAge>();
            for (int i = 0; i < 10; i++)
            {
                legalAges.Add(new LegalAge() { Age = rand.Next(15, 23), Name = Ipsum.GetWord() });
            }
            context.LegalAges.AddRange(legalAges);
        }
    }
}
