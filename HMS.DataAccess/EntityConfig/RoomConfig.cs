using System.Data.Entity.ModelConfiguration;
using HMS.Model.Core.DomainModels;

namespace HMS.DataAccess.EntityConfig
{
    public class RoomConfig : EntityTypeConfiguration<Room>
    {
        public RoomConfig()
        {
            // Room * ------------ 0-1 Facility
            this.HasOptional(c => c.Facility)
                .WithMany(c => c.Rooms).HasForeignKey(c => c.FacilityId);

            // Room 0-1 ------------ * Image
            this.HasMany(c => c.Images)
                .WithOptional(c => c.Room);

            this.HasMany(c => c.RoomPrices).WithRequired(c => c.Room).HasForeignKey(c => c.RoomId).WillCascadeOnDelete(true);
        }
    }
}
