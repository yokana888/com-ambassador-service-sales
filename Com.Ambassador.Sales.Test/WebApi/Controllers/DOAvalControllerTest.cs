using Com.Ambassador.Sales.Test.WebApi.Utils;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.DOAval;
using Com.Ambassador.Service.Sales.Lib.Models.DOSales;
using Com.Ambassador.Service.Sales.Lib.ViewModels.DOAval;
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
    public class DOAvalControllerTest : BaseControllerTest<DOAvalController, DOSalesModel, DOAvalViewModel, IDOAvalFacade>
    {
        [Fact]
        public async Task Get_DO_PDF_Success()
        {
            var vm = new DOAvalViewModel()
            {
                DOAvalType = "Lokal",
                DOAvalNo = "DOSalesNo",
                Date = DateTimeOffset.UtcNow,
                HeadOfStorage = "HeadOfStorage",
                DOAvalCategory = "Aval",
                Buyer = new Service.Sales.Lib.ViewModels.IntegrationViewModel.BuyerViewModel()
                {
                    Name = "BuyerName",
                },
                //DestinationBuyerName = "DestinationBuyerName",
                PackingUom = "PCS",
                Construction = "c",
                DestinationBuyerAddress = "a",
                DestinationBuyerName = "s",
                Remark = "r",
                Type = "us",
                WeightUom = "kg",
                SalesName = "n",
                Disp = 1,
                DOAvalItems = new List<DOAvalItemViewModel>()
                {
                    new DOAvalItemViewModel()
                    {
                        AvalType = "avalTYpe",
                        Packing = 1,
                        Weight = 1
                    }
                }
            };
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(s => s.Map<DOAvalViewModel>(It.IsAny<DOSalesModel>()))
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
