using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class add_coloumn_vat_weaving_sales_contract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VatId",
                table: "WeavingSalesContract",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "VatRate",
                table: "WeavingSalesContract",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VatId",
                table: "WeavingSalesContract");

            migrationBuilder.DropColumn(
                name: "VatRate",
                table: "WeavingSalesContract");
        }
    }
}
