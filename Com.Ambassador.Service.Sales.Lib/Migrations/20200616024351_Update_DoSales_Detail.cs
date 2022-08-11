using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class Update_DoSales_Detail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Grade",
                table: "DOSalesLocalItems",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoSOP",
                table: "DOSalesLocalItems",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThreadNumber",
                table: "DOSalesLocalItems",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "DOSalesLocalItems");

            migrationBuilder.DropColumn(
                name: "NoSOP",
                table: "DOSalesLocalItems");

            migrationBuilder.DropColumn(
                name: "ThreadNumber",
                table: "DOSalesLocalItems");
        }
    }
}
