using System;
using ClienteApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClienteApi.Data.Mappings
{
    public class DespesaMap : IEntityTypeConfiguration<Despesa>
    {
        public void Configure(EntityTypeBuilder<Despesa> builder)
        {
            builder.ToTable("Despesas");

            builder.HasKey(x => x.Id);

            builder
                .HasIndex(x => x.Id, "IX_CodigoDeBarras_Despesas")
                .IsUnique();

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.CodigoDeBarras)
                .IsRequired()
                .HasColumnName("CodigoDeBarras")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(155);

            builder.Property(x => x.CriadoEm)
                .IsRequired()
                .HasColumnName("CriadoEm")
                .HasColumnType("datetime")
                .HasDefaultValue(DateTime.UtcNow);

            builder.Property(x => x.DataExpiracao)
                .IsRequired()
                .HasColumnName("DataExpiracao")
                .HasColumnType("datetime2");

            builder.Property(x => x.Valor)
                .IsRequired()
                .HasColumnName("Valor")
                .HasColumnType("decimal");

            builder.Property(x => x.Anotacoes)
                .HasColumnName("Anotacoes")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(150)
                .HasDefaultValue(null);

            builder
                .HasOne(x => x.DescricaoDespesa)
                .WithMany(x => x.Despesas)
                .HasConstraintName("FK_Despesas_DescricaoDespesa")
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.Acomodacao)
                .WithMany(x => x.Expenses)
                .HasConstraintName("FK_Expenses_Acomodacao")
                .OnDelete(DeleteBehavior.Cascade);

            //TODO UserID
        }
    }
}