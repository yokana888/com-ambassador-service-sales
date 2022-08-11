using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class UpdateColumnInDOSales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaterialConstructionRemark",
                table: "DOSales",
                newName: "LocalMaterialName");

            migrationBuilder.RenameColumn(
                name: "MaterialConstructionName",
                table: "DOSales",
                newName: "MaterialWidth");

            migrationBuilder.RenameColumn(
                name: "MaterialConstructionId",
                table: "DOSales",
                newName: "LocalMaterialConstructionId");

            migrationBuilder.RenameColumn(
                name: "MaterialConstructionCode",
                table: "DOSales",
                newName: "LocalMaterialConstructionCode");

            migrationBuilder.AddColumn<string>(
                name: "ColorRequest",
                table: "DOSalesLocalItems",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColorTemplate",
                table: "DOSalesLocalItems",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaterialCode",
                table: "DOSalesLocalItems",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MaterialId",
                table: "DOSalesLocalItems",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "MaterialName",
                table: "DOSalesLocalItems",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MaterialPrice",
                table: "DOSalesLocalItems",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "MaterialTags",
                table: "DOSalesLocalItems",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaterialWidth",
                table: "DOSalesLocalItems",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColorRequest",
                table: "DOSales",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColorTemplate",
                table: "DOSales",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExportMaterialConstructionCode",
                table: "DOSales",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExportMaterialConstructionId",
                table: "DOSales",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ExportMaterialConstructionName",
                table: "DOSales",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocalBuyerAddress",
                table: "DOSales",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocalMaterialCode",
                table: "DOSales",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocalMaterialConstructionName",
                table: "DOSales",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LocalMaterialId",
                table: "DOSales",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<double>(
                name: "LocalMaterialPrice",
                table: "DOSales",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "LocalMaterialTags",
                table: "DOSales",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorRequest",
                table: "DOSalesLocalItems");

            migrationBuilder.DropColumn(
                name: "ColorTemplate",
                table: "DOSalesLocalItems");

            migrationBuilder.DropColumn(
                name: "MaterialCode",
                table: "DOSalesLocalItems");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "DOSalesLocalItems");

            migrationBuilder.DropColumn(
                name: "MaterialName",
                table: "DOSalesLocalItems");

            migrationBuilder.DropColumn(
                name: "MaterialPrice",
                table: "DOSalesLocalItems");

            migrationBuilder.DropColumn(
                name: "MaterialTags",
                table: "DOSalesLocalItems");

            migrationBuilder.DropColumn(
                name: "MaterialWidth",
                table: "DOSalesLocalItems");

            migrationBuilder.DropColumn(
                name: "ColorRequest",
                table: "DOSales");

            migrationBuilder.DropColumn(
                name: "ColorTemplate",
                table: "DOSales");

            migrationBuilder.DropColumn(
                name: "ExportMaterialConstructionCode",
                table: "DOSales");

            migrationBuilder.DropColumn(
                name: "ExportMaterialConstructionId",
                table: "DOSales");

            migrationBuilder.DropColumn(
                name: "ExportMaterialConstructionName",
                table: "DOSales");

            migrationBuilder.DropColumn(
                name: "LocalBuyerAddress",
                table: "DOSales");

            migrationBuilder.DropColumn(
                name: "LocalMaterialCode",
                table: "DOSales");

            migrationBuilder.DropColumn(
                name: "LocalMaterialConstructionName",
                table: "DOSales");

            migrationBuilder.DropColumn(
                name: "LocalMaterialId",
                table: "DOSales");

            migrationBuilder.DropColumn(
                name: "LocalMaterialPrice",
                table: "DOSales");

            migrationBuilder.DropColumn(
                name: "LocalMaterialTags",
                table: "DOSales");

            migrationBuilder.RenameColumn(
                name: "MaterialWidth",
                table: "DOSales",
                newName: "MaterialConstructionName");

            migrationBuilder.RenameColumn(
                name: "LocalMaterialName",
                table: "DOSales",
                newName: "MaterialConstructionRemark");

            migrationBuilder.RenameColumn(
                name: "LocalMaterialConstructionId",
                table: "DOSales",
                newName: "MaterialConstructionId");

            migrationBuilder.RenameColumn(
                name: "LocalMaterialConstructionCode",
                table: "DOSales",
                newName: "MaterialConstructionCode");
        }
    }
}
