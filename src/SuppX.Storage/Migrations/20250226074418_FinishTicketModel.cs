using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuppX.Storage.Migrations
{
    /// <inheritdoc />
    public partial class FinishTicketModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_CloseReason_ReasonId",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "ReasonId",
                table: "Tickets",
                newName: "CloseReasonId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_ReasonId",
                table: "Tickets",
                newName: "IX_Tickets_CloseReasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_CloseReason_CloseReasonId",
                table: "Tickets",
                column: "CloseReasonId",
                principalTable: "CloseReason",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_CloseReason_CloseReasonId",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "CloseReasonId",
                table: "Tickets",
                newName: "ReasonId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_CloseReasonId",
                table: "Tickets",
                newName: "IX_Tickets_ReasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_CloseReason_ReasonId",
                table: "Tickets",
                column: "ReasonId",
                principalTable: "CloseReason",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
