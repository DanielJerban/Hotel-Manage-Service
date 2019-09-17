using HMS.Model.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.DataAccess.EntityConfig
{
    public class ReserveConfig : EntityTypeConfiguration<Reserve>
    {
        public ReserveConfig()
        {
            this.HasRequired(c => c.Customer).WithMany(c => c.Reserves).HasForeignKey(c => c.CustomerId);
        }
    }
}
