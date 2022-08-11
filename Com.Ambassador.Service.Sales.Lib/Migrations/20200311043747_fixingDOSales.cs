using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class fixingDOSales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DOSalesLocalItems_DOSalesItems_DOSalesModelId",
                table: "DOSalesLocalItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DOSalesItems",
                table: "DOSalesItems");

            migrationBuilder.RenameTable(
                name: "DOSalesItems",
                newName: "DOSales");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DOSales",
                table: "DOSales",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DOSalesLocalItems_DOSales_DOSalesModelId",
                table: "DOSalesLocalItems",
                column: "DOSalesModelId",
                principalTable: "DOSales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DOSalesLocalItems_DOSales_DOSalesModelId",
                table: "DOSalesLocalItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DOSales",
                table: "DOSales");

            migrationBuilder.RenameTable(
                name: "DOSales",
                newName: "DOSalesItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DOSalesItems",
                table: "DOSalesItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DOSalesLocalItems_DOSalesItems_DOSalesModelId",
                table: "DOSalesLocalItems",
                column: "DOSalesModelId",
                principalTable: "DOSalesItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
