using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class AddTotalPaidAndTotalPaymentInSalesReceipt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UseVat",
                table: "SalesReceiptDetails");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "SalesReceiptDetails",
                newName: "TotalPayment");

            migrationBuilder.AddColumn<double>(
                name: "TotalPaid",
                table: "SalesReceipts",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPaid",
                table: "SalesReceipts");

            migrationBuilder.RenameColumn(
                name: "TotalPayment",
                table: "SalesReceiptDetails",
                newName: "TotalAmount");

            migrationBuilder.AddColumn<bool>(
                name: "UseVat",
                table: "SalesReceiptDetails",
                nullable: false,
                defaultValue: false);
        }
    }
}
