using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class FixingSalesReceipt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountCOA",
                table: "SalesReceipts");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "SalesReceipts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountCOA",
                table: "SalesReceipts",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "SalesReceipts",
                nullable: false,
                defaultValue: 0);
        }
    }
}
