using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Data.ShortRentMap
{
    public class DiscussMap:EntityTypeConfiguration<Discuss>
    {
        public DiscussMap()
        {
            this.ToTable("Discuss");
            this.HasKey(c=>c.ID);
            this.Property(c=>c.Floor).IsOptional();
            this.Property(c=>c.Message).IsRequired().HasMaxLength(200);
            this.HasRequired(c => c.UserType).WithMany().HasForeignKey(c=>c.UserTypeId);
            this.HasRequired(c => c.PublishMsg).WithMany().HasForeignKey(c => c.PublishId);
            this.HasRequired(c=>c.Parent).WithMany(c=>c.ChildDiscuss).HasForeignKey(c=>c.ParentId);
        }
    }
}
