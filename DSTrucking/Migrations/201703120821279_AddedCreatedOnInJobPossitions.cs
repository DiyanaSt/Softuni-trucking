namespace DSTrucking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCreatedOnInJobPossitions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobPossition", "CreatedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobPossition", "CreatedOn");
        }
    }
}
