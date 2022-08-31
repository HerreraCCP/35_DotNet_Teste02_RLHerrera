﻿using ClienteApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System;

namespace ClienteApi.Migrations
{
    [DbContext(typeof(ClienteDbContext))]
    partial class ClienteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ClienteApi.Models.Acomodacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Anotacoes")
                        .HasMaxLength(150)
                        .HasColumnType("NVARCHAR(150)")
                        .HasColumnName("Notes");

                    b.Property<string>("DescricaoDaAcomodacao")
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR(100)")
                        .HasColumnName("DescricaoDaAcomodacao");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Id" }, "IX_File_Acomodacao")
                        .IsUnique();

                    b.ToTable("Acomodacao");
                });

            modelBuilder.Entity("ClienteApi.Models.Arquivo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Anotacoes")
                        .HasMaxLength(150)
                        .HasColumnType("NVARCHAR(150)")
                        .HasColumnName("Notes");

                    b.Property<int?>("DespesasId")
                        .HasColumnType("int");

                    b.Property<string>("File")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDone")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("DespesasId");

                    b.HasIndex(new[] { "Id" }, "IX_File_Archive")
                        .IsUnique();

                    b.ToTable("Archive");
                });

            modelBuilder.Entity("ClienteApi.Models.DescricaoDespesa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Anotacoes")
                        .HasMaxLength(150)
                        .HasColumnType("NVARCHAR(150)")
                        .HasColumnName("Notes");

                    b.Property<string>("DescricaoTipoDeDepesa")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR(50)")
                        .HasColumnName("DescricaoTipoDeDepesa");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Id" }, "IX_DescricaoTipoDespesas_DescricaoDespesa")
                        .IsUnique();

                    b.ToTable("DescricaoDespesa");
                });

            modelBuilder.Entity("ClienteApi.Models.Despesa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AcomodacaoId")
                        .HasColumnType("int");

                    b.Property<string>("Anotacoes")
                        .HasMaxLength(150)
                        .HasColumnType("NVARCHAR(150)")
                        .HasColumnName("Anotacoes");

                    b.Property<string>("CodigoDeBarras")
                        .IsRequired()
                        .HasMaxLength(155)
                        .HasColumnType("NVARCHAR(155)")
                        .HasColumnName("CodigoDeBarras");

                    b.Property<DateTime>("CriadoEm")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValue(new DateTime(2022, 8, 29, 13, 52, 29, 806, DateTimeKind.Utc).AddTicks(7346))
                        .HasColumnName("CriadoEm");

                    b.Property<DateTime>("DataExpiracao")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataExpiracao");

                    b.Property<int?>("DescricaoDespesaId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal")
                        .HasColumnName("Valor");

                    b.HasKey("Id");

                    b.HasIndex("AcomodacaoId");

                    b.HasIndex("DescricaoDespesaId");

                    b.HasIndex(new[] { "Id" }, "IX_CodigoDeBarras_Despesas")
                        .IsUnique();

                    b.ToTable("Despesas");
                });

            modelBuilder.Entity("ClienteApi.Models.Arquivo", b =>
                {
                    b.HasOne("ClienteApi.Models.Despesa", "Despesas")
                        .WithMany("Arquivos")
                        .HasForeignKey("DespesasId")
                        .HasConstraintName("FK_Despesas_Arquivos")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Despesas");
                });

            modelBuilder.Entity("ClienteApi.Models.Despesa", b =>
                {
                    b.HasOne("ClienteApi.Models.Acomodacao", "Acomodacao")
                        .WithMany("Despesas")
                        .HasForeignKey("AcomodacaoId")
                        .HasConstraintName("FK_Despesas_Acomodacao")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ClienteApi.Models.DescricaoDespesa", "DescricaoDespesa")
                        .WithMany("Despesas")
                        .HasForeignKey("DescricaoDespesaId")
                        .HasConstraintName("FK_Despesas_DescricaoDespesa")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Acomodacao");

                    b.Navigation("DescricaoDespesa");
                });

            modelBuilder.Entity("ClienteApi.Models.Acomodacao", b =>
                {
                    b.Navigation("Despesas");
                });

            modelBuilder.Entity("ClienteApi.Models.DescricaoDespesa", b =>
                {
                    b.Navigation("Despesas");
                });

            modelBuilder.Entity("ClienteApi.Models.Despesa", b =>
                {
                    b.Navigation("Arquivos");
                });
#pragma warning restore 612, 618
        }
    }
}
