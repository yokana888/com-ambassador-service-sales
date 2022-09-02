using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class Enhance_GarmentSC_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DownPayment",
                table: "GarmentSalesContracts",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FreightCost",
                table: "GarmentSalesContracts",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "GarmentSalesContracts",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DownPayment",
                table: "GarmentSalesContracts");

            migrationBuilder.DropColumn(
                name: "FreightCost",
                table: "GarmentSalesContracts");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "GarmentSalesContracts");
        }
    }
}
