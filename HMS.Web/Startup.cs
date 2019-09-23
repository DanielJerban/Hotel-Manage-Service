using System;
using System.Collections.Generic;
using System.Linq;
using HMS.Model.Core.DomainModels;
using HMS.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HMS.Web.Startup))]
namespace HMS.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateAdmin();
        }

        public void CreateAdmin()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var currentUser = context.Users.SingleOrDefault(c => c.Email == "Admin@Admin.com");

            if (currentUser == null)
            {
                PasswordHasher hasher = new PasswordHasher();
                Random random = new Random();

                List<ContactInfo> contacts = new List<ContactInfo>()
                {
                    new ContactInfo()
                    {
                        TelNo = "09122119554",
                        Address = "Tehran"
                    }
                };
                contacts.ForEach(c => context.ContactInfos.Add(c));

                // Create new customer 
                Employee admin = new Employee()
                {
                    FirstName = "Daniel",
                    LastName = "Jerban",
                    NationalNo = "0021359253",
                    Infos = contacts,
                    Duty = "Administrator",
                };
                context.Employees.Add(admin);


                // Create new user 
                ApplicationUser user = new ApplicationUser()
                {
                    Email = "Admin@Admin.com",
                    UserName = "Admin@Admin.com",
                    PasswordHash = hasher.HashPassword("2181998"),
                    Person = admin,
                    SecurityStamp = random.Next(100000).ToString()
                };
                context.Users.Add(user);

                if (!roleManager.RoleExists("Admin"))
                {
                    roleManager.Create(new IdentityRole()
                    {
                        Name = "Admin"
                    });
                }
                context.SaveChanges();


                // Set Role 
                userManager.AddToRole(user.Id, "Admin");
            }
        }
    }
}
