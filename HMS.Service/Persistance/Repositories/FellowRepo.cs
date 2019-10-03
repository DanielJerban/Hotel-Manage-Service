using System.Linq;
using HMS.Model.Core.DomainModels;
using HMS.Service.Core.Interfaces;
using HMS.Web.Models;
using System.Data.Entity;

namespace HMS.Service.Persistance.Repositories
{
    public class FellowRepo : Repository<Fellow>, IFellowRepo
    {
        public FellowRepo(ApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<Fellow> IncludeCustomer()
        {
            return context.Fellows.Include(c => c.Customer);
        }
    }
}