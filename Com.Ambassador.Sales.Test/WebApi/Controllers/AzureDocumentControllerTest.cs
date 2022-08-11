using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface;
using Com.Ambassador.Service.Sales.Lib.Services;
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
using static Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.AzureDocumentFacade;

namespace Com.Ambassador.Sales.Test.WebApi.Controllers
{
    public class AzureDocumentControllerTest
    {
        protected AzureDocumentController GetController((Mock<IIdentityService> IdentityService, Mock<IAzureDocumentFacade> Facade) mocks)
        {
            var user = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            user.Setup(u => u.Claims).Returns(claims);
            AzureDocumentController controller = new AzureDocumentController(mocks.IdentityService.Object, mocks.Facade.Object);
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

        private (Mock<IIdentityService> IdentityService, Mock<IAzureDocumentFacade> Facade) GetMocks()
        {
            return (IdentityService: new Mock<IIdentityService>(), Facade: new Mock<IAzureDocumentFacade>());
        }

        [Fact]
        public async Task Get_WithoutException_ReturnOK()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(f => f.DownloadDocument(It.IsAny<string>()))
                .ReturnsAsync(new DocumentFileResult(new MemoryStream(), "FILE_NAME", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"));

            var controller = GetController(mocks);

            var response = await controller.DownloadDocument(It.IsAny<string>(), It.IsAny<string>());

            var fileType = response.GetType().GetProperty("ContentType").GetValue(response, null);
            Assert.Equal("application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileType);
        }

        [Fact]
        public async Task Get_ReadThrowException_ReturnInternalServerError()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(f => f.DownloadDocument(It.IsAny<string>()))
                .Throws(new Exception());

            var controller = GetController(mocks);

            var response = await controller.DownloadDocument(It.IsAny<string>(), It.IsAny<string>());

            var statusCode = (int)response.GetType().GetProperty("StatusCode").GetValue(response, null); ;
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }
    }
}
