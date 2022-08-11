using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class addDBsetFPPreSC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FinishingPrintingPreSalesContractModel",
                table: "FinishingPrintingPreSalesContractModel");

            migrationBuilder.RenameTable(
                name: "FinishingPrintingPreSalesContractModel",
                newName: "FinishingPrintingPreSalesContracts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinishingPrintingPreSalesContracts",
                table: "FinishingPrintingPreSalesContracts",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FinishingPrintingPreSalesContracts",
                table: "FinishingPrintingPreSalesContracts");

            migrationBuilder.RenameTable(
                name: "FinishingPrintingPreSalesContracts",
                newName: "FinishingPrintingPreSalesContractModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinishingPrintingPreSalesContractModel",
                table: "FinishingPrintingPreSalesContractModel",
                column: "Id");
        }
    }
}
