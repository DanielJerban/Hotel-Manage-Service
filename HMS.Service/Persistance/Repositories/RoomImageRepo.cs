using HMS.Model.Core.DomainModels;
using HMS.Service.Core.Interfaces;
using HMS.Web.Models;

namespace HMS.Service.Persistance.Repositories
{
    public class RoomImageRepo : Repository<RoomImage> , IRoomImageRepo
    {
        public RoomImageRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}