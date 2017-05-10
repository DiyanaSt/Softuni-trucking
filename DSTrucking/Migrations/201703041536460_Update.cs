namespace DSTrucking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExperiencePeriod",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimePeriod = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.JobApplication", "AmountOfOTRExperienceId", c => c.Int());
            CreateIndex("dbo.JobApplication", "AmountOfOTRExperienceId");
            AddForeignKey("dbo.JobApplication", "AmountOfOTRExperienceId", "dbo.ExperiencePeriod", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobApplication", "AmountOfOTRExperienceId", "dbo.ExperiencePeriod");
            DropIndex("dbo.JobApplication", new[] { "AmountOfOTRExperienceId" });
            DropColumn("dbo.JobApplication", "AmountOfOTRExperienceId");
            DropTable("dbo.ExperiencePeriod");
        }
    }
}
