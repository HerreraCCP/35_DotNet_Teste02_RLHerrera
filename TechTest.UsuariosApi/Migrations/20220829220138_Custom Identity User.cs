using Microsoft.EntityFrameworkCore.Migrations;

namespace UsuariosApi.Migrations
{
    public partial class CustomIdentityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 10,
                column: "ConcurrencyStamp",
                value: "1e2a57fa-3a1c-4048-85ed-77d08cdfa9ce");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999999,
                column: "ConcurrencyStamp",
                value: "6c309d18-7090-498f-9014-761b56c9e0fe");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "59454311-b6e4-4411-ac09-4435e411e5a7", "AQAAAAEAACcQAAAAELmdPgzVdP5YG8OpWvw+XqnYcf9te8MSlJhuf9YUqp+n0sFQYKYw4jOrb1U50KnSIA==", "991bd12f-9608-416b-b783-cec6e664f3bd" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 10,
                column: "ConcurrencyStamp",
                value: "30b34b43-50bd-4006-b5c1-9c7881d1a9fa");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999999,
                column: "ConcurrencyStamp",
                value: "3a407a4f-1451-469e-975c-93ed7211913d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f8ad2981-0e4c-444d-af62-bbf48f26e7d3", "AQAAAAEAACcQAAAAEMEOEDZV8AGdn66i0cVFUqpAGN0gSOUJsC1A7C0syuHM3MC7r9CpKnAHhl2bX480AA==", "bc475f09-9b69-4210-a9e2-be21a253e668" });
        }
    }
}
