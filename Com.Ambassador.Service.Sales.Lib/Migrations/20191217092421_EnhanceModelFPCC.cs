using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class EnhanceModelFPCC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comission",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "InsuranceCost",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "OrderQuantity",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "TKLQuantity",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.RenameColumn(
                name: "CargoCost",
                table: "FinishingPrintingCostCalculations",
                newName: "StructureMaintenance");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionUnitValue",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<decimal>(
                name: "BankMiscCost",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DirectorOfficeCost",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Embalase",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "GeneralAdministrationCost",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "HelperMaterial",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Lubricant",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MachineMaintenance",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ManufacturingServiceCost",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MiscMaterial",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ScreenCost",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ScreenDocumentNo",
                table: "FinishingPrintingCostCalculations",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SparePart",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankMiscCost",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "DirectorOfficeCost",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "Embalase",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "GeneralAdministrationCost",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "HelperMaterial",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "Lubricant",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "MachineMaintenance",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "ManufacturingServiceCost",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "MiscMaterial",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "ScreenCost",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "ScreenDocumentNo",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "SparePart",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.RenameColumn(
                name: "StructureMaintenance",
                table: "FinishingPrintingCostCalculations",
                newName: "CargoCost");

            migrationBuilder.AlterColumn<double>(
                name: "ProductionUnitValue",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<double>(
                name: "Comission",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InsuranceCost",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "OrderQuantity",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "TKLQuantity",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                defaultValue: 0);
        }
    }
}
