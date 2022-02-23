using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
namespace NHCM.Persistence.Configurations
{
    public class EducationConfiguration : IEntityTypeConfiguration<Education>
    {
        public void Configure(EntityTypeBuilder<Education> builder)
        {

            builder.ToTable("Education", "rec");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasColumnType("numeric")
                .HasDefaultValueSql("nextval('rec.education_id_seq'::regclass)");

            builder.Property(e => e.Course).HasMaxLength(200);

            builder.Property(e => e.CreatedOn)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("now()");

            builder.Property(e => e.Department).HasMaxLength(200);

            builder.Property(e => e.EducationLevelId).HasColumnName("EducationLevelID");

            builder.Property(e => e.EndDate).HasColumnType("date");

            builder.Property(e => e.Faculty).HasMaxLength(200);

            builder.Property(e => e.Institute).HasMaxLength(200);

            builder.Property(e => e.LocationId).HasColumnName("LocationID");

            builder.Property(e => e.Major).HasMaxLength(200);

            builder.Property(e => e.MigratedLocation).HasMaxLength(100);

            builder.Property(e => e.ModifiedBy)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.ModifiedOn)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("now()");

            builder.Property(e => e.OfficialDocumentNo).HasMaxLength(50);

            builder.Property(e => e.PersonId)
                .HasColumnName("PersonID")
                .HasColumnType("numeric");

            builder.Property(e => e.ReferenceNo).HasMaxLength(200);

            builder.Property(e => e.Remarks).HasMaxLength(300);

            builder.Property(e => e.StartDate).HasColumnType("date");

            builder.HasOne(d => d.EducationLevel)
                .WithMany(p => p.Education)
                .HasForeignKey(d => d.EducationLevelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_education_educationlevel");
        }
    }
}
