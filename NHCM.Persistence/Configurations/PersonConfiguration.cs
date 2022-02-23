using System;
using System.Collections.Generic;
using System.Text;
using NHCM.Domain;
using NHCM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NHCM.Persistence.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {



            builder.ToTable("Person", "rec");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasColumnType("numeric")
                .HasDefaultValueSql("nextval('rec.person_id_seq'::regclass)");

            builder.Property(e => e.BirthLocationId).HasColumnName("BirthLocationID");

            builder.Property(e => e.BloodGroupId).HasColumnName("BloodGroupID");

            builder.Property(e => e.Comments).HasMaxLength(400);

            builder.Property(e => e.CreatedOn)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("now()");

            builder.Property(e => e.DateOfBirth).HasColumnType("timestamp with time zone");

            builder.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");

            builder.Property(e => e.EthnicityId).HasColumnName("EthnicityID");

            builder.Property(e => e.FatherName)
                .IsRequired()
                .HasMaxLength(90);

            builder.Property(e => e.FatherNameEng).HasMaxLength(90);

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(90);

            builder.Property(e => e.FirstNameEng).HasMaxLength(90);

            builder.Property(e => e.GenderId).HasColumnName("GenderID");

            builder.Property(e => e.GrandFatherName).HasMaxLength(90);

            builder.Property(e => e.GrandFatherNameEng).HasMaxLength(90);

            builder.Property(e => e.HashKey).HasMaxLength(32);

            builder.Property(e => e.Hiringofficenumber)
                .HasColumnName("hiringofficenumber")
                .HasMaxLength(50);

            builder.Property(e => e.Hrcode).HasMaxLength(90);

            builder.Property(e => e.LastName).HasMaxLength(90);

            builder.Property(e => e.LastNameEng).HasMaxLength(90);

            builder.Property(e => e.MaritalStatusId).HasColumnName("MaritalStatusID");

            builder.Property(e => e.ModifiedBy)
                //.IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.ModifiedOn)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("now()");

            builder.Property(e => e.Nid)
                .HasColumnName("NID")
                .HasMaxLength(50);

            builder.Property(e => e.OrganizationId).HasColumnName("OrganizationID");

            builder.Property(e => e.PhotoPath).HasMaxLength(200);

            builder.Property(e => e.PreFix).HasMaxLength(14);

            builder.Property(e => e.ReferenceNo).HasMaxLength(200);

            builder.Property(e => e.ReligionId).HasColumnName("ReligionID");

            builder.Property(e => e.Remark).HasMaxLength(50);

            builder.Property(e => e.StatusId).HasColumnName("StatusID");

            builder.HasOne(d => d.DocumentType)
                .WithMany(p => p.Person)
                .HasForeignKey(d => d.DocumentTypeId)
                .HasConstraintName("Person_DocumentTypeID_fkey");

            //builder.HasOne(d => d.Organization)
            //    .WithMany(p => p.Person)
            //    .HasForeignKey(d => d.OrganizationId)
            //    .HasConstraintName("Person_OrganizationID_fkey");

        }
    }
}
