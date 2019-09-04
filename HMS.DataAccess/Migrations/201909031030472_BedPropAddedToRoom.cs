namespace HMS.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BedPropAddedToRoom : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoomFacilities", "SingleBedCount", c => c.Byte(nullable: false));
            AddColumn("dbo.RoomFacilities", "DoubleBedCount", c => c.Byte(nullable: false));
            DropColumn("dbo.RoomFacilities", "BedModel");
            DropColumn("dbo.RoomFacilities", "bedCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RoomFacilities", "bedCount", c => c.Byte(nullable: false));
            AddColumn("dbo.RoomFacilities", "BedModel", c => c.Int(nullable: false));
            DropColumn("dbo.RoomFacilities", "DoubleBedCount");
            DropColumn("dbo.RoomFacilities", "SingleBedCount");
        }
    }
}
