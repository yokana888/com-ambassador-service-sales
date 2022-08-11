using AutoMapper;
using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.SpinningSalesContractDataUtil;
using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.Spinning;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.Spinning;
using Com.Ambassador.Service.Sales.Lib.Models.Spinning;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.ViewModels.Spinning;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.BussinesLogic.Facades.SpinningSalesContract
{
    public class SpinningSalesContractFacadeTest : BaseFacadeTest<SalesDbContext, SpinningSalesContractFacade, SpinningSalesContractLogic, SpinningSalesContractModel, SpinningSalesContractDataUtil>
    {
        private const string ENTITY = "SpinningSalesContract";
        public SpinningSalesContractFacadeTest() : base(ENTITY)
        {
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            
            var spinningLogic = new SpinningSalesContractLogic(serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(SpinningSalesContractLogic)))
                .Returns(spinningLogic);

            return serviceProviderMock;
        }

        [Fact]
        public virtual async void Create_Sales_Contract_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            SpinningSalesContractFacade facade = new SpinningSalesContractFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetNewData();
            data.BuyerType = "lokal";

            var response = await facade.CreateAsync(data);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Service.Sales.Lib.AutoMapperProfiles.SpinningProfiles.SpinningSalesContract>();
            });
            var mapper = configuration.CreateMapper();

            SpinningSalesContractViewModel vm = new SpinningSalesContractViewModel { Id = 1 };
            SpinningSalesContractModel model = mapper.Map<SpinningSalesContractModel>(vm);

            Assert.Equal(vm.Id, model.Id);

        }
    }
}
