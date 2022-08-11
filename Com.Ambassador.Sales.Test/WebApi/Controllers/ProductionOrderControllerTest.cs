using AutoMapper;
using Com.Ambassador.Sales.Test.WebApi.Utils;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.ProductionOrder;
using Com.Ambassador.Service.Sales.Lib.Models.ProductionOrder;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using Com.Ambassador.Service.Sales.Lib.ViewModels.ProductionOrder;
using Com.Ambassador.Service.Sales.Lib.ViewModels.Report.OrderStatusReport;
using Com.Ambassador.Service.Sales.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Com.Ambassador.Service.Sales.WebApi.Controllers.ProductionOrderController;

namespace Com.Ambassador.Sales.Test.WebApi.Controllers
{
    public class ProductionOrderControllerTest : BaseControllerTest<ProductionOrderController, ProductionOrderModel, ProductionOrderViewModel, IProductionOrder>
    {
        [Fact]
        public void Get_PDF_Success()
        {
            var vm = new ProductionOrderViewModel()
            {
                ShippingQuantityTolerance = 1,
                DistributedQuantity = 1,
                OrderQuantity = 1,
                FinishingPrintingSalesContract = new FinishingPrintingSalesContractViewModel(),
                Buyer = new BuyerViewModel(),
                Material = new MaterialViewModel(),
                MaterialConstruction = new MaterialConstructionViewModel(),
                YarnMaterial = new YarnMaterialViewModel(),
                FinishType = new FinishTypeViewModel(),
                OrderType = new OrderTypeViewModel(),
                ProcessType = new ProcessTypeViewModel(),
                Uom = new UomViewModel(),
                DesignCode = "code",
                DesignNumber = "num",
                DesignMotive = new DesignMotiveViewModel(),
                Run = "ru",
                RunWidth = new List<ProductionOrder_RunWidthViewModel>()
                {
                    new ProductionOrder_RunWidthViewModel()
                    {
                        Value = 1
                    },
                    new ProductionOrder_RunWidthViewModel()
                    {
                        Value = 2
                    },
                    new ProductionOrder_RunWidthViewModel()
                    {
                        Value = 3
                    }
                },
                StandardTests = new StandardTestsViewModel(),
                DeliveryDate = DateTimeOffset.UtcNow,
                Account = new AccountViewModel(),
                LampStandards = new List<ProductionOrder_LampStandardViewModel>()
                {
                    new ProductionOrder_LampStandardViewModel()
                },
                Details = new List<ProductionOrder_DetailViewModel>()
                {
                    new ProductionOrder_DetailViewModel()
                    {
                        ColorType = new ColorTypeViewModel(),
                        Quantity = 1,
                        Uom = new UomViewModel()
                    }
                }
            };
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(s => s.Map<ProductionOrderViewModel>(It.IsAny<ProductionOrderModel>()))
                .Returns(vm);
            var controller = GetController(mocks);
            var response = controller.GetPDF(1).Result;

            Assert.NotNull(response);

        }

        [Fact]
        public void Get_PDF_NotFound()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(default(ProductionOrderModel));
            var controller = GetController(mocks);
            var response = controller.GetPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.NotFound, statusCode);

        }

        [Fact]
        public void GetPDF_When_Model_State_Invalid()
        {
            var mocks = GetMocks();
            var controller = GetController(mocks);
            controller.ModelState.AddModelError("key", "test");

            var response = controller.GetPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.BadRequest, statusCode);

        }

        [Fact]
        public void Get_PDF_Exception()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("error"));
            var controller = GetController(mocks);
            var response = controller.GetPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);

        }

        private async Task<int> GetStatusCodePut((Mock<IIdentityService> IdentityService, Mock<IValidateService> ValidateService, Mock<IProductionOrder> Facade, Mock<IMapper> Mapper, Mock<IServiceProvider> ServiceProvider) mocks, int id, ProductionOrderViewModel viewModel)
        {
            ProductionOrderController controller = this.GetController(mocks);
            IActionResult response = await controller.Put(id, viewModel);

            return this.GetStatusCode(response);
        }

        [Fact]
        public async Task Put_IsRequested_True_Success()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<ProductionOrderViewModel>())).Verifiable();
            var id = 1;
            var viewModel = new ProductionOrderViewModel()
            {
                Id = id
            };
            mocks.Mapper.Setup(m => m.Map<ProductionOrderViewModel>(It.IsAny<ProductionOrderViewModel>())).Returns(viewModel);
            mocks.Facade.Setup(f => f.UpdateRequestedTrue(It.IsAny<List<int>>())).ReturnsAsync(1);
            List<int> ids = new List<int>((int)viewModel.Id);
            var controller = GetController(mocks);
            var response = await controller.PutRequestedTrue(ids);

            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.NoContent, statusCode);
        }

        [Fact]
        public async Task Put_IsRequested_True_Null_BadRequest()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<ProductionOrderViewModel>())).Verifiable();
            var id = 1;
            var viewModel = new ProductionOrderViewModel()
            {
                Id = id
            };
            mocks.Mapper.Setup(m => m.Map<ProductionOrderViewModel>(It.IsAny<ProductionOrderViewModel>())).Returns(viewModel);
            mocks.Facade.Setup(f => f.UpdateRequestedTrue(It.IsAny<List<int>>())).ReturnsAsync(1);
            List<int> ids = new List<int>((int)viewModel.Id);
            var controller = GetController(mocks);
            var response = await controller.PutRequestedTrue(null);

            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.BadRequest, statusCode);
        }

        [Fact]
        public async Task Put_IsRequested_True_Exception_InternalServerError()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<ProductionOrderViewModel>())).Verifiable();
            var id = 1;
            var viewModel = new ProductionOrderViewModel()
            {
                Id = id
            };
            mocks.Mapper.Setup(m => m.Map<ProductionOrderViewModel>(It.IsAny<ProductionOrderViewModel>())).Returns(viewModel);
            mocks.Facade.Setup(f => f.UpdateRequestedTrue(It.IsAny<List<int>>())).ThrowsAsync(new Exception());
            List<int> ids = new List<int>((int)viewModel.Id);
            var controller = GetController(mocks);
            var response = await controller.PutRequestedTrue(ids);

            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        [Fact]
        public async Task PutRequestedTrue_Return_BadRequest()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<ProductionOrderViewModel>())).Verifiable();
            var id = 1;
            var viewModel = new ProductionOrderViewModel()
            {
                Id = id
            };
            //mocks.Mapper.Setup(m => m.Map<ProductionOrderViewModel>(It.IsAny<ProductionOrderViewModel>())).Returns(viewModel);
            //mocks.Facade.Setup(f => f.UpdateRequestedTrue(It.IsAny<List<int>>())).ThrowsAsync(new Exception());
            List<int> ids = new List<int>((int)viewModel.Id);
            var controller = GetController(mocks);
            controller.ModelState.AddModelError("key", "test");
            var response = await controller.PutRequestedTrue(ids);

            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.BadRequest, statusCode);
        }

        [Fact]
        public async Task Put_IsRequested_False_Success()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<ProductionOrderViewModel>())).Verifiable();
            var id = 1;
            var viewModel = new ProductionOrderViewModel()
            {
                Id = id
            };
            mocks.Mapper.Setup(m => m.Map<ProductionOrderViewModel>(It.IsAny<ProductionOrderViewModel>())).Returns(viewModel);
            mocks.Facade.Setup(f => f.UpdateRequestedFalse(It.IsAny<List<int>>())).ReturnsAsync(1);
            List<int> ids = new List<int>((int)viewModel.Id);
            var controller = GetController(mocks);
            var response = await controller.PutRequestedFalse(ids);
            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.NoContent, statusCode);
        }

        [Fact]
        public async Task Put_IsRequested_False_Null_BadRequest()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<ProductionOrderViewModel>())).Verifiable();
            var id = 1;
            var viewModel = new ProductionOrderViewModel()
            {
                Id = id
            };
            mocks.Mapper.Setup(m => m.Map<ProductionOrderViewModel>(It.IsAny<ProductionOrderViewModel>())).Returns(viewModel);
            mocks.Facade.Setup(f => f.UpdateRequestedFalse(It.IsAny<List<int>>())).ReturnsAsync(1);
            List<int> ids = new List<int>((int)viewModel.Id);
            var controller = GetController(mocks);
            var response = await controller.PutRequestedFalse(null);
            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.BadRequest, statusCode);
        }

        [Fact]
        public async Task PutRequestedFalse_When_ModelState_Invalid()
        {
            var mocks = GetMocks();

            var controller = GetController(mocks);
            controller.ModelState.AddModelError("key", "test");
            var response = await controller.PutRequestedFalse(null);
            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.BadRequest, statusCode);
        }

        [Fact]
        public async Task Put_IsRequested_False_Exception_InternalServer()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<ProductionOrderViewModel>())).Verifiable();
            var id = 1;
            var viewModel = new ProductionOrderViewModel()
            {
                Id = id
            };
            mocks.Mapper.Setup(m => m.Map<ProductionOrderViewModel>(It.IsAny<ProductionOrderViewModel>())).Returns(viewModel);
            mocks.Facade.Setup(f => f.UpdateRequestedFalse(It.IsAny<List<int>>())).ThrowsAsync(new Exception());
            List<int> ids = new List<int>((int)viewModel.Id);
            var controller = GetController(mocks);
            var response = await controller.PutRequestedFalse(ids);
            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        [Fact]
        public async Task Put_IsCompleted_True_Success()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<ProductionOrderViewModel>())).Verifiable();
            var id = 1;
            var viewModel = new ProductionOrderViewModel()
            {
                Id = id
            };
            mocks.Mapper.Setup(m => m.Map<ProductionOrderViewModel>(It.IsAny<ProductionOrderViewModel>())).Returns(viewModel);
            mocks.Facade.Setup(f => f.UpdateIsCompletedTrue(It.IsAny<int>())).ReturnsAsync(1);
            var controller = GetController(mocks);
            var response = await controller.PutIsCompletedTrue((int)viewModel.Id);
            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.NoContent, statusCode);
        }

        [Fact]
        public async Task Put_IsCompleted_True_BadRequest()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<ProductionOrderViewModel>())).Verifiable();
            var id = 1;
            var viewModel = new ProductionOrderViewModel()
            {
                Id = id
            };
            mocks.Mapper.Setup(m => m.Map<ProductionOrderViewModel>(It.IsAny<ProductionOrderViewModel>())).Returns(viewModel);
            mocks.Facade.Setup(f => f.UpdateIsCompletedTrue(It.IsAny<int>())).ReturnsAsync(1);
            var controller = GetController(mocks);
            var response = await controller.PutIsCompletedTrue(0);
            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.BadRequest, statusCode);
        }

        [Fact]
        public async Task PutIsCompletedTrue_WHen_ModelStateInvalid()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<ProductionOrderViewModel>())).Verifiable();
            var controller = GetController(mocks);
            controller.ModelState.AddModelError("key", "test");
            var response = await controller.PutIsCompletedTrue(0);
            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.BadRequest, statusCode);
        }

        [Fact]
        public async Task Put_IsCompleted_True_InternalServer()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<ProductionOrderViewModel>())).Verifiable();
            var id = 1;
            var viewModel = new ProductionOrderViewModel()
            {
                Id = id
            };
            mocks.Mapper.Setup(m => m.Map<ProductionOrderViewModel>(It.IsAny<ProductionOrderViewModel>())).Returns(viewModel);
            mocks.Facade.Setup(f => f.UpdateIsCompletedTrue(It.IsAny<int>())).ThrowsAsync(new Exception());
            var controller = GetController(mocks);
            var response = await controller.PutIsCompletedTrue((int)viewModel.Id);
            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        [Fact]
        public async Task Put_IsCompleted_False_Success()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<ProductionOrderViewModel>())).Verifiable();
            var id = 1;
            var viewModel = new ProductionOrderViewModel()
            {
                Id = id
            };
            mocks.Mapper.Setup(m => m.Map<ProductionOrderViewModel>(It.IsAny<ProductionOrderViewModel>())).Returns(viewModel);
            mocks.Facade.Setup(f => f.UpdateIsCompletedFalse(It.IsAny<int>())).ReturnsAsync(1);
            var controller = GetController(mocks);
            var response = await controller.PutIsCompletedFalse((int)viewModel.Id);
            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.NoContent, statusCode);
        }

        [Fact]
        public async Task Put_IsCompleted_False_BadRequest()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<ProductionOrderViewModel>())).Verifiable();
            var id = 1;
            var viewModel = new ProductionOrderViewModel()
            {
                Id = id
            };
            mocks.Mapper.Setup(m => m.Map<ProductionOrderViewModel>(It.IsAny<ProductionOrderViewModel>())).Returns(viewModel);
            mocks.Facade.Setup(f => f.UpdateIsCompletedFalse(It.IsAny<int>())).ReturnsAsync(1);
            var controller = GetController(mocks);
            var response = await controller.PutIsCompletedFalse(0);
            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.BadRequest, statusCode);
        }

        [Fact]
        public async Task PutIsCompletedFalse_When_Model_State_Invalid()
        {
            var mocks = GetMocks();

            var controller = GetController(mocks);
            controller.ModelState.AddModelError("key", "test");
            var response = await controller.PutIsCompletedFalse(0);
            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.BadRequest, statusCode);
        }

        [Fact]
        public async Task Put_IsCompleted_False_InternalServer()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<ProductionOrderViewModel>())).Verifiable();
            var id = 1;
            var viewModel = new ProductionOrderViewModel()
            {
                Id = id
            };
            mocks.Mapper.Setup(m => m.Map<ProductionOrderViewModel>(It.IsAny<ProductionOrderViewModel>())).Returns(viewModel);
            mocks.Facade.Setup(f => f.UpdateIsCompletedFalse(It.IsAny<int>())).ThrowsAsync(new Exception());
            var controller = GetController(mocks);
            var response = await controller.PutIsCompletedFalse((int)viewModel.Id);
            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        [Fact]
        public async Task Put_Distributed_Quantity_Success()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<ProductionOrderViewModel>())).Verifiable();
            var id = 1;
            var distributedQty = 1;
            var viewModel = new ProductionOrderViewModel()
            {
                Id = id,
                DistributedQuantity = distributedQty
            };
            mocks.Mapper.Setup(m => m.Map<ProductionOrderViewModel>(It.IsAny<ProductionOrderViewModel>())).Returns(viewModel);
            mocks.Facade.Setup(f => f.UpdateDistributedQuantity(It.IsAny<List<int>>(), It.IsAny<List<double>>())).ReturnsAsync(1);
            List<SppParams> data = new List<SppParams>()
            {
                new SppParams()
                {
                    id = "1",
                    context= "cpmt",
                    distributedQuantity = 1
                }
            };
            List<int> ids = new List<int>();
            List<double> distributedQuantity = new List<double>();
            foreach (var item in data)
            {
                ids.Add(int.Parse(item.id));
                distributedQuantity.Add((double)item.distributedQuantity);
            };

            var controller = GetController(mocks);
            var response = await controller.PutDistributedQuantity(data);
            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.NoContent, statusCode);
        }

        [Fact]
        public async Task Put_Distributed_Quantity_BadRequest()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<ProductionOrderViewModel>())).Verifiable();
            var id = 1;
            var distributedQty = 1;
            var viewModel = new ProductionOrderViewModel()
            {
                Id = id,
                DistributedQuantity = distributedQty
            };
            mocks.Mapper.Setup(m => m.Map<ProductionOrderViewModel>(It.IsAny<ProductionOrderViewModel>())).Returns(viewModel);
            mocks.Facade.Setup(f => f.UpdateDistributedQuantity(It.IsAny<List<int>>(), It.IsAny<List<double>>())).ReturnsAsync(1);
            List<SppParams> data = new List<SppParams>();
            List<int> ids = new List<int>();
            List<double> distributedQuantity = new List<double>();
            foreach (var item in data)
            {
                ids.Add(int.Parse(item.id));
                distributedQuantity.Add((double)item.distributedQuantity);
            };

            var controller = GetController(mocks);
            var response = await controller.PutDistributedQuantity(null);
            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.BadRequest, statusCode);
        }

        [Fact]
        public async Task Put_Distributed_Quantity_InternalServer()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<ProductionOrderViewModel>())).Verifiable();
            var id = 1;
            var distributedQty = 1;
            var viewModel = new ProductionOrderViewModel()
            {
                Id = id,
                DistributedQuantity = distributedQty
            };
            mocks.Mapper.Setup(m => m.Map<ProductionOrderViewModel>(It.IsAny<ProductionOrderViewModel>())).Returns(viewModel);
            mocks.Facade.Setup(f => f.UpdateDistributedQuantity(It.IsAny<List<int>>(), It.IsAny<List<double>>())).ThrowsAsync(new Exception());
            List<SppParams> data = new List<SppParams>()
            {
                new SppParams()
                {
                    id = "1",
                    context= "cpmt",
                    distributedQuantity = 1
                }
            };
            List<int> ids = new List<int>();
            List<double> distributedQuantity = new List<double>();
            foreach (var item in data)
            {
                ids.Add(int.Parse(item.id));
                distributedQuantity.Add((double)item.distributedQuantity);
            };

            var controller = GetController(mocks);
            var response = await controller.PutDistributedQuantity(data);
            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        [Fact]
        public async Task PutDistributedQuantity_When_Model_State_Invalid()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<ProductionOrderViewModel>())).Verifiable();

            List<SppParams> data = new List<SppParams>()
            {
                new SppParams()
                {
                    id = "1",
                    context= "cpmt",
                    distributedQuantity = 1
                }
            };
            List<int> ids = new List<int>();
            List<double> distributedQuantity = new List<double>();
            foreach (var item in data)
            {
                ids.Add(int.Parse(item.id));
                distributedQuantity.Add((double)item.distributedQuantity);
            };

            var controller = GetController(mocks);
            controller.ModelState.AddModelError("key", "test");

            var response = await controller.PutDistributedQuantity(data);
            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.BadRequest, statusCode);
        }

        [Fact]
        public void Should_ReturnOK_GetMonthlySummaryByYearAndOrderType_Success()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.GetMonthlyOrderQuantityByYearAndOrderType(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(new List<YearlyOrderQuantity>());
            var controller = GetController(mocks);
            var response = controller.GetMonthlySummaryByYearAndOrderType(0, 1);

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void Should_ReturnFailed_GetMonthlySummaryByYearAndOrderType_ThrowException()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.GetMonthlyOrderQuantityByYearAndOrderType(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Throws(new Exception());
            var controller = GetController(mocks);
            var response = controller.GetMonthlySummaryByYearAndOrderType(1, 1);

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        [Fact]
        public void Should_ReturnOK_GetMonthlyOrderIdsByOrderTypeId_Success()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.GetMonthlyOrderIdsByOrderType(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(new List<MonthlyOrderQuantity>());
            var controller = GetController(mocks);
            var response = controller.GetMonthlyOrderIdsByOrderTypeId(0, 0, 1);

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void Should_ReturnFailed_GetMonthlyOrderIdsByOrderTypeId_ThrowException()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.GetMonthlyOrderIdsByOrderType(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Throws(new Exception());
            var controller = GetController(mocks);
            var response = controller.GetMonthlyOrderIdsByOrderTypeId(1, 1, 1);

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        [Fact]
        public async Task Put_IsCalculated_Success()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(f => f.UpdateIsCalculated(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(1);
            var controller = GetController(mocks);
            var response = await controller.PutIsCalculated(1, true);
            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.NoContent, statusCode);
        }

        [Fact]
        public async Task Put_IsCalculated_Exception()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(f => f.UpdateIsCalculated(It.IsAny<int>(), It.IsAny<bool>())).ThrowsAsync(new Exception());
            var controller = GetController(mocks);
            var response = await controller.PutIsCalculated(1, true);
            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        [Fact]
        public async Task Put_IsCalculated_ID_0()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(f => f.UpdateIsCalculated(It.IsAny<int>(), It.IsAny<bool>())).ThrowsAsync(new Exception());
            var controller = GetController(mocks);
            var response = await controller.PutIsCalculated(0, true);
            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.BadRequest, statusCode);
        }
        [Fact]
        public void Validate_ViewModel()
        {
            List<ProductionOrderViewModel> viewModels = new List<ProductionOrderViewModel>
            {
                new ProductionOrderViewModel{
                    Code = "ABC",
                    OrderQuantity = 5.48,
                    LampStandards = new List<ProductionOrder_LampStandardViewModel>{
                        new ProductionOrder_LampStandardViewModel{
                            Name = "Lampu",
                            Description = "Lampu Luar"
                        }
                    },
                    Details = new List<ProductionOrder_DetailViewModel>{
                        new ProductionOrder_DetailViewModel{
                            ColorRequest = "A",
                            Quantity = 5.4,
                            Uom = new UomViewModel{
                                Id = 1,
                                Unit = "MTR"
                            }
                        }
                    }
                }
            };
            foreach (var viewModel in viewModels)
            {
                var defaultValidationResult = viewModel.Validate(null);
                Assert.True(defaultValidationResult.Count() > 0);
            }
        }

        [Fact]
        public void Should_Success_Read_By_Sales_Contract()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(f => f.ReadBySalesContractId(It.IsAny<long>())).Returns(new List<ProductionOrderModel>() { new ProductionOrderModel() });
            mocks.Mapper.Setup(m => m.Map<List<ProductionOrderViewModel>>(It.IsAny<List<ProductionOrderModel>>())).Returns(new List<ProductionOrderViewModel>());
            var controller = GetController(mocks);
            var response = controller.ReadBySalesContractId(It.IsAny<long>());
            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void Should_ReturnFailed_Read_By_Sales_Contract_ThrowException()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(f => f.ReadBySalesContractId(It.IsAny<long>())).Returns(new List<ProductionOrderModel>() { new ProductionOrderModel() });
            mocks.Mapper.Setup(m => m.Map<List<ProductionOrderViewModel>>(It.IsAny<List<ProductionOrderModel>>())).Throws(new Exception());
            var controller = GetController(mocks);
            var response = controller.ReadBySalesContractId(It.IsAny<long>());
            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        [Fact]
        public void ReadBySalesContractId_BadRequest()
        {
            var mocks = GetMocks();
            var controller = GetController(mocks);
            controller.ModelState.AddModelError("key", "test");
            var response = controller.ReadBySalesContractId(It.IsAny<long>());
            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.BadRequest, statusCode);
        }

        [Fact]
        public void GetConstruction_WithoutException_ReturnOK()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.ReadConstruction(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new List<string>() { "s" });

            var controller = GetController(mocks);
            var response = controller.GetConstruction();
            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void GetConstruction_Exception_InternalServer()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.ReadConstruction(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new Exception());

            var controller = GetController(mocks);
            var response = controller.GetConstruction();
            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }
    }
}
