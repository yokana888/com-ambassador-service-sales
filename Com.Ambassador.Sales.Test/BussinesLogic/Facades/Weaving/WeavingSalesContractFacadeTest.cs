using AutoMapper;
using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.Weaving;
using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.Weaving;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.Weaving;
using Com.Ambassador.Service.Sales.Lib.Models.Weaving;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.ViewModels.Weaving;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.BussinesLogic.Facades.Weaving
{
    public class WeavingSalesContractFacadeTest : BaseFacadeTest<SalesDbContext, WeavingSalesContractFacade, WeavingSalesContractLogic, WeavingSalesContractModel, WeavingSalesContractDataUtil>
    {
        private const string ENTITY = "SpinningSalesContract";
        public WeavingSalesContractFacadeTest() : base(ENTITY)
        {
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);


            var spinningLogic = new WeavingSalesContractLogic(serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(WeavingSalesContractLogic)))
                .Returns(spinningLogic);

            return serviceProviderMock;
        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Service.Sales.Lib.AutoMapperProfiles.WeavingProfiles.WeavingSalesContract>();
            });
            var mapper = configuration.CreateMapper();

            WeavingSalesContractViewModel vm = new WeavingSalesContractViewModel { Id = 1 };
            WeavingSalesContractModel model = mapper.Map<WeavingSalesContractModel>(vm);

            Assert.Equal(vm.Id, model.Id);

        }
    }
}
