using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class SeedMonths : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {         
            migrationBuilder.InsertData(
                table: "Months",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Enero" },
                    { 2, "Febrero" },
                    { 3, "Marzo" },
                    { 4, "Abril" },
                    { 5, "Mayo" },
                    { 6, "Junio" },
                    { 7, "Julio" },
                    { 8, "Agosto" },
                    { 9, "Septiembre" },
                    { 10, "Octubre" },
                    { 11, "Noviembre" },
                    { 12, "Diciembre" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Months",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Months",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Months",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Months",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Months",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Months",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Months",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Months",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Months",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Months",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Months",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Months",
                keyColumn: "Id",
                keyValue: 12);
        }
    }
}
