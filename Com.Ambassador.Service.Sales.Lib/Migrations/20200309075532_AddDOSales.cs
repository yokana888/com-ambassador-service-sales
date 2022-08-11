using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class AddDOSales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DebtorIndexNo",
                table: "SalesInvoices");

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "SalesReceipts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaidOff",
                table: "SalesInvoices",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "DOSalesModel",
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
                    Code = table.Column<string>(maxLength: 255, nullable: true),
                    AutoIncreament = table.Column<long>(nullable: false),
                    DOSalesNo = table.Column<string>(maxLength: 255, nullable: true),
                    DOSalesType = table.Column<string>(maxLength: 255, nullable: true),
                    Status = table.Column<string>(maxLength: 255, nullable: true),
                    Accepted = table.Column<bool>(nullable: false),
                    Declined = table.Column<bool>(nullable: false),
                    LocalType = table.Column<string>(maxLength: 255, nullable: true),
                    LocalDate = table.Column<DateTimeOffset>(nullable: false),
                    LocalSalesContractId = table.Column<int>(nullable: false),
                    LocalSalesContractNo = table.Column<string>(maxLength: 255, nullable: true),
                    LocalBuyerId = table.Column<long>(nullable: false),
                    LocalBuyerCode = table.Column<string>(maxLength: 255, nullable: true),
                    LocalBuyerName = table.Column<string>(maxLength: 1000, nullable: true),
                    LocalBuyerType = table.Column<string>(maxLength: 255, nullable: true),
                    DestinationBuyerName = table.Column<string>(maxLength: 255, nullable: true),
                    DestinationBuyerAddress = table.Column<string>(maxLength: 1000, nullable: true),
                    SalesName = table.Column<string>(maxLength: 255, nullable: true),
                    LocalHeadOfStorage = table.Column<string>(maxLength: 255, nullable: true),
                    PackingUom = table.Column<string>(maxLength: 255, nullable: true),
                    MetricUom = table.Column<string>(maxLength: 255, nullable: true),
                    ImperialUom = table.Column<string>(maxLength: 255, nullable: true),
                    Disp = table.Column<int>(nullable: false),
                    Op = table.Column<int>(nullable: false),
                    Sc = table.Column<int>(nullable: false),
                    LocalRemark = table.Column<string>(maxLength: 1000, nullable: true),
                    ExportType = table.Column<string>(maxLength: 255, nullable: true),
                    ExportDate = table.Column<DateTimeOffset>(nullable: false),
                    DoneBy = table.Column<string>(maxLength: 255, nullable: true),
                    ExportSalesContractId = table.Column<int>(nullable: false),
                    ExportSalesContractNo = table.Column<string>(maxLength: 255, nullable: true),
                    MaterialConstructionId = table.Column<int>(nullable: false),
                    MaterialConstructionCode = table.Column<string>(maxLength: 25, nullable: true),
                    MaterialConstructionName = table.Column<string>(maxLength: 255, nullable: true),
                    ExportBuyerId = table.Column<long>(nullable: false),
                    ExportBuyerCode = table.Column<string>(maxLength: 255, nullable: true),
                    ExportBuyerName = table.Column<string>(maxLength: 1000, nullable: true),
                    ExportBuyerType = table.Column<string>(maxLength: 255, nullable: true),
                    CommodityId = table.Column<int>(nullable: false),
                    CommodityCode = table.Column<string>(maxLength: 25, nullable: true),
                    CommodityName = table.Column<string>(maxLength: 255, nullable: true),
                    CommodityDescription = table.Column<string>(maxLength: 1000, nullable: true),
                    PieceLength = table.Column<string>(maxLength: 255, nullable: true),
                    OrderQuantity = table.Column<double>(nullable: false),
                    FillEachBale = table.Column<double>(nullable: false),
                    ExportRemark = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOSalesModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DOSalesLocalModel",
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
                    MaterialConstructionId = table.Column<long>(nullable: false),
                    MaterialConstructionName = table.Column<string>(maxLength: 1000, nullable: true),
                    MaterialConstructionCode = table.Column<string>(maxLength: 255, nullable: true),
                    MaterialConstructionRemark = table.Column<string>(nullable: true),
                    UnitId = table.Column<int>(nullable: false),
                    UnitCode = table.Column<string>(maxLength: 128, nullable: true),
                    UnitName = table.Column<string>(maxLength: 512, nullable: true),
                    TotalPacking = table.Column<double>(nullable: false),
                    TotalImperial = table.Column<double>(nullable: false),
                    TotalMetric = table.Column<double>(nullable: false),
                    DOSalesId = table.Column<int>(nullable: false),
                    DOSalesModelId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOSalesLocalModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DOSalesLocalModel_DOSalesModel_DOSalesModelId",
                        column: x => x.DOSalesModelId,
                        principalTable: "DOSalesModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DOSalesLocalModel_DOSalesModelId",
                table: "DOSalesLocalModel",
                column: "DOSalesModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DOSalesLocalModel");

            migrationBuilder.DropTable(
                name: "DOSalesModel");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "SalesReceipts");

            migrationBuilder.DropColumn(
                name: "IsPaidOff",
                table: "SalesInvoices");

            migrationBuilder.AddColumn<string>(
                name: "DebtorIndexNo",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true);
        }
    }
}
