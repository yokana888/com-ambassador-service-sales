using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.Garment.GarmentMerchandiser;
using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.GarmentPreSalesContractDataUtils;
using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.CostCalculationGarments;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.Garment;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.GarmentPreSalesContractFacades;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.CostCalculationGarments;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.Garment;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentPreSalesContractLogics;
using Com.Ambassador.Service.Sales.Lib.Helpers;
using Com.Ambassador.Service.Sales.Lib.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.BussinesLogic.Facades.Garment.GarmentMerchandiser
{
    public class MonitoringPreSalesContractFacadeTest
    {
        private const string ENTITY = "MonitoringPreSalesContract";

        [MethodImpl(MethodImplOptions.NoInlining)]
        private string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return string.Concat(sf.GetMethod().Name, "_", ENTITY);
        }

        private SalesDbContext DbContext(string testName)
        {
            DbContextOptionsBuilder<SalesDbContext> optionsBuilder = new DbContextOptionsBuilder<SalesDbContext>();
            optionsBuilder
                .UseInMemoryDatabase(testName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            SalesDbContext dbContext = new SalesDbContext(optionsBuilder.Options);

            return dbContext;
        }

        protected virtual Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IIdentityService)))
                .Returns(identityService);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(GarmentPreSalesContractLogic)))
                .Returns(new GarmentPreSalesContractLogic(identityService, dbContext));

            CostCalculationGarmentMaterialLogic costCalculationGarmentMaterialLogic = new CostCalculationGarmentMaterialLogic(serviceProviderMock.Object, identityService, dbContext);
            serviceProviderMock
                .Setup(x => x.GetService(typeof(CostCalculationGarmentMaterialLogic)))
                .Returns(costCalculationGarmentMaterialLogic);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(CostCalculationGarmentLogic)))
                .Returns(new CostCalculationGarmentLogic(costCalculationGarmentMaterialLogic, serviceProviderMock.Object, identityService, dbContext));

            serviceProviderMock
                .Setup(x => x.GetService(typeof(MonitoringPreSalesContractLogic)))
                .Returns(new MonitoringPreSalesContractLogic(serviceProviderMock.Object, dbContext, identityService));

            var azureImageFacadeMock = new Mock<IAzureImageFacade>();
            azureImageFacadeMock
                .Setup(s => s.DownloadImage(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync("");

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IAzureImageFacade)))
                .Returns(azureImageFacadeMock.Object);

            return serviceProviderMock;
        }

        protected virtual CostCalculationGarmentDataUtil DataUtil(CostCalculationGarmentFacade facade, IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            GarmentPreSalesContractFacade garmentPreSalesContractFacade = new GarmentPreSalesContractFacade(serviceProvider, dbContext);
            GarmentPreSalesContractDataUtil garmentPreSalesContractDataUtil = new GarmentPreSalesContractDataUtil(garmentPreSalesContractFacade);

            return new CostCalculationGarmentDataUtil(facade, garmentPreSalesContractDataUtil);
        }

        [Fact]
        public async void Get_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProviderMock = GetServiceProviderMock(dbContext);

            Mock<IHttpClientService> httpClientServiceMock = new Mock<IHttpClientService>();

            httpClientServiceMock
                .Setup(x => x.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("{ data: [{}] }", Encoding.UTF8, General.JsonMediaType) });

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IHttpClientService)))
                .Returns(httpClientServiceMock.Object);

            var serviceProvider = serviceProviderMock.Object;

            CostCalculationGarmentFacade costCalculationGarmentFacade = new CostCalculationGarmentFacade(serviceProvider, dbContext);

            await DataUtil(costCalculationGarmentFacade, serviceProvider, dbContext).GetTestData();

            var facade = new MonitoringPreSalesContractFacade(serviceProvider);
            var Response = facade.Read();

            Assert.NotEqual(Response.Item2, 0);
        }

        [Fact]
        public async void Get_Success_With_Filter()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProviderMock = GetServiceProviderMock(dbContext);

            var serviceProvider = serviceProviderMock.Object;

            GarmentPreSalesContractFacade garmentPreSalesContractFacade = new GarmentPreSalesContractFacade(serviceProvider, dbContext);
            GarmentPreSalesContractDataUtil garmentPreSalesContractDataUtil = new GarmentPreSalesContractDataUtil(garmentPreSalesContractFacade);
            var garmentPreSalesContractData = await garmentPreSalesContractDataUtil.GetNewData();
            await garmentPreSalesContractDataUtil.GetTestData(garmentPreSalesContractData);

            CostCalculationGarmentFacade costCalculationGarmentFacade = new CostCalculationGarmentFacade(serviceProvider, dbContext);
            CostCalculationGarmentDataUtil costCalculationGarmentDataUtil = new CostCalculationGarmentDataUtil(costCalculationGarmentFacade, garmentPreSalesContractDataUtil);
            var costCalculationGarmentData = await costCalculationGarmentDataUtil.GetNewData(garmentPreSalesContractData);
            await costCalculationGarmentDataUtil.GetTestData(costCalculationGarmentData);

            var filter = new
            {
                section = garmentPreSalesContractData.SectionCode,
                preSCNo = garmentPreSalesContractData.SCNo,
                preSCType = garmentPreSalesContractData.SCType,
                buyerAgent = garmentPreSalesContractData.BuyerAgentCode,
                buyerBrand = garmentPreSalesContractData.BuyerBrandCode,

                prNoMaster = "PRNo",
                roNoMaster = "RONo",
                unitMaster = "Unit",

                roNoJob = costCalculationGarmentData.RO_Number,
                unitJob = costCalculationGarmentData.UnitCode,

                dateStart = garmentPreSalesContractData.SCDate,
                dateEnd = garmentPreSalesContractData.SCDate
            };

            Mock<IHttpClientService> httpClientServiceMock = new Mock<IHttpClientService>();

            httpClientServiceMock
                .Setup(x => x.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("{ data: [{ PRNo: \"" + filter.prNoMaster + "\", RONo: \"" + filter.roNoMaster + "\", SCNo: \"" + filter.preSCNo + "\", Unit: { Name: \"" + filter.unitMaster + "\" } }] }", Encoding.UTF8, General.JsonMediaType) });

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IHttpClientService)))
                .Returns(httpClientServiceMock.Object);

            var facade = new MonitoringPreSalesContractFacade(serviceProviderMock.Object);

            var Response = facade.Read(filter: JsonConvert.SerializeObject(filter));

            Assert.NotEqual(Response.Item2, 0);
        }

        [Fact]
        public async void Get_Error_Get_Garment_Purchase_Request_by_SCNo_Failed()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProviderMock = GetServiceProviderMock(dbContext);

            Mock<IHttpClientService> httpClientServiceMock = new Mock<IHttpClientService>();

            httpClientServiceMock
                .Setup(x => x.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.InternalServerError));

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IHttpClientService)))
                .Returns(httpClientServiceMock.Object);

            var serviceProvider = serviceProviderMock.Object;

            CostCalculationGarmentFacade costCalculationGarmentFacade = new CostCalculationGarmentFacade(serviceProvider, dbContext);

            await DataUtil(costCalculationGarmentFacade, serviceProvider, dbContext).GetTestData();

            var facade = new MonitoringPreSalesContractFacade(serviceProvider);
            var error = Assert.Throws<Exception>(() => facade.Read());

            Assert.NotNull(error);
        }

        [Fact]
        public void Get_Error_Get_SCNo_from_Garment_Purchase_Request_Failed()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProviderMock = GetServiceProviderMock(dbContext);

            Mock<IHttpClientService> httpClientServiceMock = new Mock<IHttpClientService>();

            httpClientServiceMock
                .Setup(x => x.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.InternalServerError));

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IHttpClientService)))
                .Returns(httpClientServiceMock.Object);

            var serviceProvider = serviceProviderMock.Object;

            var facade = new MonitoringPreSalesContractFacade(serviceProvider);

            var filter = new
            {
                prNoMaster = "PRNo"
            };

            var error = Assert.Throws<Exception>(() => facade.Read(filter: JsonConvert.SerializeObject(filter)));

            Assert.NotNull(error);
        }

        [Fact]
        public async void Get_Success_Excel()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProviderMock = GetServiceProviderMock(dbContext);

            var serviceProvider = serviceProviderMock.Object;

            GarmentPreSalesContractFacade garmentPreSalesContractFacade = new GarmentPreSalesContractFacade(serviceProvider, dbContext);
            GarmentPreSalesContractDataUtil garmentPreSalesContractDataUtil = new GarmentPreSalesContractDataUtil(garmentPreSalesContractFacade);
            var garmentPreSalesContractData1 = await garmentPreSalesContractDataUtil.GetNewData();
            await garmentPreSalesContractDataUtil.GetTestData(garmentPreSalesContractData1);
            var garmentPreSalesContractData2 = await garmentPreSalesContractDataUtil.GetNewData();
            await garmentPreSalesContractDataUtil.GetTestData(garmentPreSalesContractData2);
            var garmentPreSalesContractData3 = await garmentPreSalesContractDataUtil.GetNewData();
            await garmentPreSalesContractDataUtil.GetTestData(garmentPreSalesContractData3);

            CostCalculationGarmentFacade costCalculationGarmentFacade = new CostCalculationGarmentFacade(serviceProvider, dbContext);
            CostCalculationGarmentDataUtil costCalculationGarmentDataUtil = new CostCalculationGarmentDataUtil(costCalculationGarmentFacade, garmentPreSalesContractDataUtil);
            var costCalculationGarmentData11 = await costCalculationGarmentDataUtil.GetNewData(garmentPreSalesContractData1);
            await costCalculationGarmentDataUtil.GetTestData(costCalculationGarmentData11);
            var costCalculationGarmentData12 = await costCalculationGarmentDataUtil.GetNewData(garmentPreSalesContractData1);
            await costCalculationGarmentDataUtil.GetTestData(costCalculationGarmentData12);
            var costCalculationGarmentData2 = await costCalculationGarmentDataUtil.GetNewData(garmentPreSalesContractData2);
            await costCalculationGarmentDataUtil.GetTestData(costCalculationGarmentData2);

            Mock<IHttpClientService> httpClientServiceMock = new Mock<IHttpClientService>();

            httpClientServiceMock
                .Setup(x => x.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("{ data: [{ SCNo: \"" + garmentPreSalesContractData1.SCNo + "\", Unit: { Name: \"" + garmentPreSalesContractData1.SCNo + "\" } }, { SCNo: \"" + garmentPreSalesContractData2.SCNo + "\", Unit: { Name: \"" + garmentPreSalesContractData2.SCNo + "\" } }, { SCNo: \"" + garmentPreSalesContractData2.SCNo + "\", Unit: { Name: \"" + garmentPreSalesContractData2.SCNo + "\" } }] }", Encoding.UTF8, General.JsonMediaType) });

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IHttpClientService)))
                .Returns(httpClientServiceMock.Object);

            var facade = new MonitoringPreSalesContractFacade(serviceProviderMock.Object);

            var Response = facade.GenerateExcel();

            Assert.NotNull(Response.Item2);
        }

        [Fact]
        public void Get_Success_Empty_Excel()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            var facade = new MonitoringPreSalesContractFacade(serviceProvider);

            var Response = facade.GenerateExcel();

            Assert.NotNull(Response.Item2);
        }
    }
}
