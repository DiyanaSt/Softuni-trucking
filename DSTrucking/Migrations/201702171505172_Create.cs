namespace DSTrucking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CDLInformation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CDLNumber = c.String(),
                        CDLStateId = c.Int(),
                        CDLSchoolAttended = c.String(),
                        GraduationDate = c.DateTime(),
                        PreviousCDL = c.Boolean(),
                        PreviousCDLNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.State", t => t.CDLStateId)
                .Index(t => t.CDLStateId);
            
            CreateTable(
                "dbo.State",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        Telephone = c.String(),
                        Email = c.String(),
                        BirthDate = c.DateTime(),
                        StreetAddress = c.String(),
                        City = c.String(),
                        StateId = c.Int(),
                        ZipCode = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.State", t => t.StateId)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.JobApplication",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobApplicationType = c.Int(nullable: false),
                        UserId = c.Int(),
                        CDLInformationId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CDLInformation", t => t.CDLInformationId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CDLInformationId);
            
            CreateTable(
                "dbo.Experience",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExperienceTypeName = c.String(),
                        IsChecked = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WorkHistory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ContactPhone = c.String(),
                        PositionHeld = c.String(),
                        FromDate = c.DateTime(),
                        ToDate = c.DateTime(),
                        ReasonsForLiving = c.String(),
                        DateAvailableToStart = c.DateTime(),
                        JobApplicationId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JobApplication", t => t.JobApplicationId)
                .Index(t => t.JobApplicationId);
            
            CreateTable(
                "dbo.File",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        FileType = c.String(),
                        FileContent = c.Binary(),
                        JobApplicationId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JobApplication", t => t.JobApplicationId)
                .Index(t => t.JobApplicationId);
            
            CreateTable(
                "dbo.JobApplicationExperience",
                c => new
                    {
                        ExperienceId = c.Int(nullable: false),
                        JobApplicationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ExperienceId, t.JobApplicationId })
                .ForeignKey("dbo.Experience", t => t.ExperienceId, cascadeDelete: true)
                .ForeignKey("dbo.JobApplication", t => t.JobApplicationId, cascadeDelete: true)
                .Index(t => t.ExperienceId)
                .Index(t => t.JobApplicationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.File", "JobApplicationId", "dbo.JobApplication");
            DropForeignKey("dbo.User", "StateId", "dbo.State");
            DropForeignKey("dbo.WorkHistory", "JobApplicationId", "dbo.JobApplication");
            DropForeignKey("dbo.JobApplication", "UserId", "dbo.User");
            DropForeignKey("dbo.JobApplicationExperience", "JobApplicationId", "dbo.JobApplication");
            DropForeignKey("dbo.JobApplicationExperience", "ExperienceId", "dbo.Experience");
            DropForeignKey("dbo.JobApplication", "CDLInformationId", "dbo.CDLInformation");
            DropForeignKey("dbo.CDLInformation", "CDLStateId", "dbo.State");
            DropIndex("dbo.JobApplicationExperience", new[] { "JobApplicationId" });
            DropIndex("dbo.JobApplicationExperience", new[] { "ExperienceId" });
            DropIndex("dbo.File", new[] { "JobApplicationId" });
            DropIndex("dbo.WorkHistory", new[] { "JobApplicationId" });
            DropIndex("dbo.JobApplication", new[] { "CDLInformationId" });
            DropIndex("dbo.JobApplication", new[] { "UserId" });
            DropIndex("dbo.User", new[] { "StateId" });
            DropIndex("dbo.CDLInformation", new[] { "CDLStateId" });
            DropTable("dbo.JobApplicationExperience");
            DropTable("dbo.File");
            DropTable("dbo.WorkHistory");
            DropTable("dbo.Experience");
            DropTable("dbo.JobApplication");
            DropTable("dbo.User");
            DropTable("dbo.State");
            DropTable("dbo.CDLInformation");
        }
    }
}
