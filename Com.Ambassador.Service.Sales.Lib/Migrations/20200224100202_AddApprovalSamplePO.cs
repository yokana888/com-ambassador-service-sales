using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class AddApprovalSamplePO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApprovedSampleBy",
                table: "ProductionOrder",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedSampleDate",
                table: "ProductionOrder",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<bool>(
                name: "IsApprovedSample",
                table: "ProductionOrder",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedSampleBy",
                table: "ProductionOrder");

            migrationBuilder.DropColumn(
                name: "ApprovedSampleDate",
                table: "ProductionOrder");

            migrationBuilder.DropColumn(
                name: "IsApprovedSample",
                table: "ProductionOrder");
        }
    }
}
