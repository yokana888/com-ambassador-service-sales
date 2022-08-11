using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class AddApproveMDPPICInFPCC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApprovedMDBy",
                table: "FinishingPrintingCostCalculations",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedMDDate",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "ApprovedPPICBy",
                table: "FinishingPrintingCostCalculations",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedPPICDate",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<bool>(
                name: "IsApprovedMD",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApprovedPPIC",
                table: "FinishingPrintingCostCalculations",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedMDBy",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "ApprovedMDDate",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "ApprovedPPICBy",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "ApprovedPPICDate",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "IsApprovedMD",
                table: "FinishingPrintingCostCalculations");

            migrationBuilder.DropColumn(
                name: "IsApprovedPPIC",
                table: "FinishingPrintingCostCalculations");
        }
    }
}
