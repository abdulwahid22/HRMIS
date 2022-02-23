using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
    public class ReferenceTypeConfiguration : IEntityTypeConfiguration<ReferenceType>
    {
        public void Configure(EntityTypeBuilder<ReferenceType> builder)
        {

            builder.ToTable("ReferenceType", "look");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("nextval('look.referencetype_id_seq'::regclass)");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

        }
    }
}
