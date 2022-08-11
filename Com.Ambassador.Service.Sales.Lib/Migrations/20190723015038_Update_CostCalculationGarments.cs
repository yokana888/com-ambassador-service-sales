using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class Update_CostCalculationGarments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsValidatedPPIC",
                table: "RO_Garments");

            migrationBuilder.DropColumn(
                name: "IsValidatedSample",
                table: "RO_Garments");

            migrationBuilder.AddColumn<bool>(
                name: "IsValidatedROPPIC",
                table: "CostCalculationGarments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsValidatedROSample",
                table: "CostCalculationGarments",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsValidatedROPPIC",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "IsValidatedROSample",
                table: "CostCalculationGarments");

            migrationBuilder.AddColumn<bool>(
                name: "IsValidatedPPIC",
                table: "RO_Garments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsValidatedSample",
                table: "RO_Garments",
                nullable: false,
                defaultValue: false);
        }
    }
}
