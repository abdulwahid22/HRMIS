using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
    public class TravelConfiguration : IEntityTypeConfiguration<Travel>
    {
        public void Configure(EntityTypeBuilder<Travel> builder)
        {
          
                builder.ToTable("Travel", "rec");

                builder.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("numeric")
                    .HasDefaultValueSql("nextval('rec.travel_id_seq'::regclass)");

                builder.Property(e => e.AccompanyWith).HasMaxLength(200);

                builder.Property(e => e.CountryId).HasColumnName("CountryID");

                builder.Property(e => e.CreatedOn)
                    .HasColumnType("timestamp with time zone")
                    .HasDefaultValueSql("now()");

                builder.Property(e => e.PersonId)
                    .HasColumnName("PersonID")
                    .HasColumnType("numeric");

                builder.Property(e => e.Place).HasMaxLength(150);

                builder.Property(e => e.Reason).HasMaxLength(1000);

                builder.Property(e => e.ReferenceNo).HasMaxLength(200);

                builder.Property(e => e.ReturnDate).HasColumnType("date");

                builder.Property(e => e.TravelDate).HasColumnType("date");

                builder.HasOne(d => d.Person)
                    .WithMany(p => p.Travel)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_travel_person");
           
        }
    }
}
