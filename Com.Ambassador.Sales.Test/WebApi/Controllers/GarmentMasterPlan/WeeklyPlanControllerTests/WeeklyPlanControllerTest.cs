using Com.Ambassador.Sales.Test.WebApi.Utils;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.Garment.WeeklyPlanInterfaces;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentMasterPlan.WeeklyPlanModels;
using Com.Ambassador.Service.Sales.Lib.ViewModels.Garment.WeeklyPlanViewModels;
using Com.Ambassador.Service.Sales.WebApi.Controllers.GarmentMasterPlan.WeeklyPlanControllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.WebApi.Controllers.GarmentMasterPlan.WeeklyPlanControllerTests
{
    public class WeeklyPlanControllerTest : BaseControllerTest<WeeklyPlanController, GarmentWeeklyPlan, GarmentWeeklyPlanViewModel, IWeeklyPlanFacade>
    {
        protected override GarmentWeeklyPlanViewModel ViewModel
        {
            get
            {
                var viewModel = base.ViewModel;
                viewModel.Items = new List<GarmentWeeklyPlanItemViewModel>
                {
                    new GarmentWeeklyPlanItemViewModel()
                };
                return viewModel;
            }
        }

        [Fact]
        public void Get_Years_WithoutException_ReturnOK()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.GetYears(It.IsAny<string>()))
                .Returns(new List<string> { });

            var controller = GetController(mocks);
            var response = controller.GetYears();

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void Get_Years_ReadThrowException_ReturnInternalServerError()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.GetYears(It.IsAny<string>()))
                .Throws(new Exception());

            var controller = GetController(mocks);
            var response = controller.GetYears();

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        [Fact]
        public void Get_Week_WithoutException_ReturnOK()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.GetWeekById(It.IsAny<long>()))
                .Returns(new GarmentWeeklyPlanItem { });

            var controller = GetController(mocks);
            var response = controller.GetWeek(It.IsAny<long>());

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void Get_Week_ReadThrowException_ReturnInternalServerError()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.GetWeekById(It.IsAny<long>()))
                .Throws(new Exception());

            var controller = GetController(mocks);
            var response = controller.GetWeek(It.IsAny<long>());

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }
    }
}
