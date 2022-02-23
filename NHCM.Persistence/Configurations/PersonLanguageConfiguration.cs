using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
    public class PersonLanguageConfiguration : IEntityTypeConfiguration<PersonLanguage>
    {
        public void Configure(EntityTypeBuilder<PersonLanguage> builder)
        {

            builder.ToTable("PersonLanguage", "rec");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasColumnType("numeric")
                .HasDefaultValueSql("nextval('rec.personlanguage_id_seq'::regclass)");

            builder.Property(e => e.CreatedOn)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("now()");

            builder.Property(e => e.LanguageId).HasColumnName("LanguageID");

            builder.Property(e => e.PersonId)
                .HasColumnName("PersonID")
                .HasColumnType("numeric");

            builder.Property(e => e.ReferenceNo).HasMaxLength(200);

            builder.HasOne(d => d.Language)
                .WithMany(p => p.PersonLanguage)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_personlanguage_language");

            builder.HasOne(d => d.Person)
                .WithMany(p => p.PersonLanguage)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_personlanguage_person");

            builder.HasOne(d => d.ReadingExpertiseNavigation)
                .WithMany(p => p.PersonLanguageReadingExpertiseNavigation)
                .HasForeignKey(d => d.ReadingExpertise)
                .HasConstraintName("fk_personlanguage_expertise");

            builder.HasOne(d => d.SpeakingExpertiseNavigation)
                .WithMany(p => p.PersonLanguageSpeakingExpertiseNavigation)
                .HasForeignKey(d => d.SpeakingExpertise)
                .HasConstraintName("fk_personlanguage_expertise3");

            builder.HasOne(d => d.UnderstandingExpertiseNavigation)
                .WithMany(p => p.PersonLanguageUnderstandingExpertiseNavigation)
                .HasForeignKey(d => d.UnderstandingExpertise)
                .HasConstraintName("fk_personlanguage_expertise1");

            builder.HasOne(d => d.WritingExpertiseNavigation)
                .WithMany(p => p.PersonLanguageWritingExpertiseNavigation)
                .HasForeignKey(d => d.WritingExpertise)
                .HasConstraintName("fk_personlanguage_expertise2");

        }
    }
}
