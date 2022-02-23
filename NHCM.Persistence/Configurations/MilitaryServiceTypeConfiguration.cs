using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
    public class MilitaryServiceTypeConfiguration : IEntityTypeConfiguration<MilitaryServiceType>
    {
        public void Configure(EntityTypeBuilder<MilitaryServiceType> builder)
        {

            builder.ToTable("MilitaryServiceType", "look");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .ValueGeneratedNever();

            builder.Property(e => e.Name)
                        .IsRequired()
                        .HasMaxLength(100);
        }
    }
}
