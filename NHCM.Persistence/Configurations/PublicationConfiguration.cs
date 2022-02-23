using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;

namespace NHCM.Persistence.Configurations
{
    class PublicationConfiguration : IEntityTypeConfiguration<Publication>
    {




      

        public void Configure(EntityTypeBuilder<Publication> builder)
        {
            builder.ToTable("Publication", "rec");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasColumnType("numeric")
                .HasDefaultValueSql("nextval('rec.publication_id_seq'::regclass)");

            builder.Property(e => e.CreatedOn)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("now()");

            builder.Property(e => e.Isbn).HasMaxLength(50);

            builder.Property(e => e.ModifiedBy)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.ModifiedOn)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("now()");

            builder.Property(e => e.PersonId)
                .HasColumnName("PersonID")
                .HasColumnType("numeric");

            builder.Property(e => e.PublicationTypeId).HasColumnName("PublicationTypeID");

            builder.Property(e => e.PublishDate).HasColumnType("date");

            builder.Property(e => e.ReferenceNo).HasMaxLength(200);

            builder.Property(e => e.Subject)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasOne(d => d.Person)
                .WithMany(p => p.Publication)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_publication_person");

            builder.HasOne(d => d.PublicationType)
                .WithMany(p => p.Publication)
                .HasForeignKey(d => d.PublicationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_publication_publicationtype");
        }
    }
}
