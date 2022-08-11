using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class SeparateSalesInvoiceLocalAndExport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalesInvoiceExports",
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
                    SalesInvoiceNo = table.Column<string>(maxLength: 255, nullable: true),
                    SalesInvoiceCategory = table.Column<string>(maxLength: 255, nullable: true),
                    SalesInvoiceType = table.Column<string>(maxLength: 255, nullable: true),
                    SalesInvoiceDate = table.Column<DateTimeOffset>(nullable: false),
                    FPType = table.Column<string>(maxLength: 255, nullable: true),
                    BuyerName = table.Column<string>(maxLength: 255, nullable: true),
                    BuyerAddress = table.Column<string>(maxLength: 1000, nullable: true),
                    Authorized = table.Column<string>(maxLength: 255, nullable: true),
                    ShippedPer = table.Column<string>(maxLength: 1000, nullable: true),
                    SailingDate = table.Column<DateTimeOffset>(nullable: false),
                    LetterOfCreditNumber = table.Column<string>(maxLength: 255, nullable: true),
                    LCDate = table.Column<DateTimeOffset>(nullable: false),
                    BankName = table.Column<string>(maxLength: 255, nullable: true),
                    From = table.Column<string>(maxLength: 255, nullable: true),
                    To = table.Column<string>(maxLength: 255, nullable: true),
                    HSCode = table.Column<string>(maxLength: 255, nullable: true),
                    TermOfPaymentType = table.Column<string>(maxLength: 255, nullable: true),
                    TermOfPaymentRemark = table.Column<string>(maxLength: 255, nullable: true),
                    Color = table.Column<string>(maxLength: 255, nullable: true),
                    OrderNo = table.Column<string>(maxLength: 255, nullable: true),
                    Indent = table.Column<string>(maxLength: 255, nullable: true),
                    QuantityLength = table.Column<double>(nullable: false),
                    CartonNo = table.Column<string>(maxLength: 255, nullable: true),
                    ShippingRemark = table.Column<string>(maxLength: 1000, nullable: true),
                    Remark = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesInvoiceExports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalesInvoiceExportDetails",
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
                    ShippingOutId = table.Column<int>(nullable: false),
                    BonNo = table.Column<string>(maxLength: 255, nullable: true),
                    WeightUom = table.Column<string>(maxLength: 255, nullable: true),
                    TotalUom = table.Column<string>(maxLength: 255, nullable: true),
                    GrossWeight = table.Column<double>(nullable: false),
                    NetWeight = table.Column<double>(nullable: false),
                    TotalMeas = table.Column<double>(nullable: false),
                    SalesInvoiceExportModelId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesInvoiceExportDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesInvoiceExportDetails_SalesInvoiceExports_SalesInvoiceExportModelId",
                        column: x => x.SalesInvoiceExportModelId,
                        principalTable: "SalesInvoiceExports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesInvoiceExportItems",
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
                    ProductId = table.Column<int>(nullable: false),
                    ProductCode = table.Column<string>(maxLength: 255, nullable: true),
                    ProductName = table.Column<string>(maxLength: 255, nullable: true),
                    QuantityPacking = table.Column<double>(maxLength: 255, nullable: false),
                    PackingUom = table.Column<string>(maxLength: 255, nullable: true),
                    ItemUom = table.Column<string>(maxLength: 255, nullable: true),
                    QuantityItem = table.Column<double>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    ConvertUnit = table.Column<string>(nullable: true),
                    ConvertValue = table.Column<double>(nullable: false),
                    SalesInvoiceExportDetailModelId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesInvoiceExportItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesInvoiceExportItems_SalesInvoiceExportDetails_SalesInvoiceExportDetailModelId",
                        column: x => x.SalesInvoiceExportDetailModelId,
                        principalTable: "SalesInvoiceExportDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoiceExportDetails_SalesInvoiceExportModelId",
                table: "SalesInvoiceExportDetails",
                column: "SalesInvoiceExportModelId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoiceExportItems_SalesInvoiceExportDetailModelId",
                table: "SalesInvoiceExportItems",
                column: "SalesInvoiceExportDetailModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesInvoiceExportItems");

            migrationBuilder.DropTable(
                name: "SalesInvoiceExportDetails");

            migrationBuilder.DropTable(
                name: "SalesInvoiceExports");
        }
    }
}
