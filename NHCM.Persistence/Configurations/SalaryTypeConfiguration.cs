using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
    public class SalaryTypeConfiguration : IEntityTypeConfiguration<SalaryType>
    {
        public void Configure(EntityTypeBuilder<SalaryType> builder)
        {
            builder.ToTable("SalaryType", "look");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("nextval('look.salarytype_id_seq'::regclass)");

            builder.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

            builder.Property(e => e.Dari).HasMaxLength(50);

            builder.Property(e => e.ModifiedOn).HasColumnType("timestamp with time zone");

            builder.Property(e => e.Name).HasMaxLength(50);

            builder.Property(e => e.Pashto).HasMaxLength(50);
        }
    }
}
