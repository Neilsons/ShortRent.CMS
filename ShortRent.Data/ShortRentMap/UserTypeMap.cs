using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Data.ShortRentMap
{
   public  class UserTypeMap:EntityTypeConfiguration<UserType>
    {
        public UserTypeMap()
        {
            this.ToTable("UserType");
            this.HasKey(c=>c.ID);
            this.Property(c=>c.IdCardFront).HasMaxLength(200);
            this.Property(c => c.IdCardBack).HasMaxLength(200);
            this.Property(c => c.CompanyName).HasMaxLength(100);
            this.Property(c=>c.CompanyImg).HasMaxLength(500);
            this.HasRequired(c => c.Person).WithMany(c=>c.UserTypes).HasForeignKey(c=>c.PerId);
            this.HasMany(c => c.PublishMsgs).WithRequired().HasForeignKey(c => c.UserTypeInfoId).WillCascadeOnDelete(false);
            this.HasMany(c => c.PerOrComIntros).WithRequired().HasForeignKey(c => c.UserTypeInfoId);
        }
    }
}
