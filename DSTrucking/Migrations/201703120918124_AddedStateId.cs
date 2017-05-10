namespace DSTrucking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedStateId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.JobPossition", name: "State_Id", newName: "StateId");
            RenameIndex(table: "dbo.JobPossition", name: "IX_State_Id", newName: "IX_StateId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.JobPossition", name: "IX_StateId", newName: "IX_State_Id");
            RenameColumn(table: "dbo.JobPossition", name: "StateId", newName: "State_Id");
        }
    }
}
