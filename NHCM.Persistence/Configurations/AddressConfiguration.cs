using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;

namespace NHCM.Persistence.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {



        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Address", "rec");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasColumnType("numeric")
                .HasDefaultValueSql("nextval('rec.address_id_seq'::regclass)");

            builder.Property(e => e.Address1)
                .HasColumnName("Address")
                .HasMaxLength(250);

            builder.Property(e => e.AddressTypeId).HasColumnName("AddressTypeID");

            builder.Property(e => e.CdistrictId).HasColumnName("CDistrictID");

            builder.Property(e => e.ClocationId).HasColumnName("CLocationID");

            builder.Property(e => e.CreatedOn)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("now()");

            builder.Property(e => e.Cvillage)
                .HasColumnName("CVillage")
                .HasMaxLength(400);

            builder.Property(e => e.DistrictId).HasColumnName("DistrictID");

            builder.Property(e => e.EmailAddress).HasMaxLength(50);

            builder.Property(e => e.HouseNo).HasMaxLength(50);

            builder.Property(e => e.LocationId).HasColumnName("LocationID");

            builder.Property(e => e.Mobile).HasMaxLength(50);

            builder.Property(e => e.ModifiedBy)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.ModifiedOn)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("now()");

            builder.Property(e => e.Paddress)
                .HasColumnName("PAddress")
                .HasMaxLength(400);

            builder.Property(e => e.PersonId)
                .HasColumnName("PersonID")
                .HasColumnType("numeric");

            builder.Property(e => e.Phone).HasMaxLength(50);

            builder.Property(e => e.ReferenceNo).HasMaxLength(200);

            builder.Property(e => e.StreetNo).HasMaxLength(150);

            builder.Property(e => e.Village).HasMaxLength(400);
        }
    }
}
