using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class EnhanceFPPreSCAddOrderType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderTypeCode",
                table: "FinishingPrintingPreSalesContracts",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderTypeId",
                table: "FinishingPrintingPreSalesContracts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OrderTypeName",
                table: "FinishingPrintingPreSalesContracts",
                maxLength: 512,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderTypeCode",
                table: "FinishingPrintingPreSalesContracts");

            migrationBuilder.DropColumn(
                name: "OrderTypeId",
                table: "FinishingPrintingPreSalesContracts");

            migrationBuilder.DropColumn(
                name: "OrderTypeName",
                table: "FinishingPrintingPreSalesContracts");
        }
    }
}
