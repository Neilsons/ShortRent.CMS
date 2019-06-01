using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Data.ShortRentMap
{
    public  class CompanyMap:EntityTypeConfiguration<Company>
    {
        public CompanyMap()
        {
            this.ToTable("Company");
            this.Property(c=>c.CompanyImg).HasMaxLength(500);
            this.Property(c => c.CompanyLicense).HasMaxLength(500);
            this.Property(c => c.CompanyMessage).HasMaxLength(100);
            this.Property(c => c.Name).HasMaxLength(50).IsRequired();
            this.Property(c=>c.Score).HasPrecision(18,2).IsRequired();
        }
    }
}
