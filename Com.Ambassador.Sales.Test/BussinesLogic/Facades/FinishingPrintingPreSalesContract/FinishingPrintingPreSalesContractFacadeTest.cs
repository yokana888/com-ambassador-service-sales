using AutoMapper;
using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.FinishingPrintingPreSalesContract;
using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.FinishingPrintingProfiles;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.BussinesLogic.Facades.FinishingPrintingPreSalesContract
{
    public class FinishingPrintingPreSalesContractFacadeTest : BaseFacadeTest<SalesDbContext, FinishingPrintingPreSalesContractFacade, FinishingPrintingPreSalesContractLogic, FinishingPrintingPreSalesContractModel, FinishingPrintingPreSalesContractDataUtil>
    {
        private const string ENTITY = "FinishingPrintingPreSalesContracts";
        public FinishingPrintingPreSalesContractFacadeTest() : base(ENTITY)
        {
        }

        [Fact]
        public virtual async void Create_Sample_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            FinishingPrintingPreSalesContractFacade facade = new FinishingPrintingPreSalesContractFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();
            data.Type = "SAMPLE";
            data.Id = 0;
            data.UnitCode = "F1";
            var response = await facade.CreateAsync(data);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public virtual async void Create_Sample_Success2()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            FinishingPrintingPreSalesContractFacade facade = new FinishingPrintingPreSalesContractFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();
            data.Type = "SAMPLE";
            data.Id = 0;
            var response = await facade.CreateAsync(data);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public async void PreSalesPost_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            FinishingPrintingPreSalesContractFacade facade = new FinishingPrintingPreSalesContractFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();
            List<long> listData = new List<long> { data.Id };
            var Response = await facade.PreSalesPost(listData);
            Assert.NotEqual(Response, 0);
        }

        [Fact]
        public async void PreSalesUnPost_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            FinishingPrintingPreSalesContractFacade facade = new FinishingPrintingPreSalesContractFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();
            var Response = await facade.PreSalesUnpost(data.Id);
            Assert.NotEqual(Response, 0);
        }

        [Fact]
        public void Validate_VM()
        {
            FinishingPrintingPreSalesContractViewModel vm = new FinishingPrintingPreSalesContractViewModel()
            {
                No = "no",
                Remark = "remark",
                IsPosted = true
            };
            Assert.NotNull(vm.No);
            Assert.NotNull(vm.Remark);
            Assert.True(vm.IsPosted);

            var response = vm.Validate(null);

            Assert.NotEmpty(response);

            vm.Date = DateTimeOffset.UtcNow;
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.Buyer = new BuyerViewModel()
            {
                Id = 1
            };
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.Unit = new UnitViewModel()
            {
                Id = 1
            };
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.ProcessType = new ProcessTypeViewModel()
            {
                Id = 1
            };
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.Type = "type";
            vm.OrderQuantity = -1;
            response = vm.Validate(null);
            Assert.NotEmpty(response);

            vm.OrderQuantity = 1;
            response = vm.Validate(null);
            Assert.Empty(response);
        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg => {
                cfg.AddProfile<FinishingPrintingPreSalesContractMapper>();
            });
            var mapper = configuration.CreateMapper();

            FinishingPrintingPreSalesContractViewModel vm = new FinishingPrintingPreSalesContractViewModel { Id = 1 };
            FinishingPrintingPreSalesContractModel model = mapper.Map<FinishingPrintingPreSalesContractModel>(vm);

            Assert.Equal(vm.Id, model.Id);

        }
    }
}
