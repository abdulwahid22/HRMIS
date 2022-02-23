using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
    public class OrgPositionConfiguration : IEntityTypeConfiguration<OrgPosition>
    {
        public void Configure(EntityTypeBuilder<OrgPosition> builder)
        {
            builder.ToTable("OrgPosition", "look");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("nextval('look.\"OrgPosition_ID_seq\"'::regclass)");

            builder.Property(e => e.OrgUnitTypeId).HasColumnName("OrgUnitTypeID");

            builder.Property(e => e.ParentId).HasColumnName("ParentID");

            builder.Property(e => e.PositionTypeId).HasColumnName("PositionTypeID");

            builder.Property(e => e.RankId).HasColumnName("RankID");
        }
    }
}
