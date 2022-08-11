using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class EnhancementDOSales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExportBuyerCode",
                table: "DOSales");

            migrationBuilder.DropColumn(
                name: "ExportBuyerId",
                table: "DOSales");

            migrationBuilder.DropColumn(
                name: "ExportBuyerName",
                table: "DOSales");

            migrationBuilder.DropColumn(
                name: "ExportBuyerType",
                table: "DOSales");

            migrationBuilder.DropColumn(
                name: "ExportDate",
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
                name: "ExportSalesContractId",
                table: "DOSales");

            migrationBuilder.DropColumn(
                name: "ExportSalesContractNo",
                table: "DOSales");

            migrationBuilder.DropColumn(
                name: "ExportType",
                table: "DOSales");

            migrationBuilder.RenameColumn(
                name: "LocalType",
                table: "DOSales",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "LocalSalesContractNo",
                table: "DOSales",
                newName: "SalesContractNo");

            migrationBuilder.RenameColumn(
                name: "LocalSalesContractId",
                table: "DOSales",
                newName: "SalesContractId");

            migrationBuilder.RenameColumn(
                name: "LocalRemark",
                table: "DOSales",
                newName: "Remark");

            migrationBuilder.RenameColumn(
                name: "LocalMaterialTags",
                table: "DOSales",
                newName: "MaterialTags");

            migrationBuilder.RenameColumn(
                name: "LocalMaterialPrice",
                table: "DOSales",
                newName: "MaterialPrice");

            migrationBuilder.RenameColumn(
                name: "LocalMaterialName",
                table: "DOSales",
                newName: "MaterialName");

            migrationBuilder.RenameColumn(
                name: "LocalMaterialId",
                table: "DOSales",
                newName: "MaterialId");

            migrationBuilder.RenameColumn(
                name: "LocalMaterialConstructionName",
                table: "DOSales",
                newName: "MaterialConstructionName");

            migrationBuilder.RenameColumn(
                name: "LocalMaterialConstructionId",
                table: "DOSales",
                newName: "MaterialConstructionId");

            migrationBuilder.RenameColumn(
                name: "LocalMaterialConstructionCode",
                table: "DOSales",
                newName: "MaterialConstructionCode");

            migrationBuilder.RenameColumn(
                name: "LocalMaterialCode",
                table: "DOSales",
                newName: "MaterialCode");

            migrationBuilder.RenameColumn(
                name: "LocalHeadOfStorage",
                table: "DOSales",
                newName: "HeadOfStorage");

            migrationBuilder.RenameColumn(
                name: "LocalDate",
                table: "DOSales",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "LocalBuyerType",
                table: "DOSales",
                newName: "BuyerType");

            migrationBuilder.RenameColumn(
                name: "LocalBuyerName",
                table: "DOSales",
                newName: "MaterialConstructionRemark");

            migrationBuilder.RenameColumn(
                name: "LocalBuyerId",
                table: "DOSales",
                newName: "BuyerId");

            migrationBuilder.RenameColumn(
                name: "LocalBuyerCode",
                table: "DOSales",
                newName: "BuyerCode");

            migrationBuilder.RenameColumn(
                name: "LocalBuyerAddress",
                table: "DOSales",
                newName: "BuyerName");

            migrationBuilder.RenameColumn(
                name: "ExportRemark",
                table: "DOSales",
                newName: "BuyerAddress");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "DOSales",
                newName: "LocalType");

            migrationBuilder.RenameColumn(
                name: "SalesContractNo",
                table: "DOSales",
                newName: "LocalSalesContractNo");

            migrationBuilder.RenameColumn(
                name: "SalesContractId",
                table: "DOSales",
                newName: "LocalSalesContractId");

            migrationBuilder.RenameColumn(
                name: "Remark",
                table: "DOSales",
                newName: "LocalRemark");

            migrationBuilder.RenameColumn(
                name: "MaterialTags",
                table: "DOSales",
                newName: "LocalMaterialTags");

            migrationBuilder.RenameColumn(
                name: "MaterialPrice",
                table: "DOSales",
                newName: "LocalMaterialPrice");

            migrationBuilder.RenameColumn(
                name: "MaterialName",
                table: "DOSales",
                newName: "LocalMaterialName");

            migrationBuilder.RenameColumn(
                name: "MaterialId",
                table: "DOSales",
                newName: "LocalMaterialId");

            migrationBuilder.RenameColumn(
                name: "MaterialConstructionRemark",
                table: "DOSales",
                newName: "LocalBuyerName");

            migrationBuilder.RenameColumn(
                name: "MaterialConstructionName",
                table: "DOSales",
                newName: "LocalMaterialConstructionName");

            migrationBuilder.RenameColumn(
                name: "MaterialConstructionId",
                table: "DOSales",
                newName: "LocalMaterialConstructionId");

            migrationBuilder.RenameColumn(
                name: "MaterialConstructionCode",
                table: "DOSales",
                newName: "LocalMaterialConstructionCode");

            migrationBuilder.RenameColumn(
                name: "MaterialCode",
                table: "DOSales",
                newName: "LocalMaterialCode");

            migrationBuilder.RenameColumn(
                name: "HeadOfStorage",
                table: "DOSales",
                newName: "LocalHeadOfStorage");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "DOSales",
                newName: "LocalDate");

            migrationBuilder.RenameColumn(
                name: "BuyerType",
                table: "DOSales",
                newName: "LocalBuyerType");

            migrationBuilder.RenameColumn(
                name: "BuyerName",
                table: "DOSales",
                newName: "LocalBuyerAddress");

            migrationBuilder.RenameColumn(
                name: "BuyerId",
                table: "DOSales",
                newName: "LocalBuyerId");

            migrationBuilder.RenameColumn(
                name: "BuyerCode",
                table: "DOSales",
                newName: "LocalBuyerCode");

            migrationBuilder.RenameColumn(
                name: "BuyerAddress",
                table: "DOSales",
                newName: "ExportRemark");

            migrationBuilder.AddColumn<string>(
                name: "ExportBuyerCode",
                table: "DOSales",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ExportBuyerId",
                table: "DOSales",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ExportBuyerName",
                table: "DOSales",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExportBuyerType",
                table: "DOSales",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ExportDate",
                table: "DOSales",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

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

            migrationBuilder.AddColumn<int>(
                name: "ExportSalesContractId",
                table: "DOSales",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ExportSalesContractNo",
                table: "DOSales",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExportType",
                table: "DOSales",
                maxLength: 255,
                nullable: true);
        }
    }
}
