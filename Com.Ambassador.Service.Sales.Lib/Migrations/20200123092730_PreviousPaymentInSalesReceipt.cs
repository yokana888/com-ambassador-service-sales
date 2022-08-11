using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class PreviousPaymentInSalesReceipt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isPaidOff",
                table: "SalesReceiptDetails",
                newName: "IsPaidOff");

            migrationBuilder.AddColumn<double>(
                name: "PreviousPayment",
                table: "SalesReceiptDetails",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreviousPayment",
                table: "SalesReceiptDetails");

            migrationBuilder.RenameColumn(
                name: "IsPaidOff",
                table: "SalesReceiptDetails",
                newName: "isPaidOff");
        }
    }
}
