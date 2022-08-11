using AutoMapper;
using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.FinisihingPrintingSalesContract;
using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.ProductionOrder;
using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.ProductionOrderProfiles;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrintingCostCalculation;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.ProductionOrder;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.ProductionOrder;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrintingCostCalculation;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.ProductionOrder;
using Com.Ambassador.Service.Sales.Lib.Models.ProductionOrder;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrintingCostCalculation;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using Com.Ambassador.Service.Sales.Lib.ViewModels.ProductionOrder;
using Com.Ambassador.Service.Sales.Lib.ViewModels.Report;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Ambassador.Sales.Test.BussinesLogic.Facades.ProductionOrder
{
    public class ShinProductionOrderFacadeTest : BaseFacadeTest<SalesDbContext, ShinProductionOrderFacade, ShinProductionOrderLogic, ProductionOrderModel, ShinProductionOrderDataUtil>
    {
        private const string ENTITY = "ShinProductionOrder";
        public ShinProductionOrderFacadeTest() : base(ENTITY)
        {
        }


        public override async void Get_All_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            ShinProductionOrderFacade facade = Activator.CreateInstance(typeof(ShinProductionOrderFacade), serviceProvider, dbContext) as ShinProductionOrderFacade;
            await DataUtil(facade, dbContext).GetTestData();


            var Response = facade.Read(1, 25, "{}", new List<string>(), null, "{}");

            Assert.NotEqual(Response.Data.Count, 0);
        }

        public override async void Get_By_Id_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            ShinProductionOrderFacade facade = Activator.CreateInstance(typeof(ShinProductionOrderFacade), serviceProvider, dbContext) as ShinProductionOrderFacade;
            await DataUtil(facade, dbContext).GetTestData();
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");
            var Response = await facade.ReadByIdAsync((int)all.Data.FirstOrDefault().Id);

            Assert.NotEqual(Response.Id, 0);
        }

        public override async void Create_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            ShinProductionOrderFacade facade = Activator.CreateInstance(typeof(ShinProductionOrderFacade), serviceProvider, dbContext) as ShinProductionOrderFacade;
            var data = await DataUtil(facade, dbContext).GetNewData();
            var response = await facade.CreateAsync(data);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public async void Validate_ViewModel()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var vm = new ShinProductionOrderViewModel()
            {
                Code = "a",
                ArticleFabricEdge = "a",
                DeliveryDate = DateTimeOffset.UtcNow,
                Remark = "a",
                ApprovalMD = new ApprovalViewModel()
                {
                    ApprovedBy = "a",
                    ApprovedDate = DateTimeOffset.UtcNow,
                    IsApproved = true
                },
                IsCalculated = true,
                IsClosed = true,
                IsCompleted = true,
                IsRequested = true,
                IsUsed = true,
                AutoIncreament = 0,
                LampStandards=new List<ProductionOrder_LampStandardViewModel>(),
                Details =new List<ProductionOrder_DetailViewModel>()   
            };
            var sp = GetServiceProviderMock(DbContext(GetCurrentMethod()));

            var facade = new ShinProductionOrderFacade(sp.Object, dbContext);
            sp.Setup(s => s.GetService(typeof(IShinProductionOrder)))
                .Returns(facade);

            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(vm, sp.Object, null);
            var response = vm.Validate(validationContext);
            Assert.NotEmpty(response);
            Assert.True(0 < response.Count());

            vm.FinishingPrintingSalesContract = new ShinFinishingPrintingSalesContractViewModel()
            {
                Id = 1,
                PreSalesContract = new FinishingPrintingPreSalesContractViewModel()
                {
                    Unit = new UnitViewModel()
                    {
                        Name = "printing"
                    }
                }
            };
            response = vm.Validate(validationContext);
            Assert.NotEmpty(response);
            Assert.True(0 < response.Count());

            vm.Run = "1 run";
            response = vm.Validate(validationContext);
            Assert.NotEmpty(response);
            Assert.True(0 < response.Count());

            vm.RunWidth = new List<ProductionOrder_RunWidthViewModel>()
            {
                new ProductionOrder_RunWidthViewModel()
                {
                    Value = -1
                }
            };

            response = vm.Validate(validationContext);
            Assert.NotEmpty(response);
            Assert.True(0 < response.Count());

            vm.RunWidth = new List<ProductionOrder_RunWidthViewModel>()
            {
                new ProductionOrder_RunWidthViewModel()
                {
                    Value = 1
                }
            };
            response = vm.Validate(validationContext);
            Assert.NotEmpty(response);
            Assert.True(0 < response.Count());

            vm.StandardTests = new StandardTestsViewModel()
            {
                Id = 1
            };
            response = vm.Validate(validationContext);
            Assert.NotEmpty(response);
            Assert.True(0 < response.Count());

            vm.Account = new AccountViewModel();
            response = vm.Validate(validationContext);
            Assert.NotEmpty(response);
            Assert.True(0 < response.Count());

            vm.MaterialOrigin = "a";
            response = vm.Validate(validationContext);
            Assert.NotEmpty(response);
            Assert.True(0 < response.Count());

            vm.HandlingStandard = "e";
            response = vm.Validate(validationContext);
            Assert.NotEmpty(response);
            Assert.True(0 < response.Count());

            vm.ShrinkageStandard = "e";
            response = vm.Validate(validationContext);
            Assert.NotEmpty(response);
            Assert.True(0 < response.Count());

          //  var data = await DataUtil(facade, dbContext).GetTestData();
            vm.OrderQuantity = 1;
            //vm.FinishingPrintingSalesContract.Id = data.SalesContractId;
            vm.FinishingPrintingSalesContract.Id = 1;
            vm.Details = new List<ProductionOrder_DetailViewModel>()
            {
                new ProductionOrder_DetailViewModel()
                {
                    Quantity = 5,
                    
                }
            };
            vm.LampStandards = new List<ProductionOrder_LampStandardViewModel>();
            response = vm.Validate(validationContext);
            Assert.NotEmpty(response);
            Assert.True(0 < response.Count());

            vm.LampStandards.Add(new ProductionOrder_LampStandardViewModel()
            {

            });

            vm.Details = new List<ProductionOrder_DetailViewModel>()
            {
                new ProductionOrder_DetailViewModel()
                {
                    Quantity = -1,

                }
            };
            response = vm.Validate(validationContext);
            Assert.NotEmpty(response);
            Assert.True(0 < response.Count());

            vm.LampStandards.FirstOrDefault().Name = "a";
            response = vm.Validate(validationContext);
            Assert.NotEmpty(response);
            Assert.True(0 < response.Count());

            vm.Details = new List<ProductionOrder_DetailViewModel>();
            response = vm.Validate(validationContext);
            Assert.NotEmpty(response);
            Assert.True(0 < response.Count());

            vm.Details = new List<ProductionOrder_DetailViewModel>()
            {
                new ProductionOrder_DetailViewModel()
            };
            response = vm.Validate(validationContext);
            Assert.NotEmpty(response);
            Assert.True(0 < response.Count());

        }

        public override async void Delete_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            ShinProductionOrderFacade facade = Activator.CreateInstance(typeof(ShinProductionOrderFacade), serviceProvider, dbContext) as ShinProductionOrderFacade;
            await DataUtil(facade, dbContext).GetTestData();
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");
            var Response = await facade.DeleteAsync((int)all.Data.FirstOrDefault().Id);
            Assert.NotEqual(Response, 0);
        }



        public override async void Update_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ShinProductionOrderFacade facade = Activator.CreateInstance(typeof(ShinProductionOrderFacade), serviceProvider, dbContext) as ShinProductionOrderFacade;
            var data = await DataUtil(facade, dbContext).GetTestData();
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");

            var response = await facade.UpdateAsync((int)all.Data.FirstOrDefault().Id, all.Data.FirstOrDefault());

            Assert.NotEqual(response, 0);
        }

        protected override ShinProductionOrderDataUtil DataUtil(ShinProductionOrderFacade facade, SalesDbContext dbContext = null)
        {
            FinishingPrintingCostCalculationFacade ccFacade = new FinishingPrintingCostCalculationFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            ShinFinishingPrintingSalesContractFacade scFacade = new ShinFinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinishingPrintingPreSalesContractFacade prescFacade = new FinishingPrintingPreSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            ShinProductionOrderDataUtil dataUtil = new ShinProductionOrderDataUtil(facade, scFacade, ccFacade, prescFacade);
            return dataUtil;
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IIdentityService)))
                .Returns(identityService);

            var httpClientService = new Mock<IHttpClientService>();
            DailyAPiResult dailyAPiResult = new DailyAPiResult
            {
                data = new List<DailyOperationViewModel> {
                    new DailyOperationViewModel {
                        area = "Test",
                        color = "Color Test",
                        machine = "Machine Test",
                        orderNo = "a",
                        orderQuantity = 1,
                        step = "Test"
                    }
                }
            };

            FabricAPiResult fabricAPiResult = new FabricAPiResult
            {
                data = new List<FabricQualityControlViewModel> {
                    new FabricQualityControlViewModel {
                        grade = "Test",
                        orderNo = "a",
                        orderQuantity = 1
                    }
                }
            };

            httpClientService.Setup(x => x.GetAsync(It.Is<string>(s => s.Contains("finishing-printing/daily-operations/production-order-report"))))
                .ReturnsAsync(new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(dailyAPiResult)) });
            httpClientService.Setup(x => x.GetAsync(It.Is<string>(s => s.Contains("finishing-printing/quality-control/defect"))))
                .ReturnsAsync(new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(fabricAPiResult)) });


            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(new IdentityService { Username = "Username", TimezoneOffset = 7 });
            serviceProviderMock
                .Setup(x => x.GetService(typeof(IHttpClientService)))
                .Returns(httpClientService.Object);

            var ccLogic = new FinishingPrintingCostCalculationLogic(identityService, dbContext);
            serviceProviderMock.Setup(s => s.GetService(typeof(FinishingPrintingCostCalculationLogic)))
                .Returns(ccLogic);

            var preSalesContractLogic = new FinishingPrintingPreSalesContractLogic(identityService, dbContext);
            serviceProviderMock
                .Setup(s => s.GetService(typeof(FinishingPrintingPreSalesContractLogic)))
                .Returns(preSalesContractLogic);

            var productionOrderDetailLogic = new ProductionOrder_DetailLogic(serviceProviderMock.Object, identityService, dbContext);
            var productionOrderlsLogic = new ProductionOrder_LampStandardLogic(serviceProviderMock.Object, identityService, dbContext);
            var productionOrderrwLogic = new ProductionOrder_RunWidthLogic(serviceProviderMock.Object, identityService, dbContext);

            var poDetailMock = new Mock<ProductionOrder_DetailLogic>();
            var poRWk = new Mock<ProductionOrder_RunWidthLogic>();
            var poLSMock = new Mock<ProductionOrder_LampStandardLogic>();
            serviceProviderMock
                .Setup(x => x.GetService(typeof(ProductionOrder_DetailLogic)))
                .Returns(productionOrderDetailLogic);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(ProductionOrder_LampStandardLogic)))
                .Returns(productionOrderlsLogic);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(ProductionOrder_RunWidthLogic)))
                .Returns(productionOrderrwLogic);


            var finishingprintingDetailObject = new FinishingPrintingSalesContractDetailLogic(serviceProviderMock.Object, identityService, dbContext);
            var finishingprintingLogic = new ShinFinishingPrintingSalesContractLogic(finishingprintingDetailObject, serviceProviderMock.Object, identityService, dbContext);
            serviceProviderMock
                .Setup(x => x.GetService(typeof(ShinFinishingPrintingSalesContractLogic)))
                .Returns(finishingprintingLogic);

            var productionOrderLogic = new ShinProductionOrderLogic(serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(ShinProductionOrderLogic)))
                .Returns(productionOrderLogic);

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

        [Fact]
        public async void UpdateIsRequestedTrue_Success()
        {
            var dbContext = DbContext(GetCurrentMethod() + "requestedTrue");
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ShinProductionOrderFacade facade = Activator.CreateInstance(typeof(ShinProductionOrderFacade), serviceProvider, dbContext) as ShinProductionOrderFacade;
            await DataUtil(facade, dbContext).GetTestData();
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");
            List<int> ids = new List<int>()
            {
                (int)all.Data.FirstOrDefault().Id
            };
            var response = await facade.UpdateRequestedTrue(ids);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public async void UpdateIsRequestedFalse_Success()
        {
            var dbContext = DbContext(GetCurrentMethod() + "requestedFalse");
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ShinProductionOrderFacade facade = Activator.CreateInstance(typeof(ShinProductionOrderFacade), serviceProvider, dbContext) as ShinProductionOrderFacade;
            await DataUtil(facade, dbContext).GetTestData();
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");
            List<int> ids = new List<int>()
            {
                (int)all.Data.FirstOrDefault().Id
            };
            var response = await facade.UpdateRequestedFalse(ids);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public async void UpdateIsCompletedTrue_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ShinProductionOrderFacade facade = Activator.CreateInstance(typeof(ShinProductionOrderFacade), serviceProvider, dbContext) as ShinProductionOrderFacade;
            await DataUtil(facade, dbContext).GetTestData();
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");
            var response = await facade.UpdateIsCompletedTrue(Convert.ToInt32(all.Data.FirstOrDefault().Id));

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public async void UpdateIsCompletedFalse_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ShinProductionOrderFacade facade = Activator.CreateInstance(typeof(ShinProductionOrderFacade), serviceProvider, dbContext) as ShinProductionOrderFacade;
            await DataUtil(facade, dbContext).GetTestData();
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");
            var response = await facade.UpdateIsCompletedFalse(Convert.ToInt32(all.Data.FirstOrDefault().Id));

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public async void UpdateDistributedQuantity_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ShinProductionOrderFacade facade = Activator.CreateInstance(typeof(ShinProductionOrderFacade), serviceProvider, dbContext) as ShinProductionOrderFacade;
            await DataUtil(facade, dbContext).GetTestData();
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");
            List<int> ids = new List<int>()
            {
                (int)all.Data.FirstOrDefault().Id
            };
            List<double> distributedQuantity = new List<double>()
            {
                (int)all.Data.FirstOrDefault().DistributedQuantity
            };
            var response = await facade.UpdateDistributedQuantity(ids, distributedQuantity);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public async Task Should_Success_GetMonthlyOrderQuantityByYearAndOrderType()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ShinProductionOrderFacade facade = Activator.CreateInstance(typeof(ShinProductionOrderFacade), serviceProvider, dbContext) as ShinProductionOrderFacade;
            var data = await DataUtil(facade, dbContext).GetTestData();

            var result = facade.GetMonthlyOrderQuantityByYearAndOrderType(data.DeliveryDate.Year, (int)data.OrderTypeId, 0);

            Assert.NotEqual(result.Count, 0);
        }

        [Fact]
        public async Task Should_Success_GetMonthlyOrderIdsByOrderType()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ShinProductionOrderFacade facade = Activator.CreateInstance(typeof(ShinProductionOrderFacade), serviceProvider, dbContext) as ShinProductionOrderFacade;
            var data = await DataUtil(facade, dbContext).GetTestData();
            var result = facade.GetMonthlyOrderIdsByOrderType(data.DeliveryDate.Year, data.DeliveryDate.Month, (int)data.OrderTypeId, 0);

            Assert.NotEqual(result.Count, 0);
        }

        [Fact]
        public async Task Should_Success_GetDetailReport()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ShinProductionOrderFacade facade = Activator.CreateInstance(typeof(ShinProductionOrderFacade), serviceProvider, dbContext) as ShinProductionOrderFacade;
            var data = await DataUtil(facade, dbContext).GetTestData();

            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");
            var result = await facade.GetDetailReport(all.Data.FirstOrDefault().Id);

            Assert.NotNull(result);
        }

        [Fact]
        public async void UpdateIsCalculated_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ShinProductionOrderFacade facade = Activator.CreateInstance(typeof(ShinProductionOrderFacade), serviceProvider, dbContext) as ShinProductionOrderFacade;
            await DataUtil(facade, dbContext).GetTestData();
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");
            var response = await facade.UpdateIsCalculated(Convert.ToInt32(all.Data.FirstOrDefault().Id), true);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public async void UpdateIsCalculated_Exception()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ShinProductionOrderFacade facade = Activator.CreateInstance(typeof(ShinProductionOrderFacade), serviceProvider, dbContext) as ShinProductionOrderFacade;
            await Assert.ThrowsAnyAsync<Exception>(() => facade.UpdateIsCalculated(0, true));

        }
        [Fact]
        public async void Should_Success_Get_ProductionReport()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProviderMock = GetServiceProviderMock(dbContext);

            ShinProductionOrderFacade facade = Activator.CreateInstance(typeof(ShinProductionOrderFacade), serviceProviderMock.Object, dbContext) as ShinProductionOrderFacade;
            var data = await DataUtil(facade, dbContext).GetTestData();


            var tuple = await facade.GetReport(data.SalesContractNo, null, null, null, null, null, DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(3), 1, 25, "{}", 7);
            Assert.NotNull(tuple.Item1);


        }
        [Fact]
        public async void Shoould_Success_Get_Excel()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProviderMock = GetServiceProviderMock(dbContext);

            ShinProductionOrderFacade facade = Activator.CreateInstance(typeof(ShinProductionOrderFacade), serviceProviderMock.Object, dbContext) as ShinProductionOrderFacade;
            var data = await DataUtil(facade, dbContext).GetTestData();

            var tuple = await facade.GenerateExcel(data.SalesContractNo, null, null, null, null, null, DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(3), 7);
            Assert.IsType<System.IO.MemoryStream>(tuple);


        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ShinProductionOrderMapper>();
                cfg.AddProfile<ProductionOrderRunWidthMapper>();
                cfg.AddProfile<ProductionOrderLampStandardMapper>();
                cfg.AddProfile<ProductionOrderDetailMapper>();
            });
            var mapper = configuration.CreateMapper();

            ShinProductionOrderViewModel vm = new ShinProductionOrderViewModel { Id = 1 };
            ProductionOrderModel model = mapper.Map<ProductionOrderModel>(vm);

            Assert.Equal(vm.Id, model.Id);

        }
        [Fact]
        public async void Should_Success_Approve_MD()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProviderMock = GetServiceProviderMock(dbContext);

            ShinProductionOrderFacade facade = Activator.CreateInstance(typeof(ShinProductionOrderFacade), serviceProviderMock.Object, dbContext) as ShinProductionOrderFacade;
            await DataUtil(facade, dbContext).GetTestData();
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");


            var tuple = await facade.ApproveByMD(all.Data.FirstOrDefault().Id);
            Assert.NotEqual(0, tuple);


        }

        [Fact]
        public async void Should_Success_Approve_Sample()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProviderMock = GetServiceProviderMock(dbContext);

            ShinProductionOrderFacade facade = Activator.CreateInstance(typeof(ShinProductionOrderFacade), serviceProviderMock.Object, dbContext) as ShinProductionOrderFacade;
            await DataUtil(facade, dbContext).GetTestData();
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");


            var tuple = await facade.ApproveBySample(all.Data.FirstOrDefault().Id);
            Assert.NotEqual(0, tuple);


        }
    }
}
