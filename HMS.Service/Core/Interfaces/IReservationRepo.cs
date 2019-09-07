﻿using HMS.Model.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Service.Core.Interfaces
{
    public interface IReservationRepo : IRepository<Reservation>
    {
        List<Room> GetEmptyRoom(DateTime fromDate, DateTime toDate);
    }
}
