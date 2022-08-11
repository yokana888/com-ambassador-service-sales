using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class CostCalculation_From_PRMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "POMaster",
                table: "CostCalculationGarment_Materials",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PRMasterId",
                table: "CostCalculationGarment_Materials",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PRMasterItemId",
                table: "CostCalculationGarment_Materials",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "POMaster",
                table: "CostCalculationGarment_Materials");

            migrationBuilder.DropColumn(
                name: "PRMasterId",
                table: "CostCalculationGarment_Materials");

            migrationBuilder.DropColumn(
                name: "PRMasterItemId",
                table: "CostCalculationGarment_Materials");
        }
    }
}
