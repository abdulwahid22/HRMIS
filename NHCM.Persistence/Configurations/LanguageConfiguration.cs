using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;
using NHCM.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NHCM.Persistence.Configurations
{
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {

            builder.ToTable("Language", "look");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("nextval('look.language_id_seq'::regclass)");

            builder.Property(e => e.Category).HasMaxLength(20);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

        }
    }
}
