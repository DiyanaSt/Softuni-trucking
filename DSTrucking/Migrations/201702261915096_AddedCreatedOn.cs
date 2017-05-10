namespace DSTrucking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCreatedOn : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.User", newName: "Candidate");
            AddColumn("dbo.JobApplication", "CreatedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobApplication", "CreatedOn");
            RenameTable(name: "dbo.Candidate", newName: "User");
        }
    }
}
