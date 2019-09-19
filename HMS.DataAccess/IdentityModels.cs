using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using HMS.DataAccess.EntityConfig;
using HMS.Model.Core;
using HMS.Model.Core.DomainModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HMS.Web.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // User 0-1 ------------ 1 Person
            modelBuilder.Entity<ApplicationUser>().HasRequired(r => r.Person)
                .WithMany(w => w.User).HasForeignKey(d => d.PersonId).WillCascadeOnDelete(true);

            // TPT(Table Per Type) inheritance from person 
            modelBuilder.Entity<Employee>().ToTable("Employee");

            // Add Reference to config classes 
            modelBuilder.Configurations.Add(new PersonConfig());
            modelBuilder.Configurations.Add(new HotelConfig());
            modelBuilder.Configurations.Add(new CustomerConfig());
            modelBuilder.Configurations.Add(new Reserve_RoomConfig());
            modelBuilder.Configurations.Add(new ReserveConfig());
            modelBuilder.Configurations.Add(new FellowConfig());
            modelBuilder.Configurations.Add(new CheckingConfig());
            modelBuilder.Configurations.Add(new RoomConfig());

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<HotelData> Hotels { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomFacility> RoomFacilities { get; set; }
        public DbSet<RoomImage> RoomImages { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Reserve> Reserves { get; set; }
        public DbSet<Reserve_Room> Reserve_Rooms { get; set; }
        public DbSet<Fellow> Fellows { get; set; }
        public DbSet<Checking> Checkings { get; set; }
        public DbSet<RoomPrice> RoomPrices { get; set; }
    }
}