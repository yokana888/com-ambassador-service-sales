using Com.Ambassador.Sales.Test.WebApi.Utils;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.GarmentOmzetTargetInterface;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentOmzetTargetModel;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.CostCalculationGarment;
using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentOmzetTargetViewModels;
using Com.Ambassador.Service.Sales.WebApi.Controllers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Ambassador.Sales.Test.WebApi.Controllers
{
    public class GarmentOmzetTargetControllerTest : BaseControllerTest<GarmentOmzetTargetController, GarmentOmzetTarget, GarmentOmzetTargetViewModel, IGarmentOmzetTarget>
    {
        [Fact]
        public void Validate_Default()
        {
            GarmentOmzetTargetViewModel defaultViewModel = new GarmentOmzetTargetViewModel();

            var defaultValidationResult = defaultViewModel.Validate(null);
            Assert.True(defaultValidationResult.Count() > 0);
        }

        [Fact]
        public void Validate_Filled()
        {
            var mock = GetMocks();

            mock.Facade.Setup(s => s.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ReadResponse<GarmentOmzetTarget>(new List<GarmentOmzetTarget>(), 10, new Dictionary<string, string>(), new List<string>()));

            mock.ServiceProvider.Setup(s => s.GetService(typeof(IGarmentOmzetTarget)))
                .Returns(mock.Facade.Object);

            GarmentOmzetTargetViewModel filledViewModel = new GarmentOmzetTargetViewModel
            {
                SectionName = "Name",
                SectionId = 1,
                SectionCode = "Code",
                YearOfPeriod = "Test",
                MonthOfPeriod = "Test",
                QuaterCode = "Test",
                Amount = 0
            };

            ValidationContext validationContext = new ValidationContext(filledViewModel, mock.ServiceProvider.Object, null);

            var filledValidationResult = filledViewModel.Validate(validationContext);
            Assert.True(filledValidationResult.Count() > 0);
        }

        [Fact]
        public async Task Should_Success_Patch()
        {

            var mocks = GetMocks();
            mocks.Mapper
                .Setup(s => s.Map<GarmentOmzetTargetViewModel>(It.IsAny<GarmentOmzetTarget>()))
                .Returns(ViewModel);
            mocks.Facade
                .Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Model);
            mocks.Facade
                .Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<GarmentOmzetTarget>()))
                .ReturnsAsync(1);
            var controller = GetController(mocks);
            var response = await controller.Patch((int)ViewModel.Id, new JsonPatchDocument<GarmentOmzetTarget>());

            Assert.Equal((int)HttpStatusCode.NoContent, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_BadRequest_Patch()
        {

            var mocks = GetMocks();
            mocks.Mapper
                .Setup(s => s.Map<GarmentOmzetTargetViewModel>(It.IsAny<GarmentOmzetTarget>()))
                .Returns(ViewModel);
            mocks.Facade
                .Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Model);
            mocks.Facade
                .Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<GarmentOmzetTarget>()))
                .ReturnsAsync(1);
            var controller = GetController(mocks);
            var response = await controller.Patch(2, new JsonPatchDocument<GarmentOmzetTarget>());

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_NotFound_Patch()
        {

            var mocks = GetMocks();
            mocks.Mapper
                .Setup(s => s.Map<GarmentOmzetTargetViewModel>(It.IsAny<GarmentOmzetTarget>()))
                .Returns(ViewModel);
            mocks.Facade
                .Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(default(GarmentOmzetTarget));
            mocks.Facade
                .Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<GarmentOmzetTarget>()))
                .ReturnsAsync(1);
            var controller = GetController(mocks);
            var response = await controller.Patch(2, new JsonPatchDocument<GarmentOmzetTarget>());

            Assert.Equal((int)HttpStatusCode.NotFound, GetStatusCode(response));
        }

        [Fact]
        public async Task Patch_When_Model_State_Invalid()
        {
            var mocks = GetMocks(); 
            var controller = GetController(mocks);
            controller.ModelState.AddModelError("key", "test");

            var response = await controller.Patch(2, new JsonPatchDocument<GarmentOmzetTarget>());

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_ValidateError_Patch()
        {

            var mocks = GetMocks();
            mocks.ValidateService
                .Setup(s => s.Validate(It.IsAny<GarmentOmzetTargetViewModel>()))
                .Throws(GetServiceValidationException());

            mocks.Mapper
                .Setup(s => s.Map<GarmentOmzetTargetViewModel>(It.IsAny<GarmentOmzetTarget>()))
                .Returns(ViewModel);
            mocks.Facade
                .Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Model);
            mocks.Facade
                .Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<GarmentOmzetTarget>()))
                .ReturnsAsync(1);
            var controller = GetController(mocks);
            var response = await controller.Patch(2, new JsonPatchDocument<GarmentOmzetTarget>());

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_InternalServer_Patch()
        {

            var mocks = GetMocks();
            
            mocks.Mapper
                .Setup(s => s.Map<GarmentOmzetTargetViewModel>(It.IsAny<GarmentOmzetTarget>()))
                .Returns(ViewModel);
            mocks.Facade
                .Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Model);
            mocks.Facade
                .Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<GarmentOmzetTarget>()))
                .ThrowsAsync(new Exception());
            var controller = GetController(mocks);
            var response = await controller.Patch((int)ViewModel.Id, new JsonPatchDocument<GarmentOmzetTarget>());

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}