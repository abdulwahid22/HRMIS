using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
namespace NHCM.Persistence.Configurations
{
    public class PublicationTypeConfiguration : IEntityTypeConfiguration<PublicationType>
    {
        public void Configure(EntityTypeBuilder<PublicationType> builder)
        {

            builder.ToTable("PublicationType", "look");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("nextval('look.publicationtype_id_seq'::regclass)");

            builder.Property(e => e.Dari).HasMaxLength(50);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Pashto).HasMaxLength(50);

        }
    }
}
