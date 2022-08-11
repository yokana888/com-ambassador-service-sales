using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.GarmentMasterPlan.WeeklyPlanDataUtils;
using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.GarmentMasterPlan.MonitoringFacades;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.GarmentMasterPlan.WeeklyPlanFacades;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.GarmentMasterPlan.MonitoringInterfaces;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.MonitoringLogics;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.WeeklyPlanLogics;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentMasterPlan.WeeklyPlanModels;
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

namespace Com.Ambassador.Sales.Test.BussinesLogic.Facades.GarmentMasterPlan.MonitoringFacadeTests
{
    public class MonitoringFacadeTest
    {
        private const string ENTITY = "GarmentMonitoringRemainingEH";

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
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(WeeklyPlanLogic)))
                .Returns(new WeeklyPlanLogic(identityService, dbContext));

            serviceProviderMock
                .Setup(x => x.GetService(typeof(MonitoringRemainingEHLogic)))
                .Returns(new MonitoringRemainingEHLogic(identityService, dbContext));

            return serviceProviderMock;
        }

        protected virtual WeeklyPlanDataUtil DataUtil(WeeklyPlanFacade facade, SalesDbContext dbContext = null)
        {
            WeeklyPlanDataUtil dataUtil = new WeeklyPlanDataUtil(facade);
            return dataUtil;
        }

        [Fact]
        public async void Get_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            WeeklyPlanFacade facade = new WeeklyPlanFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade).GetTestData();

            IMonitoringRemainingEHFacade monitoringRemainingEHFacade = new MonitoringRemainingEHFacade(serviceProvider, dbContext);

            var filter = new
            {
                year = data.Year
            };
            var Response = monitoringRemainingEHFacade.Read(filter:JsonConvert.SerializeObject(filter));

            Assert.NotEqual(Response.Item2, 0);
        }

        [Fact]
        public async void Get_Excel_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            WeeklyPlanFacade facade = new WeeklyPlanFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade).GetTestData();

            IMonitoringRemainingEHFacade monitoringRemainingEHFacade = new MonitoringRemainingEHFacade(serviceProvider, dbContext);

            var filter = new
            {
                data.Year
            };
            var Response = monitoringRemainingEHFacade.GenerateExcel(filter:JsonConvert.SerializeObject(filter));

            // ???
            Assert.NotNull(Response.Item2);
        }
    }
}
