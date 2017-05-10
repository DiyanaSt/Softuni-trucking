namespace DSTrucking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsActive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobPossition", "IsActive", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobPossition", "IsActive");
        }
    }
}
