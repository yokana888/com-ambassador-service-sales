using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class PreSC_CC_SC_Unique_Column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_GarmentSalesContracts_SalesContractNo",
                table: "GarmentSalesContracts",
                column: "SalesContractNo",
                unique: true,
                filter: "[IsDeleted]=(0)");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentPreSalesContracts_SCNo",
                table: "GarmentPreSalesContracts",
                column: "SCNo",
                unique: true,
                filter: "[IsDeleted]=(0)");

            migrationBuilder.CreateIndex(
                name: "IX_CostCalculationGarments_RO_Number",
                table: "CostCalculationGarments",
                column: "RO_Number",
                unique: true,
                filter: "[IsDeleted]=(0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GarmentSalesContracts_SalesContractNo",
                table: "GarmentSalesContracts");

            migrationBuilder.DropIndex(
                name: "IX_GarmentPreSalesContracts_SCNo",
                table: "GarmentPreSalesContracts");

            migrationBuilder.DropIndex(
                name: "IX_CostCalculationGarments_RO_Number",
                table: "CostCalculationGarments");
        }
    }
}
