using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            



                builder.ToTable("Gender", "look");

                builder.Property(e => e.ID)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('look.gender_id_seq'::regclass)");

                builder.Property(e => e.Dari).HasMaxLength(50);

                builder.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                builder.Property(e => e.Pashto).HasMaxLength(50);
           



        }
    }
}
