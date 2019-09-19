using HMS.Model.Core.DomainModels;
using HMS.Service.Core.Interfaces;
using HMS.Web.Models;

namespace HMS.Service.Persistance.Repositories
{
    public class Reserve_RoomRepo : Repository<Reserve_Room>, IReserve_RoomRepo
    {
        public Reserve_RoomRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}