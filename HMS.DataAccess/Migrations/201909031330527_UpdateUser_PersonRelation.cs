namespace HMS.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUser_PersonRelation : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AspNetUsers", new[] { "Person_Id" });
            RenameColumn(table: "dbo.AspNetUsers", name: "Person_Id", newName: "PersonId");
            CreateIndex("dbo.AspNetUsers", "PersonId", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.AspNetUsers", new[] { "PersonId" });
            RenameColumn(table: "dbo.AspNetUsers", name: "PersonId", newName: "Person_Id");
            CreateIndex("dbo.AspNetUsers", "Person_Id");
        }
    }
}
