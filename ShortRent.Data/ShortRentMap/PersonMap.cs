using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShortRent.Core.Domain;
using System.Data.Entity.ModelConfiguration;

namespace ShortRent.Data.ShortRentMap
{
    public class PersonMap:EntityTypeConfiguration<Person>
    {
        public PersonMap()
        {
            this.HasKey(c=>c.ID);
            this.Property(c=>c.Name).IsRequired().HasMaxLength(20);
        }
    }
}
