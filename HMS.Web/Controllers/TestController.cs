using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Web.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult StrtRating()
        {
            return View();
        }

        public ActionResult Upload()
        {
            return View();
        }

        public ActionResult PersianDatePicker()
        {
            return View();
        }

        public ActionResult Table()
        {
            return View();
        }
    }
}