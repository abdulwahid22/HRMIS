using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
    public class ResultConfiguration : IEntityTypeConfiguration<Result>
    {
        public void Configure(EntityTypeBuilder<Result> builder)
        {
        
                builder.ToTable("Result", "look");

                builder.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('look.result_id_seq'::regclass)");

                builder.Property(e => e.Category)
                    .IsRequired()
                    .HasColumnType("character(2)");

                builder.Property(e => e.Dari).HasColumnType("character(50)");

                builder.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                builder.Property(e => e.Pashto).HasColumnType("character(50)");
           
        }
    }
}
