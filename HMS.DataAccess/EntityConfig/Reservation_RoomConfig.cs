using System.Data.Entity.ModelConfiguration;
using HMS.Model.Core;

namespace HMS.DataAccess.EntityConfig
{
    public class Reservation_RoomConfig : EntityTypeConfiguration<Reservation_Room>
    {
        public Reservation_RoomConfig()
        {
            this.HasRequired(c => c.Reservation).WithMany(c => c.Rooms);
            this.HasRequired(c => c.Room).WithMany(c => c.Reservations);
        }
    }
}
