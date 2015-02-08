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
                        Time = c.DateTime(nullable: false),
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
                        FirstName = c.String(maxLength: 4000),
                        LastName = c.String(maxLength: 4000),
                        MiddleName = c.String(maxLength: 4000),
                        DoB = c.DateTime(nullable: false),
                        Sex = c.String(maxLength: 4000),
                        Height = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                        HairColor = c.String(maxLength: 4000),
                        EyeColor = c.String(maxLength: 4000),
                        Street = c.String(maxLength: 4000),
                        City = c.String(maxLength: 4000),
                        State = c.String(maxLength: 4000),
                        ZipCode = c.String(maxLength: 4000),
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
                        Number = c.String(maxLength: 4000),
                        ClassCode = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LegalAges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 4000),
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
