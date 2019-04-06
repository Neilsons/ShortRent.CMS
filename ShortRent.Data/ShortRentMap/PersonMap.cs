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
            this.ToTable("Person");
            this.HasKey(c=>c.ID);
            this.Property(c=>c.Name).IsRequired().HasMaxLength(20);
            this.Property(c => c.CreditScore).IsRequired().HasPrecision(18,2);
            this.Property(c=>c.IdCard).HasMaxLength(18).IsRequired();
            this.Property(c=>c.PassWord).HasMaxLength(120);
            this.Property(c=>c.PerImage).HasMaxLength(200);
            this.Property(c=>c.PerOrder).IsOptional();
            this.Property(c => c.Qq).HasMaxLength(50);
            this.Property(c => c.WeChat).HasMaxLength(50);
            this.HasMany(c => c.Roles).WithMany().Map(m=> {
                m.ToTable("UserRole");
                m.MapLeftKey("PersonId");
                m.MapRightKey("RoleId");                
            });
        }
    }
}
