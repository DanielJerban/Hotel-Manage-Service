using HMS.Model.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.DataAccess.EntityConfig
{
    public class CheckingConfig : EntityTypeConfiguration<Checking>
    {
        public CheckingConfig()
        {
            this.HasMany(c => c.Passengers).WithRequired(c => c.Checking).HasForeignKey(c => c.CheckingId);
            this.HasOptional(c => c.Reserve).WithMany(c => c.Checkings).HasForeignKey(c => c.ReserveId);
            this.HasRequired(c => c.Room).WithMany(c => c.Checkings).HasForeignKey(c => c.RoomId);
            this.HasRequired(c => c.Customer).WithMany(c => c.Checkings).HasForeignKey(c => c.CustomerId);

            this.Property(c => c.Number).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
