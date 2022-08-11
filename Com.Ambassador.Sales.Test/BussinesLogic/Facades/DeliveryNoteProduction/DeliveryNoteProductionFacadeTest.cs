using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.DeliveryNoteProduction;
using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.DeliveryNoteProduction;
using Com.Ambassador.Service.Sales.Lib.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Ambassador.Sales.Test.BussinesLogic.Facades.DeliveryNoteProduction
{
    public class DeliveryNoteProductionFacadeTest : BaseFacadeTest<SalesDbContext, DeliveryNoteProductionFacade, DeliveryNoteProductionLogic, DeliveryNoteProductionModel, DeliveryNoteProductionDataUtil>
    {

        private const string ENTITY = "DeliveryNoteProduction";
        public DeliveryNoteProductionFacadeTest() : base(ENTITY)
        {
        }

        

        protected override Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            var doSalesLogic = new DeliveryNoteProductionLogic(serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(DeliveryNoteProductionLogic)))
                .Returns(doSalesLogic);

            return serviceProviderMock;
        }

        [Fact]
        public async Task CreateAsync_Return_Success()
        {
            //Setup
            var dbContext = DbContext(GetCurrentAsyncMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            DeliveryNoteProductionFacade facade = new DeliveryNoteProductionFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade).GetNewData();

            //Act
            int result =await facade.CreateAsync(data);

            Assert.NotEqual(0, result);
        }


        [Fact]
        public async Task CreateAsync_Throws_Exception()
        {
            //Setup
            var dbContext = DbContext(GetCurrentAsyncMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            DeliveryNoteProductionFacade facade = new DeliveryNoteProductionFacade(serviceProvider, dbContext);
            //Act

            await Assert.ThrowsAsync<Exception>(() => facade.CreateAsync(null));
        }


        [Fact]
        public async Task UpdateAsync_Throws_Exception()
        {
            //Setup
            var dbContext = DbContext(GetCurrentAsyncMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            DeliveryNoteProductionFacade facade = new DeliveryNoteProductionFacade(serviceProvider, dbContext);
            var data = await DataUtil(facade).GetTestData();
            //Act

            //Assert
            await Assert.ThrowsAsync<Exception>(() => facade.UpdateAsync((int)data.Id,null));
        }

    }
}
