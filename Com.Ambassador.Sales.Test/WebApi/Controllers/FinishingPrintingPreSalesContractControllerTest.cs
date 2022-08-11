using Com.Ambassador.Sales.Test.WebApi.Utils;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrinting;
using Com.Ambassador.Service.Sales.WebApi.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Ambassador.Sales.Test.WebApi.Controllers
{
    public class FinishingPrintingPreSalesContractControllerTest : BaseControllerTest<FinishingPrintingPreSalesContractController, FinishingPrintingPreSalesContractModel, FinishingPrintingPreSalesContractViewModel, IFinishingPrintingPreSalesContractFacade>
    {
        [Fact]
        public async Task Should_Success_PreSalesPost()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<FinishingPrintingPreSalesContractViewModel>())).Verifiable();
            var id = 1;
            var viewModel = new FinishingPrintingPreSalesContractViewModel()
            {
                Id = id
            };
            mocks.Mapper.Setup(m => m.Map<FinishingPrintingPreSalesContractViewModel>(It.IsAny<FinishingPrintingPreSalesContractModel>())).Returns(viewModel);
            mocks.Facade.Setup(f => f.PreSalesPost(It.IsAny<List<long>>())).ReturnsAsync(1);
            List<long> listId = new List<long> { viewModel.Id };

            var controller = GetController(mocks);
            var response = await controller.PreSalesPost(listId);
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Fail_PreSalesPost()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<FinishingPrintingPreSalesContractViewModel>())).Verifiable();
            var id = 1;
            var viewModel = new FinishingPrintingPreSalesContractViewModel()
            {
                Id = id
            };
            mocks.Mapper.Setup(m => m.Map<FinishingPrintingPreSalesContractViewModel>(It.IsAny<FinishingPrintingPreSalesContractModel>())).Returns(viewModel);
            mocks.Facade.Setup(f => f.PreSalesPost(It.IsAny<List<long>>())).ThrowsAsync(new Exception());
            List<long> listId = new List<long> { viewModel.Id };

            var controller = GetController(mocks);
            var response = await controller.PreSalesPost(listId);
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_PreSalesUnpost()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<FinishingPrintingPreSalesContractViewModel>())).Verifiable();
            var id = 1;
            var viewModel = new FinishingPrintingPreSalesContractViewModel()
            {
                Id = id
            };
            mocks.Mapper.Setup(m => m.Map<FinishingPrintingPreSalesContractViewModel>(It.IsAny<FinishingPrintingPreSalesContractModel>())).Returns(viewModel);
            mocks.Facade.Setup(f => f.PreSalesUnpost(It.IsAny<long>())).ReturnsAsync(1);
            List<long> listId = new List<long> { viewModel.Id };

            var controller = GetController(mocks);
            var response = await controller.PreSalesUnpost(id);
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Fail_PreSalesUnpost()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<FinishingPrintingPreSalesContractViewModel>())).Verifiable();
            var id = 1;
            var viewModel = new FinishingPrintingPreSalesContractViewModel()
            {
                Id = id
            };
            mocks.Mapper.Setup(m => m.Map<FinishingPrintingPreSalesContractViewModel>(It.IsAny<FinishingPrintingPreSalesContractModel>())).Returns(viewModel);
            mocks.Facade.Setup(f => f.PreSalesUnpost(It.IsAny<long>())).ThrowsAsync(new Exception());
            List<long> listId = new List<long> { viewModel.Id };

            var controller = GetController(mocks);
            var response = await controller.PreSalesUnpost(id);
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
