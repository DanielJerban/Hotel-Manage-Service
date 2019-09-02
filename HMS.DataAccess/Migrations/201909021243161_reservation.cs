namespace HMS.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reservation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservation_Room",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        Reservation_Id = c.Guid(nullable: false),
                        Room_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Reservations", t => t.Reservation_Id, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.Room_Id, cascadeDelete: true)
                .Index(t => t.Reservation_Id)
                .Index(t => t.Room_Id);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        Customer_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservation_Room", "Room_Id", "dbo.Rooms");
            DropForeignKey("dbo.Reservation_Room", "Reservation_Id", "dbo.Reservations");
            DropForeignKey("dbo.Reservations", "Customer_Id", "dbo.Customer");
            DropIndex("dbo.Reservations", new[] { "Customer_Id" });
            DropIndex("dbo.Reservation_Room", new[] { "Room_Id" });
            DropIndex("dbo.Reservation_Room", new[] { "Reservation_Id" });
            DropTable("dbo.Reservations");
            DropTable("dbo.Reservation_Room");
        }
    }
}
