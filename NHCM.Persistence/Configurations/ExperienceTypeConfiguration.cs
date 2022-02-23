using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;

namespace NHCM.Persistence.Configurations
{
    public class ExperienceTypeConfiguration : IEntityTypeConfiguration<ExperienceType>
    {
        public void Configure(EntityTypeBuilder<ExperienceType> builder)
        {

            builder.ToTable("ExperienceType", "look");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .ValueGeneratedNever();

            builder.Property(e => e.Dari).HasMaxLength(50);

        }
    }
}
