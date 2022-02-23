using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHCM.Domain.Entities;


namespace NHCM.Persistence.Configurations
{
    class ModuleConfiguration : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.ToTable("Module", "look");
            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .ValueGeneratedNever();
            builder.Property(e => e.Description).HasColumnType("character varying");
            builder.Property(e => e.Name).HasColumnType("character varying");
        }
    }
}
