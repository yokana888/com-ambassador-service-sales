using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class update_SCTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Claim",
                table: "GarmentSalesContracts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LatePayment",
                table: "GarmentSalesContracts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LateReturn",
                table: "GarmentSalesContracts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Claim",
                table: "GarmentSalesContracts");

            migrationBuilder.DropColumn(
                name: "LatePayment",
                table: "GarmentSalesContracts");

            migrationBuilder.DropColumn(
                name: "LateReturn",
                table: "GarmentSalesContracts");
        }
    }
}
