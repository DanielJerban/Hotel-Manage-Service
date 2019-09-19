using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.Model.Core.DomainModels;
using HMS.Model.Core.ViewModels;
using HMS.Web.Controllers.Base;

namespace HMS.Web.Areas.Admin.Controllers
{
    public class RoomController : BaseController
    {
        // GET: Admin/Room
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Room_Read()
        {
            return Json(uow.Room.GetRooms(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Room_Create(Room_FacilityViewModel model , Guid hotelId)
        {
            if (ModelState.IsValid)
            {
                List<RoomImage> roomImages = new List<RoomImage>();

                foreach (var item in ImageList.PostedFiles)
                {
                    var image = SaveImage(item);
                    roomImages.Add(image);
                }

                var facility = new RoomFacility()
                {
                    SingleBedCount = model.SingleBedCount,
                    DoubleBedCount = model.DoubleBedCount,
                    Entertainment = model.Entertainment
                };
                uow.RoomFacility.Add(facility);


                var room = new Room()
                {
                    RoomNumber = model.RoomNumber,
                    Rate = model.Rate,
                    Description = model.Description,
                    Facility = facility,
                    Images = roomImages,
                    Hotel = uow.Hotel.Get(c => c.Id == hotelId).SingleOrDefault()
                };
                uow.Room.Add(room);

                uow.Complete();

                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Room_Destroy(Guid Id)
        {
            uow.Room.Remove(Id);
            uow.Complete();

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public void GetImages(IEnumerable<HttpPostedFileBase> files)
        {

            foreach (var item in files)
            {
                // Skip the empty file 
                if (item == null)
                {
                    continue;
                }

                var postedImage = ImageList.PostedFiles.SingleOrDefault(c => c.FileName == item.FileName);

                if (postedImage == null)
                {
                    ImageList.PostedFiles.Add(item);
                }
            }
        }

        private RoomImage SaveImage(HttpPostedFileBase file)
        {
            // Save file in the directory 
            // Check if the directory exists 
            if (!Directory.Exists(Server.MapPath("~/App_Data/Uploads")))
            {
                Directory.CreateDirectory(Server.MapPath("~/App_Data/Uploads"));
            }

            var fileName = Path.GetFileName(file.FileName);
            var fileLocation = Path.Combine(Server.MapPath("~/App_Data/Uploads"), fileName);

            file.SaveAs(fileLocation);

            // Checking if the image path exist in the database 
            var imageExist = uow.RoomImage.Get(c => c.Path == fileLocation).SingleOrDefault();
            if (imageExist == null)
            {
                // Save the path to database 
                RoomImage img = new RoomImage()
                {
                    Path = fileLocation,
                    FileName = fileName,
                    TotalFileSize = file.ContentLength
                };
                uow.RoomImage.Add(img);
                uow.Complete();
            }

            var roomImage = uow.RoomImage.Get(c => c.Path == fileLocation).SingleOrDefault();

            return roomImage;
        }


        #region Partial Views 

        public PartialViewResult _CreateRoom()
        {
            ViewBag.HotelId = new SelectList(uow.Hotel.GetAll(), "Id", "Name");
            return PartialView();
        }

        #endregion


    }



    // Static class to save the upload files in the property 
    public static class ImageList
    {
        public static List<HttpPostedFileBase> PostedFiles = new List<HttpPostedFileBase>();
    }
}