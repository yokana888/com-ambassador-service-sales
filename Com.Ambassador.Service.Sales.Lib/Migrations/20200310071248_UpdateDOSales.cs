using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class UpdateDOSales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DOSalesLocalModel_DOSalesModel_DOSalesModelId",
                table: "DOSalesLocalModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DOSalesModel",
                table: "DOSalesModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DOSalesLocalModel",
                table: "DOSalesLocalModel");

            migrationBuilder.RenameTable(
                name: "DOSalesModel",
                newName: "DOSalesItems");

            migrationBuilder.RenameTable(
                name: "DOSalesLocalModel",
                newName: "LocalItems");

            migrationBuilder.RenameIndex(
                name: "IX_DOSalesLocalModel_DOSalesModelId",
                table: "LocalItems",
                newName: "IX_LocalItems_DOSalesModelId");

            migrationBuilder.AddColumn<string>(
                name: "MaterialConstructionRemark",
                table: "DOSalesItems",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DOSalesItems",
                table: "DOSalesItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocalItems",
                table: "LocalItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalItems_DOSalesItems_DOSalesModelId",
                table: "LocalItems",
                column: "DOSalesModelId",
                principalTable: "DOSalesItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocalItems_DOSalesItems_DOSalesModelId",
                table: "LocalItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocalItems",
                table: "LocalItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DOSalesItems",
                table: "DOSalesItems");

            migrationBuilder.DropColumn(
                name: "MaterialConstructionRemark",
                table: "DOSalesItems");

            migrationBuilder.RenameTable(
                name: "LocalItems",
                newName: "DOSalesLocalModel");

            migrationBuilder.RenameTable(
                name: "DOSalesItems",
                newName: "DOSalesModel");

            migrationBuilder.RenameIndex(
                name: "IX_LocalItems_DOSalesModelId",
                table: "DOSalesLocalModel",
                newName: "IX_DOSalesLocalModel_DOSalesModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DOSalesLocalModel",
                table: "DOSalesLocalModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DOSalesModel",
                table: "DOSalesModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DOSalesLocalModel_DOSalesModel_DOSalesModelId",
                table: "DOSalesLocalModel",
                column: "DOSalesModelId",
                principalTable: "DOSalesModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
