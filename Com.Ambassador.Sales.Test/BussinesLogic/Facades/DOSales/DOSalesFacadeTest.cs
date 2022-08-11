using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.DOSales;
using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.DOSales;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.DOSales;
using Com.Ambassador.Service.Sales.Lib.Models.DOSales;
using Com.Ambassador.Service.Sales.Lib.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Com.Ambassador.Sales.Test.BussinesLogic.Facades.DOSales
{
    public class DOSalesFacadeTest : BaseFacadeTest<SalesDbContext, DOSalesFacade, DOSalesLogic, DOSalesModel, DOSalesDataUtil>
    {
        private const string ENTITY = "DOSales";
        public DOSalesFacadeTest() : base(ENTITY)
        {
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            var doSalesLocalLogic = new DOSalesDetailLogic(serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(DOSalesDetailLogic)))
                .Returns(doSalesLocalLogic);

            var doSalesLogic = new DOSalesLogic(serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(DOSalesLogic)))
                .Returns(doSalesLogic);

            return serviceProviderMock;
        }

        [Fact]
        public async Task CreateAsync_Return_Success()
        {
            //Setup
            var dbContext = DbContext(GetCurrentAsyncMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            DOSalesFacade facade = new DOSalesFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade).GetNewData();

            //Act
            int result = await facade.CreateAsync(data);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task CreateAsync_Throws_Exception()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            DOSalesFacade facade = new DOSalesFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();

            await Assert.ThrowsAsync<Exception>(() => facade.CreateAsync(null));
        }


        [Fact]
        public async Task UpdateAsync_Success()
        {
            //Setup
            var dbContext = DbContext(GetCurrentAsyncMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            DOSalesFacade facade = new DOSalesFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade).GetTestData();
            var NewData = await DataUtil(facade).GetNewData();
            //Act
            int result = await facade.UpdateAsync((int)data.Id, NewData);
            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Get_DPStock()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            DOSalesFacade facade = new DOSalesFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.ReadDPAndStock(1, 25, "{}", new List<string>(), "", "{}");

            Assert.NotEqual(Response.Data.Count, 0);
        }
    }
}
