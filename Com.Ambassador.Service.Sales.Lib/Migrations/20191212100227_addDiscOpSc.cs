using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class addDiscOpSc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Disc",
                table: "SalesInvoices",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Op",
                table: "SalesInvoices",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sc",
                table: "SalesInvoices",
                maxLength: 25,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Disc",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "Op",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "Sc",
                table: "SalesInvoices");
        }
    }
}
