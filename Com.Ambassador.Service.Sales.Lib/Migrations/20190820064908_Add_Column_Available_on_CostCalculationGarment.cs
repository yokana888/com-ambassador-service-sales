using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class Add_Column_Available_on_CostCalculationGarment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsROAvailable",
                table: "CostCalculationGarments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ROAvailableBy",
                table: "CostCalculationGarments",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ROAvailableDate",
                table: "CostCalculationGarments",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsROAvailable",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "ROAvailableBy",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "ROAvailableDate",
                table: "CostCalculationGarments");
        }
    }
}
