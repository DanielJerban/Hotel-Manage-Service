namespace HMS.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateRoomPrice : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RoomPrice", "Price", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RoomPrice", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
