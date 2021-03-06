﻿using HMS.Model.Core.DomainModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Model.Core.DomainModels
{
    public enum Status
    {
        Payed,
        Absolute,
        Temporary,
        Canceled 
    }

    public class Reserve : ObjectModel
    {
        public int Number { get; set; }
        public Status Status { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual ICollection<Fellow> Fellows { get; set; }
        public virtual ICollection<Reserve_Room> Reserve_Reserve_Rooms { get; set; }
        public virtual ICollection<Checking> Checkings { get; set; }
    }
}
