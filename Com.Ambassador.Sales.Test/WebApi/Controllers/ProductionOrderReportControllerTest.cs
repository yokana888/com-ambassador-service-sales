using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.ProductionOrder;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.ViewModels.Report;
using Com.Ambassador.Service.Sales.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Ambassador.Sales.Test.WebApi.Controllers
{
    public class ProductionOrderReportControllerTest
    {
        protected (Mock<IIdentityService> IdentityService, Mock<IProductionOrder> Facade) GetMocks()
        {
            return (IdentityService: new Mock<IIdentityService>(), Facade: new Mock<IProductionOrder>());
        }

        protected ProductionOrderReportController GetController((Mock<IIdentityService> IdentityService, Mock<IProductionOrder> Facade) mocks)
        {
            var user = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            user.Setup(u => u.Claims).Returns(claims);
            ProductionOrderReportController controller = (ProductionOrderReportController)Activator.CreateInstance(typeof(ProductionOrderReportController), mocks.Facade.Object, mocks.IdentityService.Object);
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

        protected int GetStatusCode(IActionResult response)
        {
            return (int)response.GetType().GetProperty("StatusCode").GetValue(response, null);
        }

        [Fact]
        public async Task GetReportAll_WithoutException_ReturnOK()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.GetReport(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(new Tuple<List<ProductionOrderReportViewModel>, int>(new List<ProductionOrderReportViewModel>(), 0));

            var controller = GetController(mocks);
            var response = await controller.GetReportAll(null, null, null, null, null, null, null, null, 1, 25);

            int statusCode = this.GetStatusCode(response);

            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public async Task GetReportAll_Exception_InternalServer()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.GetReport(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()))
                .ThrowsAsync(new Exception());

            var controller = GetController(mocks);
            var response = await controller.GetReportAll(null, null, null, null, null, null, null, null, 1, 25);

            int statusCode = this.GetStatusCode(response);

            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        [Fact]
        public async Task DownloadExcel_WithoutException_ReturnOK()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.GenerateExcel(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<int>()))
                .ReturnsAsync(new MemoryStream());

            var controller = GetController(mocks);
            var response = await controller.GetXlsAll(null, null, null, null, null, null, null, null);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task DownloadExcel_Exception_InternalServer()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.GenerateExcel(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
               It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<int>()))
               .ThrowsAsync(new Exception());

            var controller = GetController(mocks);
            var response = await controller.GetXlsAll(null, null, null, null, null, null, null, null);

            int statusCode = this.GetStatusCode(response);

            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        [Fact]
        public async Task GetDetail_WithoutException_ReturnOK()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.GetDetailReport(It.IsAny<long>()))
                .ReturnsAsync(new ProductionOrderReportDetailViewModel());

            var controller = GetController(mocks);
            var response = await controller.GetDetail(1);
            int statusCode = this.GetStatusCode(response);

            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public async Task GetDetail_Exception_InternalServer()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.GetDetailReport(It.IsAny<long>()))
               .ThrowsAsync(new Exception());

            var controller = GetController(mocks);
            var response = await controller.GetDetail(1);

            int statusCode = this.GetStatusCode(response);

            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }
    }
}
