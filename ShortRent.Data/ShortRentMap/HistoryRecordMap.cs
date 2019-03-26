using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Data.ShortRentMap
{
    public class HistoryRecordMap:EntityTypeConfiguration<HistoryRecord>
    {
        public HistoryRecordMap()
        {
            this.ToTable("HistoryRecord");
            this.HasKey(c=>c.ID);
            this.Property(c => c.PublishId).IsRequired();
            this.Property(c => c.UserTypeId).IsRequired();
        }
    }
}
