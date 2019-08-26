using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Model.Core.DomainModels;
using HMS.Service.Core.Interfaces;
using HMS.Web.Models;

namespace HMS.Service.Persistance.Repositories
{
    public class RoomFacilityRepo : Repository<RoomFacility>, IRoomFacilityRepo
    {
        public RoomFacilityRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
