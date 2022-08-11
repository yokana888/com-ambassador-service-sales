using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class AddIndexSalesContractNoFPSC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FinishingPrintingSalesContracts_SalesContractNo",
                table: "FinishingPrintingSalesContracts",
                column: "SalesContractNo",
                unique: true,
                filter: "[IsDeleted]=(0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FinishingPrintingSalesContracts_SalesContractNo",
                table: "FinishingPrintingSalesContracts");
        }
    }
}
