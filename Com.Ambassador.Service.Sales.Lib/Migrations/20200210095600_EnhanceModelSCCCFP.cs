using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class EnhanceModelSCCCFP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FinishingPrintingSalesContracts_SalesContractNo",
                table: "FinishingPrintingSalesContracts");

            migrationBuilder.DropColumn(
                name: "ImageFile",
                table: "FinishingPrintingCostCalculations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageFile",
                table: "FinishingPrintingCostCalculations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinishingPrintingSalesContracts_SalesContractNo",
                table: "FinishingPrintingSalesContracts",
                column: "SalesContractNo",
                unique: true,
                filter: "[IsDeleted]=(0)");
        }
    }
}
