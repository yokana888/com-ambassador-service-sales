using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class RenamePreviousPaymentToTotalPaidInSalesReceipt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PreviousPayment",
                table: "SalesReceiptDetails",
                newName: "TotalPaid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalPaid",
                table: "SalesReceiptDetails",
                newName: "PreviousPayment");
        }
    }
}
