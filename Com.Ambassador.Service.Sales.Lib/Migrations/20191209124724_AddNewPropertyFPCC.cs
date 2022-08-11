using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class AddNewPropertyFPCC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "CurrencyRate",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "CargoCost",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "ActualPrice",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<decimal>(
                name: "FreightCost",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "GreigePrice",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FreightCost",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "GreigePrice",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.AlterColumn<double>(
                name: "CurrencyRate",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<double>(
                name: "CargoCost",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<double>(
                name: "ActualPrice",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
