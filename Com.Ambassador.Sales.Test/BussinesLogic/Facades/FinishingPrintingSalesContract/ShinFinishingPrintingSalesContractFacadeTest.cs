using AutoMapper;
using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.FinisihingPrintingSalesContract;
using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.FinishingPrintingProfiles;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrintingCostCalculation;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrintingCostCalculation;
using Com.Ambassador.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.Models.FinishingPrintingCostCalculation;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrintingCostCalculation;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Com.Ambassador.Sales.Test.BussinesLogic.Facades.FinishingPrintingSalesContract
{
    public class ShinFinishingPrintingSalesContractFacadeTest : BaseFacadeTest<SalesDbContext, ShinFinishingPrintingSalesContractFacade, ShinFinishingPrintingSalesContractLogic, FinishingPrintingSalesContractModel, ShinFinisihingPrintingSalesContractDataUtil>
    {
        private const string ENTITY = "NewFinishingPrintingSalesContract";
        public ShinFinishingPrintingSalesContractFacadeTest() : base(ENTITY)
        {
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            var finishingprintingDetailLogic = new FinishingPrintingSalesContractDetailLogic(serviceProviderMock.Object, identityService, dbContext);
            var finishingprintingLogic = new ShinFinishingPrintingSalesContractLogic(finishingprintingDetailLogic, serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(ShinFinishingPrintingSalesContractLogic)))
                .Returns(finishingprintingLogic);

            var preSalesContractLogic = new FinishingPrintingPreSalesContractLogic(identityService, dbContext);
            serviceProviderMock
                .Setup(s => s.GetService(typeof(FinishingPrintingPreSalesContractLogic)))
                .Returns(preSalesContractLogic);

            var ccLogic = new FinishingPrintingCostCalculationLogic(identityService, dbContext);

            serviceProviderMock
                .Setup(s => s.GetService(typeof(FinishingPrintingCostCalculationLogic)))
                .Returns(ccLogic);

            var azureImageFacadeMock = new Mock<IAzureImageFacade>();
            azureImageFacadeMock
                .Setup(s => s.DownloadImage(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync("");

            azureImageFacadeMock
                .Setup(s => s.UploadImage(It.IsAny<string>(), It.IsAny<long>(), It.IsAny<DateTime>(), It.IsAny<string>()))
                .ReturnsAsync("");

            serviceProviderMock
               .Setup(x => x.GetService(typeof(IAzureImageFacade)))
               .Returns(azureImageFacadeMock.Object);

            return serviceProviderMock;
        }

        protected override ShinFinisihingPrintingSalesContractDataUtil DataUtil(ShinFinishingPrintingSalesContractFacade facade, SalesDbContext dbContext = null)
        {
            FinishingPrintingPreSalesContractFacade ccFacade = new FinishingPrintingPreSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            ShinFinisihingPrintingSalesContractDataUtil dataUtil = Activator.CreateInstance(typeof(ShinFinisihingPrintingSalesContractDataUtil), facade, ccFacade) as ShinFinisihingPrintingSalesContractDataUtil;
            return dataUtil;
        }

        [Fact]
        public virtual async void Create_Buyer_Type_Ekspor_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            ShinFinishingPrintingSalesContractFacade facade = new ShinFinishingPrintingSalesContractFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetNewData();
            data.BuyerType = "ekspor";

            var response = await facade.CreateAsync(data);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public async void ReadParent_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ShinFinishingPrintingSalesContractFacade facade = new ShinFinishingPrintingSalesContractFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = await facade.ReadParent(data.Id);
            Assert.NotNull(Response);
        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ShinFinishingPrintingSalesContractMapper>();
                cfg.AddProfile<FinishingPrintingSalesContractDetailMapper>();
            });
            var mapper = configuration.CreateMapper();

            ShinFinishingPrintingSalesContractViewModel vm = new ShinFinishingPrintingSalesContractViewModel { Id = 1 };
            FinishingPrintingSalesContractModel model = mapper.Map<FinishingPrintingSalesContractModel>(vm);

            Assert.Equal(vm.Id, model.Id);

        }

        [Fact]
        public void ValidateVM()
        {
            var vm = new ShinFinishingPrintingSalesContractViewModel()
            {
                AutoIncrementNumber = 1,
                Code = "code",
                CommodityDescription = "a",
                Condition = "1",
                DispositionNumber = "nu",
                Packing = "a",
                PieceLength = "1",
                ShipmentDescription = "a",
                ShippingQuantityTolerance = 1,
                TransportFee = "e",
                UseIncomeTax = true,
                RemainingQuantity = 1
            };
            var response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.PreSalesContract = new FinishingPrintingPreSalesContractViewModel();
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.PreSalesContract.Id = 1;
            vm.PreSalesContract.Buyer = new BuyerViewModel()
            {
                Type = "ekspor"
            };
            //vm.CostCalculation.PreSalesContract = new FinishingPrintingPreSalesContractViewModel()
            //{
            //    Buyer = new BuyerViewModel()
            //    {
            //        Type = "ekspor"
            //    }
            //};
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.PreSalesContract.Buyer.Type = "export";
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.TermOfShipment = "test";
            vm.Amount = -1;
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.Amount = 1;
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.Agent = new AgentViewModel()
            {
                Id = 1
            };
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.Commission = "1";
            response = vm.Validate(null);
            Assert.NotEmpty(response);


            vm.Commodity = new CommodityViewModel()
            {
                Id = 1
            };
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            //vm.MaterialConstruction = new MaterialConstructionViewModel()
            //{
            //    Id = 1
            //};
            //response = vm.Validate(null);
            //Assert.NotEmpty(response);

            vm.YarnMaterial = new YarnMaterialViewModel()
            {
                Id = 1
            };
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            //vm.MaterialWidth = "tes";
            //response = vm.Validate(null);
            //Assert.NotEmpty(response);

            vm.Quality = new QualityViewModel()
            {
                Id = 1
            };
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.TermOfPayment = new TermOfPaymentViewModel()
            {
                Id = 1
            };
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.AccountBank = new AccountBankViewModel()
            {
                Id = 1
            };
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.DeliveredTo = "test";
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.DeliverySchedule = DateTimeOffset.UtcNow.AddDays(-1);
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.DeliverySchedule = DateTimeOffset.UtcNow.AddDays(1);
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.PointSystem = 0;
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.PointSystem = 10;
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.PointSystem = 4;
            vm.PointLimit = -1;
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.PointLimit = 1;
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.Details = new List<FinishingPrintingSalesContractDetailViewModel>();
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.Details = new List<FinishingPrintingSalesContractDetailViewModel>()
            {
                new FinishingPrintingSalesContractDetailViewModel()
            };
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.Details = new List<FinishingPrintingSalesContractDetailViewModel>()
            {
                new FinishingPrintingSalesContractDetailViewModel()
                {
                    Color = "a0",
                    Price = 0,
                    ScreenCost = 1,
                    CostCalculationId = 1,
                    ProductionOrderNo = "s"
                }
            };
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.Details = new List<FinishingPrintingSalesContractDetailViewModel>()
            {
                new FinishingPrintingSalesContractDetailViewModel()
                {
                    Color = "a0",
                    Price = 1,
                    UseIncomeTax = true
                }
            };
            Assert.True(vm.Details.FirstOrDefault().UseIncomeTax);
            response = vm.Validate(null);
            Assert.Empty(response);
        }
    }
}
