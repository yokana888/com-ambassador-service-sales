using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class UpdateSalesReceiptDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalPaid",
                table: "SalesReceiptDetails",
                newName: "Unpaid");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "SalesReceiptDetails",
                newName: "TotalAmount");

            migrationBuilder.AddColumn<double>(
                name: "Nominal",
                table: "SalesReceiptDetails",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Paid",
                table: "SalesReceiptDetails",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nominal",
                table: "SalesReceiptDetails");

            migrationBuilder.DropColumn(
                name: "Paid",
                table: "SalesReceiptDetails");

            migrationBuilder.RenameColumn(
                name: "Unpaid",
                table: "SalesReceiptDetails",
                newName: "TotalPaid");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "SalesReceiptDetails",
                newName: "Amount");
        }
    }
}
