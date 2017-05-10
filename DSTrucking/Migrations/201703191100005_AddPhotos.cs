namespace DSTrucking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhotos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CDLInformation", "CDLPhoto", c => c.String());
            AddColumn("dbo.CDLInformation", "MedicalPhoto", c => c.String());
            AddColumn("dbo.Candidate", "SSNPhoto", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Candidate", "SSNPhoto");
            DropColumn("dbo.CDLInformation", "MedicalPhoto");
            DropColumn("dbo.CDLInformation", "CDLPhoto");
        }
    }
}
