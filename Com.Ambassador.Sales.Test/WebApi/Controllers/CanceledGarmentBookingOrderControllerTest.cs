using Com.Ambassador.Sales.Test.WebApi.Utils;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.GarmentBookingOrderInterface;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentBookingOrderViewModels;
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
using Xunit;

namespace Com.Ambassador.Sales.Test.WebApi.Controllers
{
    public class CanceledGarmentBookingOrderControllerTest 
    {

        protected (Mock<IIdentityService> IdentityService, Mock<ICanceledGarmentBookingOrderReportFacade> Facade) GetMocks()
        {
            return (IdentityService: new Mock<IIdentityService>(), Facade: new Mock<ICanceledGarmentBookingOrderReportFacade>());
        }

        protected CanceledGarmentBookingOrderReportController GetController((Mock<IIdentityService> IdentityService, Mock<ICanceledGarmentBookingOrderReportFacade> Facade) mocks)
        {
            var user = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            user.Setup(u => u.Claims).Returns(claims);
            CanceledGarmentBookingOrderReportController controller = (CanceledGarmentBookingOrderReportController)Activator.CreateInstance(typeof(CanceledGarmentBookingOrderReportController), mocks.IdentityService.Object, mocks.Facade.Object);
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

        private readonly List<CanceledGarmentBookingOrderReportViewModel> viewModels = new List<CanceledGarmentBookingOrderReportViewModel>();

        [Fact]
        public void Get_WithoutException_ReturnOK()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.Read(null,null,null,null,null,It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(Tuple.Create(viewModels, 1));

            var controller = GetController(mocks);
            var response = controller.GetReportAll(null, null, null, null, null, It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>());

            int statusCode = this.GetStatusCode(response);

            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void Get_Accept_Xls_WithoutException_ReturnOK()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.GenerateExcel(null, null, null, null, null, It.IsAny<int>()))
                .Returns(new MemoryStream());

            var controller = GetController(mocks);
            controller.ControllerContext.HttpContext.Request.Headers["Accept"] = "application/xls";

            var response = controller.GetXlsAll(null, null, null, null, null);

            Assert.Equal("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", response.GetType().GetProperty("ContentType").GetValue(response, null));
        }

        [Fact]
        public void Get_ReadThrowException_ReturnInternalServerError()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.Read(null, null, null, null, null, It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()))
                .Throws(new Exception());

            var controller = GetController(mocks);
            var response = controller.GetReportAll(null, null, null, null, null, It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>());

            int statusCode = this.GetStatusCode(response);

            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        [Fact]
        public void Get_Accept_Xls_Exception_ReturnInternalServerError()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.GenerateExcel(null, null, null, null, null, It.IsAny<int>()))
                .Throws(new Exception());

            var controller = GetController(mocks);
            controller.ControllerContext.HttpContext.Request.Headers["Accept"] = "application/xls";

            var response = controller.GetXlsAll(null, null, null, null, null);
            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }
    }
}