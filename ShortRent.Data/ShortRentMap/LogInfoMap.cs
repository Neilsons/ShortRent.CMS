using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Data.ShortRentMap
{
    public class LogInfoMap:EntityTypeConfiguration<LogInfo>
    {
        public LogInfoMap()
        {
            this.ToTable("LogInfo");
            this.HasKey(c=>c.ID);
            this.Property(c=>c.MachineName).HasMaxLength(100);
            this.Property(c=>c.Message).HasMaxLength(50);
            this.Property(c => c.MethodFullName).HasMaxLength(100);
            this.Property(c=>c.ProcessId).IsOptional();
            this.Property(c=>c.ThreadId).IsOptional();
            this.Property(c=>c.ProcessName).HasMaxLength(100);
            this.Property(c=>c.StachTrace).HasColumnType("ntext");
            this.Property(c => c.Exception).HasColumnType("ntext");
            this.Property(c=>c.Catalogue).HasMaxLength(50);
        }
    }
}
