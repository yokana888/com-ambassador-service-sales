using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class UpdateAfterUATSalesInvoiceAndSalesReceipt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UseVat",
                table: "SalesInvoices");

            migrationBuilder.RenameColumn(
                name: "DOSalesNo",
                table: "SalesInvoices",
                newName: "VatType");

            migrationBuilder.RenameColumn(
                name: "DOSalesId",
                table: "SalesInvoices",
                newName: "ShipmentDocumentId");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "SalesInvoiceDetails",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "UnitName",
                table: "SalesInvoiceDetails",
                newName: "ProductName");

            migrationBuilder.RenameColumn(
                name: "UnitCode",
                table: "SalesInvoiceDetails",
                newName: "ProductCode");

            migrationBuilder.AddColumn<string>(
                name: "ShipmentDocumentCode",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShipmentDocumentCode",
                table: "SalesInvoices");

            migrationBuilder.RenameColumn(
                name: "VatType",
                table: "SalesInvoices",
                newName: "DOSalesNo");

            migrationBuilder.RenameColumn(
                name: "ShipmentDocumentId",
                table: "SalesInvoices",
                newName: "DOSalesId");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "SalesInvoiceDetails",
                newName: "UnitName");

            migrationBuilder.RenameColumn(
                name: "ProductCode",
                table: "SalesInvoiceDetails",
                newName: "UnitCode");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "SalesInvoiceDetails",
                newName: "UnitPrice");

            migrationBuilder.AddColumn<bool>(
                name: "UseVat",
                table: "SalesInvoices",
                nullable: false,
                defaultValue: false);
        }
    }
}
