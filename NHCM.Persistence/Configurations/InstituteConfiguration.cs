using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
   public class InstituteConfiguration : IEntityTypeConfiguration<Institute>
    {


        
               

        public void Configure(EntityTypeBuilder<Institute> builder)
        {
            builder.ToTable("Institute", "look");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .ValueGeneratedNever();

            builder.Property(e => e.LocationId).HasColumnName("LocationID");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.ParentId).HasColumnName("ParentID");

            builder.HasOne(d => d.Parent)
                .WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("fk_institute_institute1");
        }
    }
}
