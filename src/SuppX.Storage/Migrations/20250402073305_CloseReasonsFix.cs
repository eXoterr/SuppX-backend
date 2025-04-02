using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuppX.Storage.Migrations
{
    /// <inheritdoc />
    public partial class CloseReasonsFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_CloseReason_CloseReasonId",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CloseReason",
                table: "CloseReason");

            migrationBuilder.RenameTable(
                name: "CloseReason",
                newName: "CloseReasons");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CloseReasons",
                table: "CloseReasons",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_CloseReasons_CloseReasonId",
                table: "Tickets",
                column: "CloseReasonId",
                principalTable: "CloseReasons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_CloseReasons_CloseReasonId",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CloseReasons",
                table: "CloseReasons");

            migrationBuilder.RenameTable(
                name: "CloseReasons",
                newName: "CloseReason");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CloseReason",
                table: "CloseReason",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_CloseReason_CloseReasonId",
                table: "Tickets",
                column: "CloseReasonId",
                principalTable: "CloseReason",
                principalColumn: "Id");
        }
    }
}
