using Com.Ambassador.Sales.Test.WebApi.Utils;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.DOStock;
using Com.Ambassador.Service.Sales.Lib.Models.DOSales;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.DOStock;
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
    public class DOStockControllerTest : BaseControllerTest<DOStockController, DOSalesModel, DOStockViewModel, IDOStockFacade>
    {
        [Fact]
        public async Task Get_DO_PDF_Success()
        {
            var vm = new DOStockViewModel()
            {
                DOStockType = "Lokal",
                DOStockNo = "DOSalesNo",
                Date = DateTimeOffset.UtcNow,
                HeadOfStorage = "HeadOfStorage",
                DOStockCategory = "Aval",
                Buyer = new Service.Sales.Lib.ViewModels.IntegrationViewModel.BuyerViewModel()
                {
                    Name = "BuyerName",
                },
                //DestinationBuyerName = "DestinationBuyerName",
                PackingUom = "PCS",
                DestinationBuyerAddress = "a",
                DestinationBuyerName = "s",
                Remark = "r",
                Type = "us",
                SalesName = "n",
                Disp = 1,
                LengthUom = "s",
                DOStockItems = new List<DOStockItemViewModel>()
                {
                    new DOStockItemViewModel()
                    {
                        Packing = 1,
                        ProductionOrder = new Service.Sales.Lib.ViewModels.ProductionOrder.ProductionOrderViewModel()
                        {
                            Id = 1,
                            OrderNo = "s"
                        },
                        Length = 1,
                        UnitOrCode = "s0",
                        ConstructionName = "sd",
                    }
                }
            };
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(s => s.Map<DOStockViewModel>(It.IsAny<DOSalesModel>()))
                .Returns(vm);
            var controller = GetController(mocks);
            var response = await controller.GetPdf(1);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Get_DO_PDF_NotFound()
        {

            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(default(DOSalesModel));
            var controller = GetController(mocks);
            var response = await controller.GetPdf(1);

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.NotFound, statusCode);
        }

        [Fact]
        public async Task Get_DO_PDF_InternalError()
        {

            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception());
            var controller = GetController(mocks);
            var response = await controller.GetPdf(1);

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        
    }
}
