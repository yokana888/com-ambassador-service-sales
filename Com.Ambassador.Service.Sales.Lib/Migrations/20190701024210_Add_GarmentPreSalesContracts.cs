using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class Add_GarmentPreSalesContracts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GarmentPreSalesContracts",
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
                    SCNo = table.Column<string>(maxLength: 255, nullable: true),
                    SCDate = table.Column<DateTimeOffset>(nullable: false),
                    SCType = table.Column<string>(nullable: true),
                    SectionId = table.Column<int>(nullable: false),
                    SectionCode = table.Column<string>(nullable: true),
                    BuyerAgentId = table.Column<int>(nullable: false),
                    BuyerAgentCode = table.Column<string>(nullable: true),
                    BuyerAgentName = table.Column<string>(nullable: true),
                    BuyerBrandId = table.Column<int>(nullable: false),
                    BuyerBrandCode = table.Column<string>(nullable: true),
                    BuyerBrandName = table.Column<string>(nullable: true),
                    OrderQuantity = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    IsCC = table.Column<bool>(nullable: false),
                    IsPR = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentPreSalesContracts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarmentPreSalesContracts");
        }
    }
}
