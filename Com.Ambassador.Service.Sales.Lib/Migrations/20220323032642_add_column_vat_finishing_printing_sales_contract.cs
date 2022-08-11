using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class add_column_vat_finishing_printing_sales_contract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VatId",
                table: "FinishingPrintingSalesContracts",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "VatRate",
                table: "FinishingPrintingSalesContracts",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VatId",
                table: "FinishingPrintingSalesContracts");

            migrationBuilder.DropColumn(
                name: "VatRate",
                table: "FinishingPrintingSalesContracts");
        }
    }
}
