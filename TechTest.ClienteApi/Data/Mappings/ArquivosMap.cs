using ClienteApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClienteApi.Data.Mappings
{
    public class ArquivosMap : IEntityTypeConfiguration<Arquivo>
    {
        public void Configure(EntityTypeBuilder<Arquivo> builder)
        {
            builder.ToTable("Archive");

            builder.HasKey(x => x.Id);

            builder
                .HasIndex(x => x.Id, "IX_File_Archive")
                .IsUnique();

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Anotacoes)
                .HasColumnName("Notes")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(150)
                .HasDefaultValue(null);

            builder
                .HasOne(x => x.Despesas)
                .WithMany(x => x.Arquivos)
                .HasConstraintName("FK_Despesas_Arquivos")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}