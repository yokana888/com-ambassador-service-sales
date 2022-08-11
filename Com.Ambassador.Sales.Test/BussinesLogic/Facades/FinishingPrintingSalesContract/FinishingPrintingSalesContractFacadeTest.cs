using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.FinisihingPrintingSalesContract;
using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

using Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.FinishingPrintingProfiles;
using Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrinting;
using AutoMapper;

namespace Com.Ambassador.Sales.Test.BussinesLogic.Facades.FinishingPrintingSalesContract
{
    public class FinishingPrintingSalesContractFacadeTest : BaseFacadeTest<SalesDbContext, FinishingPrintingSalesContractFacade, FinishingPrintingSalesContractLogic, FinishingPrintingSalesContractModel, FinisihingPrintingSalesContractDataUtil>
    {
        private const string ENTITY = "FinishingPrintingSalesContract";
        public FinishingPrintingSalesContractFacadeTest() : base(ENTITY)
        {
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            var finishingprintingDetailLogic = new FinishingPrintingSalesContractDetailLogic(serviceProviderMock.Object, identityService, dbContext);
            var finishingprintingLogic = new FinishingPrintingSalesContractLogic(finishingprintingDetailLogic, serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(FinishingPrintingSalesContractLogic)))
                .Returns(finishingprintingLogic);

            return serviceProviderMock;
        }

        [Fact]
        public virtual async void Create_Buyer_Type_Ekspor_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            FinishingPrintingSalesContractFacade facade = new FinishingPrintingSalesContractFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetNewData();
            data.BuyerType = "ekspor";

            var response = await facade.CreateAsync(data);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<FinishingSalesContractMapper>();
                cfg.AddProfile<FinishingPrintingSalesContractDetailMapper>();
            });
            var mapper = configuration.CreateMapper();

            FinishingPrintingSalesContractViewModel vm = new FinishingPrintingSalesContractViewModel { Id = 1 };
            FinishingPrintingSalesContractModel model = mapper.Map<FinishingPrintingSalesContractModel>(vm);

            Assert.Equal(vm.Id, model.Id);

        }
    }
}
