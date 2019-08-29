using HMS.Model.Core.DomainModels;
using HMS.Model.Core.DTOs.ContactInfo;
using HMS.Web.Controllers.Base;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HMS.Web.Areas.Admin.Controllers
{
    public class ContactController : BaseController
    {
        // GET: Admin/Contact
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult Contact_Read([DataSourceRequest] DataSourceRequest request)
        {
            var contacts = uow.ContactInfo.GetAll().ToList();
            var contactdtos = new List<ContactInfoDto>();


            foreach (var contact in contacts)
            {
                contactdtos.Add(new ContactInfoDto()
                {
                    Id = contact.Id,
                    TelNo = contact.TelNo,
                    TelType = contact.TelType,
                    Address = contact.Address
                });
            }



            DataSourceResult result = contactdtos.ToDataSourceResult(request);

            return Json(result,JsonRequestBehavior.AllowGet);
        }

        public ActionResult Contact_Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<ContactInfoDto> contacts)
        {
            // will keep the inserted entities here. used to return the result later.
            var entities = new List<ContactInfo>();
            if (ModelState.IsValid)
            {
                foreach (var contact in contacts)
                {
                    var entity = new ContactInfo()
                    {
                        Id = contact.Id,
                        TelNo = contact.TelNo,
                        TelType = contact.TelType,
                        Address = contact.Address
                    };
                    uow.ContactInfo.Add(entity);

                    // store the entity for later user 
                    entities.Add(entity);
                }

                uow.Complete();
            }

            return Json(entities.ToDataSourceResult(request, ModelState, contact => new ContactInfoDto()
            {
                Id = contact.Id,
                TelNo = contact.TelNo,
                TelType = contact.TelType,
                Address = contact.Address

            }));
        }

        public ActionResult Contact_Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<ContactInfoDto> contacts)
        {
            // will keep the updated entities here. used to return the result later.
            var entities = new List<ContactInfo>();
            if (ModelState.IsValid)
            {
                foreach (var contact in contacts)
                {
                    var entity = new ContactInfo()
                    {
                        Id = contact.Id,
                        TelType = contact.TelType,
                        TelNo = contact.TelNo,
                        Address = contact.Address
                    };

                    // Store the entity for later use
                    entities.Add(entity);

                    // Attach the entity
                    uow.ContactInfo.Update(entity);
                }

                uow.Complete();

            }
            return Json(entities.ToDataSourceResult(request, ModelState, contact => new ContactInfoDto()
            {
                Id = contact.Id,
                TelNo = contact.TelNo,
                TelType = contact.TelType,
                Address = contact.Address

            }));
        }

        public ActionResult Contact_Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<ContactInfoDto> contacts)
        {
            // will keep the destroyed entities here. used to return the result later 
            var entities = new List<ContactInfo>();
            if (ModelState.IsValid)
            {
                foreach (var contact in contacts)
                {
                    var entity = new ContactInfo()
                    {
                        Id = contact.Id,
                        TelType = contact.TelType,
                        TelNo = contact.TelNo,
                        Address = contact.Address
                    };

                    entities.Add(entity);
                    uow.ContactInfo.Remove(entity);
                }
                uow.Complete();
            }

            return Json(entities.ToDataSourceResult(request, ModelState, contact => new ContactInfoDto()
            {
                Id = contact.Id,
                TelNo = contact.TelNo,
                TelType = contact.TelType,
                Address = contact.Address

            }));
        }
    }
}