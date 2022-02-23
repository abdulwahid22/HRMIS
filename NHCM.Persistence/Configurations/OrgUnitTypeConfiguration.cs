using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
    public class OrgUnitTypeConfiguration : IEntityTypeConfiguration<OrgUnitType>
    {
        public void Configure(EntityTypeBuilder<OrgUnitType> builder)
        {
            builder.ToTable("OrgUnitType", "look");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("nextval('look.\"OrgUnitType_ID_seq\"'::regclass)");

            builder.Property(e => e.IsHead).HasDefaultValueSql("false");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.NameEng).HasMaxLength(250);

            builder.Property(e => e.ParentId).HasColumnName("ParentID");
        }
    }
}
