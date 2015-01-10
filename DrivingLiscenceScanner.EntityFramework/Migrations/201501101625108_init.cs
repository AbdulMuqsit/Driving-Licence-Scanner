namespace DrivingLicenceScanner.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CheckIns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.Int(nullable: false),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MiddleName = c.String(),
                        DoB = c.DateTime(nullable: false),
                        Sex = c.String(),
                        Height = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                        HairColor = c.String(),
                        EyeColor = c.String(),
                        Street = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                        LicenceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Licences", t => t.LicenceId, cascadeDelete: true)
                .Index(t => t.LicenceId);
            
            CreateTable(
                "dbo.Licences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IssueDate = c.DateTime(nullable: false),
                        ExpiryDate = c.DateTime(nullable: false),
                        Number = c.String(),
                        ClassCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LegalAges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.Int(nullable: false),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "LicenceId", "dbo.Licences");
            DropForeignKey("dbo.CheckIns", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Customers", new[] { "LicenceId" });
            DropIndex("dbo.CheckIns", new[] { "Customer_Id" });
            DropTable("dbo.LegalAges");
            DropTable("dbo.Licences");
            DropTable("dbo.Customers");
            DropTable("dbo.CheckIns");
        }
    }
}
