using Microsoft.EntityFrameworkCore.Migrations;

namespace UsuariosApi.Migrations
{
    public partial class RoleRegular : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999999,
                column: "ConcurrencyStamp",
                value: "3a407a4f-1451-469e-975c-93ed7211913d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 10, "30b34b43-50bd-4006-b5c1-9c7881d1a9fa", "regular", "REGULAR" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f8ad2981-0e4c-444d-af62-bbf48f26e7d3", "AQAAAAEAACcQAAAAEMEOEDZV8AGdn66i0cVFUqpAGN0gSOUJsC1A7C0syuHM3MC7r9CpKnAHhl2bX480AA==", "bc475f09-9b69-4210-a9e2-be21a253e668" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999999,
                column: "ConcurrencyStamp",
                value: "a77b74d7-1b11-484d-b0f0-40fde8f6dec3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "420f4005-d1b9-49e9-bcc0-6678284f7ef3", "AQAAAAEAACcQAAAAECnZzGCzu1LrgnzDwWbFEz4dfGKFkzMR8JiCmTEz9DEi20BCUeUpp9l9gi33+hnbmg==", "6e6e1d41-f802-4f5c-82a0-7b9df26818b1" });
        }
    }
}
