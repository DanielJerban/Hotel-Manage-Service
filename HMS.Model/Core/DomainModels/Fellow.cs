using HMS.Model.Core.DomainModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Model.Core.DomainModels
{
    public class Fellow : ObjectModel
    {
        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public Guid ReserveId { get; set; }
        public virtual Reserve Reserve { get; set; }
    }
}
