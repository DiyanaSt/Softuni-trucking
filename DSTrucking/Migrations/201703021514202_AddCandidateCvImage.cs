namespace DSTrucking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCandidateCvImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobApplication", "CandidateCvImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobApplication", "CandidateCvImage");
        }
    }
}
