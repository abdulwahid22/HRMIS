using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
  public  class ExperienceConfiguration : IEntityTypeConfiguration<Experience>
    {

    
               

        public void Configure(EntityTypeBuilder<Experience> builder)
        {
            builder.ToTable("Experience", "rec");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasColumnType("numeric")
                .HasDefaultValueSql("nextval('rec.experience_id_seq'::regclass)");

            builder.Property(e => e.ContactInfo).HasMaxLength(250);

            builder.Property(e => e.CreatedOn)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("now()");

            builder.Property(e => e.Designation)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.DocumentNo).HasMaxLength(50);

            builder.Property(e => e.EndDate).HasColumnType("date");

            builder.Property(e => e.ExperienceTypeId).HasColumnName("ExperienceTypeID");

            builder.Property(e => e.JobDescription).HasMaxLength(500);

            builder.Property(e => e.JobstatusId).HasColumnName("JobstatusID");

            builder.Property(e => e.LocationId).HasColumnName("LocationID");

            builder.Property(e => e.ModifiedBy)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.ModifiedOn)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("now()");

            builder.Property(e => e.Organization)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.PersonId)
                .HasColumnName("PersonID")
                .HasColumnType("numeric");

            builder.Property(e => e.PromotionId).HasColumnName("PromotionID");

            builder.Property(e => e.RankId).HasColumnName("RankID");

            builder.Property(e => e.ReferenceNo).HasMaxLength(200);

            builder.Property(e => e.Remarks).HasMaxLength(500);

            builder.Property(e => e.StartDate).HasColumnType("date");
        }
   
    }
}
