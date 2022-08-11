using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class AddContractNoAndDescInSalesInvoiceExport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContractNo",
                table: "SalesInvoiceExports",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SalesInvoiceExportDetails",
                maxLength: 1000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractNo",
                table: "SalesInvoiceExports");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "SalesInvoiceExportDetails");
        }
    }
}
