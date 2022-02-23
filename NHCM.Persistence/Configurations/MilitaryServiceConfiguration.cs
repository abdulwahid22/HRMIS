using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
    public class MilitaryServiceConfiguration : IEntityTypeConfiguration<MilitaryService>
    {
        public void Configure(EntityTypeBuilder<MilitaryService> builder)
        {

            builder.ToTable("MilitaryService", "rec");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("nextval('rec.militaryservice_id_seq'::regclass)");

            builder.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

            builder.Property(e => e.EndDate).HasColumnType("date");

            builder.Property(e => e.MilitaryServiceTypeId).HasColumnName("MilitaryServiceTypeID");

            builder.Property(e => e.ModifiedOn).HasColumnType("timestamp with time zone");

            builder.Property(e => e.PersonId)
                .HasColumnName("PersonID")
                .HasColumnType("numeric");

            builder.Property(e => e.Remark).HasMaxLength(200);

            builder.Property(e => e.StartDate).HasColumnType("date");

            builder.HasOne(d => d.MilitaryServiceType)
                .WithMany(p => p.MilitaryService)
                .HasForeignKey(d => d.MilitaryServiceTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_militaryservicetype_militaryservice");

            builder.HasOne(d => d.Person)
                .WithMany(p => p.MilitaryService)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_person_militaryservice");
                

        }
    }
}
