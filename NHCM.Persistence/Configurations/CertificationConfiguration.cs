using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
    class CertificationConfiguration : IEntityTypeConfiguration<Certification>
    {
        public void Configure(EntityTypeBuilder<Certification> builder)
        {
            builder.ToTable("Certification", "look");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("nextval('look.certification_id_seq'::regclass)");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.SkillTypeId).HasColumnName("SkillTypeID");

            builder.HasOne(d => d.SkillType)
                .WithMany(p => p.Certification)
                .HasForeignKey(d => d.SkillTypeId)
                .HasConstraintName("fk_certification_skilltype");
        }
    }
}

