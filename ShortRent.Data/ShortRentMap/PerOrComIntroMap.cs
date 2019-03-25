using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Data.ShortRentMap
{
    public class PerOrComIntroMap:EntityTypeConfiguration<PerOrComIntro>
    {
        public PerOrComIntroMap()
        {
            this.ToTable("PerOrComIntro");
            this.HasKey(c=>c.ID);
            this.Property(c=>c.Answer).IsRequired().HasMaxLength(500);
            this.Property(c=>c.QuestionId).IsRequired();
            this.Property(c => c.UserTypeId).IsRequired();
        }
    }
}
