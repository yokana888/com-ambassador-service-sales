using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class EnhanceSalesInvoiceExport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExportBuyerAddress",
                table: "SalesInvoices",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExportBuyerName",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FPType",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "From",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HSCode",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LCDate",
                table: "SalesInvoices",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "LetterOfCreditNumber",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TermOfPaymentRemark",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TermOfPaymentType",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "To",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankName",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "ExportBuyerAddress",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "ExportBuyerName",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "FPType",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "From",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "HSCode",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "LCDate",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "LetterOfCreditNumber",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "TermOfPaymentRemark",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "TermOfPaymentType",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "To",
                table: "SalesInvoices");
        }
    }
}
