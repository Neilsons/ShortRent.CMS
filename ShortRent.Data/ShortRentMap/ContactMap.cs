using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Data.ShortRentMap
{
    public class ContactMap : EntityTypeConfiguration<Contact>
    {
        public ContactMap()
        {
            this.ToTable("Contact");
            this.HasKey(c => c.ID);
            this.Property(c => c.Name).HasMaxLength(50).IsRequired();
            this.Property(c => c.Email).HasMaxLength(100).IsRequired();
            this.Property(c => c.Brief).HasMaxLength(50).IsRequired();
            this.Property(c => c.Content).IsRequired();
        }
    }
}
