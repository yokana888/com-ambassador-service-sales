using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.SalesInvoiceExport;
using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.SalesInvoiceExport;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.SalesInvoiceExport;
using Com.Ambassador.Service.Sales.Lib.Models.SalesInvoiceExport;
using Com.Ambassador.Service.Sales.Lib.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Ambassador.Sales.Test.BussinesLogic.Facades.SalesInvoiceExport
{
    public class SalesInvoiceExportFacadeTest : BaseFacadeTest<SalesDbContext, SalesInvoiceExportFacade, SalesInvoiceExportLogic, SalesInvoiceExportModel, SalesInvoiceExportDataUtil>
    {
        private const string ENTITY = "SalesInvoiceExport";
        public SalesInvoiceExportFacadeTest() : base(ENTITY)
        {
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(new HttpClientTestService());

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            var salesInvoiceItemLogic = new SalesInvoiceExportItemLogic(serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(SalesInvoiceExportItemLogic)))
                .Returns(salesInvoiceItemLogic);

            var salesInvoiceDetailLogic = new SalesInvoiceExportDetailLogic(serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(SalesInvoiceExportDetailLogic)))
                .Returns(salesInvoiceDetailLogic);

            var salesInvoiceLogic = new SalesInvoiceExportLogic(serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(SalesInvoiceExportLogic)))
                .Returns(salesInvoiceLogic);


            return serviceProviderMock;
        }

        [Fact]
        public virtual async void Create_Success_SalesInvoiceNo_Category_Spinning()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext);

            serviceProvider.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(new HttpClientTestService());

            SalesInvoiceExportFacade facade = Activator.CreateInstance(typeof(SalesInvoiceExportFacade), serviceProvider.Object, dbContext) as SalesInvoiceExportFacade;

            var data = await DataUtil(facade, dbContext).GetNewData();
            data.SalesInvoiceCategory = "SPINNING";

            var response = await facade.CreateAsync(data);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public virtual async void Create_Success_SalesInvoiceNo_Category_DyeingPrinting_FPType_DyeingFinishing()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext);

            serviceProvider.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(new HttpClientTestService());

            SalesInvoiceExportFacade facade = Activator.CreateInstance(typeof(SalesInvoiceExportFacade), serviceProvider.Object, dbContext) as SalesInvoiceExportFacade;

            var data = await DataUtil(facade, dbContext).GetNewData();
            data.SalesInvoiceCategory = "DYEINGPRINTING";
            data.FPType = "Dyeing/Finishing";

            var response = await facade.CreateAsync(data);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public virtual async void Create_Success_SalesInvoiceNo_Category_DyeingPrinting_FPType_Printing()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext);

            serviceProvider.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(new HttpClientTestService());

            SalesInvoiceExportFacade facade = Activator.CreateInstance(typeof(SalesInvoiceExportFacade), serviceProvider.Object, dbContext) as SalesInvoiceExportFacade;

            var data = await DataUtil(facade, dbContext).GetNewData();
            data.SalesInvoiceCategory = "DYEINGPRINTING";
            data.FPType = "Printing";

            var response = await facade.CreateAsync(data);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public virtual async void Create_Success_SalesInvoiceNo_Category_Weaving()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext);

            serviceProvider.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(new HttpClientTestService());

            SalesInvoiceExportFacade facade = Activator.CreateInstance(typeof(SalesInvoiceExportFacade), serviceProvider.Object, dbContext) as SalesInvoiceExportFacade;

            var data = await DataUtil(facade, dbContext).GetNewData();
            data.SalesInvoiceCategory = "WEAVING";

            var response = await facade.CreateAsync(data);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public async Task CreateAsync_Throws_Exception()
        {
            //Setup
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext);

            serviceProvider.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(new HttpClientTestService());

            SalesInvoiceExportFacade facade = new SalesInvoiceExportFacade( serviceProvider.Object, dbContext) ;

            var data = await DataUtil(facade, dbContext).GetNewData();
            data.SalesInvoiceCategory = "SPINNING";
            data.SalesInvoiceExportDetails = null;

            //Assert
            await Assert.ThrowsAsync<Exception>(() => facade.CreateAsync(data));
        }
    }
}
