using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.DOReturn;
using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.DOReturn;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.DOReturn;
using Com.Ambassador.Service.Sales.Lib.Models.DOReturn;
using Com.Ambassador.Service.Sales.Lib.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Com.Ambassador.Sales.Test.BussinesLogic.Facades.DOReturn
{
    public class DOReturnFacadeTest : BaseFacadeTest<SalesDbContext, DOReturnFacade, DOReturnLogic, DOReturnModel, DOReturnDataUtil>
    {
        private const string ENTITY = "DOReturn";
        public DOReturnFacadeTest() : base(ENTITY)
        {
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            var doReturnItemLogic = new DOReturnItemLogic(serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(DOReturnItemLogic)))
                .Returns(doReturnItemLogic);

            var doReturnDetailItemLogic = new DOReturnDetailItemLogic(serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(DOReturnDetailItemLogic)))
                .Returns(doReturnDetailItemLogic);

            var doReturnDetailLogic = new DOReturnDetailLogic(serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(DOReturnDetailLogic)))
                .Returns(doReturnDetailLogic);

            var doReturnLogic = new DOReturnLogic(serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(DOReturnLogic)))
                .Returns(doReturnLogic);

            return serviceProviderMock;
        }

        [Fact]
        public async Task CreateAsync_Return_Success()
        {
            //Setup
            var dbContext = DbContext(GetCurrentAsyncMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            DOReturnFacade facade = new DOReturnFacade(serviceProvider, dbContext);

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

            DOReturnFacade facade = new DOReturnFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();

            await Assert.ThrowsAsync<Exception>(() => facade.CreateAsync(null));
        }

        [Fact]
        public async void Delete_DoReturn_Detail()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            DOReturnFacade facade = Activator.CreateInstance(typeof(DOReturnFacade), serviceProvider, dbContext) as DOReturnFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();
            var response = await facade.UpdateAsync((int)data.Id, data);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public async void Update_DoReturn_Detail()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            DOReturnFacade facade = Activator.CreateInstance(typeof(DOReturnFacade), serviceProvider, dbContext) as DOReturnFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();

            data.DOReturnDetails = new List<DOReturnDetailModel>()
            {
                new DOReturnDetailModel()
                {
                    DOReturnDetailItems = new List<DOReturnDetailItemModel>()
                    {
                        new DOReturnDetailItemModel(){ },
                    },
                    DOReturnItems = new List<DOReturnItemModel>()
                    {
                        new DOReturnItemModel(){ },
                    },
                }
            };

            var response = await facade.UpdateAsync((int)data.Id, data);

            Assert.NotEqual(response, 0);
        }
    }
}
