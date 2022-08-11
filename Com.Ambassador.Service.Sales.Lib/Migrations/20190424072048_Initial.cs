using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Sales.Lib.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticleColors",
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
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleColors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CostCalculationGarments",
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
                    Code = table.Column<string>(nullable: true),
                    RO_Number = table.Column<string>(nullable: true),
                    Article = table.Column<string>(nullable: true),
                    ComodityID = table.Column<string>(nullable: true),
                    ComodityCode = table.Column<string>(nullable: true),
                    Commodity = table.Column<string>(nullable: true),
                    CommodityDescription = table.Column<string>(nullable: true),
                    FabricAllowance = table.Column<double>(nullable: false),
                    AccessoriesAllowance = table.Column<double>(nullable: false),
                    Section = table.Column<string>(nullable: true),
                    UOMID = table.Column<string>(nullable: true),
                    UOMCode = table.Column<string>(nullable: true),
                    UOMUnit = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    SizeRange = table.Column<string>(nullable: true),
                    DeliveryDate = table.Column<DateTimeOffset>(nullable: false),
                    ConfirmDate = table.Column<DateTimeOffset>(nullable: false),
                    LeadTime = table.Column<int>(nullable: false),
                    SMV_Cutting = table.Column<double>(nullable: false),
                    SMV_Sewing = table.Column<double>(nullable: false),
                    SMV_Finishing = table.Column<double>(nullable: false),
                    SMV_Total = table.Column<double>(nullable: false),
                    BuyerId = table.Column<string>(nullable: true),
                    BuyerCode = table.Column<string>(nullable: true),
                    BuyerName = table.Column<string>(nullable: true),
                    EfficiencyId = table.Column<int>(nullable: false),
                    EfficiencyValue = table.Column<double>(nullable: false),
                    Index = table.Column<double>(nullable: false),
                    WageId = table.Column<int>(nullable: false),
                    WageRate = table.Column<double>(nullable: false),
                    THRId = table.Column<int>(nullable: false),
                    THRRate = table.Column<double>(nullable: false),
                    ConfirmPrice = table.Column<double>(nullable: false),
                    RateId = table.Column<int>(nullable: false),
                    RateValue = table.Column<double>(nullable: false),
                    Freight = table.Column<double>(nullable: false),
                    Insurance = table.Column<double>(nullable: false),
                    CommissionPortion = table.Column<double>(nullable: false),
                    CommissionRate = table.Column<double>(nullable: false),
                    OTL1Id = table.Column<int>(nullable: false),
                    OTL1Rate = table.Column<double>(nullable: false),
                    OTL1CalculatedRate = table.Column<double>(nullable: false),
                    OTL2Id = table.Column<int>(nullable: false),
                    OTL2Rate = table.Column<double>(nullable: false),
                    OTL2CalculatedRate = table.Column<double>(nullable: false),
                    Risk = table.Column<double>(nullable: false),
                    ProductionCost = table.Column<double>(nullable: false),
                    NETFOB = table.Column<double>(nullable: false),
                    FreightCost = table.Column<double>(nullable: false),
                    NETFOBP = table.Column<double>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ImageFile = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    RO_GarmentId = table.Column<int>(nullable: true),
                    UnitId = table.Column<int>(nullable: false),
                    UnitCode = table.Column<string>(nullable: true),
                    UnitName = table.Column<string>(nullable: true),
                    AutoIncrementNumber = table.Column<int>(nullable: false),
                    BuyerBrandId = table.Column<int>(nullable: false),
                    BuyerBrandCode = table.Column<string>(nullable: true),
                    BuyerBrandName = table.Column<string>(nullable: true),
                    SCGarmentId = table.Column<long>(nullable: true),
                    IsValidated = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCalculationGarments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Efficiencies",
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
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    InitialRange = table.Column<int>(nullable: false),
                    FinalRange = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Efficiencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinishingPrintingSalesContracts",
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
                    AccountBankAccountName = table.Column<string>(nullable: true),
                    AccountBankID = table.Column<int>(nullable: false),
                    AccountBankCode = table.Column<string>(maxLength: 25, nullable: true),
                    AccountBankName = table.Column<string>(maxLength: 255, nullable: true),
                    AccountBankNumber = table.Column<string>(maxLength: 255, nullable: true),
                    AccountBankCurrencyID = table.Column<int>(maxLength: 255, nullable: false),
                    AccountBankCurrencyCode = table.Column<string>(maxLength: 25, nullable: true),
                    AccountBankCurrencySymbol = table.Column<string>(maxLength: 25, nullable: true),
                    AccountBankCurrencyRate = table.Column<double>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    AgentID = table.Column<int>(nullable: false),
                    AgentCode = table.Column<string>(maxLength: 25, nullable: true),
                    AgentName = table.Column<string>(maxLength: 255, nullable: true),
                    AutoIncrementNumber = table.Column<int>(nullable: false),
                    BuyerID = table.Column<int>(nullable: false),
                    BuyerCode = table.Column<string>(maxLength: 25, nullable: true),
                    BuyerName = table.Column<string>(maxLength: 1000, nullable: true),
                    BuyerType = table.Column<string>(maxLength: 255, nullable: true),
                    Code = table.Column<string>(maxLength: 25, nullable: true),
                    Commission = table.Column<string>(maxLength: 255, nullable: true),
                    CommodityID = table.Column<int>(nullable: false),
                    CommodityCode = table.Column<string>(maxLength: 25, nullable: true),
                    CommodityName = table.Column<string>(maxLength: 255, nullable: true),
                    CommodityDescription = table.Column<string>(maxLength: 1000, nullable: true),
                    Condition = table.Column<string>(maxLength: 1000, nullable: true),
                    DeliveredTo = table.Column<string>(maxLength: 1000, nullable: true),
                    DeliverySchedule = table.Column<DateTimeOffset>(nullable: false),
                    DesignMotiveID = table.Column<int>(nullable: false),
                    DesignMotiveCode = table.Column<string>(maxLength: 25, nullable: true),
                    DesignMotiveName = table.Column<string>(maxLength: 255, nullable: true),
                    DispositionNumber = table.Column<string>(maxLength: 255, nullable: true),
                    FromStock = table.Column<bool>(nullable: false),
                    MaterialID = table.Column<int>(nullable: false),
                    MaterialCode = table.Column<string>(maxLength: 25, nullable: true),
                    MaterialName = table.Column<string>(maxLength: 255, nullable: true),
                    MaterialConstructionId = table.Column<int>(nullable: false),
                    MaterialConstructionCode = table.Column<string>(maxLength: 25, nullable: true),
                    MaterialConstructionName = table.Column<string>(maxLength: 255, nullable: true),
                    MaterialWidth = table.Column<string>(maxLength: 255, nullable: true),
                    OrderQuantity = table.Column<double>(nullable: false),
                    OrderTypeID = table.Column<int>(nullable: false),
                    OrderTypeCode = table.Column<string>(maxLength: 25, nullable: true),
                    OrderTypeName = table.Column<string>(maxLength: 255, nullable: true),
                    Packing = table.Column<string>(maxLength: 1000, nullable: true),
                    PieceLength = table.Column<string>(maxLength: 255, nullable: true),
                    PointLimit = table.Column<double>(nullable: false),
                    PointSystem = table.Column<int>(nullable: false),
                    QualityID = table.Column<int>(nullable: false),
                    QualityCode = table.Column<string>(maxLength: 25, nullable: true),
                    QualityName = table.Column<string>(maxLength: 255, nullable: true),
                    SalesContractNo = table.Column<string>(maxLength: 25, nullable: true),
                    ShipmentDescription = table.Column<string>(maxLength: 1000, nullable: true),
                    ShippingQuantityTolerance = table.Column<double>(maxLength: 1000, nullable: false),
                    TermOfPaymentID = table.Column<int>(nullable: false),
                    TermOfPaymentCode = table.Column<string>(maxLength: 25, nullable: true),
                    TermOfPaymentName = table.Column<string>(maxLength: 255, nullable: true),
                    TermOfPaymentIsExport = table.Column<bool>(nullable: false),
                    TermOfShipment = table.Column<string>(maxLength: 1000, nullable: true),
                    TransportFee = table.Column<string>(maxLength: 1000, nullable: true),
                    UseIncomeTax = table.Column<bool>(nullable: false),
                    RemainingQuantity = table.Column<double>(nullable: false),
                    UOMID = table.Column<int>(nullable: false),
                    UOMUnit = table.Column<string>(maxLength: 255, nullable: true),
                    YarnMaterialID = table.Column<int>(nullable: false),
                    YarnMaterialCode = table.Column<string>(maxLength: 25, nullable: true),
                    YarnMaterialName = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinishingPrintingSalesContracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarmentBookingOrders",
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
                    BookingOrderNo = table.Column<string>(nullable: true),
                    BookingOrderDate = table.Column<DateTimeOffset>(nullable: false),
                    DeliveryDate = table.Column<DateTimeOffset>(nullable: false),
                    BuyerId = table.Column<long>(nullable: false),
                    BuyerCode = table.Column<string>(nullable: true),
                    BuyerName = table.Column<string>(nullable: true),
                    SectionId = table.Column<long>(nullable: false),
                    SectionCode = table.Column<string>(nullable: true),
                    SectionName = table.Column<string>(nullable: true),
                    OrderQuantity = table.Column<double>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    IsBlockingPlan = table.Column<bool>(nullable: false),
                    IsCanceled = table.Column<bool>(nullable: false),
                    CanceledDate = table.Column<DateTimeOffset>(nullable: true),
                    CanceledQuantity = table.Column<double>(nullable: false),
                    ExpiredBookingDate = table.Column<DateTimeOffset>(nullable: true),
                    ExpiredBookingQuantity = table.Column<double>(nullable: false),
                    ConfirmedQuantity = table.Column<double>(nullable: false),
                    HadConfirmed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentBookingOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarmentSalesContracts",
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
                    SalesContractNo = table.Column<string>(maxLength: 255, nullable: true),
                    CostCalculationId = table.Column<int>(nullable: false),
                    RONumber = table.Column<string>(maxLength: 255, nullable: true),
                    BuyerBrandId = table.Column<int>(nullable: false),
                    BuyerBrandCode = table.Column<string>(nullable: true),
                    BuyerBrandName = table.Column<string>(nullable: true),
                    ComodityId = table.Column<int>(nullable: false),
                    ComodityName = table.Column<string>(maxLength: 500, nullable: true),
                    ComodityCode = table.Column<string>(maxLength: 500, nullable: true),
                    Packing = table.Column<string>(maxLength: 1000, nullable: true),
                    Article = table.Column<string>(maxLength: 1000, nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    UomId = table.Column<string>(nullable: true),
                    UomUnit = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 3000, nullable: true),
                    Material = table.Column<string>(maxLength: 3000, nullable: true),
                    DocPresented = table.Column<string>(maxLength: 3000, nullable: true),
                    FOB = table.Column<string>(maxLength: 3000, nullable: true),
                    Price = table.Column<double>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    DeliveryDate = table.Column<DateTimeOffset>(nullable: false),
                    Delivery = table.Column<string>(maxLength: 255, nullable: true),
                    Country = table.Column<string>(maxLength: 255, nullable: true),
                    NoHS = table.Column<string>(maxLength: 3000, nullable: true),
                    IsMaterial = table.Column<bool>(nullable: false),
                    IsTrimming = table.Column<bool>(nullable: false),
                    IsEmbrodiary = table.Column<bool>(nullable: false),
                    IsPrinted = table.Column<bool>(nullable: false),
                    IsWash = table.Column<bool>(nullable: false),
                    IsTTPayment = table.Column<bool>(nullable: false),
                    PaymentDetail = table.Column<string>(nullable: true),
                    AccountBankId = table.Column<long>(nullable: false),
                    AccountBankName = table.Column<string>(maxLength: 500, nullable: true),
                    AccountName = table.Column<string>(maxLength: 500, nullable: true),
                    DocPrinted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentSalesContracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarmentSewingBlockingPlans",
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
                    BookingOrderId = table.Column<long>(nullable: false),
                    BookingOrderNo = table.Column<string>(maxLength: 255, nullable: true),
                    BookingOrderDate = table.Column<DateTimeOffset>(nullable: false),
                    DeliveryDate = table.Column<DateTimeOffset>(nullable: false),
                    BuyerId = table.Column<long>(nullable: false),
                    BuyerCode = table.Column<string>(maxLength: 255, nullable: true),
                    BuyerName = table.Column<string>(maxLength: 255, nullable: true),
                    OrderQuantity = table.Column<double>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    BookingItems = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentSewingBlockingPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarmentWeeklyPlans",
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
                    Year = table.Column<short>(nullable: false),
                    UnitId = table.Column<long>(nullable: false),
                    UnitCode = table.Column<string>(maxLength: 255, nullable: true),
                    UnitName = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentWeeklyPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductionOrder",
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
                    OrderNo = table.Column<string>(maxLength: 255, nullable: true),
                    OrderQuantity = table.Column<double>(nullable: false),
                    ShippingQuantityTolerance = table.Column<double>(nullable: false),
                    MaterialOrigin = table.Column<string>(nullable: true),
                    FinishWidth = table.Column<string>(maxLength: 255, nullable: true),
                    DesignNumber = table.Column<string>(maxLength: 255, nullable: true),
                    DesignCode = table.Column<string>(nullable: true),
                    HandlingStandard = table.Column<string>(maxLength: 255, nullable: true),
                    Run = table.Column<string>(maxLength: 255, nullable: true),
                    ShrinkageStandard = table.Column<string>(maxLength: 255, nullable: true),
                    ArticleFabricEdge = table.Column<string>(nullable: true),
                    MaterialWidth = table.Column<string>(maxLength: 1000, nullable: true),
                    PackingInstruction = table.Column<string>(maxLength: 1000, nullable: true),
                    Sample = table.Column<string>(maxLength: 1000, nullable: true),
                    DeliveryDate = table.Column<DateTimeOffset>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    DistributedQuantity = table.Column<double>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    IsClosed = table.Column<bool>(nullable: false),
                    IsRequested = table.Column<bool>(nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false),
                    AutoIncreament = table.Column<long>(nullable: false),
                    SalesContractId = table.Column<long>(nullable: false),
                    SalesContractNo = table.Column<string>(maxLength: 255, nullable: true),
                    YarnMaterialId = table.Column<long>(nullable: false),
                    YarnMaterialName = table.Column<string>(maxLength: 1000, nullable: true),
                    YarnMaterialCode = table.Column<string>(maxLength: 255, nullable: true),
                    YarnMaterialRemark = table.Column<string>(maxLength: 1000, nullable: true),
                    BuyerId = table.Column<long>(nullable: false),
                    BuyerCode = table.Column<string>(maxLength: 255, nullable: true),
                    BuyerName = table.Column<string>(maxLength: 1000, nullable: true),
                    BuyerType = table.Column<string>(maxLength: 255, nullable: true),
                    ProcessTypeId = table.Column<long>(nullable: false),
                    ProcessTypeCode = table.Column<string>(maxLength: 255, nullable: true),
                    ProcessTypeName = table.Column<string>(maxLength: 1000, nullable: true),
                    ProcessTypeRemark = table.Column<string>(maxLength: 1000, nullable: true),
                    OrderTypeId = table.Column<long>(nullable: false),
                    OrderTypeCode = table.Column<string>(maxLength: 255, nullable: true),
                    OrderTypeName = table.Column<string>(maxLength: 1000, nullable: true),
                    OrderTypeRemark = table.Column<string>(maxLength: 1000, nullable: true),
                    MaterialId = table.Column<long>(nullable: false),
                    MaterialCode = table.Column<string>(maxLength: 255, nullable: true),
                    MaterialName = table.Column<string>(maxLength: 1000, nullable: true),
                    MaterialPrice = table.Column<double>(nullable: false),
                    MaterialTags = table.Column<string>(maxLength: 255, nullable: true),
                    DesignMotiveID = table.Column<int>(nullable: false),
                    DesignMotiveCode = table.Column<string>(maxLength: 25, nullable: true),
                    DesignMotiveName = table.Column<string>(maxLength: 255, nullable: true),
                    UomId = table.Column<long>(nullable: false),
                    UomUnit = table.Column<string>(maxLength: 255, nullable: true),
                    MaterialConstructionId = table.Column<long>(nullable: false),
                    MaterialConstructionName = table.Column<string>(maxLength: 1000, nullable: true),
                    MaterialConstructionCode = table.Column<string>(maxLength: 255, nullable: true),
                    MaterialConstructionRemark = table.Column<string>(nullable: true),
                    FinishTypeId = table.Column<long>(nullable: false),
                    FinishTypeCode = table.Column<string>(maxLength: 255, nullable: true),
                    FinishTypeName = table.Column<string>(maxLength: 1000, nullable: true),
                    FinishTypeRemark = table.Column<string>(maxLength: 1000, nullable: true),
                    StandardTestId = table.Column<long>(nullable: false),
                    StandardTestCode = table.Column<string>(maxLength: 255, nullable: true),
                    StandardTestName = table.Column<string>(maxLength: 1000, nullable: true),
                    StandardTestRemark = table.Column<string>(maxLength: 1000, nullable: true),
                    AccountId = table.Column<long>(nullable: false),
                    AccountUserName = table.Column<string>(nullable: true),
                    ProfileFirstName = table.Column<string>(maxLength: 1000, nullable: true),
                    ProfileLastName = table.Column<string>(maxLength: 1000, nullable: true),
                    ProfileGender = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionOrder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rates",
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
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpinningSalesContract",
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
                    SalesContractNo = table.Column<string>(maxLength: 255, nullable: true),
                    DispositionNumber = table.Column<string>(maxLength: 255, nullable: true),
                    FromStock = table.Column<bool>(nullable: false),
                    OrderQuantity = table.Column<double>(nullable: false),
                    ShippingQuantityTolerance = table.Column<double>(nullable: false),
                    ComodityDescription = table.Column<string>(nullable: true),
                    IncomeTax = table.Column<string>(maxLength: 255, nullable: true),
                    TermOfShipment = table.Column<string>(maxLength: 1000, nullable: true),
                    TransportFee = table.Column<string>(maxLength: 1000, nullable: true),
                    Packing = table.Column<string>(maxLength: 1000, nullable: true),
                    DeliveredTo = table.Column<string>(maxLength: 1000, nullable: true),
                    Price = table.Column<double>(nullable: false),
                    Comission = table.Column<string>(maxLength: 1000, nullable: true),
                    DeliverySchedule = table.Column<DateTimeOffset>(nullable: false),
                    ShipmentDescription = table.Column<string>(nullable: true),
                    Condition = table.Column<string>(maxLength: 1000, nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    PieceLength = table.Column<string>(maxLength: 1000, nullable: true),
                    AutoIncrementNumber = table.Column<int>(nullable: false),
                    BuyerId = table.Column<long>(nullable: false),
                    BuyerCode = table.Column<string>(maxLength: 255, nullable: true),
                    BuyerName = table.Column<string>(maxLength: 1000, nullable: true),
                    BuyerType = table.Column<string>(maxLength: 255, nullable: true),
                    ComodityId = table.Column<long>(nullable: false),
                    ComodityCode = table.Column<string>(maxLength: 255, nullable: true),
                    ComodityName = table.Column<string>(maxLength: 1000, nullable: true),
                    ComodityType = table.Column<string>(maxLength: 255, nullable: true),
                    QualityId = table.Column<long>(nullable: false),
                    QualityCode = table.Column<string>(maxLength: 255, nullable: true),
                    QualityName = table.Column<string>(maxLength: 1000, nullable: true),
                    TermOfPaymentId = table.Column<long>(nullable: false),
                    TermOfPaymentCode = table.Column<string>(maxLength: 255, nullable: true),
                    TermOfPaymentName = table.Column<string>(maxLength: 1000, nullable: true),
                    TermOfPaymentIsExport = table.Column<bool>(nullable: false),
                    AccountBankId = table.Column<long>(nullable: false),
                    AccountBankCode = table.Column<string>(maxLength: 255, nullable: true),
                    AccountBankName = table.Column<string>(maxLength: 1000, nullable: true),
                    AccountBankNumber = table.Column<string>(nullable: true),
                    BankName = table.Column<string>(maxLength: 255, nullable: true),
                    AccountCurrencyId = table.Column<string>(maxLength: 255, nullable: true),
                    AccountCurrencyCode = table.Column<string>(maxLength: 255, nullable: true),
                    AgentId = table.Column<long>(nullable: false),
                    AgentName = table.Column<string>(maxLength: 1000, nullable: true),
                    AgentCode = table.Column<string>(maxLength: 255, nullable: true),
                    UomUnit = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpinningSalesContract", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeavingSalesContract",
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
                    SalesContractNo = table.Column<string>(maxLength: 255, nullable: true),
                    DispositionNumber = table.Column<string>(maxLength: 255, nullable: true),
                    FromStock = table.Column<bool>(nullable: false),
                    MaterialWidth = table.Column<string>(maxLength: 255, nullable: true),
                    OrderQuantity = table.Column<double>(nullable: false),
                    ShippingQuantityTolerance = table.Column<double>(nullable: false),
                    ComodityDescription = table.Column<string>(nullable: true),
                    IncomeTax = table.Column<string>(maxLength: 255, nullable: true),
                    TermOfShipment = table.Column<string>(maxLength: 1000, nullable: true),
                    TransportFee = table.Column<string>(maxLength: 1000, nullable: true),
                    Packing = table.Column<string>(maxLength: 1000, nullable: true),
                    DeliveredTo = table.Column<string>(maxLength: 1000, nullable: true),
                    Price = table.Column<double>(nullable: false),
                    Comission = table.Column<string>(maxLength: 1000, nullable: true),
                    DeliverySchedule = table.Column<DateTimeOffset>(nullable: false),
                    ShipmentDescription = table.Column<string>(nullable: true),
                    Condition = table.Column<string>(maxLength: 1000, nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    PieceLength = table.Column<string>(maxLength: 1000, nullable: true),
                    AutoIncrementNumber = table.Column<int>(nullable: false),
                    BuyerId = table.Column<long>(nullable: false),
                    BuyerCode = table.Column<string>(maxLength: 255, nullable: true),
                    BuyerName = table.Column<string>(maxLength: 1000, nullable: true),
                    BuyerType = table.Column<string>(maxLength: 255, nullable: true),
                    ProductId = table.Column<long>(nullable: false),
                    ProductCode = table.Column<string>(maxLength: 255, nullable: true),
                    ProductName = table.Column<string>(maxLength: 1000, nullable: true),
                    ProductPrice = table.Column<double>(nullable: false),
                    ProductTags = table.Column<string>(maxLength: 255, nullable: true),
                    UomId = table.Column<long>(nullable: false),
                    UomUnit = table.Column<string>(maxLength: 255, nullable: true),
                    MaterialConstructionId = table.Column<long>(nullable: false),
                    MaterialConstructionName = table.Column<string>(maxLength: 1000, nullable: true),
                    MaterialConstructionCode = table.Column<string>(maxLength: 255, nullable: true),
                    MaterialConstructionRemark = table.Column<string>(nullable: true),
                    YarnMaterialId = table.Column<long>(nullable: false),
                    YarnMaterialName = table.Column<string>(maxLength: 1000, nullable: true),
                    YarnMaterialCode = table.Column<string>(maxLength: 255, nullable: true),
                    YarnMaterialRemark = table.Column<string>(nullable: true),
                    ComodityId = table.Column<long>(nullable: false),
                    ComodityCode = table.Column<string>(maxLength: 255, nullable: true),
                    ComodityName = table.Column<string>(maxLength: 1000, nullable: true),
                    ComodityType = table.Column<string>(maxLength: 255, nullable: true),
                    QualityId = table.Column<long>(nullable: false),
                    QualityCode = table.Column<string>(maxLength: 255, nullable: true),
                    QualityName = table.Column<string>(maxLength: 1000, nullable: true),
                    TermOfPaymentId = table.Column<long>(nullable: false),
                    TermOfPaymentCode = table.Column<string>(maxLength: 255, nullable: true),
                    TermOfPaymentName = table.Column<string>(maxLength: 1000, nullable: true),
                    TermOfPaymentIsExport = table.Column<bool>(nullable: false),
                    AccountBankId = table.Column<long>(nullable: false),
                    AccountBankCode = table.Column<string>(maxLength: 255, nullable: true),
                    AccountBankName = table.Column<string>(maxLength: 1000, nullable: true),
                    AccountBankNumber = table.Column<string>(nullable: true),
                    BankName = table.Column<string>(maxLength: 255, nullable: true),
                    AccountCurrencyId = table.Column<string>(maxLength: 255, nullable: true),
                    AccountCurrencyCode = table.Column<string>(maxLength: 255, nullable: true),
                    AgentId = table.Column<long>(nullable: false),
                    AgentName = table.Column<string>(maxLength: 1000, nullable: true),
                    AgentCode = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeavingSalesContract", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CostCalculationGarment_Materials",
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
                    CostCalculationGarmentId = table.Column<long>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    PO_SerialNumber = table.Column<string>(nullable: true),
                    PO = table.Column<string>(nullable: true),
                    CategoryId = table.Column<string>(nullable: true),
                    CategoryCode = table.Column<string>(nullable: true),
                    CategoryName = table.Column<string>(nullable: true),
                    AutoIncrementNumber = table.Column<int>(nullable: false),
                    ProductId = table.Column<string>(nullable: true),
                    ProductCode = table.Column<string>(nullable: true),
                    Composition = table.Column<string>(nullable: true),
                    Construction = table.Column<string>(nullable: true),
                    Yarn = table.Column<string>(nullable: true),
                    Width = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    UOMQuantityId = table.Column<string>(nullable: true),
                    UOMQuantityName = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    UOMPriceId = table.Column<string>(nullable: true),
                    UOMPriceName = table.Column<string>(nullable: true),
                    Conversion = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    isFabricCM = table.Column<bool>(nullable: false),
                    CM_Price = table.Column<double>(nullable: true),
                    ShippingFeePortion = table.Column<double>(nullable: false),
                    TotalShippingFee = table.Column<double>(nullable: false),
                    BudgetQuantity = table.Column<double>(nullable: false),
                    Information = table.Column<string>(nullable: true),
                    IsPosted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCalculationGarment_Materials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostCalculationGarment_Materials_CostCalculationGarments_CostCalculationGarmentId",
                        column: x => x.CostCalculationGarmentId,
                        principalTable: "CostCalculationGarments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RO_Garments",
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
                    CostCalculationGarmentId = table.Column<long>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Instruction = table.Column<string>(nullable: true),
                    Total = table.Column<int>(nullable: false),
                    ImagesPath = table.Column<string>(nullable: true),
                    ImagesName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RO_Garments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RO_Garments_CostCalculationGarments_CostCalculationGarmentId",
                        column: x => x.CostCalculationGarmentId,
                        principalTable: "CostCalculationGarments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FinishingPrintingSalesContractDetails",
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
                    FinishingPrintingSalesContractId = table.Column<long>(nullable: true),
                    Color = table.Column<string>(maxLength: 255, nullable: true),
                    CurrencyID = table.Column<int>(nullable: false),
                    CurrencyCode = table.Column<string>(maxLength: 25, nullable: true),
                    CurrencySymbol = table.Column<string>(maxLength: 25, nullable: true),
                    CurrencyRate = table.Column<double>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    UseIncomeTax = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinishingPrintingSalesContractDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinishingPrintingSalesContractDetails_FinishingPrintingSalesContracts_FinishingPrintingSalesContractId",
                        column: x => x.FinishingPrintingSalesContractId,
                        principalTable: "FinishingPrintingSalesContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GarmentBookingOrderItems",
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
                    BookingOrderId = table.Column<long>(nullable: false),
                    ComodityId = table.Column<long>(nullable: false),
                    ComodityCode = table.Column<string>(nullable: true),
                    ComodityName = table.Column<string>(nullable: true),
                    ConfirmQuantity = table.Column<double>(nullable: false),
                    DeliveryDate = table.Column<DateTimeOffset>(nullable: false),
                    ConfirmDate = table.Column<DateTimeOffset>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    IsCanceled = table.Column<bool>(nullable: false),
                    CanceledDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentBookingOrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentBookingOrderItems_GarmentBookingOrders_BookingOrderId",
                        column: x => x.BookingOrderId,
                        principalTable: "GarmentBookingOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GarmentSalesContractItems",
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
                    Description = table.Column<string>(maxLength: 3000, nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    GSCId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentSalesContractItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentSalesContractItems_GarmentSalesContracts_GSCId",
                        column: x => x.GSCId,
                        principalTable: "GarmentSalesContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GarmentSewingBlockingPlanItems",
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
                    BlockingPlanId = table.Column<long>(nullable: false),
                    IsConfirm = table.Column<bool>(nullable: false),
                    ComodityId = table.Column<long>(nullable: false),
                    ComodityCode = table.Column<string>(maxLength: 255, nullable: true),
                    ComodityName = table.Column<string>(maxLength: 500, nullable: true),
                    SMVSewing = table.Column<double>(nullable: false),
                    WeeklyPlanId = table.Column<long>(nullable: false),
                    UnitId = table.Column<long>(nullable: false),
                    UnitCode = table.Column<string>(maxLength: 255, nullable: true),
                    UnitName = table.Column<string>(maxLength: 255, nullable: true),
                    Year = table.Column<short>(nullable: false),
                    WeeklyPlanItemId = table.Column<long>(nullable: false),
                    WeekNumber = table.Column<byte>(nullable: false),
                    StartDate = table.Column<DateTimeOffset>(nullable: false),
                    EndDate = table.Column<DateTimeOffset>(nullable: false),
                    OrderQuantity = table.Column<double>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    DeliveryDate = table.Column<DateTimeOffset>(nullable: false),
                    Efficiency = table.Column<double>(nullable: false),
                    EHBooking = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentSewingBlockingPlanItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentSewingBlockingPlanItems_GarmentSewingBlockingPlans_BlockingPlanId",
                        column: x => x.BlockingPlanId,
                        principalTable: "GarmentSewingBlockingPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GarmentWeeklyPlanItems",
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
                    WeekNumber = table.Column<byte>(nullable: false),
                    StartDate = table.Column<DateTimeOffset>(nullable: false),
                    EndDate = table.Column<DateTimeOffset>(nullable: false),
                    Month = table.Column<byte>(nullable: false),
                    Efficiency = table.Column<double>(nullable: false),
                    Operator = table.Column<int>(nullable: false),
                    WorkingHours = table.Column<double>(nullable: false),
                    AHTotal = table.Column<double>(nullable: false),
                    EHTotal = table.Column<int>(nullable: false),
                    UsedEH = table.Column<int>(nullable: false),
                    RemainingEH = table.Column<int>(nullable: false),
                    WeeklyPlanId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentWeeklyPlanItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentWeeklyPlanItems_GarmentWeeklyPlans_WeeklyPlanId",
                        column: x => x.WeeklyPlanId,
                        principalTable: "GarmentWeeklyPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductionOrder_Details",
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
                    ColorRequest = table.Column<string>(maxLength: 255, nullable: true),
                    ColorTemplate = table.Column<string>(maxLength: 255, nullable: true),
                    ColorTypeId = table.Column<string>(maxLength: 255, nullable: true),
                    ColorType = table.Column<string>(maxLength: 255, nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    ProductionOrderModelId = table.Column<long>(nullable: true),
                    UomId = table.Column<long>(nullable: false),
                    UomUnit = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionOrder_Details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductionOrder_Details_ProductionOrder_ProductionOrderModelId",
                        column: x => x.ProductionOrderModelId,
                        principalTable: "ProductionOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductionOrder_LampStandard",
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
                    Name = table.Column<string>(maxLength: 1000, nullable: true),
                    Code = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    LampStandardId = table.Column<long>(nullable: false),
                    ProductionOrderModelId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionOrder_LampStandard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductionOrder_LampStandard_ProductionOrder_ProductionOrderModelId",
                        column: x => x.ProductionOrderModelId,
                        principalTable: "ProductionOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductionOrder_RunWidth",
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
                    ProductionOrderModelId = table.Column<long>(nullable: true),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionOrder_RunWidth", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductionOrder_RunWidth_ProductionOrder_ProductionOrderModelId",
                        column: x => x.ProductionOrderModelId,
                        principalTable: "ProductionOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RO_Garment_SizeBreakdowns",
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
                    RO_GarmentId = table.Column<long>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    ColorId = table.Column<int>(nullable: false),
                    ColorName = table.Column<string>(nullable: true),
                    Total = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RO_Garment_SizeBreakdowns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RO_Garment_SizeBreakdowns_RO_Garments_RO_GarmentId",
                        column: x => x.RO_GarmentId,
                        principalTable: "RO_Garments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RO_Garment_SizeBreakdown_Details",
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
                    RO_Garment_SizeBreakdownId = table.Column<long>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Information = table.Column<string>(nullable: true),
                    Size = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RO_Garment_SizeBreakdown_Details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RO_Garment_SizeBreakdown_Details_RO_Garment_SizeBreakdowns_RO_Garment_SizeBreakdownId",
                        column: x => x.RO_Garment_SizeBreakdownId,
                        principalTable: "RO_Garment_SizeBreakdowns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CostCalculationGarment_Materials_CostCalculationGarmentId",
                table: "CostCalculationGarment_Materials",
                column: "CostCalculationGarmentId");

            migrationBuilder.CreateIndex(
                name: "IX_FinishingPrintingSalesContractDetails_FinishingPrintingSalesContractId",
                table: "FinishingPrintingSalesContractDetails",
                column: "FinishingPrintingSalesContractId");

            migrationBuilder.CreateIndex(
                name: "IX_FinishingPrintingSalesContracts_SalesContractNo",
                table: "FinishingPrintingSalesContracts",
                column: "SalesContractNo",
                unique: true,
                filter: "[SalesContractNo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentBookingOrderItems_BookingOrderId",
                table: "GarmentBookingOrderItems",
                column: "BookingOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentSalesContractItems_GSCId",
                table: "GarmentSalesContractItems",
                column: "GSCId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentSewingBlockingPlanItems_BlockingPlanId",
                table: "GarmentSewingBlockingPlanItems",
                column: "BlockingPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentWeeklyPlanItems_WeeklyPlanId",
                table: "GarmentWeeklyPlanItems",
                column: "WeeklyPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionOrder_Details_ProductionOrderModelId",
                table: "ProductionOrder_Details",
                column: "ProductionOrderModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionOrder_LampStandard_ProductionOrderModelId",
                table: "ProductionOrder_LampStandard",
                column: "ProductionOrderModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionOrder_RunWidth_ProductionOrderModelId",
                table: "ProductionOrder_RunWidth",
                column: "ProductionOrderModelId");

            migrationBuilder.CreateIndex(
                name: "IX_RO_Garment_SizeBreakdown_Details_RO_Garment_SizeBreakdownId",
                table: "RO_Garment_SizeBreakdown_Details",
                column: "RO_Garment_SizeBreakdownId");

            migrationBuilder.CreateIndex(
                name: "IX_RO_Garment_SizeBreakdowns_RO_GarmentId",
                table: "RO_Garment_SizeBreakdowns",
                column: "RO_GarmentId");

            migrationBuilder.CreateIndex(
                name: "IX_RO_Garments_CostCalculationGarmentId",
                table: "RO_Garments",
                column: "CostCalculationGarmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleColors");

            migrationBuilder.DropTable(
                name: "CostCalculationGarment_Materials");

            migrationBuilder.DropTable(
                name: "Efficiencies");

            migrationBuilder.DropTable(
                name: "FinishingPrintingSalesContractDetails");

            migrationBuilder.DropTable(
                name: "GarmentBookingOrderItems");

            migrationBuilder.DropTable(
                name: "GarmentSalesContractItems");

            migrationBuilder.DropTable(
                name: "GarmentSewingBlockingPlanItems");

            migrationBuilder.DropTable(
                name: "GarmentWeeklyPlanItems");

            migrationBuilder.DropTable(
                name: "ProductionOrder_Details");

            migrationBuilder.DropTable(
                name: "ProductionOrder_LampStandard");

            migrationBuilder.DropTable(
                name: "ProductionOrder_RunWidth");

            migrationBuilder.DropTable(
                name: "Rates");

            migrationBuilder.DropTable(
                name: "RO_Garment_SizeBreakdown_Details");

            migrationBuilder.DropTable(
                name: "SpinningSalesContract");

            migrationBuilder.DropTable(
                name: "WeavingSalesContract");

            migrationBuilder.DropTable(
                name: "FinishingPrintingSalesContracts");

            migrationBuilder.DropTable(
                name: "GarmentBookingOrders");

            migrationBuilder.DropTable(
                name: "GarmentSalesContracts");

            migrationBuilder.DropTable(
                name: "GarmentSewingBlockingPlans");

            migrationBuilder.DropTable(
                name: "GarmentWeeklyPlans");

            migrationBuilder.DropTable(
                name: "ProductionOrder");

            migrationBuilder.DropTable(
                name: "RO_Garment_SizeBreakdowns");

            migrationBuilder.DropTable(
                name: "RO_Garments");

            migrationBuilder.DropTable(
                name: "CostCalculationGarments");
        }
    }
}
