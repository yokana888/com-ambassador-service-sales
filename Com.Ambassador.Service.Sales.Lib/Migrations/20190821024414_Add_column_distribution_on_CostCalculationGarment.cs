using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class Add_column_distribution_on_CostCalculationGarment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRODistributed",
                table: "CostCalculationGarments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RODistributionBy",
                table: "CostCalculationGarments",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "RODistributionDate",
                table: "CostCalculationGarments",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRODistributed",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "RODistributionBy",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "RODistributionDate",
                table: "CostCalculationGarments");
        }
    }
}
