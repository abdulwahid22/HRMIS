using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
    public class WorkAreaConfiguration : IEntityTypeConfiguration<WorkArea>
    {
        public void Configure(EntityTypeBuilder<WorkArea> builder)
        {
            builder.ToTable("WorkArea", "org");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasColumnType("numeric")
                .HasDefaultValueSql("nextval('org.position_id_seq'::regclass)");

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100);

           // builder.Property(e => e.TitleEng).HasMaxLength(250);


        }
    }
}
