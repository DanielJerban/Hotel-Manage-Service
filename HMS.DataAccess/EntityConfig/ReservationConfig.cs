using System.Data.Entity.ModelConfiguration;
using HMS.Model.Core.DomainModels;

namespace HMS.DataAccess.EntityConfig
{
    public class ReservationConfig : EntityTypeConfiguration<Reservation>
    {
        public ReservationConfig()
        {
            // Reservation * -------------- 1 Customer 
            this.HasRequired(c => c.Customer)
                .WithMany(c => c.Reservations)
                .WillCascadeOnDelete(true);

            // Reservation 1 ------------- * Room
            this.HasMany(c => c.Rooms)
                .WithOptional(c => c.Reservation);
        }
    }
}
