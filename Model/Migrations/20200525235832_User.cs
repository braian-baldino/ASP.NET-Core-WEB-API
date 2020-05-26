using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Spendings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Incomes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Balances",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AnualBalances",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AnualBalances_UserId",
                table: "AnualBalances",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnualBalances_Users_UserId",
                table: "AnualBalances",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnualBalances_Users_UserId",
                table: "AnualBalances");

            migrationBuilder.DropIndex(
                name: "IX_AnualBalances_UserId",
                table: "AnualBalances");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Spendings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Balances");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AnualBalances");
        }
    }
}
