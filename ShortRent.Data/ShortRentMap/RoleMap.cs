using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Data.ShortRentMap
{
    public class RoleMap:EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            this.ToTable("Role");
            this.Property(c => c.Name).IsRequired().HasMaxLength(100);
        }
    }
}
