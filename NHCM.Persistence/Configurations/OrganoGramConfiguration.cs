using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
    public class OrganoGramConfiguration : IEntityTypeConfiguration<OrganoGram>
    {
        public void Configure(EntityTypeBuilder<OrganoGram> builder)
        {
            builder.ToTable("OrganoGram", "org");
            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("nextval('org.organogram_id_seq'::regclass)");

            builder.Property(e => e.IsPositionsCopied)
               .IsRequired();

            builder.Property(e => e.OrganizationId).HasColumnName("OrganizationID");
            builder.Property(e => e.StatusId).HasColumnName("StatusID");

            builder.Property(e => e.IsPositionsCopied).HasColumnName("IsPositionsCopied").IsRequired();

        }
    }
}
