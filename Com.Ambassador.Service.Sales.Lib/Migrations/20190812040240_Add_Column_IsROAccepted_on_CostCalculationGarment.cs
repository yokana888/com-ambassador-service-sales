using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class Add_Column_IsROAccepted_on_CostCalculationGarment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsROAccepted",
                table: "CostCalculationGarments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ROAcceptedBy",
                table: "CostCalculationGarments",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ROAcceptedDate",
                table: "CostCalculationGarments",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsROAccepted",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "ROAcceptedBy",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "ROAcceptedDate",
                table: "CostCalculationGarments");
        }
    }
}
