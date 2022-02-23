using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;

namespace NHCM.Persistence.Configurations
{
    public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.ToTable("Organization", "dbo");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("nextval('dbo.\"Organization_ID_seq\"'::regclass)");

            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(e => e.Dari)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.OrgUnitTypeId).HasColumnName("OrgUnitTypeID");

            builder.Property(e => e.Pashto)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.StatusId).HasColumnName("StatusID");
        }
    }
}
