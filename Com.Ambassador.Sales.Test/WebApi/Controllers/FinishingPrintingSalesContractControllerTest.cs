using Com.Ambassador.Sales.Test.WebApi.Utils;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrinting;
using Com.Ambassador.Service.Sales.WebApi.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Com.Ambassador.Sales.Test.WebApi.Controllers
{
    public class FinishingPrintingSalesContractControllerTest : BaseControllerTest<FinishingPrintingSalesContractController, FinishingPrintingSalesContractModel, FinishingPrintingSalesContractViewModel, IFinishingPrintingSalesContract>
    {
        [Fact]
        public void Get_PDF_NotFound()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(default(FinishingPrintingSalesContractModel));
            var controller = GetController(mocks);
            var response = controller.GetPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.NotFound, statusCode);

        }

        [Fact]
        public void Get_PDF_Local_OK()
        {
            var mocks = GetMocks();

            var vm = new FinishingPrintingSalesContractViewModel
            {
                Agent = new Service.Sales.Lib.ViewModels.IntegrationViewModel.AgentViewModel()
                {
                    Id = 1,
                    Name = "name"
                },
                Buyer = new Service.Sales.Lib.ViewModels.IntegrationViewModel.BuyerViewModel
                {
                    Id = 1,
                    Type = "Lokal"
                },
                AccountBank = new Service.Sales.Lib.ViewModels.IntegrationViewModel.AccountBankViewModel
                {
                    Id = 1
                },
                OrderQuantity = 1,
                UOM = new Service.Sales.Lib.ViewModels.IntegrationViewModel.UomViewModel()
                {
                    Unit = "unit"
                },
                Commodity = new Service.Sales.Lib.ViewModels.IntegrationViewModel.CommodityViewModel()
                {
                    Name = "comm"
                },
                Quality = new Service.Sales.Lib.ViewModels.IntegrationViewModel.QualityViewModel()
                {
                    Name = "name"
                },
                DesignMotive = new Service.Sales.Lib.ViewModels.IntegrationViewModel.OrderTypeViewModel()
                {
                    Name = "name"
                },
                TermOfPayment = new Service.Sales.Lib.ViewModels.IntegrationViewModel.TermOfPaymentViewModel()
                {
                    Name = "tp"
                },
                DeliverySchedule = DateTimeOffset.UtcNow,
                UseIncomeTax = false,
                Details = new List<FinishingPrintingSalesContractDetailViewModel>()
                {
                    new FinishingPrintingSalesContractDetailViewModel()
                    {
                        UseIncomeTax = false,
                        Currency = new Service.Sales.Lib.ViewModels.IntegrationViewModel.CurrencyViewModel()
                        {
                            Code = "code",
                            Symbol = "c"
                        }
                    }
                },
                Amount = 1,
                Material = new Service.Sales.Lib.ViewModels.IntegrationViewModel.ProductViewModel(),
                MaterialConstruction = new Service.Sales.Lib.ViewModels.IntegrationViewModel.MaterialConstructionViewModel(),
                YarnMaterial = new Service.Sales.Lib.ViewModels.IntegrationViewModel.YarnMaterialViewModel(),
                OrderType = new Service.Sales.Lib.ViewModels.IntegrationViewModel.OrderTypeViewModel()
                {
                    Name="SOLID"
                }
                
            };

            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(f => f.Map<FinishingPrintingSalesContractViewModel>(It.IsAny<FinishingPrintingSalesContractModel>())).Returns(vm);

            var controller = GetController(mocks);
            var response = controller.GetPDF(1).Result;
            
            Assert.NotNull(response);

        }

        [Fact]
        public void Get_PDF_Ekspor_OK()
        {
            var mocks = GetMocks();

            var vm = new FinishingPrintingSalesContractViewModel
            {
                Agent = new Service.Sales.Lib.ViewModels.IntegrationViewModel.AgentViewModel()
                {
                    Id = 1,
                    Name = "name"
                },
                Buyer = new Service.Sales.Lib.ViewModels.IntegrationViewModel.BuyerViewModel
                {
                    Id = 1,
                    Type = "Ekspor"
                },
                AccountBank = new Service.Sales.Lib.ViewModels.IntegrationViewModel.AccountBankViewModel
                {
                    Id = 1
                },
                OrderQuantity = 1,
                UOM = new Service.Sales.Lib.ViewModels.IntegrationViewModel.UomViewModel()
                {
                    Unit = "unit"
                },
                Commodity = new Service.Sales.Lib.ViewModels.IntegrationViewModel.CommodityViewModel()
                {
                    Name = "comm"
                },
                Quality = new Service.Sales.Lib.ViewModels.IntegrationViewModel.QualityViewModel()
                {
                    Name = "name"
                },
                DesignMotive = new Service.Sales.Lib.ViewModels.IntegrationViewModel.OrderTypeViewModel()
                {
                    Name = "name"
                },
                TermOfPayment = new Service.Sales.Lib.ViewModels.IntegrationViewModel.TermOfPaymentViewModel()
                {
                    Name = "tp"
                },
                DeliverySchedule = DateTimeOffset.UtcNow,
                UseIncomeTax = false,
                Details = new List<FinishingPrintingSalesContractDetailViewModel>()
                {
                    new FinishingPrintingSalesContractDetailViewModel()
                    {
                        UseIncomeTax = false,
                        Currency = new Service.Sales.Lib.ViewModels.IntegrationViewModel.CurrencyViewModel()
                        {
                            Code = "code",
                            Symbol = "c"
                        }
                    }
                },
                Amount = 1,
                Material = new Service.Sales.Lib.ViewModels.IntegrationViewModel.ProductViewModel(),
                MaterialConstruction = new Service.Sales.Lib.ViewModels.IntegrationViewModel.MaterialConstructionViewModel(),
                YarnMaterial = new Service.Sales.Lib.ViewModels.IntegrationViewModel.YarnMaterialViewModel()
            };

            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(f => f.Map<FinishingPrintingSalesContractViewModel>(It.IsAny<FinishingPrintingSalesContractModel>())).Returns(vm);
            
            var controller = GetController(mocks);
            var response = controller.GetPDF(1).Result;

            Assert.NotNull(response);

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
    }
}
