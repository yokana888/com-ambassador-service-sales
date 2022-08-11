using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class AddProductionOrderNoInSCDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CostCalculationId",
                table: "FinishingPrintingSalesContractDetails",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ProductionOrderNo",
                table: "FinishingPrintingSalesContractDetails",
                maxLength: 64,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostCalculationId",
                table: "FinishingPrintingSalesContractDetails");

            migrationBuilder.DropColumn(
                name: "ProductionOrderNo",
                table: "FinishingPrintingSalesContractDetails");
        }
    }
}
