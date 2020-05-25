using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class SeedCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "IncomeCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Salario" },
                    { 2, "Aguinaldo" },
                    { 3, "Bonus" },
                    { 4, "Intereses Ganados" },
                    { 5, "Comision" },
                    { 6, "Prestamo" },
                    { 7, "Venta" },
                    { 8, "Honorarios" },
                    { 9, "Otro" }
                });

            migrationBuilder.InsertData(
                table: "SpendingCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 13, "Productos Varios" },
                    { 12, "Productos Alimenticios" },
                    { 11, "Perdida" },
                    { 10, "Debito" },
                    { 9, "Tarjeta de Credito" },
                    { 8, "Intereses" },
                    { 3, "Alquiler" },
                    { 6, "Combustible" },
                    { 5, "Vehiculo" },
                    { 4, "Educacion" },
                    { 14, "Ocio" },
                    { 2, "Impuesto" },
                    { 1, "Servicio" },
                    { 7, "Documentacion" },
                    { 15, "Otro" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
               table: "IncomeCategories",
               keyColumn: "Id",
               keyValue: 1);

            migrationBuilder.DeleteData(
                table: "IncomeCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "IncomeCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "IncomeCategories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "IncomeCategories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "IncomeCategories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "IncomeCategories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "IncomeCategories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "IncomeCategories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "SpendingCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SpendingCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SpendingCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SpendingCategories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SpendingCategories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SpendingCategories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SpendingCategories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SpendingCategories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "SpendingCategories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "SpendingCategories",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "SpendingCategories",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "SpendingCategories",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "SpendingCategories",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "SpendingCategories",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "SpendingCategories",
                keyColumn: "Id",
                keyValue: 15);
           
        }
    }
}
