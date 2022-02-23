using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
   public  class DocumentTypeConfiguration : IEntityTypeConfiguration<DocumentType>
    {
        public void Configure(EntityTypeBuilder<DocumentType> builder)
        {
           





            builder.ToTable("DocumentType", "look");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("nextval('look.\"DocumentType_ID_seq\"'::regclass)");

            builder.Property(e => e.Description).HasMaxLength(200);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.ScreenId).HasColumnName("ScreenID");

            builder.HasOne(d => d.Screen)
                .WithMany(p => p.DocumentType)
                .HasForeignKey(d => d.ScreenId)
                .HasConstraintName("DocumentType_ScreenID_fkey");
        }
    }
}
