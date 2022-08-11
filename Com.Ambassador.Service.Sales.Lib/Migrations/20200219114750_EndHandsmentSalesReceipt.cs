using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class EndHandsmentSalesReceipt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SalesReceiptType",
                table: "SalesReceipts",
                newName: "UnitName");

            migrationBuilder.AddColumn<double>(
                name: "AdministrationFee",
                table: "SalesReceipts",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyCode",
                table: "SalesReceipts",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "SalesReceipts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "CurrencyRate",
                table: "SalesReceipts",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "CurrencySymbol",
                table: "SalesReceipts",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OriginAccountNumber",
                table: "SalesReceipts",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OriginBankName",
                table: "SalesReceipts",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdministrationFee",
                table: "SalesReceipts");

            migrationBuilder.DropColumn(
                name: "CurrencyCode",
                table: "SalesReceipts");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "SalesReceipts");

            migrationBuilder.DropColumn(
                name: "CurrencyRate",
                table: "SalesReceipts");

            migrationBuilder.DropColumn(
                name: "CurrencySymbol",
                table: "SalesReceipts");

            migrationBuilder.DropColumn(
                name: "OriginAccountNumber",
                table: "SalesReceipts");

            migrationBuilder.DropColumn(
                name: "OriginBankName",
                table: "SalesReceipts");

            migrationBuilder.RenameColumn(
                name: "UnitName",
                table: "SalesReceipts",
                newName: "SalesReceiptType");
        }
    }
}
