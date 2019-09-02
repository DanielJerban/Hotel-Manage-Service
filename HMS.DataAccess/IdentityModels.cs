using System.Data.Entity;
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
            modelBuilder.Entity<ApplicationUser>().HasRequired(r => r.Person).WithOptional(w => w.User).WillCascadeOnDelete(true);

            // TPT(Table Per Type) inheritance from person 
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Employee>().ToTable("Employee");

            // Add Reference to config classes 
            modelBuilder.Configurations.Add(new PersonConfig());
            modelBuilder.Configurations.Add(new HotelConfig());
            modelBuilder.Configurations.Add(new ReservationConfig());
            modelBuilder.Configurations.Add(new Reservation_RoomConfig());

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
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Reservation_Room> Reservation_Room { get; set; }
    }
}