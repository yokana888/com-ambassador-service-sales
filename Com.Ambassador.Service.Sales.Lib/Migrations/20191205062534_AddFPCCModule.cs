using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class AddFPCCModule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinishingPrintingCostCalculations",
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
                    ProductionOrderNo = table.Column<string>(maxLength: 64, nullable: true),
                    InstructionId = table.Column<int>(nullable: false),
                    InstructionName = table.Column<string>(maxLength: 128, nullable: true),
                    BuyerName = table.Column<string>(maxLength: 128, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    GreigeId = table.Column<int>(nullable: false),
                    GreigeName = table.Column<string>(maxLength: 128, nullable: true),
                    PreparationValue = table.Column<double>(nullable: false),
                    CurrencyRate = table.Column<double>(nullable: false),
                    ProductionUnitValue = table.Column<double>(nullable: false),
                    TKLQuantity = table.Column<int>(nullable: false),
                    PreparationFabricWeight = table.Column<double>(nullable: false),
                    RFDFabricWeight = table.Column<double>(nullable: false),
                    ActualPrice = table.Column<double>(nullable: false),
                    CargoCost = table.Column<double>(nullable: false),
                    InsuranceCost = table.Column<double>(nullable: false),
                    Remark = table.Column<double>(maxLength: 2048, nullable: false),
                    ProductionOrderId = table.Column<int>(nullable: false),
                    Code = table.Column<string>(maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinishingPrintingCostCalculations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinishingPrintingCostCalculationMachines",
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
                    CostCalculationId = table.Column<long>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    MachineId = table.Column<int>(nullable: false),
                    StepProcessId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinishingPrintingCostCalculationMachines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinishingPrintingCostCalculationMachines_FinishingPrintingCostCalculations_CostCalculationId",
                        column: x => x.CostCalculationId,
                        principalTable: "FinishingPrintingCostCalculations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FinishingPrintingCostCalculationChemicals",
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
                    CostCalculationId = table.Column<long>(nullable: false),
                    CostCalculationMachineId = table.Column<long>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    ChemicalId = table.Column<int>(nullable: false),
                    ChemicalQuantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinishingPrintingCostCalculationChemicals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinishingPrintingCostCalculationChemicals_FinishingPrintingCostCalculationMachines_CostCalculationMachineId",
                        column: x => x.CostCalculationMachineId,
                        principalTable: "FinishingPrintingCostCalculationMachines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinishingPrintingCostCalculationChemicals_CostCalculationMachineId",
                table: "FinishingPrintingCostCalculationChemicals",
                column: "CostCalculationMachineId");

            migrationBuilder.CreateIndex(
                name: "IX_FinishingPrintingCostCalculationMachines_CostCalculationId",
                table: "FinishingPrintingCostCalculationMachines",
                column: "CostCalculationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinishingPrintingCostCalculationChemicals");

            migrationBuilder.DropTable(
                name: "FinishingPrintingCostCalculationMachines");

            migrationBuilder.DropTable(
                name: "FinishingPrintingCostCalculations");
        }
    }
}
