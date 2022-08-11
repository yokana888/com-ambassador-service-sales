using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class AddSPPCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProcessTypeSPPCode",
                table: "ProductionOrder",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProcessTypeUnit",
                table: "ProductionOrder",
                maxLength: 1000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcessTypeSPPCode",
                table: "ProductionOrder");

            migrationBuilder.DropColumn(
                name: "ProcessTypeUnit",
                table: "ProductionOrder");
        }
    }
}
