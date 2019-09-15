using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using HMS.Model.Core.DomainModels;

namespace HMS.DataAccess.EntityConfig
{
    public class CustomerConfig : EntityTypeConfiguration<Customer>
    {
        public CustomerConfig()
        {
            this.HasMany(c => c.Passengers)
                .WithOptional(c => c.Customer)
                .HasForeignKey(c => c.CustomerId);

            this.ToTable("Customer");

        }
    }
}
