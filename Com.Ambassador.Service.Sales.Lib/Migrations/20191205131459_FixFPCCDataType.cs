using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class FixFPCCDataType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "FinishingPrintingCostCalculations",
                maxLength: 2048,
                nullable: true,
                oldClrType: typeof(double),
                oldMaxLength: 2048);

            migrationBuilder.AddColumn<decimal>(
                name: "Depretiation",
                table: "FinishingPrintingCostCalculationMachines",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "FinishingPrintingCostCalculationMachines",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Utility",
                table: "FinishingPrintingCostCalculationMachines",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "FinishingPrintingCostCalculationChemicals",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Depretiation",
                table: "FinishingPrintingCostCalculationMachines");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "FinishingPrintingCostCalculationMachines");

            migrationBuilder.DropColumn(
                name: "Utility",
                table: "FinishingPrintingCostCalculationMachines");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "FinishingPrintingCostCalculationChemicals");

            migrationBuilder.AlterColumn<double>(
                name: "Remark",
                table: "FinishingPrintingCostCalculations",
                maxLength: 2048,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 2048,
                oldNullable: true);
        }
    }
}
