namespace HMS.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRatePropToHotel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HotelDatas", "Rate", c => c.Int());
            AlterColumn("dbo.Rooms", "Rate", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Rooms", "Rate", c => c.Int(nullable: false));
            DropColumn("dbo.HotelDatas", "Rate");
        }
    }
}
