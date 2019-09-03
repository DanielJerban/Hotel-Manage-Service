using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.Model.Core.DomainModels;
using HMS.Model.Core.DTOs.Hotel;
using HMS.Web.Controllers.Base;

namespace HMS.Web.Areas.Admin.Controllers
{
    public class HotelController : BaseController
    {
        // GET: Admin/Hotel
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Hotel_Read()
        {
            return Json(uow.Hotel.Read_Hotel(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Hotel_Destroy(Guid? Id)
        {
            var removeStatus = uow.Hotel.Remove_Hotel(Id);

            if (removeStatus == false)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            uow.Complete();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Hotel_Create(CreateHotelDto model)
        {
            if (ModelState.IsValid)
            {
                uow.Hotel.Create_Hotel(model);
                uow.Complete();

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Hotel_Update(CreateHotelDto model, Guid Id)
        {
            if (ModelState.IsValid)
            {
                uow.Hotel.Update_Hotel(model , Id);
                uow.Complete();

                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }



        #region Partial views

        public PartialViewResult _CreateHotel()
        {
            return PartialView();
        }

        public PartialViewResult _UpdateHotel(Guid Id)
        {
            var hotel = uow.Hotel.Find(Id);
            CreateHotelDto model = new CreateHotelDto()
            {
                Name = hotel.Name,
                Rate = hotel.Rate,
                Description = hotel.Description
            };

            ViewBag.hotelId = Id;

            return PartialView(model);
        }


        #endregion

    }
}