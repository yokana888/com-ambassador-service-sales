using AutoMapper;
using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.FinisihingPrintingSalesContract;
using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.ProductionOrder;
using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.ProductionOrderProfiles;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.ProductionOrder;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.ProductionOrder;
using Com.Ambassador.Service.Sales.Lib.Models.ProductionOrder;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.ViewModels.ProductionOrder;
using Com.Ambassador.Service.Sales.Lib.ViewModels.Report;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Com.Ambassador.Sales.Test.BussinesLogic.Facades.ProductionOrder
{
    public class ProductionOrderFacadeTest : BaseFacadeTest<SalesDbContext, ProductionOrderFacade, ProductionOrderLogic, ProductionOrderModel, ProductionOrderDataUtil>
    {
        private const string ENTITY = "ProductionOrder";
        public ProductionOrderFacadeTest() : base(ENTITY)
        {
        }

        public override async void Get_All_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = await DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            var model = await facade.CreateAsync(data);


            var Response = facade.Read(1, 25, "{}", new List<string>(), null, "{}");

            Assert.NotEqual(Response.Data.Count, 0);
        }

        public override async void Get_By_Id_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            ProductionOrderFacade facade = new ProductionOrderFacade(serviceProvider, dbContext);
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = await DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            await facade.CreateAsync(data);
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");
            var Response = await facade.ReadByIdAsync((int)all.Data.FirstOrDefault().Id);

            Assert.NotEqual(Response.Id, 0);
        }

        public override async void Create_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = await DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            var response = await facade.CreateAsync(data);

            Assert.NotEqual(response, 0);

            var salesData2 = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data2 = await DataUtil(facade).GetNewData();
            data2.OrderTypeName = "PRINTING";
            data2.SalesContractId = salesData2.Id;
            var response2 = await facade.CreateAsync(data2);

            Assert.NotEqual(response2, 0);
        }

        [Fact]
        public async void UpdateDistributedQuantity_Return_Success()
        {
            //Setup
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();

            var NewData = await DataUtil(facade).GetNewData();
            NewData.SalesContractId = salesData.Id;
            var response = await facade.CreateAsync(NewData);

            //Act
            var result = await facade.UpdateDistributedQuantity(new List<int>() { 1 }, new List<double>() { NewData.DistributedQuantity });

            //Assert
            Assert.NotEqual(0, result);


        }

        [Fact]
        public async Task Create_Throws_Exception()
        {
            //Setup
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            ProductionOrderFacade facade = new ProductionOrderFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade).GetNewData();
            data.Details = null;

            //Assert
            await Assert.ThrowsAsync<Exception>(() => facade.CreateAsync(data));


        }



        public override async void Delete_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = await DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            var model = await facade.CreateAsync(data);
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");
            var Response = await facade.DeleteAsync((int)all.Data.FirstOrDefault().Id);
            Assert.NotEqual(Response, 0);
        }



        public override async void Update_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = await DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            var model = await facade.CreateAsync(data);
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");

            var response = await facade.UpdateAsync((int)all.Data.FirstOrDefault().Id, data);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public async Task Update_Exception()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = await DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            var model = await facade.CreateAsync(data);
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");

            await Assert.ThrowsAnyAsync<Exception>(() => facade.UpdateAsync((int)all.Data.FirstOrDefault().Id, null));

        }

        [Fact]
        public async void Update_Delete_Success()
        {
            var dbContext = DbContext(GetCurrentMethod() + "Update_Delete_Success");
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ProductionOrderFacade facade1 = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            ProductionOrderFacade facade2 = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = await DataUtil(facade1).GetNewData();
            data.SalesContractId = salesData.Id;
            var model = await facade1.CreateAsync(data);
            var all = facade1.Read(1, 25, "{}", new List<string>(), null, "{}");

            data.Details = new List<ProductionOrder_DetailModel>();
            data.Details.Add(new ProductionOrder_DetailModel()
            {
                ColorRequest = "c",
                Quantity = 10,
                ColorTemplate = "ct",
                ColorType = "type",
                UomUnit = "unit"
            });

            var response = await facade2.UpdateAsync((int)all.Data.FirstOrDefault().Id, data);

            Assert.NotEqual(response, 0);
        }


        protected override Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IIdentityService)))
                .Returns(identityService);
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
            var finishingprintingLogic = new FinishingPrintingSalesContractLogic(finishingprintingDetailObject, serviceProviderMock.Object, identityService, dbContext);
            serviceProviderMock
                .Setup(x => x.GetService(typeof(FinishingPrintingSalesContractLogic)))
                .Returns(finishingprintingLogic);

            var productionOrderLogic = new ProductionOrderLogic(serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(ProductionOrderLogic)))
                .Returns(productionOrderLogic);

            return serviceProviderMock;
        }

        [Fact]
        public async void UpdateIsRequestedTrue_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = await DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            var model = await facade.CreateAsync(data);
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
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = await DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            var model = await facade.CreateAsync(data);
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
            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = await DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            var model = await facade.CreateAsync(data);
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");
            var response = await facade.UpdateIsCompletedTrue((int)all.Data.FirstOrDefault().Id);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public async void UpdateIsCompletedFalse_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = await DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            var model = await facade.CreateAsync(data);
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");
            var response = await facade.UpdateIsCompletedFalse((int)all.Data.FirstOrDefault().Id);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public async void UpdateDistributedQuantity_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = await DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            var model = await facade.CreateAsync(data);
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");
            List<int> ids = new List<int>()
            {
                (int)all.Data.FirstOrDefault().Id
            };
            List<double> distributedQuantity = new List<double>()
            {
                (int)all.Data.FirstOrDefault().DistributedQuantity
            };
            var response = await facade.UpdateRequestedFalse(ids);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public async Task Should_Success_GetMonthlyOrderQuantityByYearAndOrderType()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = await DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            await facade.CreateAsync(data);

            var result = facade.GetMonthlyOrderQuantityByYearAndOrderType(data.DeliveryDate.Year, (int)data.OrderTypeId, 0);

            Assert.NotEqual(result.Count, 0);
        }

        [Fact]
        public async Task Should_Success_GetMonthlyOrderIdsByOrderType()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = await DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            await facade.CreateAsync(data);

            var result = facade.GetMonthlyOrderIdsByOrderType(data.DeliveryDate.Year, data.DeliveryDate.Month, (int)data.OrderTypeId, 0);

            Assert.NotEqual(result.Count, 0);
        }

        [Fact]
        public async void UpdateIsCalculated_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = await DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            var model = await facade.CreateAsync(data);
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");
            var response = await facade.UpdateIsCalculated((int)all.Data.FirstOrDefault().Id, true);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public async void UpdateIsCalculated_Exception()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = await DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            var model = await facade.CreateAsync(data);
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");
            await Assert.ThrowsAnyAsync<Exception>(() => facade.UpdateIsCalculated(0, true));

        }
        [Fact]
        public async void Should_Success_Get_ProductionReport()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProviderMock = GetServiceProviderMock(dbContext);
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
            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProviderMock.Object, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade facadeSC = new FinishingPrintingSalesContractFacade(serviceProviderMock.Object, dbContext);
            FinisihingPrintingSalesContractDataUtil dataUtilSC = new FinisihingPrintingSalesContractDataUtil(facadeSC);
            var data2 = await dataUtilSC.GetNewData();
            data2.SalesContractNo = "a";
            await facadeSC.CreateAsync(data2);

            var data = await DataUtil(facade).GetNewData();
            data.SalesContractId = data2.Id;
            data.SalesContractNo = data2.SalesContractNo;
            var model = await facade.CreateAsync(data);


            var tuple = await facade.GetReport(data2.SalesContractNo, null, null, null, null, null, null, null, 1, 25, "{}", 7);
            Assert.NotNull(tuple.Item1);

            var tuple2 = await facade.GetReport(data2.SalesContractNo, null, null, null, null, null, DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(2), 1, 25, "{}", 7);
            Assert.NotNull(tuple2.Item1);

            var tuple3 = await facade.GetReport(data2.SalesContractNo, null, "1", null, null, null, DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(2), 1, 25, "{}", 7);
            Assert.NotNull(tuple3.Item1);


        }
        [Fact]
        public async void Shoould_Success_Get_Excel()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProviderMock = GetServiceProviderMock(dbContext);
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
            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProviderMock.Object, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade facadeSC = new FinishingPrintingSalesContractFacade(serviceProviderMock.Object, dbContext);
            FinisihingPrintingSalesContractDataUtil dataUtilSC = new FinisihingPrintingSalesContractDataUtil(facadeSC);
            var data2 = await dataUtilSC.GetNewData();
            data2.SalesContractNo = "a";
            await facadeSC.CreateAsync(data2);

            var data = await DataUtil(facade).GetNewData();
            data.SalesContractId = data2.Id;
            data.SalesContractNo = data2.SalesContractNo;
            var model = await facade.CreateAsync(data);

            var tuple = await facade.GenerateExcel(data2.SalesContractNo, null, null, null, null, null, null, null, 7);
            Assert.IsType<System.IO.MemoryStream>(tuple);
            var tuple2 = await facade.GenerateExcel(data2.SalesContractNo, null, "1", null, null, null, null, null, 7);
            Assert.IsType<System.IO.MemoryStream>(tuple2);


        }

        [Fact]
        public async void Shoould_Success_Get_Excel_Null_Result()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProviderMock = GetServiceProviderMock(dbContext);
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
            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProviderMock.Object, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade facadeSC = new FinishingPrintingSalesContractFacade(serviceProviderMock.Object, dbContext);
            FinisihingPrintingSalesContractDataUtil dataUtilSC = new FinisihingPrintingSalesContractDataUtil(facadeSC);
            var data2 = await dataUtilSC.GetNewData();
            data2.SalesContractNo = "a";
            await facadeSC.CreateAsync(data2);

            var data = await DataUtil(facade).GetNewData();
            data.SalesContractId = data2.Id;
            data.SalesContractNo = data2.SalesContractNo;
            var model = await facade.CreateAsync(data);

            var tuple = await facade.GenerateExcel("ab", null, null, null, null, null, null, null, 7);
            Assert.IsType<System.IO.MemoryStream>(tuple);


        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductionOrderMapper>();
                cfg.AddProfile<ProductionOrderRunWidthMapper>();
                cfg.AddProfile<ProductionOrderLampStandardMapper>();
                cfg.AddProfile<ProductionOrderDetailMapper>();
            });
            var mapper = configuration.CreateMapper();

            ProductionOrderViewModel vm = new ProductionOrderViewModel { Id = 1 };
            ProductionOrderModel model = mapper.Map<ProductionOrderModel>(vm);

            Assert.Equal(vm.Id, model.Id);

        }

        [Fact]
        public async void ReadSalesByContractId_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = await DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            var model = await facade.CreateAsync(data);
            var all = facade.Read(1, 25, "{}", new List<string>(), null, "{}");
            var response = facade.ReadBySalesContractId(all.Data.FirstOrDefault().SalesContractId);

            Assert.NotEmpty(response);
        }

        [Fact]
        public async void ReadConstruction_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = await DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            var model = await facade.CreateAsync(data);


            var Response = facade.ReadConstruction(1, 25, data.MaterialName, "{}");

            Assert.NotEqual(Response.Count, 0);
        }
    }
}
