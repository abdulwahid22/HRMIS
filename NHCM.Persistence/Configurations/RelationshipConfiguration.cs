using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;


namespace NHCM.Persistence.Configurations
{
    class RelationshipConfiguration : IEntityTypeConfiguration<Relationship>
    {
        public void Configure(EntityTypeBuilder<Relationship> builder)
        {

           
            builder.ToTable("Relationship", "look");

            builder.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('look.relationship_id_seq'::regclass)");

            builder.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
           
        }
    }
}
