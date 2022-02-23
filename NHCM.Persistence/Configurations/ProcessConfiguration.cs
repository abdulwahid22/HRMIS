using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
    public class ProcessConfiguration : IEntityTypeConfiguration<Process>
    {
        public void Configure(EntityTypeBuilder<Process> builder)
        {


            builder.ToTable("Process", "look");
            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .ValueGeneratedNever();
            builder.Property(e => e.Description).HasColumnType("character varying");
           
            builder.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("character varying");
            builder.Property(e => e.ScreenId).HasColumnName("ScreenID");
        }
    }
}
