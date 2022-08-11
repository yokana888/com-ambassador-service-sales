using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class Add_CostCalculation_Approval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApprovedIEBy",
                table: "CostCalculationGarments",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedIEDate",
                table: "CostCalculationGarments",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "ApprovedMDBy",
                table: "CostCalculationGarments",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedMDDate",
                table: "CostCalculationGarments",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "ApprovedPPICBy",
                table: "CostCalculationGarments",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedPPICDate",
                table: "CostCalculationGarments",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "ApprovedPurchasingBy",
                table: "CostCalculationGarments",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedPurchasingDate",
                table: "CostCalculationGarments",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<bool>(
                name: "IsApprovedIE",
                table: "CostCalculationGarments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApprovedMD",
                table: "CostCalculationGarments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApprovedPPIC",
                table: "CostCalculationGarments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApprovedPurchasing",
                table: "CostCalculationGarments",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedIEBy",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "ApprovedIEDate",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "ApprovedMDBy",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "ApprovedMDDate",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "ApprovedPPICBy",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "ApprovedPPICDate",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "ApprovedPurchasingBy",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "ApprovedPurchasingDate",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "IsApprovedIE",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "IsApprovedMD",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "IsApprovedPPIC",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "IsApprovedPurchasing",
                table: "CostCalculationGarments");
        }
    }
}
