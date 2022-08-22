using ClienteApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClienteApi.Data.Mappings
{
    public class ExpenseDescriptionMap : IEntityTypeConfiguration<ExpenseDescription>
    {
        public void Configure(EntityTypeBuilder<ExpenseDescription> builder)
        {
            //TODO Mapear o [AspNetUsers] do Identity

            builder.ToTable("ExpenseDescription");

            builder.HasKey(x => x.Id);
            
            builder
                .HasIndex(x => x.Id, "IX_ExpenseDescription_DescriptionOfExpensive")
                .IsUnique();
            
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.DescriptionOfExpensive)
                .IsRequired()
                .HasColumnName("DescriptionOfExpensive")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50);

            builder.Property(x => x.Notes)
                .HasColumnName("Notes")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(150)
                .HasDefaultValue(null);
        }
    }
}