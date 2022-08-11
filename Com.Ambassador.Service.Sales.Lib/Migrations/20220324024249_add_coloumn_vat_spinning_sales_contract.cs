using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class add_coloumn_vat_spinning_sales_contract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VatId",
                table: "SpinningSalesContract",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "VatRate",
                table: "SpinningSalesContract",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VatId",
                table: "SpinningSalesContract");

            migrationBuilder.DropColumn(
                name: "VatRate",
                table: "SpinningSalesContract");
        }
    }
}
