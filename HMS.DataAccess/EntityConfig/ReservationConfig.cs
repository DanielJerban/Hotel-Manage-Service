using System.Data.Entity.ModelConfiguration;
using HMS.Model.Core.DomainModels;

namespace HMS.DataAccess.EntityConfig
{
    public class ReservationConfig : EntityTypeConfiguration<Reservation>
    {
        public ReservationConfig()
        {
            this.HasRequired(c => c.Customer)
                .WithMany(c => c.Reservations)
                .WillCascadeOnDelete(true);
        }
    }
}
