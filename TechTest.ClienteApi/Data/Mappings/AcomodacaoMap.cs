using ClienteApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClienteApi.Data.Mappings
{
    public class AcomodacaoMap : IEntityTypeConfiguration<Acomodacao>
    {
        public void Configure(EntityTypeBuilder<Acomodacao> builder)
        {
            builder.ToTable("Acomodacao");

            builder.HasKey(x => x.Id);

            builder
                .HasIndex(x => x.Id, "IX_File_Acomodacao")
                .IsUnique();

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.DescricaoDaAcomodacao)
                .HasColumnName("DescricaoDaAcomodacao")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100)
                .HasDefaultValue(null);

            builder.Property(x => x.Anotacoes)
                .HasColumnName("Notes")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(150)
                .HasDefaultValue(null);
        }
    }
}