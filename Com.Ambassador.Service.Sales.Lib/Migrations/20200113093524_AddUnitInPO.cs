using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class AddUnitInPO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UnitCode",
                table: "ProductionOrder",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "ProductionOrder",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UnitName",
                table: "ProductionOrder",
                maxLength: 512,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitCode",
                table: "ProductionOrder");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "ProductionOrder");

            migrationBuilder.DropColumn(
                name: "UnitName",
                table: "ProductionOrder");
        }
    }
}
