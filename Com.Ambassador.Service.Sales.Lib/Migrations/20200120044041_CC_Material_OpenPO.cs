using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class CC_Material_OpenPO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApprovedOpenPOMD",
                table: "CostCalculationGarment_Materials",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApprovedOpenPOPurchasing",
                table: "CostCalculationGarment_Materials",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOpenPO",
                table: "CostCalculationGarment_Materials",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApprovedOpenPOMD",
                table: "CostCalculationGarment_Materials");

            migrationBuilder.DropColumn(
                name: "IsApprovedOpenPOPurchasing",
                table: "CostCalculationGarment_Materials");

            migrationBuilder.DropColumn(
                name: "IsOpenPO",
                table: "CostCalculationGarment_Materials");
        }
    }
}
