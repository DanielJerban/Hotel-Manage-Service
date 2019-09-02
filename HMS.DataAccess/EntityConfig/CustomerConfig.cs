using System.Data.Entity.ModelConfiguration;
using HMS.Model.Core.DomainModels;

namespace HMS.DataAccess.EntityConfig
{
    class CustomerConfig : EntityTypeConfiguration<Customer>
    {
        public CustomerConfig()
        {
            // Self Relation for fellow-traveler 
            // Customer * -------------- 0-1 CustomerParent
            this.HasOptional(c => c.CustomerParent)
                .WithMany(c => c.CustomerChild).HasForeignKey(c => c.ParentId)
                .WillCascadeOnDelete(false);
        }
    }
}
