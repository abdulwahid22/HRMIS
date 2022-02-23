using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
   public class AuditversionConfiguration : IEntityTypeConfiguration<Auditversion>
    {
        public void Configure(EntityTypeBuilder<Auditversion> builder)
        {
            builder.ToTable("auditversion", "au");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("nextval('au.\"auditversion_ID_seq\"'::regclass)");

            builder.Property(e => e.AuditId).HasColumnName("AuditID");

         
        }
    }
}
