using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class ChangeModelDOReturnItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "DOReturnItems");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "DOReturnItems");

            migrationBuilder.RenameColumn(
                name: "UomUnit",
                table: "DOReturnItems",
                newName: "ItemUom");

            migrationBuilder.RenameColumn(
                name: "UomId",
                table: "DOReturnItems",
                newName: "ShippingOutId");

            migrationBuilder.RenameColumn(
                name: "ShipmentDocumentId",
                table: "DOReturnItems",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "ShipmentDocumentCode",
                table: "DOReturnItems",
                newName: "BonNo");

            migrationBuilder.AddColumn<double>(
                name: "QuantityItem",
                table: "DOReturnItems",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "QuantityPacking",
                table: "DOReturnItems",
                maxLength: 255,
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantityItem",
                table: "DOReturnItems");

            migrationBuilder.DropColumn(
                name: "QuantityPacking",
                table: "DOReturnItems");

            migrationBuilder.RenameColumn(
                name: "ShippingOutId",
                table: "DOReturnItems",
                newName: "UomId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "DOReturnItems",
                newName: "ShipmentDocumentId");

            migrationBuilder.RenameColumn(
                name: "ItemUom",
                table: "DOReturnItems",
                newName: "UomUnit");

            migrationBuilder.RenameColumn(
                name: "BonNo",
                table: "DOReturnItems",
                newName: "ShipmentDocumentCode");

            migrationBuilder.AddColumn<string>(
                name: "Quantity",
                table: "DOReturnItems",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Total",
                table: "DOReturnItems",
                nullable: true);
        }
    }
}
