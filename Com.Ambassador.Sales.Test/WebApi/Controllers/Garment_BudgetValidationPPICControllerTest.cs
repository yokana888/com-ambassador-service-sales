using Com.Ambassador.Sales.Test.WebApi.Utils;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.Garment;
using Com.Ambassador.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Ambassador.Service.Sales.Lib.ViewModels.CostCalculationGarment;
using Com.Ambassador.Service.Sales.Lib.ViewModels.Garment;
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
    public class Garment_BudgetValidationPPICControllerTest : BaseEmptyControllerTest<Garment_BudgetValidationPPICController, CostCalculationGarment, CostCalculationGarment_RO_Garment_ValidationViewModel, IGarment_BudgetValidationPPIC>
    {
        protected override CostCalculationGarment_RO_Garment_ValidationViewModel ViewModel
        {
            get {
                var ViewModel = base.ViewModel;
                ViewModel.CostCalculationGarment_Materials = new List<CostCalculationGarment_MaterialViewModel>();
				ViewModel.CostCalculationGarment_Materials.Add(
					new CostCalculationGarment_MaterialViewModel
					{

						Code = "code",
						PO_SerialNumber = "possd",
						PO = "po",
						Category = { },
						AutoIncrementNumber = 1,
						Product =new GarmentProductViewModel { Id=0,Name="product"},
						Description = "desc",
						Quantity = 9,
						UOMQuantity = { },
						Price = 100,
						UOMPrice = { },
						Conversion = 1,
						Total = 900,
						isFabricCM = true,
						CM_Price = 300,
						ShippingFeePortion = 3,
						TotalShippingFee = 50,
						BudgetQuantity = 60,
						Information = "inf",
						IsPosted = true
					});
                return ViewModel;
            }
        }

        [Fact]
        public async Task Post_WithoutException_ReturnCreated()
        {
            var mocks = GetMocks();
            mocks.Facade
                .Setup(f => f.ValidateROGarment(It.IsAny<CostCalculationGarment>(), It.IsAny<Dictionary<long, string>>()))
                .ReturnsAsync(1);

            var controller = GetController(mocks);
			var response = await controller.Post(ViewModel);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task Post_ThrowServiceValidationExeption_ReturnBadRequest()
        {
            var mocks = GetMocks();
            mocks.ValidateService
                .Setup(s => s.Validate(It.IsAny<CostCalculationGarment_RO_Garment_ValidationViewModel>()))
                .Throws(GetServiceValidationException());

            var controller = GetController(mocks);
            var response = await controller.Post(ViewModel);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Post_ThrowException_ReturnInternalServerError()
        {
            var mocks = GetMocks();
            mocks.ValidateService
                .Setup(s => s.Validate(It.IsAny<CostCalculationGarment_RO_Garment_ValidationViewModel>()))
                .Verifiable();
            mocks.Facade
                .Setup(s => s.ValidateROGarment(It.IsAny<CostCalculationGarment>(), It.IsAny<Dictionary<long, string>>()))
                .ThrowsAsync(new Exception());

            var controller = GetController(mocks);
            var response = await controller.Post(ViewModel);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
