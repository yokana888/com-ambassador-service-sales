using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class AddNewSalesInvoiceColumnsForExport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CartonNo",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "GrossWeight",
                table: "SalesInvoices",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Indent",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "NetWeight",
                table: "SalesInvoices",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "OrderNo",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "QuantityLength",
                table: "SalesInvoices",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalMeas",
                table: "SalesInvoices",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "TotalUom",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WeightUom",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CartonNo",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "GrossWeight",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "Indent",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "NetWeight",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "OrderNo",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "QuantityLength",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "TotalMeas",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "TotalUom",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "WeightUom",
                table: "SalesInvoices");
        }
    }
}
