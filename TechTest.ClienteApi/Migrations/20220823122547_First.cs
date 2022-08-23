using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClienteApi.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Archive",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    File = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "NVARCHAR(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Archive", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseDescription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionOfExpensive = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false),
                    Notes = table.Column<string>(type: "NVARCHAR(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseDescription", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfAccommodation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionOfAccommodation = table.Column<string>(type: "NVARCHAR(20)", maxLength: 20, nullable: false),
                    Notes = table.Column<string>(type: "NVARCHAR(155)", maxLength: 155, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfAccommodation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfLocation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionOfLocation = table.Column<string>(type: "NVARCHAR(20)", maxLength: 20, nullable: false),
                    Notes = table.Column<string>(type: "NVARCHAR(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfLocation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Expense",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal", nullable: false),
                    ExpireIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BarCode = table.Column<string>(type: "NVARCHAR(155)", maxLength: 155, nullable: false),
                    CreatedIn = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2022, 8, 23, 12, 25, 47, 152, DateTimeKind.Utc).AddTicks(2336)),
                    Notes = table.Column<string>(type: "NVARCHAR(150)", maxLength: 150, nullable: true),
                    ExpenseDescriptionId = table.Column<int>(type: "int", nullable: true),
                    TypeOfLocationId = table.Column<int>(type: "int", nullable: true),
                    TypeOfAccommodationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expense", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_ExpenseDescription",
                        column: x => x.ExpenseDescriptionId,
                        principalTable: "ExpenseDescription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expenses_TypeOfAccommodation",
                        column: x => x.TypeOfAccommodationId,
                        principalTable: "TypeOfAccommodation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expenses_TypeOfLocation",
                        column: x => x.TypeOfLocationId,
                        principalTable: "TypeOfLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_File_Archive",
                table: "Archive",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BarCode_Expense",
                table: "Expense",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expense_ExpenseDescriptionId",
                table: "Expense",
                column: "ExpenseDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_TypeOfAccommodationId",
                table: "Expense",
                column: "TypeOfAccommodationId");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_TypeOfLocationId",
                table: "Expense",
                column: "TypeOfLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseDescription_DescriptionOfExpensive",
                table: "ExpenseDescription",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TypeOfAccommodation_DescriptionOfAccommodation",
                table: "TypeOfAccommodation",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TypeOfLocation_DescriptionOfLocation",
                table: "TypeOfLocation",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Archive");

            migrationBuilder.DropTable(
                name: "Expense");

            migrationBuilder.DropTable(
                name: "ExpenseDescription");

            migrationBuilder.DropTable(
                name: "TypeOfAccommodation");

            migrationBuilder.DropTable(
                name: "TypeOfLocation");
        }
    }
}
