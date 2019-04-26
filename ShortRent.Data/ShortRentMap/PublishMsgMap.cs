using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Data.ShortRentMap
{
    public class PublishMsgMap:EntityTypeConfiguration<PublishMsg>
    {
        public PublishMsgMap()
        {
            this.ToTable("PublishMsg");
            this.Property(c=>c.Address).HasMaxLength(200);
            this.Property(c=>c.Decription).HasMaxLength(200);
            this.Property(c=>c.Email).HasMaxLength(50);
            this.Property(c=>c.Phone).HasMaxLength(20);
            this.Property(c => c.Currency).HasMaxLength(20);
            this.HasRequired(c=>c.Business).WithMany().HasForeignKey(c=>c.BusinessTypeId);
            this.HasRequired(c => c.UserType).WithMany().HasForeignKey(c => c.UserTypeInfoId).WillCascadeOnDelete(false);
            this.HasMany(c => c.Discusss).WithRequired(c => c.PublishMsg).HasForeignKey(c => c.PublishId);
            this.HasMany(c=>c.CompanyPerTags).WithMany().Map(m =>
            {
                m.ToTable("PublishCompanyTags");
                m.MapLeftKey("PublishId");
                m.MapRightKey("TagsId");
            });
        }
    }
}
