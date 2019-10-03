using System;
using System.Collections.Generic;
using System.Linq;
using HMS.Model.Core.DomainModels;
using HMS.Model.Core.DTOs.Hotel;

namespace HMS.Service.Core.Interfaces
{
    public interface IFellowRepo : IRepository<Fellow>
    {
        IQueryable<Fellow> IncludeCustomer();
    }
}