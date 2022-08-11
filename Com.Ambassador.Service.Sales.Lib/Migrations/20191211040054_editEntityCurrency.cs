using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class editEntityCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyCode",
                table: "SalesInvoiceDetails");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "SalesInvoiceDetails");

            migrationBuilder.DropColumn(
                name: "CurrencySymbol",
                table: "SalesInvoiceDetails");

            migrationBuilder.AddColumn<string>(
                name: "CurrencyCode",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "SalesInvoices",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CurrencySymbol",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UomId",
                table: "SalesInvoiceDetails",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(long),
                oldMaxLength: 25);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyCode",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "CurrencySymbol",
                table: "SalesInvoices");

            migrationBuilder.AlterColumn<long>(
                name: "UomId",
                table: "SalesInvoiceDetails",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(int),
                oldMaxLength: 25);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyCode",
                table: "SalesInvoiceDetails",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CurrencyId",
                table: "SalesInvoiceDetails",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CurrencySymbol",
                table: "SalesInvoiceDetails",
                maxLength: 255,
                nullable: true);
        }
    }
}
