using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
    public class ZoneEmployeesConfiguration : IEntityTypeConfiguration<ZoneEmployees>
    {
        public void Configure(EntityTypeBuilder<ZoneEmployees> builder)
        {
            builder.ToTable("ZoneEmployees", "rec");

            builder.Property(e => e.ID)
                .HasColumnName("ID")           
                .HasDefaultValueSql("nextval('zoemp_id_seq'::regclass)");



        }
    }
}
