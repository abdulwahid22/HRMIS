using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
    class HealthReportConfiguration : IEntityTypeConfiguration <HealthReport>
    {
      
                

        public void Configure(EntityTypeBuilder<HealthReport> builder)
        {
            builder.ToTable("HealthReport", "rec");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasColumnType("numeric")
                .HasDefaultValueSql("nextval('rec.healthreport_id_seq'::regclass)");

            builder.Property(e => e.CreatedOn)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("now()");

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

            builder.Property(e => e.Remarks).HasMaxLength(1200);

            builder.Property(e => e.ReportDate).HasColumnType("date");

            builder.Property(e => e.StatusId).HasColumnName("StatusID");

            builder.HasOne(d => d.Person)
                .WithMany(p => p.HealthReport)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("fk_healthreport_person");
        }
   
    }
}
