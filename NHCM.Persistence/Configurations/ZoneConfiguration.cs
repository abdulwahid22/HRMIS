using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
    public class ZoneConfiguration : IEntityTypeConfiguration<Zones>
    {
        public void Configure(EntityTypeBuilder<Zones> builder)
        {
            builder.ToTable("Zones", "look");

            builder.Property(e => e.ID)
                .HasColumnName("ID");
               // .HasColumnType("numeric");
                


        }
    }
}
