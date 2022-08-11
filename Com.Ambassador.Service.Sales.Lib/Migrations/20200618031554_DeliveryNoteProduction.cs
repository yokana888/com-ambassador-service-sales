using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class DeliveryNoteProduction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliveryNoteProduction",
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
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    SalesContractNo = table.Column<string>(maxLength: 255, nullable: true),
                    Unit = table.Column<string>(maxLength: 255, nullable: true),
                    Subject = table.Column<string>(maxLength: 255, nullable: true),
                    OtherSubject = table.Column<string>(maxLength: 255, nullable: true),
                    BuyerName = table.Column<string>(maxLength: 255, nullable: true),
                    BuyerType = table.Column<string>(maxLength: 255, nullable: true),
                    TypeandNumber = table.Column<string>(maxLength: 255, nullable: true),
                    Total = table.Column<double>(nullable: false),
                    Uom = table.Column<string>(maxLength: 255, nullable: true),
                    Blended = table.Column<string>(maxLength: 255, nullable: true),
                    DeliveredTo = table.Column<string>(maxLength: 255, nullable: true),
                    Month = table.Column<string>(maxLength: 255, nullable: true),
                    Year = table.Column<string>(maxLength: 255, nullable: true),
                    BallMark = table.Column<string>(maxLength: 255, nullable: true),
                    Sample = table.Column<string>(maxLength: 255, nullable: true),
                    Remark = table.Column<string>(maxLength: 255, nullable: true),
                    YarnSales = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryNoteProduction", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryNoteProduction");
        }
    }
}
