using Com.Ambassador.Sales.Test.WebApi.Utils;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface;
using Com.Ambassador.Service.Sales.Lib.Models;
using Com.Ambassador.Service.Sales.Lib.ViewModels;
using Com.Ambassador.Service.Sales.WebApi.Controllers;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Com.Ambassador.Sales.Test.WebApi.Controllers
{
    public class EfficienciesControllerTest : BaseControllerTest<EfficienciesController, Efficiency, EfficiencyViewModel, IEfficiency>
    {
        [Fact]
        public async Task Should_Success_GetByQUantity()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadModelByQuantity(It.IsAny<int>()))
                .ReturnsAsync(Model);
            var controller = GetController(mocks);
            var response = await controller.GetByQuantity(1);
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_BadRequest()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadModelByQuantity(It.IsAny<int>()))
                .ReturnsAsync(Model);
            var controller = GetController(mocks);
            controller.ModelState.AddModelError("key", "test");
            var response = await controller.GetByQuantity(1);
            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_NotFound_GetByQUantity()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadModelByQuantity(It.IsAny<int>()))
                .ReturnsAsync(default(Efficiency));
            var controller = GetController(mocks);
            var response = await controller.GetByQuantity(1);
            Assert.Equal((int)HttpStatusCode.NotFound, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_InternalServerError_GetByQUantity()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadModelByQuantity(It.IsAny<int>()))
                .ThrowsAsync(new Exception());
            var controller = GetController(mocks);
            var response = await controller.GetByQuantity(1);
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
