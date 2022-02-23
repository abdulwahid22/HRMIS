using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
    public class AuditConfiguration : IEntityTypeConfiguration<Audit>
    {
        public void Configure(EntityTypeBuilder<Audit> builder)
        {
            builder.ToTable("Audit", "au");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("nextval('au.\"Audit_ID_seq\"'::regclass)");

            builder.Property(e => e.DbContextObject).HasMaxLength(100);

            builder.Property(e => e.DbOjbectName).HasMaxLength(100);

            builder.Property(e => e.OperationTypeId).HasColumnName("OperationTypeID");

            builder.Property(e => e.ReocordId).HasColumnName("ReocordID");

            builder.Property(e => e.UserId).HasColumnName("UserID");

            builder.HasOne(d => d.OperationType)
                .WithMany(p => p.Audit)
                .HasForeignKey(d => d.OperationTypeId)
                .HasConstraintName("Audit_OperationTypeID_fkey");
        }
    }
}
