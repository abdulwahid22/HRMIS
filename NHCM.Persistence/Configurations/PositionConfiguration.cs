using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {

            builder.ToTable("Position", "org");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasColumnType("numeric")
                .HasDefaultValueSql("nextval('org.position_id_seq'::regclass)");

            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(e => e.LocationId).HasColumnName("LocationID");

            builder.Property(e => e.OrganoGramId).HasColumnName("OrganoGramID");

            builder.Property(e => e.ParentId)
                .HasColumnName("ParentID")
                .HasColumnType("numeric");

            builder.Property(e => e.PlanTypeId).HasColumnName("PlanTypeID");

            builder.Property(e => e.PositionTypeId).HasColumnName("PositionTypeID");

            builder.Property(e => e.SalaryTypeId).HasColumnName("SalaryTypeID");

            builder.Property(e => e.Sorter)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(e => e.StatusId)
                .HasColumnName("StatusID")
                .HasDefaultValueSql("21");

            builder.Property(e => e.WorkingAreaId).HasColumnName("WorkingAreaID");

        }
    }
}
