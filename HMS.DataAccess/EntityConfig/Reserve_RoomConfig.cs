using HMS.Model.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.DataAccess.EntityConfig
{
    public class Reserve_RoomConfig : EntityTypeConfiguration<Reserve_Room>
    {
        public Reserve_RoomConfig()
        {
            this.HasRequired(c => c.Reserve).WithMany(c => c.Reserve_Reserve_Rooms).HasForeignKey(c => c.ReserveId);
            this.HasRequired(c => c.Room).WithMany(c => c.Room_Reserve_Rooms).HasForeignKey(c => c.RoomId);
        }
    }
}
