using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
    class PersonSkillConfiguration : IEntityTypeConfiguration<PersonSkill>
    {
        public void Configure(EntityTypeBuilder<PersonSkill> builder)
        {


         
                builder.ToTable("PersonSkill", "rec");

                builder.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("numeric")
                    .HasDefaultValueSql("nextval('rec.personskill_id_seq'::regclass)");

                builder.Property(e => e.CertificationDate).HasColumnType("timestamp with time zone");

                builder.Property(e => e.CertificationId).HasColumnName("certificationID");

                builder.Property(e => e.CertifiedFrom).HasMaxLength(200);

                builder.Property(e => e.CreatedOn)
                    .HasColumnType("timestamp with time zone")
                    .HasDefaultValueSql("now()");

                builder.Property(e => e.EndDate).HasColumnType("timestamp with time zone");

                builder.Property(e => e.ExpertiseId).HasColumnName("ExpertiseID");

                builder.Property(e => e.LanguageId).HasColumnName("LanguageID");

                builder.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(200);

                builder.Property(e => e.ModifiedOn)
                    .HasColumnType("timestamp with time zone")
                    .HasDefaultValueSql("now()");

                builder.Property(e => e.PersonId)
                    .HasColumnName("PersonID")
                    .HasColumnType("numeric");

                builder.Property(e => e.ReferenceNo).HasMaxLength(200);

                builder.Property(e => e.Remarks).HasMaxLength(1000);

                builder.Property(e => e.StartDate).HasColumnType("timestamp with time zone");

                builder.HasOne(d => d.Person)
                    .WithMany(p => p.PersonSkill)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_personskill_person");
           

        }
    }
}
