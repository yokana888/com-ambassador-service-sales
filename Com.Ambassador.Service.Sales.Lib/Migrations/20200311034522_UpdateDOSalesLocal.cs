using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class UpdateDOSalesLocal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocalItems_DOSalesItems_DOSalesModelId",
                table: "LocalItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocalItems",
                table: "LocalItems");

            migrationBuilder.DropColumn(
                name: "UnitCode",
                table: "LocalItems");

            migrationBuilder.RenameTable(
                name: "LocalItems",
                newName: "DOSalesLocalItems");

            migrationBuilder.RenameColumn(
                name: "SalesName",
                table: "DOSalesItems",
                newName: "SalesLastName");

            migrationBuilder.RenameColumn(
                name: "UnitName",
                table: "DOSalesLocalItems",
                newName: "UnitOrCode");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "DOSalesLocalItems",
                newName: "ProductionOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_LocalItems_DOSalesModelId",
                table: "DOSalesLocalItems",
                newName: "IX_DOSalesLocalItems_DOSalesModelId");

            migrationBuilder.AddColumn<string>(
                name: "SalesFirstName",
                table: "DOSalesItems",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalesId",
                table: "DOSalesItems",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DOSalesLocalItems",
                table: "DOSalesLocalItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DOSalesLocalItems_DOSalesItems_DOSalesModelId",
                table: "DOSalesLocalItems",
                column: "DOSalesModelId",
                principalTable: "DOSalesItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DOSalesLocalItems_DOSalesItems_DOSalesModelId",
                table: "DOSalesLocalItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DOSalesLocalItems",
                table: "DOSalesLocalItems");

            migrationBuilder.DropColumn(
                name: "SalesFirstName",
                table: "DOSalesItems");

            migrationBuilder.DropColumn(
                name: "SalesId",
                table: "DOSalesItems");

            migrationBuilder.RenameTable(
                name: "DOSalesLocalItems",
                newName: "LocalItems");

            migrationBuilder.RenameColumn(
                name: "SalesLastName",
                table: "DOSalesItems",
                newName: "SalesName");

            migrationBuilder.RenameColumn(
                name: "UnitOrCode",
                table: "LocalItems",
                newName: "UnitName");

            migrationBuilder.RenameColumn(
                name: "ProductionOrderId",
                table: "LocalItems",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_DOSalesLocalItems_DOSalesModelId",
                table: "LocalItems",
                newName: "IX_LocalItems_DOSalesModelId");

            migrationBuilder.AddColumn<string>(
                name: "UnitCode",
                table: "LocalItems",
                maxLength: 128,
                nullable: true);

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
    }
}
