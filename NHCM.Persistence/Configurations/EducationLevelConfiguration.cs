using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;

namespace NHCM.Persistence.Configurations
{
    public class EducationLevelConfiguration : IEntityTypeConfiguration<EducationLevel>
    {

              

        public void Configure(EntityTypeBuilder<EducationLevel> builder)
        {
            builder.ToTable("EducationLevel", "look");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("nextval('look.educationlevel_id_seq'::regclass)");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Parentid).HasColumnName("parentid");

            builder.Property(e => e.Sorter).HasMaxLength(50);
        }
    
    }
}
