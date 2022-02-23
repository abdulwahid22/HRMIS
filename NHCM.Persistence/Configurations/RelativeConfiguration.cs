using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
    public class RelativeConfiguration : IEntityTypeConfiguration<Relative>
    {
        public void Configure(EntityTypeBuilder<Relative> builder)
        {
            // CHANGE: delete extra column configuration if decision is taken to delete them.

            builder.ToTable("Relative", "rec");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasColumnType("numeric")
                .HasDefaultValueSql("nextval('rec.relative_id_seq'::regclass)");

            builder.Property(e => e.Address).HasMaxLength(200);

            builder.Property(e => e.ContactInfo).HasMaxLength(250);

            builder.Property(e => e.CreatedOn)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("now()");

            builder.Property(e => e.CurrentLocationId).HasColumnName("CurrentLocationID");

            builder.Property(e => e.CurrentVillage).HasMaxLength(50);

            builder.Property(e => e.EmailAddress).HasMaxLength(50);

            builder.Property(e => e.FatherName)
                .IsRequired()
                .HasMaxLength(40);

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(40);

            builder.Property(e => e.GrandFatherName).HasMaxLength(40);

            builder.Property(e => e.JobLocation).HasMaxLength(400);

            builder.Property(e => e.LastName).HasMaxLength(40);

            builder.Property(e => e.LocationId).HasColumnName("LocationID");

            builder.Property(e => e.ModifiedBy).HasMaxLength(200);

            builder.Property(e => e.ModifiedOn)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("now()");

            builder.Property(e => e.NidNo).HasMaxLength(14);

            builder.Property(e => e.PersonId)
                .HasColumnName("PersonID")
                .HasColumnType("numeric");

            builder.Property(e => e.PersonalProperty).HasMaxLength(250);

            builder.Property(e => e.Profession).HasMaxLength(100);

            builder.Property(e => e.ReferenceNo).HasMaxLength(200);

            builder.Property(e => e.RelationShipId).HasColumnName("RelationShipID");

            builder.Property(e => e.ReligionId).HasColumnName("ReligionID");

            builder.Property(e => e.Remark).HasMaxLength(200);

            builder.Property(e => e.Village).HasMaxLength(50);

            builder.HasOne(d => d.Religion)
                .WithMany(p => p.Relative)
                .HasForeignKey(d => d.ReligionId)
                .HasConstraintName("fk_relative_religion");

        }
    }
}
