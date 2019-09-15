namespace HMS.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactInfoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TelNo = c.String(),
                        TelType = c.Int(nullable: false),
                        Address = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        Person_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Person_Id)
                .Index(t => t.Person_Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        NationalNo = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PersonId = c.Guid(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId, unique: true)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.HotelDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Rate = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RoomNumber = c.String(),
                        Rate = c.Int(),
                        Description = c.String(),
                        FacilityId = c.Guid(nullable: false),
                        HotelId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        Reservation_Id = c.Guid(),
                        VerbalRoomRent_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoomFacilities", t => t.FacilityId, cascadeDelete: true)
                .ForeignKey("dbo.Reservations", t => t.Reservation_Id)
                .ForeignKey("dbo.VerbalRoomRents", t => t.VerbalRoomRent_Id)
                .ForeignKey("dbo.HotelDatas", t => t.HotelId, cascadeDelete: true)
                .Index(t => t.FacilityId)
                .Index(t => t.HotelId)
                .Index(t => t.Reservation_Id)
                .Index(t => t.VerbalRoomRent_Id);
            
            CreateTable(
                "dbo.RoomFacilities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SingleBedCount = c.Byte(nullable: false),
                        DoubleBedCount = c.Byte(nullable: false),
                        Entertainment = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoomImages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UploadUid = c.String(),
                        FileName = c.String(),
                        ContentType = c.String(),
                        Index = c.Long(nullable: false),
                        Total = c.Long(nullable: false),
                        TotalFileSize = c.Long(nullable: false),
                        Path = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        Room_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.Room_Id)
                .Index(t => t.Room_Id);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        Customer_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.VerbalRoomRents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CheckIn = c.DateTime(nullable: false),
                        CheckOut = c.DateTime(nullable: false),
                        AbsoluteCheckOut = c.DateTime(),
                        CustomerId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Passengers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VerbalRoomRentID = c.Guid(),
                        CustomerId = c.Guid(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VerbalRoomRents", t => t.VerbalRoomRentID)
                .ForeignKey("dbo.Customer", t => t.CustomerId)
                .Index(t => t.VerbalRoomRentID)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Hotel_Id = c.Guid(),
                        PassportNo = c.String(),
                        ParentId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Id)
                .ForeignKey("dbo.HotelDatas", t => t.Hotel_Id)
                .Index(t => t.Id)
                .Index(t => t.Hotel_Id);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Hotel_Id = c.Guid(),
                        EmployeeNumber = c.Long(nullable: false),
                        Duty = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Id)
                .ForeignKey("dbo.HotelDatas", t => t.Hotel_Id)
                .Index(t => t.Id)
                .Index(t => t.Hotel_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employee", "Hotel_Id", "dbo.HotelDatas");
            DropForeignKey("dbo.Employee", "Id", "dbo.People");
            DropForeignKey("dbo.Customer", "Hotel_Id", "dbo.HotelDatas");
            DropForeignKey("dbo.Customer", "Id", "dbo.People");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Passengers", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.Rooms", "HotelId", "dbo.HotelDatas");
            DropForeignKey("dbo.Rooms", "VerbalRoomRent_Id", "dbo.VerbalRoomRents");
            DropForeignKey("dbo.Passengers", "VerbalRoomRentID", "dbo.VerbalRoomRents");
            DropForeignKey("dbo.VerbalRoomRents", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.Rooms", "Reservation_Id", "dbo.Reservations");
            DropForeignKey("dbo.Reservations", "Customer_Id", "dbo.Customer");
            DropForeignKey("dbo.RoomImages", "Room_Id", "dbo.Rooms");
            DropForeignKey("dbo.Rooms", "FacilityId", "dbo.RoomFacilities");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "PersonId", "dbo.People");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ContactInfoes", "Person_Id", "dbo.People");
            DropIndex("dbo.Employee", new[] { "Hotel_Id" });
            DropIndex("dbo.Employee", new[] { "Id" });
            DropIndex("dbo.Customer", new[] { "Hotel_Id" });
            DropIndex("dbo.Customer", new[] { "Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Passengers", new[] { "CustomerId" });
            DropIndex("dbo.Passengers", new[] { "VerbalRoomRentID" });
            DropIndex("dbo.VerbalRoomRents", new[] { "CustomerId" });
            DropIndex("dbo.Reservations", new[] { "Customer_Id" });
            DropIndex("dbo.RoomImages", new[] { "Room_Id" });
            DropIndex("dbo.Rooms", new[] { "VerbalRoomRent_Id" });
            DropIndex("dbo.Rooms", new[] { "Reservation_Id" });
            DropIndex("dbo.Rooms", new[] { "HotelId" });
            DropIndex("dbo.Rooms", new[] { "FacilityId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "PersonId" });
            DropIndex("dbo.ContactInfoes", new[] { "Person_Id" });
            DropTable("dbo.Employee");
            DropTable("dbo.Customer");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Passengers");
            DropTable("dbo.VerbalRoomRents");
            DropTable("dbo.Reservations");
            DropTable("dbo.RoomImages");
            DropTable("dbo.RoomFacilities");
            DropTable("dbo.Rooms");
            DropTable("dbo.HotelDatas");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.People");
            DropTable("dbo.ContactInfoes");
        }
    }
}
