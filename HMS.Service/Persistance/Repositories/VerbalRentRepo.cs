using HMS.Model.Core.DomainModels;
using HMS.Model.Core.ViewModels;
using HMS.Service.Core.Interfaces;
using HMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Data.Entity;

namespace HMS.Service.Persistance.Repositories
{
    public class VerbalRentRepo : Repository<VerbalRoomRent>, IVerbalRentRepo
    {
        public VerbalRentRepo(ApplicationDbContext context) : base(context)
        {
        }

        public List<GetVerbalRentViewModel> GetAllRents()
        {
            var rents = context.VerbalRoomRents.Include(c => c.Customer).Include(c => c.Rooms).ToList();

            List<GetVerbalRentViewModel> vms = new List<GetVerbalRentViewModel>();
            PersianCalendar pc = new PersianCalendar();


            foreach (var rent in rents)
            {
                var rentedRooms = context.Rooms.Where(c => c.VerbalRoomRent.Id == rent.Id).ToList();

                var roomsNumber = new List<string>();
                foreach (var roomnumber in rentedRooms)
                {
                    roomsNumber.Add(roomnumber.RoomNumber);
                }

                DateTime date = new DateTime();

                if (rent.AbsoluteCheckOut != null)
                {
                    date = (DateTime)rent.AbsoluteCheckOut;
                }

                vms.Add(new GetVerbalRentViewModel()
                {
                    Id = rent.Id,
                    CheckIn = pc.GetDayOfMonth(rent.CheckIn) + "/" + pc.GetMonth(rent.CheckIn) + "/" + pc.GetYear(rent.CheckIn),
                    CheckOut = pc.GetDayOfMonth(rent.CheckOut) + "/" + pc.GetMonth(rent.CheckOut) + "/" + pc.GetYear(rent.CheckOut),
                    AbsoluteCheckOut = (rent.AbsoluteCheckOut != null) ? (pc.GetDayOfMonth(date) + "/" + pc.GetMonth(date) + "/" + pc.GetYear(date)) : "--/--/--",
                    //AbsoluteCheckOut = "1398/6/25",
                    ParentCustomerName = rent.Customer.FirstName + ' ' + rent.Customer.LastName,
                    RoomsNumber = roomsNumber
                });
            }

            return vms;
        }
    }
}
