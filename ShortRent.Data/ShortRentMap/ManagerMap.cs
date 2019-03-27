using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Data.ShortRentMap
{
    public class ManagerMap:EntityTypeConfiguration<Manager>
    {
        public ManagerMap()
        {
            this.ToTable("Manager");
            this.HasKey(c => c.ID);
            this.Property(c=>c.Name).IsRequired().HasMaxLength(50);
            this.Property(c => c.ControllerName).HasMaxLength(50);
            this.Property(c => c.ActionName).HasMaxLength(50);
            this.Property(c => c.ClassIcons).HasMaxLength(50);
            this.Property(c => c.Color).HasMaxLength(50);
            this.HasOptional(c=>c.Parent);
        }
    }
}
