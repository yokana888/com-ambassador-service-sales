using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class AddPropertyFPSC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductionOrderNo",
                table: "FinishingPrintingSalesContracts",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitName",
                table: "FinishingPrintingSalesContracts",
                maxLength: 512,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductionOrderNo",
                table: "FinishingPrintingSalesContracts");

            migrationBuilder.DropColumn(
                name: "UnitName",
                table: "FinishingPrintingSalesContracts");
        }
    }
}
