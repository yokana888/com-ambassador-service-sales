using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class deleteDispOpScFromSalesInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Disp",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "Op",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "Sc",
                table: "SalesInvoices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Disp",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Op",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sc",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true);
        }
    }
}
