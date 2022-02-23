using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
    public class FolderPathConfiguration : IEntityTypeConfiguration<FolderPath>
    {
        public void Configure(EntityTypeBuilder<FolderPath> builder)
        {

            builder.ToTable("FolderPath", "look");

            builder.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('look.\"FolderPath_ID_seq\"'::regclass)");

            builder.Property(e => e.CurrentFolder).HasMaxLength(300);

            builder.Property(e => e.SettingsKey)
                    .IsRequired()
                    .HasMaxLength(100);
            
        }
    }
}
