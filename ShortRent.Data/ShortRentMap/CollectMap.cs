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
            this.HasRequired(c => c.CollectUserType).WithMany().HasForeignKey(c => c.CollectUserId);
            this.HasRequired(c => c.UserType).WithMany().HasForeignKey(c => c.UserTypeId).WillCascadeOnDelete(false);
            this.HasRequired(c => c.Company).WithMany().HasForeignKey(c => c.CollectCompanyId);
        }
    }
}
