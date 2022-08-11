using Com.Ambassador.Sales.Test.WebApi.Utils;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.Weaving;
using Com.Ambassador.Service.Sales.Lib.Models.Weaving;
using Com.Ambassador.Service.Sales.Lib.ViewModels.Weaving;
using Com.Ambassador.Service.Sales.WebApi.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.WebApi.Controllers
{
    public class WeavingSalesContractControllerTest : BaseControllerTest<WeavingSalesContractController, WeavingSalesContractModel, WeavingSalesContractViewModel, IWeavingSalesContract>
    {
        [Fact]
        public void Get_PDF_NotFound()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(default(WeavingSalesContractModel));
            var controller = GetController(mocks);
            var response = controller.GetPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.NotFound, statusCode);

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
        public void Get_PDF_BadRequest()
        {
            var mocks = GetMocks();
            var controller = GetController(mocks);
            controller.ModelState.AddModelError("key", "value");
            var response = controller.GetPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.BadRequest, statusCode);

        }

        [Fact]
        public void Get_PDF_Local_OK()
        {
            var mocks = GetMocks();

            var vm = new WeavingSalesContractViewModel
            {
                Buyer = new Service.Sales.Lib.ViewModels.IntegrationViewModel.BuyerViewModel
                {
                    Id = 1,
                    Type = "Local",
                    Country = "a"
                },
                AccountBank = new Service.Sales.Lib.ViewModels.IntegrationViewModel.AccountBankViewModel
                {
                    Id = 1,
                    Currency = new Service.Sales.Lib.ViewModels.IntegrationViewModel.CurrencyViewModel()
                    {
                        Symbol = "a",
                        Description = "a",
                        Code = "a"
                    }
                },
                OrderQuantity = 1,
                Uom = new Service.Sales.Lib.ViewModels.IntegrationViewModel.UomViewModel()
                {
                    Id = 1,
                    Unit = "unit"
                },
                Comodity = new Service.Sales.Lib.ViewModels.IntegrationViewModel.CommodityViewModel()
                {
                    Name = "comm"
                },
                Quality = new Service.Sales.Lib.ViewModels.IntegrationViewModel.QualityViewModel()
                {
                    Name = "name"
                },
                TermOfPayment = new Service.Sales.Lib.ViewModels.IntegrationViewModel.TermOfPaymentViewModel()
                {
                    Name = "tp"
                },
                Agent = new Service.Sales.Lib.ViewModels.IntegrationViewModel.AgentViewModel()
                {
                    Id = 1,
                    Name = "A",
                    Country = "a"
                },
                DeliverySchedule = DateTimeOffset.UtcNow,
                Product = new Service.Sales.Lib.ViewModels.IntegrationViewModel.ProductViewModel()
                {
                    Name = "a"
                },
                YarnMaterial = new Service.Sales.Lib.ViewModels.IntegrationViewModel.YarnMaterialViewModel()
                {
                    Name = "a"
                },
                MaterialConstruction = new Service.Sales.Lib.ViewModels.IntegrationViewModel.MaterialConstructionViewModel()
                {
                    Name = "a"
                },
                ComodityDescription = "a"
            };

            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(f => f.Map<WeavingSalesContractViewModel>(It.IsAny<WeavingSalesContractModel>())).Returns(vm);

            var controller = GetController(mocks);
            var response = controller.GetPDF(1).Result;

            Assert.NotNull(response);

        }

        [Fact]
        public void Get_PDF_Ekspor_OK()
        {
            var mocks = GetMocks();

            var vm = new WeavingSalesContractViewModel
            {
                Buyer = new Service.Sales.Lib.ViewModels.IntegrationViewModel.BuyerViewModel
                {
                    Id = 1,
                    Type = "Ekspor",
                    Country = "a"
                },
                AccountBank = new Service.Sales.Lib.ViewModels.IntegrationViewModel.AccountBankViewModel
                {
                    Id = 1,
                    Currency = new Service.Sales.Lib.ViewModels.IntegrationViewModel.CurrencyViewModel()
                    {
                        Symbol = "a",
                        Description = "a",
                        Code = "a"
                    }
                },
                OrderQuantity = 1,
                Uom = new Service.Sales.Lib.ViewModels.IntegrationViewModel.UomViewModel()
                {
                    Id = 1,
                    Unit = "unit"
                },
                Comodity = new Service.Sales.Lib.ViewModels.IntegrationViewModel.CommodityViewModel()
                {
                    Name = "comm"
                },
                Quality = new Service.Sales.Lib.ViewModels.IntegrationViewModel.QualityViewModel()
                {
                    Name = "name"
                },
                TermOfPayment = new Service.Sales.Lib.ViewModels.IntegrationViewModel.TermOfPaymentViewModel()
                {
                    Name = "tp"
                },
                Agent = new Service.Sales.Lib.ViewModels.IntegrationViewModel.AgentViewModel()
                {
                    Id = 1,
                    Name = "A",
                    Country = "a"
                },
                DeliverySchedule = DateTimeOffset.UtcNow,
                Product = new Service.Sales.Lib.ViewModels.IntegrationViewModel.ProductViewModel()
                {
                    Name ="a"
                },
                YarnMaterial = new Service.Sales.Lib.ViewModels.IntegrationViewModel.YarnMaterialViewModel()
                {
                    Name = "a"
                },
                MaterialConstruction = new Service.Sales.Lib.ViewModels.IntegrationViewModel.MaterialConstructionViewModel()
                {
                    Name ="a"
                },
                ComodityDescription = "a"
            };

            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(f => f.Map<WeavingSalesContractViewModel>(It.IsAny<WeavingSalesContractModel>())).Returns(vm);

            var controller = GetController(mocks);
            var response = controller.GetPDF(1).Result;

            Assert.NotNull(response);

        }

    }
}
