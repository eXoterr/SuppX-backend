using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuppX.Storage.Migrations
{
    /// <inheritdoc />
    public partial class MakeReasonAndCategoryNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_CloseReason_CloseReasonId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketCategory_CategoryId",
                table: "Tickets");

            migrationBuilder.AlterColumn<int>(
                name: "CloseReasonId",
                table: "Tickets",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Tickets",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_CloseReason_CloseReasonId",
                table: "Tickets",
                column: "CloseReasonId",
                principalTable: "CloseReason",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketCategory_CategoryId",
                table: "Tickets",
                column: "CategoryId",
                principalTable: "TicketCategory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_CloseReason_CloseReasonId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketCategory_CategoryId",
                table: "Tickets");

            migrationBuilder.AlterColumn<int>(
                name: "CloseReasonId",
                table: "Tickets",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Tickets",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_CloseReason_CloseReasonId",
                table: "Tickets",
                column: "CloseReasonId",
                principalTable: "CloseReason",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketCategory_CategoryId",
                table: "Tickets",
                column: "CategoryId",
                principalTable: "TicketCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
