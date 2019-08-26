using HMS.Model.Core.DomainModels;
using HMS.Service.Core.Interfaces;
using HMS.Web.Models;

namespace HMS.Service.Persistance.Repositories
{
    public class PersonRepo : Repository<Person>, IPersonRepo
    {
        public PersonRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}