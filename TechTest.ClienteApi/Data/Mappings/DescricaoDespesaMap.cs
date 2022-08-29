using ClienteApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClienteApi.Data.Mappings
{
    public class ExpenseDescriptionMap : IEntityTypeConfiguration<DescricaoDespesa>
    {
        public void Configure(EntityTypeBuilder<DescricaoDespesa> builder)
        {
            //TODO Mapear o [AspNetUsers] do Identity

            builder.ToTable("DescricaoDespesa");

            builder.HasKey(x => x.Id);

            builder
                .HasIndex(x => x.Id, "IX_DescricaoTipoDespesas_DescricaoDespesa")
                .IsUnique();

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.DescricaoTipoDeDepesa)
                .IsRequired()
                .HasColumnName("DescricaoTipoDeDepesa")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50);

            builder.Property(x => x.Anotacoes)
                .HasColumnName("Notes")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(150)
                .HasDefaultValue(null);
        }
    }
}