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
using Com.Ambassador.Service.Sales.Lib.Services;
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
    public class MonitoringUnpostCostCalculationFacadeTest
    {
        private const string ENTITY = "UnpostCostCalculationMonitoring";

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
                .Setup(x => x.GetService(typeof(MonitoringUnpostCostCalculationLogic)))
                .Returns(new MonitoringUnpostCostCalculationLogic(dbContext));

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
            await costCalculationGarmentFacade.PostCC(new List<long> { data.Id });
            await costCalculationGarmentFacade.UnpostCC(data.Id, "Alasan Unpost");

            var facade = new MonitoringUnpostCostCalculationFacade(serviceProvider);
            var Response = facade.Read();

            Assert.NotEqual(Response.Item2, 0);
        }

        [Fact]
        public async void Get_Success_Excel()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            CostCalculationGarmentFacade costCalculationGarmentFacade = new CostCalculationGarmentFacade(serviceProvider, dbContext);

            var data = await DataUtil(costCalculationGarmentFacade, serviceProvider, dbContext).GetTestData();
            await costCalculationGarmentFacade.PostCC(new List<long> { data.Id });
            await costCalculationGarmentFacade.UnpostCC(data.Id, "Alasan Unpost");
            await costCalculationGarmentFacade.PostCC(new List<long> { data.Id });
            await costCalculationGarmentFacade.UnpostCC(data.Id, "Alasan Unpost");

            var facade = new MonitoringUnpostCostCalculationFacade(serviceProvider);

            var filter = new
            {
                data.Section,
                data.RO_Number,
                data.PreSCNo,
                data.UnitCode,
                data.CreatedBy
            };
            var Response = facade.GenerateExcel(filter: JsonConvert.SerializeObject(filter));

            Assert.NotNull(Response.Item2);
        }

        [Fact]
        public void Get_Success_Empty_Excel()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            var facade = new MonitoringUnpostCostCalculationFacade(serviceProvider);

            var Response = facade.GenerateExcel();

            Assert.NotNull(Response.Item2);
        }
    }
}
