using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
    public class PlanTypeConfiguration : IEntityTypeConfiguration<PlanType>
    {
        public void Configure(EntityTypeBuilder<PlanType> builder)
        {

            builder.ToTable("PlanType", "look");

            builder.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

            builder.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Property(e => e.ParentId).HasColumnName("ParentID");
           

        }
    }
}
