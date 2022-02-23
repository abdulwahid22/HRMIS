using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Configurations
{
    class RankConfiguration : IEntityTypeConfiguration<Rank>
    {
        public void Configure(EntityTypeBuilder<Rank> builder)
        {

            builder.ToTable("Rank", "look");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("nextval('look.rank_id_seq'::regclass)");

            builder.Property(e => e.CategoryId).HasColumnName("CategoryID");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.RankNumber).HasMaxLength(10);

            builder.Property(e => e.Sorter).HasMaxLength(100);

            builder.Property(e => e.StatusId).HasColumnName("StatusID");

        }
    }
}
