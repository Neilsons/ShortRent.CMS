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
            this.Property(c=>c.PublishId).IsRequired();
            this.Property(c => c.UserTypeId).IsRequired();
        }
    }
}
