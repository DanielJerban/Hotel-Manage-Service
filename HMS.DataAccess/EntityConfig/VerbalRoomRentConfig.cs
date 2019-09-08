using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using HMS.Model.Core.DomainModels;

namespace HMS.DataAccess.EntityConfig
{
    public class VerbalRoomRentConfig : EntityTypeConfiguration<VerbalRoomRent>
    {
        public VerbalRoomRentConfig()
        {
            this.HasRequired(c => c.Customer)
                .WithMany(c => c.VerbalRoomRents)
                .WillCascadeOnDelete(false);

            this.HasMany(c => c.Rooms)
                .WithOptional(c => c.VerbalRoomRent)
                .WillCascadeOnDelete(false);
        }
    }
}
