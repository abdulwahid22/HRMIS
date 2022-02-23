using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
    public class ProcessConnectionConfiguration : IEntityTypeConfiguration<ProcessConnection>
    {
        public void Configure(EntityTypeBuilder<ProcessConnection> builder)
        {

            builder.ToTable("ProcessConnection", "look");
            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .ValueGeneratedNever();
            builder.Property(e => e.ConnectionId).HasColumnName("ConnectionID");
            builder.Property(e => e.ProcessId).HasColumnName("ProcessID");
        }
    }
}
