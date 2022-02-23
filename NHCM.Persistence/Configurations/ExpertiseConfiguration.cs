using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;

namespace NHCM.Persistence.Configurations
{
    public class ExpertiseConfiguration : IEntityTypeConfiguration<Expertise>
    {
        public void Configure(EntityTypeBuilder<Expertise> builder)
        {

            builder.ToTable("Expertise", "look");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("nextval('look.expertise_id_seq'::regclass)");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

        }
    }
}
