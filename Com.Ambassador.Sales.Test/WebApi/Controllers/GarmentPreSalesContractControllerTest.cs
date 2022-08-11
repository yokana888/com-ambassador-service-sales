using AutoMapper;
using Com.Ambassador.Sales.Test.WebApi.Utils;
using Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.GarmentPreSalesContractProfiles;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.GarmentPreSalesContractInterface;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentPreSalesContractModel;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentPreSalesContractViewModels;
using Com.Ambassador.Service.Sales.WebApi.Controllers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Ambassador.Sales.Test.WebApi.Controllers
{
    public class GarmentPreSalesContractControllerTest : BaseControllerTest<GarmentPreSalesContractController, GarmentPreSalesContract, GarmentPreSalesContractViewModel, IGarmentPreSalesContract>
    {
        private async Task<int> GetStatusCodePatch((Mock<IIdentityService> IdentityService, Mock<IValidateService> ValidateService, Mock<IGarmentPreSalesContract> Facade, Mock<IMapper> Mapper, Mock<IServiceProvider> ServiceProvider) mocks, int id)
        {
            GarmentPreSalesContractController controller = GetController(mocks);

            JsonPatchDocument<GarmentPreSalesContract> patch = new JsonPatchDocument<GarmentPreSalesContract>();
            IActionResult response = await controller.Patch(id, patch);

            return this.GetStatusCode(response);
        }

        private async Task<int> GetPreSalesPost((Mock<IIdentityService> IdentityService, Mock<IValidateService> ValidateService, Mock<IGarmentPreSalesContract> Facade, Mock<IMapper> Mapper, Mock<IServiceProvider> ServiceProvider) mocks, int id)
        {
            GarmentPreSalesContractController controller = GetController(mocks);
            List<long> Id = new List<long> { 1};
            IActionResult response = await controller.PreSalesPost(Id);

            return this.GetStatusCode(response);
        }

        private async Task<int> GetPreSalesUnPost((Mock<IIdentityService> IdentityService, Mock<IValidateService> ValidateService, Mock<IGarmentPreSalesContract> Facade, Mock<IMapper> Mapper, Mock<IServiceProvider> ServiceProvider) mocks, int id)
        {
            GarmentPreSalesContractController controller = GetController(mocks);
            IActionResult response = await controller.PreSalesUnpost(id);

            return this.GetStatusCode(response);
        }

        [Fact]
        public async Task Patch_InvalidId_ReturnNotFound()
        {
            int statusCode = await this.GetStatusCodePatch(GetMocks(), 1);
            Assert.Equal((int)HttpStatusCode.NotFound, statusCode);
        }

        [Fact]
        public async Task Patch_ValidId_ReturnNoContent()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<GarmentPreSalesContractViewModel>())).Verifiable();
            mocks.Facade.Setup(f => f.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Facade.Setup(f => f.Patch(It.IsAny<long>(), It.IsAny<JsonPatchDocument<GarmentPreSalesContract>>())).ReturnsAsync(1);

            int statusCode = await this.GetStatusCodePatch(mocks, 1);
            Assert.Equal((int)HttpStatusCode.NoContent, statusCode);
        }

        [Fact]
        public async Task Patch_ThrowException_ReturnInternalServerError()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<GarmentPreSalesContractViewModel>())).Verifiable();
            mocks.Facade.Setup(f => f.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Facade.Setup(f => f.Patch(It.IsAny<long>(), It.IsAny<JsonPatchDocument<GarmentPreSalesContract>>())).ThrowsAsync(new Exception());

            int statusCode = await this.GetStatusCodePatch(mocks, 1);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        [Fact]
        public async Task Should_Success_PreSalesPost()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<GarmentPreSalesContractViewModel>())).Verifiable();
            var id = 1;
            var viewModel = new GarmentPreSalesContractViewModel()
            {
                Id = id
            };
            mocks.Mapper.Setup(m => m.Map<GarmentPreSalesContractViewModel>(It.IsAny<GarmentPreSalesContract>())).Returns(viewModel);
            mocks.Facade.Setup(f => f.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Facade.Setup(f => f.UpdateAsync(It.IsAny<int>(), It.IsAny<GarmentPreSalesContract>())).ReturnsAsync(1);

            List<long> listId = new List<long> { viewModel.Id };

            var controller = GetController(mocks);
            var response = await controller.PreSalesPost(listId);
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Fail_PreSalesPost()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<GarmentPreSalesContractViewModel>())).Verifiable();
            var id = 1;
            var viewModel = new GarmentPreSalesContractViewModel()
            {
                Id = id
            };
            mocks.Mapper.Setup(m => m.Map<GarmentPreSalesContractViewModel>(It.IsAny<GarmentPreSalesContract>())).Returns(viewModel);
            mocks.Facade.Setup(f => f.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Facade.Setup(f => f.UpdateAsync(It.IsAny<int>(), It.IsAny<GarmentPreSalesContract>())).ReturnsAsync(1);
            mocks.Facade.Setup(f => f.PreSalesPost(It.IsAny<List<long>>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            List<long> listId = new List<long> { viewModel.Id };

            var controller = GetController(mocks);
            var response = await controller.PreSalesPost(listId);
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_PreSalesUnpost()
        {
            {
                var mocks = GetMocks();
                mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<GarmentPreSalesContractViewModel>())).Verifiable();
                var id = 1;
                var viewModel = new GarmentPreSalesContractViewModel()
                {
                    Id = id
                };
                mocks.Mapper.Setup(m => m.Map<GarmentPreSalesContractViewModel>(It.IsAny<GarmentPreSalesContract>())).Returns(viewModel);
                mocks.Facade.Setup(f => f.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
                mocks.Facade.Setup(f => f.UpdateAsync(It.IsAny<int>(), It.IsAny<GarmentPreSalesContract>())).ReturnsAsync(1);

                List<long> listId = new List<long> { viewModel.Id };

                var controller = GetController(mocks);
                var response = await controller.PreSalesUnpost(id);
                Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
            }
        }

        [Fact]
        public async Task Should_Fail_PreSalesUnpost()
        {
            {
                var mocks = GetMocks();
                mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<GarmentPreSalesContractViewModel>())).Verifiable();
                var id = 1;
                var viewModel = new GarmentPreSalesContractViewModel()
                {
                    Id = id
                };
                mocks.Mapper.Setup(m => m.Map<GarmentPreSalesContractViewModel>(It.IsAny<GarmentPreSalesContract>())).Returns(viewModel);
                mocks.Facade.Setup(f => f.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
                mocks.Facade.Setup(f => f.UpdateAsync(It.IsAny<int>(), It.IsAny<GarmentPreSalesContract>())).ReturnsAsync(1);
                mocks.Facade.Setup(f => f.PreSalesUnpost(It.IsAny<long>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());
                List<long> listId = new List<long> { viewModel.Id };

                var controller = GetController(mocks);
                var response = await controller.PreSalesUnpost(id);
                Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
            }
        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new AutoMapper.MapperConfiguration(cfg => {
                cfg.AddProfile<GarmentPreSalesContractMapper>();
            });
            var mapper = configuration.CreateMapper();

            GarmentPreSalesContract model = new GarmentPreSalesContract { Id = 1 };
            GarmentPreSalesContractViewModel viewModel = mapper.Map<GarmentPreSalesContractViewModel>(model);

            Assert.Equal(model.Id, viewModel.Id);
        }
    }
}