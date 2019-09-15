using HMS.Model.Core.DomainModels;
using HMS.Service.Core.Interfaces;
using HMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Service.Persistance.Repositories
{
    public class VerbalRentRepo : Repository<VerbalRoomRent>, IVerbalRentRepo
    {
        public VerbalRentRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
