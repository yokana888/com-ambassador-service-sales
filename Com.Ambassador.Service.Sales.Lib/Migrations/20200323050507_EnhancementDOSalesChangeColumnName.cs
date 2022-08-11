using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class EnhancementDOSalesChangeColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalPacking",
                table: "DOSalesLocalItems",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "TotalMetric",
                table: "DOSalesLocalItems",
                newName: "Packing");

            migrationBuilder.RenameColumn(
                name: "TotalImperial",
                table: "DOSalesLocalItems",
                newName: "Length");

            migrationBuilder.RenameColumn(
                name: "MetricUom",
                table: "DOSales",
                newName: "WeightUom");

            migrationBuilder.RenameColumn(
                name: "ImperialUom",
                table: "DOSales",
                newName: "LengthUom");

            migrationBuilder.AddColumn<double>(
                name: "ConvertionValue",
                table: "DOSalesLocalItems",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConvertionValue",
                table: "DOSalesLocalItems");

            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "DOSalesLocalItems",
                newName: "TotalPacking");

            migrationBuilder.RenameColumn(
                name: "Packing",
                table: "DOSalesLocalItems",
                newName: "TotalMetric");

            migrationBuilder.RenameColumn(
                name: "Length",
                table: "DOSalesLocalItems",
                newName: "TotalImperial");

            migrationBuilder.RenameColumn(
                name: "WeightUom",
                table: "DOSales",
                newName: "MetricUom");

            migrationBuilder.RenameColumn(
                name: "LengthUom",
                table: "DOSales",
                newName: "ImperialUom");
        }
    }
}
