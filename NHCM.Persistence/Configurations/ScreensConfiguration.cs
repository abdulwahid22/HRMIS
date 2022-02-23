using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
    public class ScreensConfiguration : IEntityTypeConfiguration<Screens>
    {
        public void Configure(EntityTypeBuilder<Screens> builder)
        {




            builder.ToTable("Screens", "look");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("nextval('look.screens_id_seq'::regclass)");

            builder.Property(e => e.Description).HasMaxLength(500);

            builder.Property(e => e.Icon).HasMaxLength(100);

            builder.Property(e => e.ModuleId).HasColumnName("ModuleID");

            builder.Property(e => e.ParentId).HasColumnName("ParentID");

            builder.Property(e => e.Path).HasMaxLength(500);

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(d => d.Parent)
                .WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("Screens_ParentID_fkey");





        }
    }
}
