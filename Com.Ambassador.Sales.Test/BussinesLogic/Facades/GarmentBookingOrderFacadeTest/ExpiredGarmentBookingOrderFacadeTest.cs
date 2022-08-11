using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.GarmentBookingOrderDataUtils;
using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.GarmentBookingOrderFacade;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.GarmentBookingOrderInterface;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentBookingOrderLogics;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentBookingOrderModel;
using Com.Ambassador.Service.Sales.Lib.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Ambassador.Sales.Test.BussinesLogic.Facades.GarmentBookingOrderFacadeTest
{
    public class ExpiredGarmentBookingOrderFacadeTest
    {
        private const string ENTITY = "ExpiredGarmentBookingOrder";
        [MethodImpl(MethodImplOptions.NoInlining)]
        private string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return string.Concat(sf.GetMethod().Name, "_", ENTITY);
        }
        private SalesDbContext DbContext(string testName)
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            DbContextOptionsBuilder<SalesDbContext> optionsBuilder = new DbContextOptionsBuilder<SalesDbContext>();
            optionsBuilder
                .UseInMemoryDatabase(testName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .UseInternalServiceProvider(serviceProvider);

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

        protected virtual ExpiredGarmentBookingOrderDataUtil DataUtil(ExpiredGarmentBookingOrderFacade facade, SalesDbContext dbContext = null)
        {
            ExpiredGarmentBookingOrderDataUtil dataUtil = new ExpiredGarmentBookingOrderDataUtil(facade);
            return dataUtil;
        }

        [Fact]
        public async void Read_Return_Success()
        {
            //Setup
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            IExpiredGarmentBookingOrder expiredGarmentBookingOrder = new ExpiredGarmentBookingOrderFacade(serviceProvider, dbContext);
            ExpiredGarmentBookingOrderFacade facade = new ExpiredGarmentBookingOrderFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade).GetTestData();

            //Act
            var Response = expiredGarmentBookingOrder.Read(1, 25, "{}", new List<string>(), "", "{}");

            Assert.NotEqual( 0, Response.Data.Count);
        }

        [Fact]
        public async void ReadByIdAsync_Return_Success()
        {
            //Setup
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            IExpiredGarmentBookingOrder expiredGarmentBookingOrder = new ExpiredGarmentBookingOrderFacade(serviceProvider, dbContext);
            ExpiredGarmentBookingOrderFacade facade = new ExpiredGarmentBookingOrderFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade).GetTestData();

            //Act
            var Response = expiredGarmentBookingOrder.ReadByIdAsync((int)data.Id);

            Assert.NotEqual(0, Response.Id);
        }


        [Fact]
        public async Task DeleteAsync_Return_Success()
        {
            //Setup
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            IExpiredGarmentBookingOrder expiredGarmentBookingOrder = new ExpiredGarmentBookingOrderFacade(serviceProvider, dbContext);
            ExpiredGarmentBookingOrderFacade facade = new ExpiredGarmentBookingOrderFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade).GetTestData();

            //Act
            int Response = await expiredGarmentBookingOrder.DeleteAsync((int)data.Id);

            Assert.NotEqual(0, Response);
        }

        [Fact]
        public async Task UpdateAsync_withExistData_Return_Success()
        {
            //Setup
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            IExpiredGarmentBookingOrder expiredGarmentBookingOrder = new ExpiredGarmentBookingOrderFacade(serviceProvider, dbContext);
            ExpiredGarmentBookingOrderFacade facade = new ExpiredGarmentBookingOrderFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade).GetTestData();
         

            //Act
            int Response = await expiredGarmentBookingOrder.UpdateAsync((int)data.Id, data);

            Assert.NotEqual(0, Response);
        }

        [Fact]
        public async void Get_Success_ReadExpired()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            ExpiredGarmentBookingOrderFacade facade = new ExpiredGarmentBookingOrderFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade).GetTestData();

            IExpiredGarmentBookingOrder expiredGarmentBookingOrder = new ExpiredGarmentBookingOrderFacade(serviceProvider, dbContext);


            var Response = expiredGarmentBookingOrder.ReadExpired(1, 25, "{}", new List<string>(), "", "{}");

            Assert.NotEqual(Response.Data.Count, 0);
        }

        [Fact]
        public virtual async void Update_Success_ReadExpired()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            ExpiredGarmentBookingOrderFacade facade = Activator.CreateInstance(typeof(ExpiredGarmentBookingOrderFacade), serviceProvider, dbContext) as ExpiredGarmentBookingOrderFacade;

            var data = await DataUtil(facade).GetTestData();
            data.IsBlockingPlan = false;
            data.Remark = "test";
            var listData = new List<GarmentBookingOrder> { data };

            var response = facade.BOCancelExpired(listData, "");

            Assert.NotEqual(response, 0);
        }
    }
}
