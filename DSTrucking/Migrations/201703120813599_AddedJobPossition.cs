namespace DSTrucking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedJobPossition : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobPossition",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        FromDate = c.DateTime(),
                        ToDate = c.DateTime(),
                        OpenedPositions = c.Int(),
                        City = c.String(),
                        State_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.State", t => t.State_Id)
                .Index(t => t.State_Id);
            
            AddColumn("dbo.JobApplication", "JobPossitionId", c => c.Int());
            CreateIndex("dbo.JobApplication", "JobPossitionId");
            AddForeignKey("dbo.JobApplication", "JobPossitionId", "dbo.JobPossition", "Id");
            DropColumn("dbo.JobApplication", "JobApplicationType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JobApplication", "JobApplicationType", c => c.Int(nullable: false));
            DropForeignKey("dbo.JobPossition", "State_Id", "dbo.State");
            DropForeignKey("dbo.JobApplication", "JobPossitionId", "dbo.JobPossition");
            DropIndex("dbo.JobPossition", new[] { "State_Id" });
            DropIndex("dbo.JobApplication", new[] { "JobPossitionId" });
            DropColumn("dbo.JobApplication", "JobPossitionId");
            DropTable("dbo.JobPossition");
        }
    }
}
