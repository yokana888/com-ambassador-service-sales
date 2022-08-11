using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class AddApprovalMDInSPP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApprovedMDBy",
                table: "ProductionOrder",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedMDDate",
                table: "ProductionOrder",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<bool>(
                name: "IsApprovedMD",
                table: "ProductionOrder",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedMDBy",
                table: "ProductionOrder");

            migrationBuilder.DropColumn(
                name: "ApprovedMDDate",
                table: "ProductionOrder");

            migrationBuilder.DropColumn(
                name: "IsApprovedMD",
                table: "ProductionOrder");
        }
    }
}
