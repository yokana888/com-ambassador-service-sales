using AutoMapper;
using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils;
using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic;
using Com.Ambassador.Service.Sales.Lib.Models;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Ambassador.Sales.Test.BussinesLogic.Facades
{
    public class EfficiencyFacadeTest : BaseFacadeTest<SalesDbContext, EfficiencyFacade, EfficiencyLogic, Efficiency, EfficiencyDataUtil>
    {
        private const string ENTITY = "Efficiency";
        public EfficiencyFacadeTest() : base(ENTITY)
        {
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(EfficiencyLogic)))
                .Returns(Activator.CreateInstance(typeof(EfficiencyLogic), serviceProviderMock.Object, identityService, dbContext) as EfficiencyLogic);

            return serviceProviderMock;
        }

        public override async void Get_All_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            EfficiencyFacade facade = Activator.CreateInstance(typeof(EfficiencyFacade), serviceProvider, dbContext) as EfficiencyFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.Read(1, 25, "{}", new List<string>(), null, "{}");

            Assert.NotEqual(Response.Data.Count, 0);
        }

        [Fact]
        public async Task ReadByQuantity()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            EfficiencyFacade facade = Activator.CreateInstance(typeof(EfficiencyFacade), serviceProvider, dbContext) as EfficiencyFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = await facade.ReadModelByQuantity(1);

            Assert.NotNull(Response);
        }

        [Fact]
        public async Task ReadByQuantityNull()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            EfficiencyFacade facade = Activator.CreateInstance(typeof(EfficiencyFacade), serviceProvider, dbContext) as EfficiencyFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = await facade.ReadModelByQuantity(0);

            Assert.NotNull(Response);
        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EfficiencyMapper>();
            });
            var mapper = configuration.CreateMapper();

            EfficiencyViewModel vm = new EfficiencyViewModel { Id = 1 };
            Efficiency model = mapper.Map<Efficiency>(vm);

            Assert.Equal(vm.Id, model.Id);

        }
    }
}
