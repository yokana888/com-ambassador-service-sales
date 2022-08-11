using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class RenameColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShippingOutId",
                table: "SalesInvoiceExportDetails",
                newName: "BonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BonId",
                table: "SalesInvoiceExportDetails",
                newName: "ShippingOutId");
        }
    }
}
