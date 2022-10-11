using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class update_SC_ManyRO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GarmentSalesContractItems_GarmentSalesContracts_GSCId",
                table: "GarmentSalesContractItems");

            migrationBuilder.DropColumn(
                name: "Article",
                table: "GarmentSalesContracts");

            migrationBuilder.DropColumn(
                name: "ComodityCode",
                table: "GarmentSalesContracts");

            migrationBuilder.DropColumn(
                name: "ComodityId",
                table: "GarmentSalesContracts");

            migrationBuilder.DropColumn(
                name: "ComodityName",
                table: "GarmentSalesContracts");

            migrationBuilder.DropColumn(
                name: "CostCalculationId",
                table: "GarmentSalesContracts");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "GarmentSalesContracts");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "GarmentSalesContracts");

            migrationBuilder.DropColumn(
                name: "RONumber",
                table: "GarmentSalesContracts");

            migrationBuilder.DropColumn(
                name: "UomId",
                table: "GarmentSalesContracts");

            migrationBuilder.DropColumn(
                name: "UomUnit",
                table: "GarmentSalesContracts");

            migrationBuilder.RenameColumn(
                name: "GSCId",
                table: "GarmentSalesContractItems",
                newName: "SalesContractROId");

            migrationBuilder.RenameIndex(
                name: "IX_GarmentSalesContractItems_GSCId",
                table: "GarmentSalesContractItems",
                newName: "IX_GarmentSalesContractItems_SalesContractROId");

            migrationBuilder.AddColumn<string>(
                name: "SCType",
                table: "GarmentSalesContracts",
                maxLength: 20,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GarmentSalesContractROs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
                    CreatedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 255, nullable: false),
                    LastModifiedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 255, nullable: false),
                    DeletedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    UId = table.Column<string>(maxLength: 255, nullable: true),
                    CostCalculationId = table.Column<int>(nullable: false),
                    RONumber = table.Column<string>(maxLength: 255, nullable: true),
                    Article = table.Column<string>(maxLength: 1000, nullable: true),
                    ComodityId = table.Column<int>(nullable: false),
                    ComodityName = table.Column<string>(maxLength: 500, nullable: true),
                    ComodityCode = table.Column<string>(maxLength: 500, nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    UomId = table.Column<string>(nullable: true),
                    UomUnit = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 3000, nullable: true),
                    SalesContractId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentSalesContractROs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentSalesContractROs_GarmentSalesContracts_SalesContractId",
                        column: x => x.SalesContractId,
                        principalTable: "GarmentSalesContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GarmentSalesContractROs_SalesContractId",
                table: "GarmentSalesContractROs",
                column: "SalesContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_GarmentSalesContractItems_GarmentSalesContractROs_SalesContractROId",
                table: "GarmentSalesContractItems",
                column: "SalesContractROId",
                principalTable: "GarmentSalesContractROs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GarmentSalesContractItems_GarmentSalesContractROs_SalesContractROId",
                table: "GarmentSalesContractItems");

            migrationBuilder.DropTable(
                name: "GarmentSalesContractROs");

            migrationBuilder.DropColumn(
                name: "SCType",
                table: "GarmentSalesContracts");

            migrationBuilder.RenameColumn(
                name: "SalesContractROId",
                table: "GarmentSalesContractItems",
                newName: "GSCId");

            migrationBuilder.RenameIndex(
                name: "IX_GarmentSalesContractItems_SalesContractROId",
                table: "GarmentSalesContractItems",
                newName: "IX_GarmentSalesContractItems_GSCId");

            migrationBuilder.AddColumn<string>(
                name: "Article",
                table: "GarmentSalesContracts",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ComodityCode",
                table: "GarmentSalesContracts",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ComodityId",
                table: "GarmentSalesContracts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ComodityName",
                table: "GarmentSalesContracts",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CostCalculationId",
                table: "GarmentSalesContracts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "GarmentSalesContracts",
                maxLength: 3000,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "GarmentSalesContracts",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "RONumber",
                table: "GarmentSalesContracts",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UomId",
                table: "GarmentSalesContracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UomUnit",
                table: "GarmentSalesContracts",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GarmentSalesContractItems_GarmentSalesContracts_GSCId",
                table: "GarmentSalesContractItems",
                column: "GSCId",
                principalTable: "GarmentSalesContracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
