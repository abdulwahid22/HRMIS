using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
    public class OrgUnitConfiguration : IEntityTypeConfiguration<OrgUnit>
    {
        public void Configure(EntityTypeBuilder<OrgUnit> builder)
        { 
                builder.ToTable("OrgUnit", "org");

                builder.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("numeric")
                    .HasDefaultValueSql("nextval('org.orgunit_id_seq'::regclass)");

                builder.Property(e => e.Code).HasMaxLength(15);

                builder.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

                builder.Property(e => e.LocationId).HasColumnName("LocationID");

                builder.Property(e => e.ModifiedOn).HasColumnType("timestamp with time zone");

                builder.Property(e => e.Modifiedby)
                    .IsRequired()
                    .HasColumnName("modifiedby")
                    .HasMaxLength(200);

                builder.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                builder.Property(e => e.OrganOgramId).HasColumnName("OrganOgramID");

                builder.Property(e => e.OrgunitTypeId).HasColumnName("OrgunitTypeID");

                builder.Property(e => e.ParentId)
                    .HasColumnName("ParentID")
                    .HasColumnType("numeric");

                builder.Property(e => e.ReferenceNo).HasMaxLength(14);

                builder.Property(e => e.Sorter).HasMaxLength(300);

                builder.Property(e => e.StatusId).HasColumnName("StatusID");

                builder.HasOne(d => d.OrganOgram)
                    .WithMany(p => p.OrgUnit)
                    .HasForeignKey(d => d.OrganOgramId)
                    .HasConstraintName("fk_orgunit_organogram");

                builder.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("fk_orgunit_orgunit"); 
        }
    }
}
