using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class EnhanceDOSalesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvalType",
                table: "DOSalesLocalItems",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Construction",
                table: "DOSales",
                maxLength: 4096,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvalType",
                table: "DOSalesLocalItems");

            migrationBuilder.DropColumn(
                name: "Construction",
                table: "DOSales");
        }
    }
}
