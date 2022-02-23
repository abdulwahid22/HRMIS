using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;


namespace NHCM.Persistence.Configurations
{
    class EthnicityConfiguration : IEntityTypeConfiguration<Ethnicity>
    {
        public void Configure(EntityTypeBuilder<Ethnicity> builder)
        {

            builder.ToTable("Ethnicity", "look");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("nextval('look.ethnicity_id_seq'::regclass)");

            builder.Property(e => e.CountryId).HasColumnName("CountryID");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.ParentId).HasColumnName("ParentID");

        }
    }
}
