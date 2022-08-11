using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class AddFPPreSalesContractModule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinishingPrintingPreSalesContractModel",
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
                    No = table.Column<string>(maxLength: 64, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    Type = table.Column<string>(maxLength: 32, nullable: true),
                    BuyerId = table.Column<int>(nullable: false),
                    BuyerCode = table.Column<string>(maxLength: 128, nullable: true),
                    BuyerName = table.Column<string>(maxLength: 512, nullable: true),
                    UnitId = table.Column<int>(nullable: false),
                    UnitCode = table.Column<string>(maxLength: 128, nullable: true),
                    UnitName = table.Column<string>(maxLength: 512, nullable: true),
                    ProcessTypeId = table.Column<int>(nullable: false),
                    ProcessTypeCode = table.Column<string>(maxLength: 128, nullable: true),
                    ProcessTypeName = table.Column<string>(maxLength: 512, nullable: true),
                    OrderQuantity = table.Column<double>(nullable: false),
                    Remark = table.Column<string>(maxLength: 4096, nullable: true),
                    IsPosted = table.Column<bool>(nullable: false),
                    IsPO = table.Column<bool>(nullable: false),
                    IsSC = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinishingPrintingPreSalesContractModel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinishingPrintingPreSalesContractModel");
        }
    }
}
