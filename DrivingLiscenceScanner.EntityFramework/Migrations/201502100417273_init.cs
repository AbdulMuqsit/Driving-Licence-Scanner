using System.Data.Entity.Migrations;

namespace DrivingLicenceScanner.EntityFramework.Migrations
{
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CheckIns",
                c => new
                {
                    Id = c.Int(false, true),
                    Time = c.DateTime(false),
                    Customer_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.Customer_Id);

            CreateTable(
                "dbo.Customers",
                c => new
                {
                    Id = c.Int(false, true),
                    FirstName = c.String(maxLength: 4000),
                    LastName = c.String(maxLength: 4000),
                    MiddleName = c.String(maxLength: 4000),
                    DoB = c.DateTime(false),
                    Sex = c.String(maxLength: 4000),
                    Height = c.Int(false),
                    Weight = c.Int(false),
                    HairColor = c.String(maxLength: 4000),
                    EyeColor = c.String(maxLength: 4000),
                    Street = c.String(maxLength: 4000),
                    City = c.String(maxLength: 4000),
                    State = c.String(maxLength: 4000),
                    ZipCode = c.String(maxLength: 4000),
                    LicenceId = c.Int(false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Licences",
                c => new
                {
                    Id = c.Int(false),
                    IssueDate = c.DateTime(false),
                    ExpiryDate = c.DateTime(false),
                    Number = c.String(maxLength: 4000),
                    ClassCode = c.String(maxLength: 4000),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Id)
                .Index(t => t.Id);

            CreateTable(
                "dbo.LegalAges",
                c => new
                {
                    Id = c.Int(false, true),
                    Name = c.String(maxLength: 4000),
                    Age = c.Int(false),
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Licences", "Id", "dbo.Customers");
            DropForeignKey("dbo.CheckIns", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Licences", new[] {"Id"});
            DropIndex("dbo.CheckIns", new[] {"Customer_Id"});
            DropTable("dbo.LegalAges");
            DropTable("dbo.Licences");
            DropTable("dbo.Customers");
            DropTable("dbo.CheckIns");
        }
    }
}