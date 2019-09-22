namespace HMS.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Checking",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.Int(nullable: false, identity: true),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
                        ReserveId = c.Guid(),
                        RoomId = c.Guid(nullable: false),
                        CustomerId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.CustomerId)
                .ForeignKey("dbo.Reserve", t => t.ReserveId)
                .ForeignKey("dbo.Room", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.ReserveId)
                .Index(t => t.RoomId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Person",
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
                "dbo.Fellow",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CustomerId = c.Guid(nullable: false),
                        ReserveId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.CustomerId)
                .ForeignKey("dbo.Reserve", t => t.ReserveId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.ReserveId);
            
            CreateTable(
                "dbo.Reserve",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
                        CustomerId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Reserve_Room",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ReserveId = c.Guid(nullable: false),
                        RoomId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Reserve", t => t.ReserveId, cascadeDelete: true)
                .ForeignKey("dbo.Room", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.ReserveId)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.Room",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RoomNumber = c.String(),
                        Rate = c.Int(),
                        Description = c.String(),
                        FacilityId = c.Guid(),
                        HotelId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoomFacility", t => t.FacilityId)
                .ForeignKey("dbo.HotelData", t => t.HotelId, cascadeDelete: true)
                .Index(t => t.FacilityId)
                .Index(t => t.HotelId);
            
            CreateTable(
                "dbo.RoomFacility",
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
                "dbo.HotelData",
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
                "dbo.ContactInfo",
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
                .ForeignKey("dbo.Person", t => t.Person_Id)
                .Index(t => t.Person_Id);
            
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
                .ForeignKey("dbo.Person", t => t.PersonId, cascadeDelete: true)
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
                "dbo.RoomImage",
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
                .ForeignKey("dbo.Room", t => t.Room_Id)
                .Index(t => t.Room_Id);
            
            CreateTable(
                "dbo.RoomPrice",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RoomId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Room", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.Passenger",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CustomerId = c.Guid(),
                        CheckingId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.CustomerId)
                .ForeignKey("dbo.Checking", t => t.CheckingId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.CheckingId);
            
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
                .ForeignKey("dbo.Person", t => t.Id)
                .ForeignKey("dbo.HotelData", t => t.Hotel_Id)
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
                .ForeignKey("dbo.Person", t => t.Id)
                .ForeignKey("dbo.HotelData", t => t.Hotel_Id)
                .Index(t => t.Id)
                .Index(t => t.Hotel_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employee", "Hotel_Id", "dbo.HotelData");
            DropForeignKey("dbo.Employee", "Id", "dbo.Person");
            DropForeignKey("dbo.Customer", "Hotel_Id", "dbo.HotelData");
            DropForeignKey("dbo.Customer", "Id", "dbo.Person");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Checking", "RoomId", "dbo.Room");
            DropForeignKey("dbo.Checking", "ReserveId", "dbo.Reserve");
            DropForeignKey("dbo.Passenger", "CheckingId", "dbo.Checking");
            DropForeignKey("dbo.Checking", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.Passenger", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.Fellow", "ReserveId", "dbo.Reserve");
            DropForeignKey("dbo.Reserve_Room", "RoomId", "dbo.Room");
            DropForeignKey("dbo.RoomPrice", "RoomId", "dbo.Room");
            DropForeignKey("dbo.RoomImage", "Room_Id", "dbo.Room");
            DropForeignKey("dbo.Room", "HotelId", "dbo.HotelData");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "PersonId", "dbo.Person");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ContactInfo", "Person_Id", "dbo.Person");
            DropForeignKey("dbo.Room", "FacilityId", "dbo.RoomFacility");
            DropForeignKey("dbo.Reserve_Room", "ReserveId", "dbo.Reserve");
            DropForeignKey("dbo.Reserve", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.Fellow", "CustomerId", "dbo.Customer");
            DropIndex("dbo.Employee", new[] { "Hotel_Id" });
            DropIndex("dbo.Employee", new[] { "Id" });
            DropIndex("dbo.Customer", new[] { "Hotel_Id" });
            DropIndex("dbo.Customer", new[] { "Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Passenger", new[] { "CheckingId" });
            DropIndex("dbo.Passenger", new[] { "CustomerId" });
            DropIndex("dbo.RoomPrice", new[] { "RoomId" });
            DropIndex("dbo.RoomImage", new[] { "Room_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "PersonId" });
            DropIndex("dbo.ContactInfo", new[] { "Person_Id" });
            DropIndex("dbo.Room", new[] { "HotelId" });
            DropIndex("dbo.Room", new[] { "FacilityId" });
            DropIndex("dbo.Reserve_Room", new[] { "RoomId" });
            DropIndex("dbo.Reserve_Room", new[] { "ReserveId" });
            DropIndex("dbo.Reserve", new[] { "CustomerId" });
            DropIndex("dbo.Fellow", new[] { "ReserveId" });
            DropIndex("dbo.Fellow", new[] { "CustomerId" });
            DropIndex("dbo.Checking", new[] { "CustomerId" });
            DropIndex("dbo.Checking", new[] { "RoomId" });
            DropIndex("dbo.Checking", new[] { "ReserveId" });
            DropTable("dbo.Employee");
            DropTable("dbo.Customer");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Passenger");
            DropTable("dbo.RoomPrice");
            DropTable("dbo.RoomImage");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.ContactInfo");
            DropTable("dbo.HotelData");
            DropTable("dbo.RoomFacility");
            DropTable("dbo.Room");
            DropTable("dbo.Reserve_Room");
            DropTable("dbo.Reserve");
            DropTable("dbo.Fellow");
            DropTable("dbo.Person");
            DropTable("dbo.Checking");
        }
    }
}
