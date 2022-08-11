using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseInterface;
using Com.Ambassador.Service.Sales.WebApi.Utilities;
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

namespace Com.Ambassador.Sales.Test.WebApi.Utils
{
    public abstract class BaseMonitoringControllerTest<TController, TViewModel, IFacade>
        where TController : BaseMonitoringController<TViewModel, IFacade>
        where TViewModel : new()
        where IFacade : class, IBaseMonitoringFacade<TViewModel>
    {
        protected virtual TViewModel ViewModel
        {
            get { return new TViewModel(); }
        }

        protected virtual List<TViewModel> ViewModels
        {
            get { return new List<TViewModel>(); }
        }

        protected (Mock<IIdentityService> IdentityService, Mock<IFacade> Facade) GetMocks()
        {
            return (IdentityService: new Mock<IIdentityService>(), Facade: new Mock<IFacade>());
        }

        protected TController GetController((Mock<IIdentityService> IdentityService, Mock<IFacade> Facade) mocks)
        {
            var user = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            user.Setup(u => u.Claims).Returns(claims);
            TController controller = (TController)Activator.CreateInstance(typeof(TController), mocks.IdentityService.Object, mocks.Facade.Object);
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
        public void Get_WithoutException_ReturnOK()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns(Tuple.Create(ViewModels, 1));

            var controller = GetController(mocks);
            var response = controller.Get(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>());

            int statusCode = this.GetStatusCode(response);

            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void Get_Accept_Xls_WithoutException_ReturnOK()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.GenerateExcel(It.IsAny<string>()))
                .Returns(Tuple.Create(new MemoryStream(), ""));

            var controller = GetController(mocks);
            controller.ControllerContext.HttpContext.Request.Headers["Accept"] = "application/xls";

            var response = controller.Get(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>());

            Assert.Equal("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", response.GetType().GetProperty("ContentType").GetValue(response, null));
        }

        [Fact]
        public void Get_ReadThrowException_ReturnInternalServerError()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                .Throws(new Exception());

            var controller = GetController(mocks);
            var response = controller.Get(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>());

            int statusCode = this.GetStatusCode(response);

            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }
    }
}
