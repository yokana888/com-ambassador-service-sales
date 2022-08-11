using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class AddSalesInvoiceExport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "SailingDate",
                table: "SalesInvoices",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "SalesType",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippedPer",
                table: "SalesInvoices",
                maxLength: 1000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SailingDate",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "SalesType",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "ShippedPer",
                table: "SalesInvoices");
        }
    }
}
