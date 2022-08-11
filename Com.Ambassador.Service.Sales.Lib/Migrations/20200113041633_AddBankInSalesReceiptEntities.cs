using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class AddBankInSalesReceiptEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountCOA",
                table: "SalesReceipts",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountName",
                table: "SalesReceipts",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                table: "SalesReceipts",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountCOA",
                table: "SalesReceipts");

            migrationBuilder.DropColumn(
                name: "AccountName",
                table: "SalesReceipts");

            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "SalesReceipts");
        }
    }
}
