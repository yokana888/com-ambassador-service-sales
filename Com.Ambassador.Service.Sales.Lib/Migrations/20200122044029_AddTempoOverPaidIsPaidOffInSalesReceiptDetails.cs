using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class AddTempoOverPaidIsPaidOffInSalesReceiptDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "OverPaid",
                table: "SalesReceiptDetails",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Tempo",
                table: "SalesReceiptDetails",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "isPaidOff",
                table: "SalesReceiptDetails",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OverPaid",
                table: "SalesReceiptDetails");

            migrationBuilder.DropColumn(
                name: "Tempo",
                table: "SalesReceiptDetails");

            migrationBuilder.DropColumn(
                name: "isPaidOff",
                table: "SalesReceiptDetails");
        }
    }
}
