using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class update_SC_DeliveryDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "GarmentSalesContracts");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeliveryDate",
                table: "GarmentSalesContractROs",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "GarmentSalesContractROs");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeliveryDate",
                table: "GarmentSalesContracts",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
