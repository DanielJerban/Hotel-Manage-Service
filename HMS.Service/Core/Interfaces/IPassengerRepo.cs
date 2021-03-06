﻿using HMS.Model.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Service.Core.Interfaces
{
    public interface IPassengerRepo : IRepository<Passenger>
    {
        List<string> CheckingPassengers(Guid CheckingId);
    }
}
