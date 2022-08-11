using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class AutoIncreament : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AutoIncrementNumber",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "SalesInvoices");

            migrationBuilder.AlterColumn<string>(
                name: "Sc",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SalesInvoiceNo",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "SalesInvoices",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Op",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IDNo",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Disp",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryOrderNo",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DebtorIndexNo",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DOSalesNo",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BuyerName",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BuyerNPWP",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BuyerAddress",
                table: "SalesInvoices",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AutoIncreament",
                table: "SalesInvoices",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "SalesInvoiceType",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UnitName",
                table: "SalesInvoiceDetails",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UnitCode",
                table: "SalesInvoiceDetails",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Quantity",
                table: "SalesInvoiceDetails",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AutoIncreament",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "SalesInvoiceType",
                table: "SalesInvoices");

            migrationBuilder.AlterColumn<string>(
                name: "Sc",
                table: "SalesInvoices",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SalesInvoiceNo",
                table: "SalesInvoices",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "SalesInvoices",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Op",
                table: "SalesInvoices",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IDNo",
                table: "SalesInvoices",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Disp",
                table: "SalesInvoices",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryOrderNo",
                table: "SalesInvoices",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DebtorIndexNo",
                table: "SalesInvoices",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DOSalesNo",
                table: "SalesInvoices",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BuyerName",
                table: "SalesInvoices",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BuyerNPWP",
                table: "SalesInvoices",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BuyerAddress",
                table: "SalesInvoices",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AutoIncrementNumber",
                table: "SalesInvoices",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "SalesInvoices",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UnitName",
                table: "SalesInvoiceDetails",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UnitCode",
                table: "SalesInvoiceDetails",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Quantity",
                table: "SalesInvoiceDetails",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);
        }
    }
}
