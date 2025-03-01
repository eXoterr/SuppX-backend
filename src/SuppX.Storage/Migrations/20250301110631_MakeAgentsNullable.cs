using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuppX.Storage.Migrations
{
    /// <inheritdoc />
    public partial class MakeAgentsNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Agents_AgentId",
                table: "Tickets");

            migrationBuilder.AlterColumn<int>(
                name: "AgentId",
                table: "Tickets",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Agents_AgentId",
                table: "Tickets",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Agents_AgentId",
                table: "Tickets");

            migrationBuilder.AlterColumn<int>(
                name: "AgentId",
                table: "Tickets",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Agents_AgentId",
                table: "Tickets",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
