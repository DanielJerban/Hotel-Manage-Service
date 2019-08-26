using System.Data.Entity.ModelConfiguration;
using HMS.Model.Core.DomainModels;

namespace HMS.DataAccess.EntityConfig
{
    public class HotelConfig : EntityTypeConfiguration<HotelData>
    {
        public HotelConfig()
        {
            // Hotel 0-1 ------------ * Customer 
            this.HasMany(c => c.Customers)
                .WithOptional(c => c.Hotel);

            // Hotel 0-1 ------------ * Employee
            this.HasMany(c => c.Employees)
                .WithOptional(c => c.Hotel);

            // Hotel 1 ------------ * Room
            this.HasMany(c => c.Rooms)
                .WithRequired(c => c.Hotel);
        }
    }
}
