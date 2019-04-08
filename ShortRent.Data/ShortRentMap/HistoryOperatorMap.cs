using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Data.ShortRentMap
{
    public class HistoryOperatorMap:EntityTypeConfiguration<HistoryOperator>
    {
        public HistoryOperatorMap()
        {
            this.ToTable("HistoryOperator");
            this.HasKey(c=>c.ID);
            this.Property(c => c.EntityModule).IsRequired().HasMaxLength(50);
            this.Property(c => c.Operates).IsRequired().HasMaxLength(50);
            this.Property(c => c.DetailDescirption).IsOptional().HasMaxLength(500);
            this.HasRequired(c => c.Person).WithMany(c=>c.HistoryOperators).HasForeignKey(c =>c.PersonId);
        }
    }
}
