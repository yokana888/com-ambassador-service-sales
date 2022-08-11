using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class AddFieldInSalesInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NPPKP",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "NPWP",
                table: "SalesInvoices");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "SalesInvoices",
                newName: "Remark");

            migrationBuilder.AddColumn<int>(
                name: "AutoIncrementNumber",
                table: "SalesInvoices",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "CurrencyRate",
                table: "SalesInvoices",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AutoIncrementNumber",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "CurrencyRate",
                table: "SalesInvoices");

            migrationBuilder.RenameColumn(
                name: "Remark",
                table: "SalesInvoices",
                newName: "Notes");

            migrationBuilder.AddColumn<string>(
                name: "NPPKP",
                table: "SalesInvoices",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NPWP",
                table: "SalesInvoices",
                maxLength: 100,
                nullable: true);
        }
    }
}
