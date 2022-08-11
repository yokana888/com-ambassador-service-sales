using AutoMapper;
using Com.Ambassador.Sales.Test.WebApi.Utils;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.FinishingPrintingCostCalculation;
using Com.Ambassador.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.Models.FinishingPrintingCostCalculation;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrintingCostCalculation;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using Com.Ambassador.Service.Sales.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Ambassador.Sales.Test.WebApi.Controllers
{
    public class ShinFinishingPrintingSalesContractControllerTest : BaseControllerTest<ShinFinishingPrintingSalesContractController, FinishingPrintingSalesContractModel, ShinFinishingPrintingSalesContractViewModel, IShinFinishingPrintingSalesContractFacade>
    {
        protected override FinishingPrintingSalesContractModel Model => new FinishingPrintingSalesContractModel()
        {
            Details = new List<FinishingPrintingSalesContractDetailModel>()
            {
                new FinishingPrintingSalesContractDetailModel()
            }
        };

        protected override ShinFinishingPrintingSalesContractViewModel ViewModel => new ShinFinishingPrintingSalesContractViewModel()
        {
            Material = new MaterialViewModel(),
            UOM = new UomViewModel()
            {
                Unit = "a"
            },
            Amount = 1,
            ShipmentDescription = "a",
            DeliverySchedule = DateTimeOffset.UtcNow,
            CommodityDescription = "a",
            
            Quality = new QualityViewModel()
            {
                Name = "a"
            },
            Commodity = new CommodityViewModel()
            {
                Name = "a"
            },
            
            PreSalesContract = new FinishingPrintingPreSalesContractViewModel()
            {
                Buyer = new Service.Sales.Lib.ViewModels.IntegrationViewModel.BuyerViewModel()
                {
                    Id = 1
                }
            },
            Details = new List<FinishingPrintingSalesContractDetailViewModel>()
            {
               new FinishingPrintingSalesContractDetailViewModel()
                {
                    UseIncomeTax = true,
                    Currency = new CurrencyViewModel()
                    {
                        Code = "c"
                    }
                },
                new FinishingPrintingSalesContractDetailViewModel()
                {
                    Currency = new CurrencyViewModel()
                    {
                        Code = "usd"
                    }
                }
            },
            Agent = new Service.Sales.Lib.ViewModels.IntegrationViewModel.AgentViewModel()
            {
                Id = 1,
            },
            AccountBank = new Service.Sales.Lib.ViewModels.IntegrationViewModel.AccountBankViewModel()
            {
                Id = 1
            },
            UseIncomeTax = true,
            YarnMaterial = new YarnMaterialViewModel()
            {
                Name = "a"
            },
            TermOfPayment = new TermOfPaymentViewModel()
            {
                Name = "a"
            }
        };

        protected override List<ShinFinishingPrintingSalesContractViewModel> ViewModels => new List<ShinFinishingPrintingSalesContractViewModel>() { ViewModel };

        protected override ShinFinishingPrintingSalesContractController GetController((Mock<IIdentityService> IdentityService, Mock<IValidateService> ValidateService, Mock<IShinFinishingPrintingSalesContractFacade> Facade, Mock<IMapper> Mapper, Mock<IServiceProvider> ServiceProvider) mocks)
        {
            var user = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            Mock<IFinishingPrintingPreSalesContractFacade> preSCFacade = new Mock<IFinishingPrintingPreSalesContractFacade>();
            Mock<IFinishingPrintingCostCalculationService> ccFacade = new Mock<IFinishingPrintingCostCalculationService>();
            preSCFacade.Setup(s => s.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(new FinishingPrintingPreSalesContractModel());
            ccFacade.Setup(s => s.ReadParent(It.IsAny<long>())).ReturnsAsync(new FinishingPrintingCostCalculationModel());
            user.Setup(u => u.Claims).Returns(claims);
            mocks.ServiceProvider.Setup(s => s.GetService(typeof(IHttpClientService))).Returns(new HttpClientTestService());
            mocks.Mapper.Setup(x => x.Map<FinishingPrintingCostCalculationViewModel>(It.IsAny<FinishingPrintingCostCalculationModel>()))
                .Returns(new FinishingPrintingCostCalculationViewModel() 
                { 
                    PreSalesContract = new FinishingPrintingPreSalesContractViewModel() { Id = 1 },
                    UOM = new UomViewModel()
                    {
                        Unit = "a"
                    },
                    Material = new MaterialViewModel()
                    {
                        Name = "a"
                    }
                });
            mocks.Mapper.Setup(x => x.Map<FinishingPrintingPreSalesContractViewModel>(It.IsAny<FinishingPrintingPreSalesContractModel>())).Returns(new FinishingPrintingPreSalesContractViewModel() { Buyer = new BuyerViewModel() { Id = 1 } });
            mocks.ServiceProvider.Setup(s => s.GetService(typeof(IFinishingPrintingCostCalculationService))).Returns(ccFacade.Object);
            mocks.ServiceProvider.Setup(s => s.GetService(typeof(IFinishingPrintingPreSalesContractFacade))).Returns(preSCFacade.Object);
            ShinFinishingPrintingSalesContractController controller = (ShinFinishingPrintingSalesContractController)Activator.CreateInstance(typeof(ShinFinishingPrintingSalesContractController), mocks.IdentityService.Object, mocks.ValidateService.Object, mocks.Facade.Object, mocks.Mapper.Object, mocks.ServiceProvider.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = user.Object
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["Authorization"] = "Bearer unittesttoken";
            controller.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");
            return controller;
        }

        private ShinFinishingPrintingSalesContractController GetController2((Mock<IIdentityService> IdentityService, Mock<IValidateService> ValidateService, Mock<IShinFinishingPrintingSalesContractFacade> Facade, Mock<IMapper> Mapper, Mock<IServiceProvider> ServiceProvider) mocks)
        {
            var user = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            Mock<IFinishingPrintingPreSalesContractFacade> preSCFacade = new Mock<IFinishingPrintingPreSalesContractFacade>();
            Mock<IFinishingPrintingCostCalculationService> ccFacade = new Mock<IFinishingPrintingCostCalculationService>();
            preSCFacade.Setup(s => s.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(new FinishingPrintingPreSalesContractModel());
            ccFacade.Setup(s => s.ReadParent(It.IsAny<long>())).ReturnsAsync(new FinishingPrintingCostCalculationModel());
            user.Setup(u => u.Claims).Returns(claims);
            mocks.ServiceProvider.Setup(s => s.GetService(typeof(IHttpClientService))).Returns(new HttpClientTestService());
            mocks.Mapper.Setup(x => x.Map<FinishingPrintingCostCalculationViewModel>(It.IsAny<FinishingPrintingCostCalculationModel>()))
                .Returns(new FinishingPrintingCostCalculationViewModel()
                {
                    PreSalesContract = new FinishingPrintingPreSalesContractViewModel() { Id = 1 },
                    UOM = new UomViewModel()
                    {
                        Unit = "a"
                    },
                    Material = new MaterialViewModel()
                    {
                        Name = "a"
                    }
                });
            mocks.Mapper.Setup(x => x.Map<FinishingPrintingPreSalesContractViewModel>(It.IsAny<FinishingPrintingPreSalesContractModel>()))
                .Returns(new FinishingPrintingPreSalesContractViewModel() 
                { 
                    Buyer = new BuyerViewModel() { Id = 1, Type = "Ekspor" } 
                });
            mocks.ServiceProvider.Setup(s => s.GetService(typeof(IFinishingPrintingCostCalculationService))).Returns(ccFacade.Object);
            mocks.ServiceProvider.Setup(s => s.GetService(typeof(IFinishingPrintingPreSalesContractFacade))).Returns(preSCFacade.Object);
            ShinFinishingPrintingSalesContractController controller = (ShinFinishingPrintingSalesContractController)Activator.CreateInstance(typeof(ShinFinishingPrintingSalesContractController), mocks.IdentityService.Object, mocks.ValidateService.Object, mocks.Facade.Object, mocks.Mapper.Object, mocks.ServiceProvider.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = user.Object
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["Authorization"] = "Bearer unittesttoken";
            controller.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");
            return controller;
        }

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

            ViewModel.Details = new List<FinishingPrintingSalesContractDetailViewModel>()
            {
                
            };
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(f => f.Map<ShinFinishingPrintingSalesContractViewModel>(It.IsAny<FinishingPrintingSalesContractModel>())).Returns(ViewModel);

            var controller = GetController(mocks);
            var response = controller.GetPDF(1).Result;

            Assert.NotNull(response);

        }

        [Fact]
        public void Get_PDF_Ekspor_OK()
        {
            var mocks = GetMocks();

            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(f => f.Map<ShinFinishingPrintingSalesContractViewModel>(It.IsAny<FinishingPrintingSalesContractModel>())).Returns(ViewModel);

            var controller = GetController2(mocks);
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

        public override async Task GetById_NotNullModel_ReturnOK()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(this.Model);
            mocks.Mapper.Setup(f => f.Map<ShinFinishingPrintingSalesContractViewModel>(It.IsAny<FinishingPrintingSalesContractModel>())).Returns(this.ViewModel);
            mocks.Mapper.Setup(f => f.Map<FinishingPrintingCostCalculationViewModel>(It.IsAny<FinishingPrintingCostCalculationModel>())).Returns(new FinishingPrintingCostCalculationViewModel()
            {
                PreSalesContract = new FinishingPrintingPreSalesContractViewModel()
                {
                    Id = 1
                }
            });
            ShinFinishingPrintingSalesContractController controller = this.GetController(mocks);
            IActionResult response = await controller.GetById(1);

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }
    }
}
