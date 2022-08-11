using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.GarmentPreSalesContractDataUtils;
using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.GarmentPreSalesContractFacades;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentPreSalesContractLogics;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentPreSalesContractModel;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentPreSalesContractViewModels;
using Microsoft.AspNetCore.JsonPatch;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Ambassador.Sales.Test.BussinesLogic.Facades.GarmentPreSalesContractTest
{
    public class GarmentPreSalesContractTest : BaseFacadeTest<SalesDbContext, GarmentPreSalesContractFacade, GarmentPreSalesContractLogic, GarmentPreSalesContract, GarmentPreSalesContractDataUtil>
    {
        private const string ENTITY = "GarmentPreSalesContracts";

        public GarmentPreSalesContractTest() : base(ENTITY)
        {
        }

        [Fact]
        public async void Patch_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            GarmentPreSalesContractFacade facade = new GarmentPreSalesContractFacade(serviceProvider, dbContext);
            var data = await DataUtil(facade, dbContext).GetTestData();

            JsonPatchDocument<GarmentPreSalesContract> jsonPatch = new JsonPatchDocument<GarmentPreSalesContract>();
            jsonPatch.Replace(m => m.IsPosted, true);

            int Response = await facade.Patch(data.Id, jsonPatch);
            Assert.NotEqual(Response, 0);

            var ResultData = await facade.ReadByIdAsync((int)data.Id);
            Assert.Equal(ResultData.IsPosted, true);
        }

        [Fact]
        public async void Patch_Error()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            GarmentPreSalesContractFacade facade = new GarmentPreSalesContractFacade(serviceProvider, dbContext);
            var data = await DataUtil(facade, dbContext).GetTestData();

            JsonPatchDocument<GarmentPreSalesContract> jsonPatch = new JsonPatchDocument<GarmentPreSalesContract>();
            jsonPatch.Replace(m => m.Id, 0);

            var Response = await Assert.ThrowsAnyAsync<Exception>(async () => await facade.Patch(data.Id, jsonPatch));
            Assert.NotEqual(Response.Message, null);
        }

        [Fact]
        public async void PreSalesPost_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            GarmentPreSalesContractFacade facade = new GarmentPreSalesContractFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade).GetTestData();
            List<long> listData = new List<long> { data.Id };
            var Response = await facade.PreSalesPost(listData,"test");
            Assert.NotEqual(Response, 0);
        }

        [Fact]
        public async void PreSalesPost_Throws_Exception()
        {
            //Setup
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            //Act
            GarmentPreSalesContractFacade facade = new GarmentPreSalesContractFacade(serviceProvider, dbContext);

            //Assert
            await Assert.ThrowsAsync<Exception>(() => facade.PreSalesPost(null, null));
        }

        [Fact]
        public async void PreSalesUnPost_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            GarmentPreSalesContractFacade facade = new GarmentPreSalesContractFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade).GetTestData();
            var Response = await facade.PreSalesUnpost(data.Id, "test");
            Assert.NotEqual(Response, 0);
        }

        [Fact]
        public async Task PreSalesUnPost_Throws_Exception()
        {
            //Setup
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            //Act
            GarmentPreSalesContractFacade facade = new GarmentPreSalesContractFacade(serviceProvider, dbContext);

            //Assert
            await Assert.ThrowsAsync<Exception>(() => facade.PreSalesUnpost(1,null));
        }

        [Fact]
        public void Validate_ViewModel()
        {
            GarmentPreSalesContractViewModel viewModel = new GarmentPreSalesContractViewModel();

            var response = viewModel.Validate(new ValidationContext(viewModel, null, null));

            Assert.NotEmpty(response.ToList());
        }
    }
}