using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SuppX.Storage.Migrations
{
    /// <inheritdoc />
    public partial class DefaultCloseReasons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CloseReason",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Проблема решена" },
                    { 2, "Нет ответа" },
                    { 3, "Некорректное общение" },
                    { 4, "Мошенничество" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CloseReason",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CloseReason",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CloseReason",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CloseReason",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
