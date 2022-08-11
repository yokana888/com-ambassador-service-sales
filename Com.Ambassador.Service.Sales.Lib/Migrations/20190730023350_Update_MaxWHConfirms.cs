using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class Update_MaxWHConfirms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxValue",
                table: "MaxWHConfirms",
                newName: "UnitMaxValue");

            migrationBuilder.AddColumn<double>(
                name: "SKMaxValue",
                table: "MaxWHConfirms",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SKMaxValue",
                table: "MaxWHConfirms");

            migrationBuilder.RenameColumn(
                name: "UnitMaxValue",
                table: "MaxWHConfirms",
                newName: "MaxValue");
        }
    }
}
