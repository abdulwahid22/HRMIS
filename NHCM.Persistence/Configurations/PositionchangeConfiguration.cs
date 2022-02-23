using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
    public class PositionchangeConfiguration : IEntityTypeConfiguration<PositionChange>
    {
        public void Configure(EntityTypeBuilder<PositionChange> builder)
        {
             
                builder.ToTable("PositionChange", "org");

                builder.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("numeric")
                    .HasDefaultValueSql("nextval('org.positionchange_id_seq'::regclass)");

                builder.Property(e => e.IsAddition)
                    .IsRequired()
                    .HasDefaultValueSql("true");

                builder.Property(e => e.Name).HasMaxLength(100);

                builder.Property(e => e.NewPositionId)
                    .HasColumnName("NewPositionID")
                    .HasColumnType("numeric");

                builder.Property(e => e.OrganogramId).HasColumnName("OrganogramID");

                builder.Property(e => e.PlanTypeId).HasColumnName("PlanTypeID");

                builder.Property(e => e.PositionId)
                    .HasColumnName("PositionID")
                    .HasColumnType("numeric");

                builder.HasOne(d => d.Organogram)
                    .WithMany(p => p.PositionChange)
                    .HasForeignKey(d => d.OrganogramId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_positionchange_organogram");

   
            
        }
    }
}
