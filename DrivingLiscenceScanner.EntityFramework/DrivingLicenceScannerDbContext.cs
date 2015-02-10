using System.Data.Entity;
using DrivingLicenceScanner.Entities;

namespace DrivingLicenceScanner.EntityFramework
{
    public class DrivingLicenceScannerDbContext : DbContext
    {
        public DrivingLicenceScannerDbContext() : base("DrivingLicenceScannerDb")
        {
            Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CheckIn> CheckIns { get; set; }
        public DbSet<LegalAge> LegalAges { get; set; }
        public DbSet<Licence> Licences { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasRequired(e => e.Licence).WithRequiredPrincipal(e => e.Customer);
        }
    }
}