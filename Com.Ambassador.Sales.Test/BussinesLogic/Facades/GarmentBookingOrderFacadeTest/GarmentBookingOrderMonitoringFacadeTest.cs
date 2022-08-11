using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.GarmentBookingOrderDataUtils;
using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.GarmentBookingOrderFacade;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.GarmentBookingOrderInterface;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentBookingOrderLogics;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentBookingOrderModel;
using Com.Ambassador.Service.Sales.Lib.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.BussinesLogic.Facades.GarmentBookingOrderFacadeTest
{
    public class GarmentBookingOrderMonitoringFacadeTest
    {
        private const string ENTITY = "GarmentBookingOrderMonitoringReport";
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
                .Setup(x => x.GetService(typeof(GarmentBookingOrderLogic)))
                .Returns(new GarmentBookingOrderLogic(new GarmentBookingOrderItemLogic(identityService, serviceProviderMock.Object, dbContext), identityService, dbContext));

            return serviceProviderMock;
        }
        protected virtual GarmentBookingOrderDataUtil DataUtil(GarmentBookingOrderFacade facade, SalesDbContext dbContext = null)
        {
            GarmentBookingOrderDataUtil dataUtil = new GarmentBookingOrderDataUtil(facade);
            return dataUtil;
        }

        [Fact]
        public async void Get_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            GarmentBookingOrderFacade facade = new GarmentBookingOrderFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade).GetTestData();

            IGarmentBookingOrderMonitoringInterface garmentBookingOrderMonitoringFacade = new GarmentBookingOrderMonitoringFacade(serviceProvider, dbContext);


            var Response = garmentBookingOrderMonitoringFacade.Read(null, null, null, null, null, null, null, null,null,null, 1, 25, It.IsAny<string>(), It.IsAny<int>());

            Assert.NotEqual(Response.Item2, 0);
        }

        [Fact]
        public async void Get_Excel_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            GarmentBookingOrderFacade facade = new GarmentBookingOrderFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade).GetTestData();

            IGarmentBookingOrderMonitoringInterface monitoringGarmentBookingOrder = new GarmentBookingOrderMonitoringFacade(serviceProvider, dbContext);


            var Response = monitoringGarmentBookingOrder.GenerateExcel(null, null, null, null, null, null, null, null,null,null, It.IsAny<int>());

            // ???
            Assert.IsType(typeof(MemoryStream), Response);
        }
    }
}
