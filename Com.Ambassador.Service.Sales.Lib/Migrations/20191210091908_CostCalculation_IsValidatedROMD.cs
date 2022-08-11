using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class CostCalculation_IsValidatedROMD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsValidatedROMD",
                table: "CostCalculationGarments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ValidationMDBy",
                table: "CostCalculationGarments",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ValidationMDDate",
                table: "CostCalculationGarments",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "ValidationSampleBy",
                table: "CostCalculationGarments",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ValidationSampleDate",
                table: "CostCalculationGarments",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsValidatedROMD",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "ValidationMDBy",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "ValidationMDDate",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "ValidationSampleBy",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "ValidationSampleDate",
                table: "CostCalculationGarments");
        }
    }
}
