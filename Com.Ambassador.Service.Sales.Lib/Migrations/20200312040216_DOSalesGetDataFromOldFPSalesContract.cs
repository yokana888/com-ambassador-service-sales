using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class DOSalesGetDataFromOldFPSalesContract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalesFirstName",
                table: "DOSales");

            migrationBuilder.DropColumn(
                name: "SalesId",
                table: "DOSales");

            migrationBuilder.RenameColumn(
                name: "SalesLastName",
                table: "DOSales",
                newName: "SalesName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SalesName",
                table: "DOSales",
                newName: "SalesLastName");

            migrationBuilder.AddColumn<string>(
                name: "SalesFirstName",
                table: "DOSales",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalesId",
                table: "DOSales",
                nullable: true);
        }
    }
}
