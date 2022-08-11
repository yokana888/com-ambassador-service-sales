using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentBookingOrderLogics;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentBookingOrderModel;
using Com.Ambassador.Service.Sales.Lib.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Ambassador.Sales.Test.BussinesLogic.Logic.GarmentBookingOrderLogics
{
    public class GarmentBookingOrderItemLogicTest
    {
        private const string ENTITY = "DOSalesLocalItems";

        [MethodImpl(MethodImplOptions.NoInlining)]
        public string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return string.Concat(sf.GetMethod().Name, "_", ENTITY);
        }

        private SalesDbContext _dbContext(string testName)
        {
            DbContextOptionsBuilder<SalesDbContext> optionsBuilder = new DbContextOptionsBuilder<SalesDbContext>();
            optionsBuilder
                .UseInMemoryDatabase(testName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            SalesDbContext dbContext = new SalesDbContext(optionsBuilder.Options);

            return dbContext;
        }

        public Mock<IServiceProvider> GetServiceProvider(string testname)
        {
            IIdentityService identityService = new IdentityService { Username = "Username" };
            var serviceProvider = new Mock<IServiceProvider>();

            serviceProvider
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            serviceProvider.Setup(s => s.GetService(typeof(SalesDbContext)))
                .Returns(_dbContext(testname));

            return serviceProvider;
        }

        [Fact]
        public async Task Read_With_EmptyKeyword_Return_Success()
        {
            string testName = GetCurrentMethod();
            var dbContext = _dbContext(testName);
            IIdentityService identityService = new IdentityService { Username = "Username" };

            var model = new GarmentBookingOrderItem()
            {
                GarmentBookingOrder =new GarmentBookingOrder()
            };

            dbContext.GarmentBookingOrderItems.Add(model);
            dbContext.SaveChanges();

            GarmentBookingOrderItemLogic unitUnderTest = new GarmentBookingOrderItemLogic( identityService, GetServiceProvider(testName).Object, dbContext);

            await unitUnderTest.DeleteAsync(model.Id);
           
        }

        [Fact]
        public void GetBookingOrderIds_Return_Success()
        {
            string testName = GetCurrentMethod();
            var dbContext = _dbContext(testName);
            IIdentityService identityService = new IdentityService { Username = "Username" };

            var model = new GarmentBookingOrderItem()
            {
                GarmentBookingOrder = new GarmentBookingOrder()
            };

            dbContext.GarmentBookingOrderItems.Add(model);
            dbContext.SaveChanges();

            GarmentBookingOrderItemLogic unitUnderTest = new GarmentBookingOrderItemLogic(identityService, GetServiceProvider(testName).Object, dbContext);

            HashSet<long> result = unitUnderTest.GetBookingOrderIds(model.Id);
            Assert.NotNull(result);

        }
    }
}
