using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Data.ShortRentMap
{
    public class CollectMap:EntityTypeConfiguration<Collect>
    {
        public CollectMap()
        {
            this.ToTable("Collect");
            this.HasKey(c=>c.ID);
            this.Property(c=>c.UserTypeId).IsRequired();
            this.Property(c => c.CollectCompanyId).IsRequired();
            this.Property(c => c.CollectUserId).IsRequired();
        }
    }
}
