using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class AddSalesInFPSC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SalesFirstName",
                table: "FinishingPrintingSalesContracts",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SalesId",
                table: "FinishingPrintingSalesContracts",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "SalesLastName",
                table: "FinishingPrintingSalesContracts",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalesUserName",
                table: "FinishingPrintingSalesContracts",
                maxLength: 1024,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalesFirstName",
                table: "FinishingPrintingSalesContracts");

            migrationBuilder.DropColumn(
                name: "SalesId",
                table: "FinishingPrintingSalesContracts");

            migrationBuilder.DropColumn(
                name: "SalesLastName",
                table: "FinishingPrintingSalesContracts");

            migrationBuilder.DropColumn(
                name: "SalesUserName",
                table: "FinishingPrintingSalesContracts");
        }
    }
}
