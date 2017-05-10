namespace DSTrucking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTruckInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobApplication", "Make", c => c.String());
            AddColumn("dbo.JobApplication", "Year", c => c.String());
            AddColumn("dbo.JobApplication", "CompanyName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobApplication", "CompanyName");
            DropColumn("dbo.JobApplication", "Year");
            DropColumn("dbo.JobApplication", "Make");
        }
    }
}
