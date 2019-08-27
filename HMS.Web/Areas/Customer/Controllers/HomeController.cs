using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Web.Areas.Customer.Controllers
{
    [Authorize(Roles = "User")]
    public class HomeController : Controller
    {
        // GET: Customer/CustomerHome
        public ActionResult Index()
        {
            return View();
        }
    }
}