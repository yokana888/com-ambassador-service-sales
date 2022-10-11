using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.Garment.GarmentMerchandiser;
using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.GarmentPreSalesContractDataUtils;
using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.GarmentSalesContractDataUtils;
using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.CostCalculationGarments;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.GarmentPreSalesContractFacades;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.GarmentSalesContractFacades;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.CostCalculationGarmentLogic;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.CostCalculationGarments;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.MonitoringLogics;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentPreSalesContractLogics;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentSalesContractLogics;
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
using System.Threading.Tasks;
using Xunit;

namespace Com.Ambassador.Sales.Test.BussinesLogic.Facades.Garment.GarmentMerchandiser
{
    public class CostCalculationByBuyer2ReportTest 
   {
        private const string ENTITY = "CostCalculationByBuyer2Report";

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

            GarmentPreSalesContractLogic garmentPreSalesContractLogic = new GarmentPreSalesContractLogic(identityService, dbContext);

            CostCalculationGarmentMaterialLogic costCalculationGarmentMaterialLogic = new CostCalculationGarmentMaterialLogic(serviceProviderMock.Object, identityService, dbContext);
            CostCalculationGarmentLogic costCalculationGarmentLogic = new CostCalculationGarmentLogic(costCalculationGarmentMaterialLogic, serviceProviderMock.Object, identityService, dbContext);
            CostCalculationByBuyer2ReportLogic costCalculationByBuyer2ReportLogic = new CostCalculationByBuyer2ReportLogic(identityService, dbContext);

            Mock<ICostCalculationGarment> mockCostCalculation = new Mock<ICostCalculationGarment>();
            mockCostCalculation.Setup(x => x.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new CostCalculationGarment()); 
            
            var azureImageFacadeMock = new Mock<IAzureImageFacade>();
            azureImageFacadeMock
                .Setup(s => s.DownloadImage(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync("");

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);
            serviceProviderMock
                .Setup(x => x.GetService(typeof(CostCalculationGarmentLogic)))
                .Returns(costCalculationGarmentLogic);
            serviceProviderMock
                .Setup(x => x.GetService(typeof(GarmentPreSalesContractLogic)))
                .Returns(garmentPreSalesContractLogic);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(ICostCalculationGarment)))
                .Returns(mockCostCalculation.Object);

            GarmentSalesContractROLogic garmentSalesContractROLogic = new GarmentSalesContractROLogic(serviceProviderMock.Object, identityService, dbContext);
            GarmentSalesContractItemLogic garmentSalesContractItemLogic = new GarmentSalesContractItemLogic(serviceProviderMock.Object, identityService, dbContext);
            GarmentSalesContractLogic garmentSalesContractLogic = new GarmentSalesContractLogic(garmentSalesContractROLogic,garmentSalesContractItemLogic, serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(GarmentSalesContractItemLogic)))
                .Returns(garmentSalesContractItemLogic);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(GarmentSalesContractLogic)))
                .Returns(garmentSalesContractLogic);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(CostCalculationByBuyer2ReportLogic)))
                .Returns(costCalculationByBuyer2ReportLogic);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IAzureImageFacade)))
                .Returns(azureImageFacadeMock.Object);

            return serviceProviderMock;
        }

        protected GarmentSalesContractDataUtil DataUtil(GarmentSalesContractFacade facade, SalesDbContext dbContext = null)
        {
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            GarmentPreSalesContractFacade garmentPreSalesContractFacade = new GarmentPreSalesContractFacade(serviceProvider, dbContext);
            GarmentPreSalesContractDataUtil garmentPreSalesContractDataUtil = new GarmentPreSalesContractDataUtil(garmentPreSalesContractFacade);

            CostCalculationGarmentFacade costCalculationGarmentFacade = new CostCalculationGarmentFacade(serviceProvider, dbContext);
            CostCalculationGarmentDataUtil costCalculationGarmentDataUtil = new CostCalculationGarmentDataUtil(costCalculationGarmentFacade, garmentPreSalesContractDataUtil);

            GarmentSalesContractFacade garmentSalesContractFacade = new GarmentSalesContractFacade(serviceProvider, dbContext);
            GarmentSalesContractDataUtil garmentSalesContractDataUtil = new GarmentSalesContractDataUtil(garmentSalesContractFacade, costCalculationGarmentDataUtil);

            return garmentSalesContractDataUtil;
        }

        [Fact]
        public async void Get_Report_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            GarmentSalesContractFacade facade = new GarmentSalesContractFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();

            ICostCalculationGarmentByBuyer2Report costCalculationGarmentByBuyer2Report = new CostCalculationGarmentByBuyer2ReportFacade(serviceProvider, dbContext);

            var filter = new
            {
              buyerAgent = "Test",
              buyerBrand = data.BuyerBrandCode,
              year = DateTimeOffset.Now.Year,
            };

            var Response = costCalculationGarmentByBuyer2Report.Read(filter: JsonConvert.SerializeObject(filter));

            Assert.NotEqual(Response.Item2, 0);
        }

        [Fact]
        public async void Get_Excel_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            GarmentSalesContractFacade facade = new GarmentSalesContractFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();

            ICostCalculationGarmentByBuyer2Report costCalculationGarmentByBuyer2Report = new CostCalculationGarmentByBuyer2ReportFacade(serviceProvider, dbContext);

            var filter = new
            {
                buyerAgent = "Test",
                buyerBrand = data.BuyerBrandCode,
                year = DateTimeOffset.Now.Year,
            };

            var Response = costCalculationGarmentByBuyer2Report.GenerateExcel(filter: JsonConvert.SerializeObject(filter));
           
            Assert.NotNull(Response.Item2);
        }    
    }
}