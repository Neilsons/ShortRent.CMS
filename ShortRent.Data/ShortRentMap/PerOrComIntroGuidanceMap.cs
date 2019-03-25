using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Data.ShortRentMap
{
    public class PerOrComIntroGuidanceMap:EntityTypeConfiguration<PerOrComIntroGuidance>
    {
        public PerOrComIntroGuidanceMap()
        {
            this.ToTable("PerOrComIntroGuidance");
            this.HasKey(c => c.ID);
            this.Property(c=>c.QuestionMsg).IsRequired().HasMaxLength(200);
        }
    }
}
