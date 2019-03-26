using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Data.ShortRentMap
{
    public class PermissionMap:EntityTypeConfiguration<Permission>
    {
        public PermissionMap()
        {
            this.ToTable("Permission");
            this.HasKey(c=>c.ID);
            this.Property(c=>c.Name).IsRequired().HasMaxLength(100);
            this.Property(c => c.Description).IsRequired().HasMaxLength(50);
            this.Property(c => c.Category).IsRequired().HasMaxLength(50);
        }
    }
}
