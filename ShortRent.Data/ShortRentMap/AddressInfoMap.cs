using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Data.ShortRentMap
{
    public class AddressInfoMap:EntityTypeConfiguration<AddressInfo>
    {
        public AddressInfoMap()
        {
            this.ToTable("AddressInfo");
            this.HasKey(c=>c.ID);
            this.Property(c=>c.Name).HasMaxLength(100);
            this.Property(c => c.PerId).IsOptional();
            this.HasOptional(c=>c.Parent);
        }
    }
}
