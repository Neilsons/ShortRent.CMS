using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using ShortRent.Core.Domain;

namespace ShortRent.Data.ShortRentMap
{
    class IconsInfoMap:EntityTypeConfiguration<IconsInfo>
    {
        public IconsInfoMap()
        {
            this.ToTable("IconsInfo");
            this.HasKey(c=>c.ID);
            this.Property(c=>c.prefix).HasMaxLength(50);
            this.Property(c=>c.Content).HasMaxLength(100);
        }
    }
}