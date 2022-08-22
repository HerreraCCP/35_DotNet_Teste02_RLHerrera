using ClienteApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClienteApi.Data.Mappings
{
    public class TypeOfAccommodationMap : IEntityTypeConfiguration<TypeOfAccommodation>
    {
        public void Configure(EntityTypeBuilder<TypeOfAccommodation> builder)
        {
            builder.ToTable("TypeOfAccommodation");

            builder.HasKey(x => x.Id);

            builder
                .HasIndex(x => x.Id, "IX_TypeOfAccommodation_DescriptionOfAccommodation")
                .IsUnique();

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.DescriptionOfAccommodation)
                .IsRequired()
                .HasColumnName("DescriptionOfAccommodation")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(20);

            builder.Property(x => x.Notes)
                .HasColumnName("Notes")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(155)
                .HasDefaultValue(null);
        }
    }
}