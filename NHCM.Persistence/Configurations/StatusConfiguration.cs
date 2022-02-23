using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {



        public void Configure(EntityTypeBuilder<Status> builder)
        {

            builder.ToTable("Status", "look");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("nextval('look.status_id_seq'::regclass)");

            builder.Property(e => e.Category)
                        .IsRequired()
                        .HasColumnName("category")
                        .HasColumnType("character(2)");

            builder.Property(e => e.Dari).HasMaxLength(50);

            builder.Property(e => e.Name)
                        .IsRequired()
                        .HasMaxLength(50);

            builder.Property(e => e.Pashto).HasMaxLength(50);
        }

    }
}
