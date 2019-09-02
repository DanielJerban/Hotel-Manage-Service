namespace HMS.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeCustomerSelfRelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customer", "CustomerParent_Id", "dbo.Customer");
            DropIndex("dbo.Customer", new[] { "CustomerParent_Id" });
            DropColumn("dbo.Customer", "CustomerParent_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customer", "CustomerParent_Id", c => c.Guid());
            CreateIndex("dbo.Customer", "CustomerParent_Id");
            AddForeignKey("dbo.Customer", "CustomerParent_Id", "dbo.Customer", "Id");
        }
    }
}
