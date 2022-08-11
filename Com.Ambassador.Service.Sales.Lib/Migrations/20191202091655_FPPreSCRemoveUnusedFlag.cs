using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class FPPreSCRemoveUnusedFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPO",
                table: "FinishingPrintingPreSalesContractModel");

            migrationBuilder.DropColumn(
                name: "IsSC",
                table: "FinishingPrintingPreSalesContractModel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPO",
                table: "FinishingPrintingPreSalesContractModel",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSC",
                table: "FinishingPrintingPreSalesContractModel",
                nullable: false,
                defaultValue: false);
        }
    }
}
