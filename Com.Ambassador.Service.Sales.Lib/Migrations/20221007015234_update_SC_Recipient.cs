using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class update_SC_Recipient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "GarmentSalesContracts");

            migrationBuilder.RenameColumn(
                name: "Material",
                table: "GarmentSalesContracts",
                newName: "RecipientAddress");

            migrationBuilder.AddColumn<string>(
                name: "RecipientJob",
                table: "GarmentSalesContracts",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecipientName",
                table: "GarmentSalesContracts",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "GarmentSalesContractROs",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Material",
                table: "GarmentSalesContractROs",
                maxLength: 3000,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "GarmentSalesContractROs",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecipientJob",
                table: "GarmentSalesContracts");

            migrationBuilder.DropColumn(
                name: "RecipientName",
                table: "GarmentSalesContracts");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "GarmentSalesContractROs");

            migrationBuilder.DropColumn(
                name: "Material",
                table: "GarmentSalesContractROs");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "GarmentSalesContractROs");

            migrationBuilder.RenameColumn(
                name: "RecipientAddress",
                table: "GarmentSalesContracts",
                newName: "Material");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "GarmentSalesContracts",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
