using System.Data.Entity.ModelConfiguration;
using HMS.Model.Core.DomainModels;

namespace HMS.DataAccess.EntityConfig
{
    public class PersonConfig : EntityTypeConfiguration<Person>
    {
        public PersonConfig()
        {
            // Person 0-1 --------- * ContactInfo 
            this.HasMany(c => c.Infos)
                .WithOptional(c => c.Person);
        }
    }
}
