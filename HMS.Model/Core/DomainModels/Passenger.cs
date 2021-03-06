﻿using HMS.Model.Core.DomainModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Model.Core.DomainModels
{
    public class Passenger : ObjectModel
    {
        public Guid? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public Guid CheckingId { get; set; }
        public virtual Checking Checking { get; set; }
    }
}
