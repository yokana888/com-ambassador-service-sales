using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class EnhanceAfterPOTestInSalesInvoiceExport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankName",
                table: "SalesInvoiceExports");

            migrationBuilder.DropColumn(
                name: "CartonNo",
                table: "SalesInvoiceExports");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "SalesInvoiceExports");

            migrationBuilder.DropColumn(
                name: "ContractNo",
                table: "SalesInvoiceExports");

            migrationBuilder.DropColumn(
                name: "Indent",
                table: "SalesInvoiceExports");

            migrationBuilder.DropColumn(
                name: "QuantityLength",
                table: "SalesInvoiceExports");

            migrationBuilder.DropColumn(
                name: "ConvertUnit",
                table: "SalesInvoiceExportItems");

            migrationBuilder.DropColumn(
                name: "ConvertValue",
                table: "SalesInvoiceExportItems");

            migrationBuilder.RenameColumn(
                name: "SalesInvoiceType",
                table: "SalesInvoiceExports",
                newName: "LetterOfCreditNumberType");

            migrationBuilder.RenameColumn(
                name: "OrderNo",
                table: "SalesInvoiceExports",
                newName: "IssuedBy");

            migrationBuilder.AlterColumn<string>(
                name: "ShippingRemark",
                table: "SalesInvoiceExports",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractNo",
                table: "SalesInvoiceExportDetails",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractNo",
                table: "SalesInvoiceExportDetails");

            migrationBuilder.RenameColumn(
                name: "LetterOfCreditNumberType",
                table: "SalesInvoiceExports",
                newName: "SalesInvoiceType");

            migrationBuilder.RenameColumn(
                name: "IssuedBy",
                table: "SalesInvoiceExports",
                newName: "OrderNo");

            migrationBuilder.AlterColumn<string>(
                name: "ShippingRemark",
                table: "SalesInvoiceExports",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "SalesInvoiceExports",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CartonNo",
                table: "SalesInvoiceExports",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "SalesInvoiceExports",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractNo",
                table: "SalesInvoiceExports",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Indent",
                table: "SalesInvoiceExports",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "QuantityLength",
                table: "SalesInvoiceExports",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ConvertUnit",
                table: "SalesInvoiceExportItems",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ConvertValue",
                table: "SalesInvoiceExportItems",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
