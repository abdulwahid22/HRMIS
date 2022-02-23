using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
    public class ReferenceConfiguration : IEntityTypeConfiguration<Reference>
    {
        public void Configure(EntityTypeBuilder<Reference> builder)
        {


            builder.ToTable("Reference", "rec");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasColumnType("numeric")
                .HasDefaultValueSql("nextval('rec.reference_id_seq'::regclass)");

            builder.Property(e => e.Amount).HasMaxLength(100);

            builder.Property(e => e.BankId).HasColumnName("BankID");

            builder.Property(e => e.CreatedOn)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("now()");

            builder.Property(e => e.District).HasMaxLength(50);

            builder.Property(e => e.DocumentDate).HasColumnType("date");

            builder.Property(e => e.DocumentNumber).HasMaxLength(100);

            builder.Property(e => e.FatherName).HasMaxLength(40);

            builder.Property(e => e.FirstName).HasMaxLength(50);

            builder.Property(e => e.GrandFatherName).HasMaxLength(40);

            builder.Property(e => e.LastName).HasMaxLength(50);

            builder.Property(e => e.LocationId).HasColumnName("LocationID");

            builder.Property(e => e.ModifiedBy)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.ModifiedOn)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("now()");

            builder.Property(e => e.Occupation).HasMaxLength(150);

            builder.Property(e => e.Organization).HasMaxLength(150);

            builder.Property(e => e.PersonId)
                .HasColumnName("PersonID")
                .HasColumnType("numeric");

            builder.Property(e => e.ReceiptNumber).HasMaxLength(50);

            builder.Property(e => e.ReferenceNo).HasMaxLength(200);

            builder.Property(e => e.ReferenceTypeId).HasColumnName("ReferenceTypeID");

            builder.Property(e => e.RelationShip).HasMaxLength(100);

            builder.Property(e => e.Remark).HasMaxLength(200);

            builder.Property(e => e.TelephoneNo).HasMaxLength(50);

            //builder.HasOne(d => d.Bank)
            //    .WithMany(p => p.Reference)
            //    .HasForeignKey(d => d.BankId)
            //    .HasConstraintName("fk_bank_reference");

            builder.HasOne(d => d.ReferenceType)
                .WithMany(p => p.Reference)
                .HasForeignKey(d => d.ReferenceTypeId)
                .HasConstraintName("fk_referencetype_reference");

        }
    }
}
