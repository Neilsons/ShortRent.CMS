using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Data.ShortRentMap
{
    public class BussinessMap:EntityTypeConfiguration<Business>
    {
        public BussinessMap()
        {
            this.ToTable("Bussiness");
            this.HasKey(c=>c.ID);
            this.Property(c=>c.Name).IsRequired().HasMaxLength(50);
            this.Property(c=>c.NameSpell).IsRequired().HasMaxLength(200);
        }
    }
}
