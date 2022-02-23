using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;

namespace NHCM.Persistence.Configurations
{
    public class MaritalStatusConfiguration :IEntityTypeConfiguration<MaritalStatus>
    {

        
               

        public void Configure(EntityTypeBuilder<MaritalStatus> builder)
        {
            builder.ToTable("MaritalStatus", "look");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("nextval('look.maritalstatus_id_seq'::regclass)");

            builder.Property(e => e.Name)
                        .IsRequired()
                        .HasMaxLength(50);
        }
    }
}
