using System.Web.Mvc;
using HMS.Model.Core.DomainModels;
using HMS.Service.Core;
using HMS.Service.Persistance;
using HMS.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HMS.Web.Controllers.Base
{
    public class BaseController : Controller
    {
        protected readonly IUnitOfWork uow;

        public BaseController()
        {
            uow = new UnitOfWork(new ApplicationDbContext());
        }

        static ApplicationDbContext context = new ApplicationDbContext();
        protected RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
        protected ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
    }
}