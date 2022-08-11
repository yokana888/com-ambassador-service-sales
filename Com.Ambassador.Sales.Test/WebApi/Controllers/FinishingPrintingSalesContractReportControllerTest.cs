using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.FinisihingPrintingSalesContract;
using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Ambassador.Sales.Test.WebApi.Controllers
{
    public class FinishingPrintingSalesContractReportControllerTest
    {
        private const string ENTITY = "FinishingPrintingSalesContractReport";
        protected Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            var finishingprintingDetailLogic = new FinishingPrintingSalesContractDetailLogic(serviceProviderMock.Object, identityService, dbContext);
            var finishingprintingLogic = new FinishingPrintingSalesContractLogic(finishingprintingDetailLogic, serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(FinishingPrintingSalesContractLogic)))
                .Returns(finishingprintingLogic);

            return serviceProviderMock;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return string.Concat(sf.GetMethod().Name, "_", ENTITY);
        }

        protected SalesDbContext DbContext(string testName)
        {
            DbContextOptionsBuilder<SalesDbContext> optionsBuilder = new DbContextOptionsBuilder<SalesDbContext>();
            optionsBuilder
                .UseInMemoryDatabase(testName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            SalesDbContext dbContext = Activator.CreateInstance(typeof(SalesDbContext), optionsBuilder.Options) as SalesDbContext;

            return dbContext;
        }

        protected virtual FinisihingPrintingSalesContractDataUtil DataUtil(FinishingPrintingSalesContractFacade facade, SalesDbContext dbContext = null)
        {
            FinisihingPrintingSalesContractDataUtil dataUtil = Activator.CreateInstance(typeof(FinisihingPrintingSalesContractDataUtil), facade) as FinisihingPrintingSalesContractDataUtil;
            return dataUtil;
        }

        protected FinishingPrintingSalesContractReportController GetController(FinishingPrintingSalesContractReportFacade facade)
        {
            var user = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            user.Setup(u => u.Claims).Returns(claims);

            FinishingPrintingSalesContractReportController controller = (FinishingPrintingSalesContractReportController)Activator.CreateInstance(typeof(FinishingPrintingSalesContractReportController), facade);
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
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            FinishingPrintingSalesContractReportFacade facade = new FinishingPrintingSalesContractReportFacade(serviceProvider, dbContext);
            FinishingPrintingSalesContractFacade fpFacade = new FinishingPrintingSalesContractFacade(serviceProvider, dbContext);
            var controller = GetController(facade);

            var data = await DataUtil(fpFacade, dbContext).GetTestData();

            var response = controller.GetReportAll(null, null, null, null, DateTime.MinValue, DateTime.MaxValue, 1, 25);

            int statusCode = this.GetStatusCode(response);

            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public async Task GenerateExcel_WithoutException_ReturnOK()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            FinishingPrintingSalesContractReportFacade facade = new FinishingPrintingSalesContractReportFacade(serviceProvider, dbContext);
            FinishingPrintingSalesContractFacade fpFacade = new FinishingPrintingSalesContractFacade(serviceProvider, dbContext);
            var controller = GetController(facade);

            var data = await DataUtil(fpFacade, dbContext).GetTestData();

            var response = controller.GetXlsAll(null, null, null, null, DateTime.MinValue, DateTime.MaxValue);

            Assert.NotNull(response);
        }
    }
}
