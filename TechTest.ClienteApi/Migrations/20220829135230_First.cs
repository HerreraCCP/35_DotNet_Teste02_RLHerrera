using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClienteApi.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Acomodacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescricaoDaAcomodacao = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: true),
                    Notes = table.Column<string>(type: "NVARCHAR(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acomodacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DescricaoDespesa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescricaoTipoDeDepesa = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false),
                    Notes = table.Column<string>(type: "NVARCHAR(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DescricaoDespesa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Despesas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Valor = table.Column<decimal>(type: "decimal", nullable: false),
                    DataExpiracao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CodigoDeBarras = table.Column<string>(type: "NVARCHAR(155)", maxLength: 155, nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2022, 8, 29, 13, 52, 29, 806, DateTimeKind.Utc).AddTicks(7346)),
                    Anotacoes = table.Column<string>(type: "NVARCHAR(150)", maxLength: 150, nullable: true),
                    DescricaoDespesaId = table.Column<int>(type: "int", nullable: true),
                    AcomodacaoId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Despesas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Despesas_Acomodacao",
                        column: x => x.AcomodacaoId,
                        principalTable: "Acomodacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Despesas_DescricaoDespesa",
                        column: x => x.DescricaoDespesaId,
                        principalTable: "DescricaoDespesa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Archive",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    File = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDone = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "NVARCHAR(150)", maxLength: 150, nullable: true),
                    DespesasId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Archive", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Despesas_Arquivos",
                        column: x => x.DespesasId,
                        principalTable: "Despesas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_File_Acomodacao",
                table: "Acomodacao",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Archive_DespesasId",
                table: "Archive",
                column: "DespesasId");

            migrationBuilder.CreateIndex(
                name: "IX_File_Archive",
                table: "Archive",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DescricaoTipoDespesas_DescricaoDespesa",
                table: "DescricaoDespesa",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CodigoDeBarras_Despesas",
                table: "Despesas",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_AcomodacaoId",
                table: "Despesas",
                column: "AcomodacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_DescricaoDespesaId",
                table: "Despesas",
                column: "DescricaoDespesaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Archive");

            migrationBuilder.DropTable(
                name: "Despesas");

            migrationBuilder.DropTable(
                name: "Acomodacao");

            migrationBuilder.DropTable(
                name: "DescricaoDespesa");
        }
    }
}
