using HMS.Model.Core.DomainModels;
using HMS.Service.Core.Interfaces;
using HMS.Web.Models;

namespace HMS.Service.Persistance.Repositories
{
    public class RoomPriceRepo : Repository<RoomPrice>, IRoomPriceRepo
    {
        public RoomPriceRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}