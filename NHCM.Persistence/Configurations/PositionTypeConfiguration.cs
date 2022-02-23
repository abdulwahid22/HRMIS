using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
    public class PositionTypeConfiguration : IEntityTypeConfiguration<PositionType>
    {
        public void Configure(EntityTypeBuilder<PositionType> builder)
        {

            builder.ToTable("PositionType", "look");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("nextval('look.positiontype_id_seq'::regclass)");

            builder.Property(e => e.IsUnit).HasDefaultValueSql("false");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.NameEng).HasMaxLength(250);

            builder.Property(e => e.OrgUnitTypeId).HasColumnName("OrgUnitTypeID");

            builder.Property(e => e.ParentId).HasColumnName("ParentID");

            builder.Property(e => e.RankId).HasColumnName("RankID");

        }
    }
}
