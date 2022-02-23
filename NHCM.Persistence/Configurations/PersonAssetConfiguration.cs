using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
    public class PersonAssetConfiguration : IEntityTypeConfiguration<PersonAsset>
    {
        public void Configure(EntityTypeBuilder<PersonAsset> builder)
        {





            builder.ToTable("PersonAsset", "rec");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasColumnType("numeric")
                .HasDefaultValueSql("nextval('rec.personasset_id_seq'::regclass)");

            builder.Property(e => e.AssetTypeId).HasColumnName("AssetTypeID");

            builder.Property(e => e.CreatedOn)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("now()");

            builder.Property(e => e.Description).HasMaxLength(250);

            builder.Property(e => e.ModifiedBy)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.ModifiedOn)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("now()");

            builder.Property(e => e.PersonId)
                .HasColumnName("PersonID")
                .HasColumnType("numeric");

            builder.Property(e => e.ReferenceNo).HasMaxLength(200);

            builder.Property(e => e.Value).HasColumnType("numeric");

            builder.HasOne(d => d.AssetType)
                .WithMany(p => p.PersonAssets)
                .HasForeignKey(d => d.AssetTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_personasset_assettype");

            builder.HasOne(d => d.Person)
                .WithMany(p => p.PersonAsset)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_personasset_person");

        }
    }
}
