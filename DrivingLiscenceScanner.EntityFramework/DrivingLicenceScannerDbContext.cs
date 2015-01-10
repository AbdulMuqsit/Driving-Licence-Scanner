using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrivingLicenceScanner.Entities;

namespace DrivingLicenceScanner.EntityFramework
{
    class DrivingLicenceScannerDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CheckIn> CheckIns { get; set; }
        public DbSet<LegalAge> LegalAges { get; set; }
        public DbSet<Licence> Licences { get; set; }
        public DrivingLicenceScannerDbContext():base("DrivingLicenceScannerDb")
        {
            
        }
    }
}
