using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Data.ShortRentMap
{
    public class EntityPermissionMap:EntityTypeConfiguration<EntityPermission>
    {
        public EntityPermissionMap()
        {
            this.ToTable("EntityPermission");
            this.HasKey(c=>c.ID);
            this.Property(c=>c.Name).IsRequired().HasMaxLength(20);
            this.HasRequired(c=>c.Role).WithMany().HasForeignKey(t=>t.RoleInfoId).WillCascadeOnDelete(true);
        }
    }
}
