using HMS.Model.Core.DomainModels;
using HMS.Service.Core.Interfaces;
using HMS.Web.Models;

namespace HMS.Service.Persistance.Repositories
{
    public class FellowRepo : Repository<Fellow>, IFellowRepo
    {
        public FellowRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}