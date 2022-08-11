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
using System.Runtime.CompilerServices;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.BussinesLogic.Facades.Garment.GarmentMerchandiser
{
    public class LatestAvailableROGarmentReportFacadeTest
    {
        private const string ENTITY = "LatestAvailableROGarmentReport";

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
                .Setup(x => x.GetService(typeof(LatestAvailableROGarmentReportLogic)))
                .Returns(new LatestAvailableROGarmentReportLogic(dbContext, identityService));

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
            jsonPatch.Replace(m => m.IsApprovedKadivMD, true);
            jsonPatch.Replace(m => m.ApprovedKadivMDBy, "Super Man");
            jsonPatch.Replace(m => m.ApprovedKadivMDDate, DateTimeOffset.Now);
            await costCalculationGarmentFacade.Patch(data.Id, jsonPatch);

            var filter = new
            {
                section = data.Section,
                availableDateStart = data.DeliveryDate,
                availableDateEnd = data.DeliveryDate,
            };

            var facade = new LatestAvailableROGarmentReportFacade(serviceProvider);
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
            jsonPatch.Replace(m => m.IsApprovedKadivMD, true);
            jsonPatch.Replace(m => m.ApprovedKadivMDBy, "Super Man");
            jsonPatch.Replace(m => m.ApprovedKadivMDDate, DateTimeOffset.Now);
            await costCalculationGarmentFacade.Patch(data.Id, jsonPatch);

            var filter = new
            {
                section = data.Section,
                availableDateStart = data.DeliveryDate.AddDays(-30),
                availableDateEnd = data.DeliveryDate.AddDays(30),
            };

            var facade = new LatestAvailableROGarmentReportFacade(serviceProvider);
            var Response = facade.GenerateExcel(filter: JsonConvert.SerializeObject(filter));

            Assert.NotNull(Response.Item2);
        }        
    }
}
