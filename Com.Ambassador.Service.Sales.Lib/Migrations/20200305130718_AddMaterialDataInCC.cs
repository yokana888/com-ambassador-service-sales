using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class AddMaterialDataInCC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductionOrderNo",
                table: "FinishingPrintingSalesContracts");

            migrationBuilder.RenameColumn(
                name: "CostCalculationId",
                table: "FinishingPrintingSalesContracts",
                newName: "PreSalesContractId");

            migrationBuilder.AddColumn<double>(
                name: "MaterialPrice",
                table: "FinishingPrintingSalesContracts",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "MaterialTags",
                table: "FinishingPrintingSalesContracts",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaterialCode",
                table: "FinishingPrintingCostCalculations",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MaterialPrice",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "MaterialTags",
                table: "FinishingPrintingCostCalculations",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaterialPrice",
                table: "FinishingPrintingSalesContracts");

            migrationBuilder.DropColumn(
                name: "MaterialTags",
                table: "FinishingPrintingSalesContracts");

            migrationBuilder.DropColumn(
                name: "MaterialCode",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "MaterialPrice",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "MaterialTags",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.RenameColumn(
                name: "PreSalesContractId",
                table: "FinishingPrintingSalesContracts",
                newName: "CostCalculationId");

            migrationBuilder.AddColumn<string>(
                name: "ProductionOrderNo",
                table: "FinishingPrintingSalesContracts",
                maxLength: 64,
                nullable: true);
        }
    }
}
