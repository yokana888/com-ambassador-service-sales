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
using Com.Ambassador.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Ambassador.Service.Sales.Lib.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.BussinesLogic.Facades.Garment.GarmentMerchandiser
{
    public class LatestAvailableBudgetReportFacadeTest
    {
        private const string ENTITY = "LatestAvailableBudgetReport";

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
            HttpResponseMessage message = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            message.Content = new StringContent("{\"apiVersion\":\"1.0\",\"statusCode\":200,\"message\":\"Ok\",\"data\":[{\"Id\":7,\"code\":\"USD\",\"rate\":13700.0,\"date\":\"2018/10/20\"}],\"info\":{\"count\":1,\"page\":1,\"size\":1,\"total\":2,\"order\":{\"date\":\"desc\"},\"select\":[\"Id\",\"code\",\"rate\",\"date\"]}}");

            var serviceProviderMock = new Mock<IServiceProvider>();
            var clientServiceMock = new Mock<IHttpClientService>();

            clientServiceMock
               .Setup(x => x.GetAsync(It.IsAny<string>()))
               .ReturnsAsync(message);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IHttpClientService)))
                .Returns(clientServiceMock.Object);

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
                .Setup(x => x.GetService(typeof(LatestAvailableBudgetReportLogic)))
                .Returns(new LatestAvailableBudgetReportLogic(dbContext, identityService));

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
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            CostCalculationGarmentFacade costCalculationGarmentFacade = new CostCalculationGarmentFacade(serviceProvider, dbContext);

            var data = await DataUtil(costCalculationGarmentFacade, serviceProvider, dbContext).GetTestData();
            var AvailableBy = "AvailableBy";
            await costCalculationGarmentFacade.AcceptanceCC(new List<long> { data.Id }, AvailableBy);
            await costCalculationGarmentFacade.AvailableCC(new List<long> { data.Id }, AvailableBy);
            JsonPatchDocument<CostCalculationGarment> jsonPatch = new JsonPatchDocument<CostCalculationGarment>();
            jsonPatch.Replace(m => m.IsApprovedPPIC, true);
            jsonPatch.Replace(m => m.ApprovedPPICBy, "Super Man");
            jsonPatch.Replace(m => m.ApprovedPPICDate, DateTimeOffset.Now);
            await costCalculationGarmentFacade.Patch(data.Id, jsonPatch);

            var filter = new
            {
                section = data.Section,
                availableDateStart = data.DeliveryDate,
                availableDateEnd = data.DeliveryDate,
            };

            var facade = new LatestAvailableBudgetReportFacade(serviceProvider);
            var Response = facade.Read(filter: JsonConvert.SerializeObject(filter));

            Assert.NotEqual(Response.Item2, 0);
        }

        [Fact]
        public async void Get_Success_Excel()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            CostCalculationGarmentFacade costCalculationGarmentFacade = new CostCalculationGarmentFacade(serviceProvider, dbContext);

            var data = await DataUtil(costCalculationGarmentFacade, serviceProvider, dbContext).GetNewData();
            await costCalculationGarmentFacade.CreateAsync(data);
            var data1 = await DataUtil(costCalculationGarmentFacade, serviceProvider, dbContext).GetNewData();
            data1.LeadTime = 40;
            await costCalculationGarmentFacade.CreateAsync(data1);
            var AvailableBy = "AvailableBy";
            await costCalculationGarmentFacade.AcceptanceCC(new List<long> { data.Id }, AvailableBy);
            await costCalculationGarmentFacade.AvailableCC(new List<long> { data.Id }, AvailableBy);
            JsonPatchDocument<CostCalculationGarment> jsonPatch = new JsonPatchDocument<CostCalculationGarment>();
            jsonPatch.Replace(m => m.IsApprovedPPIC, true);
            jsonPatch.Replace(m => m.ApprovedPPICBy, "Super Man");
            jsonPatch.Replace(m => m.ApprovedPPICDate, DateTimeOffset.Now);
            await costCalculationGarmentFacade.Patch(data.Id, jsonPatch);

            var filter = new
            {
                section = data.Section,    
                availableDateStart = data.DeliveryDate.AddDays(-30),
                availableDateEnd = data.DeliveryDate.AddDays(30),
            };

            var facade = new LatestAvailableBudgetReportFacade(serviceProvider);
            var Response = facade.GenerateExcel(filter: JsonConvert.SerializeObject(filter));

            Assert.NotNull(Response.Item2);
        }
    }
}
