using HMS.Model.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.DataAccess.EntityConfig
{
    public class FellowConfig : EntityTypeConfiguration<Fellow>
    {
        public FellowConfig()
        {
            this.HasRequired(c => c.Customer).WithMany(c => c.Fellows).HasForeignKey(c => c.CustomerId);
            this.HasRequired(c => c.Reserve).WithMany(c => c.Fellows).HasForeignKey(c => c.ReserveId);
        }
    }
}
