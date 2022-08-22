using ClienteApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClienteApi.Data.Mappings
{
    public class TypeOfLocationMap : IEntityTypeConfiguration<TypeOfLocation>
    {
        public void Configure(EntityTypeBuilder<TypeOfLocation> builder)
        {
            builder.ToTable("TypeOfLocation");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder
                .HasIndex(x => x.Id, "IX_TypeOfLocation_DescriptionOfLocation")
                .IsUnique();

            builder.Property(x => x.DescriptionOfLocation)
                .IsRequired()
                .HasColumnName("DescriptionOfLocation")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(20);

            builder.Property(x => x.Notes)
                .HasColumnName("Notes")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(150)
                .HasDefaultValue(null);
        }
    }
}