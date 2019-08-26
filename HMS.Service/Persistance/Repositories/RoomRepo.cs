using HMS.Model.Core.DomainModels;
using HMS.Service.Core.Interfaces;
using HMS.Web.Models;

namespace HMS.Service.Persistance.Repositories
{
    public class RoomRepo : Repository<Room> , IRoomRepo
    {
        public RoomRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}