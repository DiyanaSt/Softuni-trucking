namespace DSTrucking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateAvailableToStart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobApplication", "DateAvailableToStart", c => c.DateTime());
            DropColumn("dbo.WorkHistory", "DateAvailableToStart");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorkHistory", "DateAvailableToStart", c => c.DateTime());
            DropColumn("dbo.JobApplication", "DateAvailableToStart");
        }
    }
}
