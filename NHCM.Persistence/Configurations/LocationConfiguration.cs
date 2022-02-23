using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
namespace NHCM.Persistence.Configurations
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
           
                builder.ToTable("Location", "look");

                builder.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('look.location_id_seq'::regclass)");

                builder.Property(e => e.Code).HasColumnType("character(3)");

                builder.Property(e => e.Dari)
                    .IsRequired()
                    .HasMaxLength(255);

                builder.Property(e => e.Name).HasMaxLength(255);

                builder.Property(e => e.ParentId).HasColumnName("ParentID");

                builder.Property(e => e.Path).HasMaxLength(255);

                builder.Property(e => e.PathDari)
                    .HasColumnName("Path_Dari")
                    .HasMaxLength(255);

                builder.Property(e => e.TypeId).HasColumnName("TypeID");
           
        }
    }
}
