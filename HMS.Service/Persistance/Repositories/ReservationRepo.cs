using HMS.Model.Core.DomainModels;
using HMS.Service.Core.Interfaces;
using HMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace HMS.Service.Persistance.Repositories
{
    public class ReservationRepo : Repository<Reservation>, IReservationRepo
    {
        public ReservationRepo(ApplicationDbContext context) : base(context)
        {
        }

        public List<Room> GetEmptyRoom(DateTime fromDate, DateTime toDate)
        {


            throw new NotImplementedException();

        }
    }
}
