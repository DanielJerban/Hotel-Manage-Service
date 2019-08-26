using HMS.Model.Core.DomainModels;
using HMS.Service.Core.Interfaces;
using HMS.Web.Models;

namespace HMS.Service.Persistance.Repositories
{
    public class HotelRepo : Repository<HotelData>, IHotelRepo
    {
        public HotelRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}