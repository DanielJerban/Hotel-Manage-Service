using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.Service.Core;
using HMS.Service.Persistance;
using HMS.Web.Models;

namespace HMS.Web.Controllers.Base
{
    public class BaseController : Controller
    {
        protected readonly IUnitOfWork uow;

        public BaseController()
        {
            uow = new UnitOfWork(new ApplicationDbContext());
        }
    }
}