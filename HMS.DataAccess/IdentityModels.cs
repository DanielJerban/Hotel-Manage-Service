using System.Data.Entity;
using HMS.DataAccess.EntityConfig;
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

            base.OnModelCreating(modelBuilder);
        }
    }
}