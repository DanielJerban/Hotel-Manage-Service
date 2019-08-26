namespace HMS.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialize : DbMigration
    {
        public override void Up()
        {
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
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
                        Person_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Person_Id, cascadeDelete: true)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Person_Id);
            
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
                "dbo.HotelDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RoomNumber = c.String(),
                        Rate = c.Int(nullable: false),
                        Description = c.String(),
                        FacilityId = c.Guid(nullable: false),
                        HotelId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoomFacilities", t => t.FacilityId, cascadeDelete: true)
                .ForeignKey("dbo.HotelDatas", t => t.HotelId, cascadeDelete: true)
                .Index(t => t.FacilityId)
                .Index(t => t.HotelId);
            
            CreateTable(
                "dbo.RoomFacilities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BedModel = c.Int(nullable: false),
                        bedCount = c.Byte(nullable: false),
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
                "dbo.Customer",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CustomerParent_Id = c.Guid(),
                        Hotel_Id = c.Guid(),
                        PassportNo = c.String(),
                        ParentId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Id)
                .ForeignKey("dbo.Customer", t => t.CustomerParent_Id)
                .ForeignKey("dbo.HotelDatas", t => t.Hotel_Id)
                .Index(t => t.Id)
                .Index(t => t.CustomerParent_Id)
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
            DropForeignKey("dbo.Customer", "CustomerParent_Id", "dbo.Customer");
            DropForeignKey("dbo.Customer", "Id", "dbo.People");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Person_Id", "dbo.People");
            DropForeignKey("dbo.Rooms", "HotelId", "dbo.HotelDatas");
            DropForeignKey("dbo.RoomImages", "Room_Id", "dbo.Rooms");
            DropForeignKey("dbo.Rooms", "FacilityId", "dbo.RoomFacilities");
            DropForeignKey("dbo.ContactInfoes", "Person_Id", "dbo.People");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.Employee", new[] { "Hotel_Id" });
            DropIndex("dbo.Employee", new[] { "Id" });
            DropIndex("dbo.Customer", new[] { "Hotel_Id" });
            DropIndex("dbo.Customer", new[] { "CustomerParent_Id" });
            DropIndex("dbo.Customer", new[] { "Id" });
            DropIndex("dbo.RoomImages", new[] { "Room_Id" });
            DropIndex("dbo.Rooms", new[] { "HotelId" });
            DropIndex("dbo.Rooms", new[] { "FacilityId" });
            DropIndex("dbo.ContactInfoes", new[] { "Person_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Person_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.Employee");
            DropTable("dbo.Customer");
            DropTable("dbo.RoomImages");
            DropTable("dbo.RoomFacilities");
            DropTable("dbo.Rooms");
            DropTable("dbo.HotelDatas");
            DropTable("dbo.ContactInfoes");
            DropTable("dbo.People");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
