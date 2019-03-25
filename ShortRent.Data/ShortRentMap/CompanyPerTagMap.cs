using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Data.ShortRentMap
{
    public class CompanyPerTagMap:EntityTypeConfiguration<CompanyPerTag>
    {
        public CompanyPerTagMap()
        {
            this.ToTable("CompanyPerTag");
            this.Property(c=>c.Color).HasMaxLength(10).IsRequired();
            this.Property(c=>c.Name).HasMaxLength(20).IsRequired();
        }
    }
}
