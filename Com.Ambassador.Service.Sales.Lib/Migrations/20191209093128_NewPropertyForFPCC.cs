using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class NewPropertyForFPCC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductionOrderId",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "FinishingPrintingCostCalculationChemicals");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "FinishingPrintingCostCalculationChemicals");

            migrationBuilder.RenameColumn(
                name: "PreparationValue",
                table: "FinishingPrintingCostCalculations",
                newName: "OrderQuantity");

            migrationBuilder.RenameColumn(
                name: "Utility",
                table: "FinishingPrintingCostCalculationMachines",
                newName: "MachineWater");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "FinishingPrintingCostCalculationMachines",
                newName: "MachineSteam");

            migrationBuilder.RenameColumn(
                name: "StepProcessId",
                table: "FinishingPrintingCostCalculationMachines",
                newName: "StepId");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "FinishingPrintingCostCalculations",
                maxLength: 4096,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2048,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GreigeName",
                table: "FinishingPrintingCostCalculations",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "GreigeId",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "FinishingPrintingCostCalculations",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Comission",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<decimal>(
                name: "ConfirmPrice",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "MaterialId",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "MaterialName",
                table: "FinishingPrintingCostCalculations",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PreSalesContractId",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "PreSalesContractNo",
                table: "FinishingPrintingCostCalculations",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalesFirstName",
                table: "FinishingPrintingCostCalculations",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SalesId",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "SalesLastName",
                table: "FinishingPrintingCostCalculations",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalesUserName",
                table: "FinishingPrintingCostCalculations",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UomId",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "UomUnit",
                table: "FinishingPrintingCostCalculations",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "MachineId",
                table: "FinishingPrintingCostCalculationMachines",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<decimal>(
                name: "MachineElectric",
                table: "FinishingPrintingCostCalculationMachines",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MachineLPG",
                table: "FinishingPrintingCostCalculationMachines",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "MachineName",
                table: "FinishingPrintingCostCalculationMachines",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MachineProcess",
                table: "FinishingPrintingCostCalculationMachines",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MachineSolar",
                table: "FinishingPrintingCostCalculationMachines",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "StepProcess",
                table: "FinishingPrintingCostCalculationMachines",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StepProcessArea",
                table: "FinishingPrintingCostCalculationMachines",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ChemicalId",
                table: "FinishingPrintingCostCalculationChemicals",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "ChemicalCurrency",
                table: "FinishingPrintingCostCalculationChemicals",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChemicalName",
                table: "FinishingPrintingCostCalculationChemicals",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ChemicalPrice",
                table: "FinishingPrintingCostCalculationChemicals",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ChemicalUom",
                table: "FinishingPrintingCostCalculationChemicals",
                maxLength: 64,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "Comission",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "ConfirmPrice",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "MaterialName",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "PreSalesContractId",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "PreSalesContractNo",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "SalesFirstName",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "SalesId",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "SalesLastName",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "SalesUserName",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "UomId",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "UomUnit",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "MachineElectric",
                table: "FinishingPrintingCostCalculationMachines");

            migrationBuilder.DropColumn(
                name: "MachineLPG",
                table: "FinishingPrintingCostCalculationMachines");

            migrationBuilder.DropColumn(
                name: "MachineName",
                table: "FinishingPrintingCostCalculationMachines");

            migrationBuilder.DropColumn(
                name: "MachineProcess",
                table: "FinishingPrintingCostCalculationMachines");

            migrationBuilder.DropColumn(
                name: "MachineSolar",
                table: "FinishingPrintingCostCalculationMachines");

            migrationBuilder.DropColumn(
                name: "StepProcess",
                table: "FinishingPrintingCostCalculationMachines");

            migrationBuilder.DropColumn(
                name: "StepProcessArea",
                table: "FinishingPrintingCostCalculationMachines");

            migrationBuilder.DropColumn(
                name: "ChemicalCurrency",
                table: "FinishingPrintingCostCalculationChemicals");

            migrationBuilder.DropColumn(
                name: "ChemicalName",
                table: "FinishingPrintingCostCalculationChemicals");

            migrationBuilder.DropColumn(
                name: "ChemicalPrice",
                table: "FinishingPrintingCostCalculationChemicals");

            migrationBuilder.DropColumn(
                name: "ChemicalUom",
                table: "FinishingPrintingCostCalculationChemicals");

            migrationBuilder.RenameColumn(
                name: "OrderQuantity",
                table: "FinishingPrintingCostCalculations",
                newName: "PreparationValue");

            migrationBuilder.RenameColumn(
                name: "StepId",
                table: "FinishingPrintingCostCalculationMachines",
                newName: "StepProcessId");

            migrationBuilder.RenameColumn(
                name: "MachineWater",
                table: "FinishingPrintingCostCalculationMachines",
                newName: "Utility");

            migrationBuilder.RenameColumn(
                name: "MachineSteam",
                table: "FinishingPrintingCostCalculationMachines",
                newName: "Total");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "FinishingPrintingCostCalculations",
                maxLength: 2048,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 4096,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GreigeName",
                table: "FinishingPrintingCostCalculations",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GreigeId",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<int>(
                name: "ProductionOrderId",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "MachineId",
                table: "FinishingPrintingCostCalculationMachines",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "ChemicalId",
                table: "FinishingPrintingCostCalculationChemicals",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "FinishingPrintingCostCalculationChemicals",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "FinishingPrintingCostCalculationChemicals",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
