using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;

namespace NHCM.Persistence.Configurations
{
    public class ProcessTrackingConfiguration : IEntityTypeConfiguration<ProcessTracking>
    {
        public void Configure(EntityTypeBuilder<ProcessTracking> builder)
        {
            builder.ToTable("ProcessTracking", "dbo");
            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("nextval('dbo.processtracking_id_seq'::regclass)");
            builder.Property(e => e.ModuleId).HasColumnName("ModuleID");
            builder.Property(e => e.ProcessId).HasColumnName("ProcessID");
            builder.Property(e => e.RecordId).HasColumnName("RecordID");
            builder.Property(e => e.ReferedProcessId).HasColumnName("ReferedProcessID");
            builder.Property(e => e.StatusId).HasColumnName("StatusID");
            builder.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");
        }
    }
}
