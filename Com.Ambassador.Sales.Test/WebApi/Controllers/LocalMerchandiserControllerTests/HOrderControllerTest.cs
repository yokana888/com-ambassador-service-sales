using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.LocalMerchandiserInterfaces;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel.LocalMerchandiserViewModels;
using Com.Ambassador.Service.Sales.WebApi.Controllers.LocalMerchandiserControllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.WebApi.Controllers.LocalMerchandiserControllerTests
{
    public class HOrderControllerTest
    {
        private int GetStatusCode(IActionResult response)
        {
            return (int)response.GetType().GetProperty("StatusCode").GetValue(response, null);
        }

        [Fact]
        public void Get_Kode_By_No_Success()
        {
            var mockFacade = new Mock<IHOrderFacade>();
            mockFacade.Setup(s => s.GetKodeByNo(It.IsAny<string>()))
                .Returns(new List<string>());

            HOrderController controller = new HOrderController(mockFacade.Object);

            var response = controller.ReadKodeByNo();

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Get_Kode_By_No_Error()
        {
            var mockFacade = new Mock<IHOrderFacade>();
            mockFacade.Setup(s => s.GetKodeByNo(It.IsAny<string>()))
                .Throws(new Exception());

            HOrderController controller = new HOrderController(mockFacade.Object);

            var response = controller.ReadKodeByNo();

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Get_Data_For_ProductionReport_Success()
        {
            var mockFacade = new Mock<IHOrderFacade>();
            mockFacade.Setup(s => s.GetDataForProductionReportByNo(It.IsAny<string>()))
                .Returns(new List<HOrderDataForProductionReportViewModel>());

            HOrderController controller = new HOrderController(mockFacade.Object);

            var response = controller.GetDataForProductionReportByNo("No");

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Get_Data_For_ProductionReport_Error()
        {
            var mockFacade = new Mock<IHOrderFacade>();
            mockFacade.Setup(s => s.GetDataForProductionReportByNo(It.IsAny<string>()))
                .Throws(new Exception());

            HOrderController controller = new HOrderController(mockFacade.Object);

            var response = controller.GetDataForProductionReportByNo("No");

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
