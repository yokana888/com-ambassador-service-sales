using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.Garment.GarmentMerchandiser;
using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.GarmentPreSalesContractDataUtils;
using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.CostCalculationGarments;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.Garment;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.GarmentPreSalesContractFacades;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.CostCalculationGarments;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.Garment;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentPreSalesContractLogics;
using Com.Ambassador.Service.Sales.Lib.Helpers;
using Com.Ambassador.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Ambassador.Service.Sales.Lib.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.BussinesLogic.Facades.Garment.GarmentMerchandiser
{
    public class Garment_BudgetValidationPPICFacadeTest
    {
        private const string ENTITY = "ROGarmentValidation";

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

            SalesDbContext dbContext = new SalesDbContext(optionsBuilder.Options);

            return dbContext;
        }

        protected CostCalculationGarmentDataUtil DataUtil(CostCalculationGarmentFacade facade, IServiceProvider serviceProvider = null, SalesDbContext dbContext = null)
        {
            serviceProvider = serviceProvider ?? GetServiceProviderMock(dbContext).Object;

            GarmentPreSalesContractFacade garmentPreSalesContractFacade = new GarmentPreSalesContractFacade(serviceProvider, dbContext);
            GarmentPreSalesContractDataUtil garmentPreSalesContractDataUtil = new GarmentPreSalesContractDataUtil(garmentPreSalesContractFacade);

            CostCalculationGarmentDataUtil costCalculationGarmentDataUtil = new CostCalculationGarmentDataUtil(facade, garmentPreSalesContractDataUtil);
            return costCalculationGarmentDataUtil;
        }

        protected Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            CostCalculationGarmentMaterialLogic costCalculationGarmentMaterialLogic = new CostCalculationGarmentMaterialLogic(serviceProviderMock.Object, identityService, dbContext);
            CostCalculationGarmentLogic costCalculationGarmentLogic = new CostCalculationGarmentLogic(costCalculationGarmentMaterialLogic, serviceProviderMock.Object, identityService, dbContext);

            GarmentPreSalesContractLogic garmentPreSalesContractLogic = new GarmentPreSalesContractLogic(identityService, dbContext);

            var azureImageFacadeMock = new Mock<IAzureImageFacade>();
            azureImageFacadeMock
                .Setup(s => s.DownloadImage(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync("");

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IIdentityService)))
                .Returns(identityService);
            serviceProviderMock
                .Setup(x => x.GetService(typeof(CostCalculationGarmentLogic)))
                .Returns(costCalculationGarmentLogic);
            serviceProviderMock
                .Setup(x => x.GetService(typeof(GarmentPreSalesContractLogic)))
                .Returns(garmentPreSalesContractLogic);
            serviceProviderMock
                .Setup(x => x.GetService(typeof(IAzureImageFacade)))
                .Returns(azureImageFacadeMock.Object);

            Garment_BudgetValidationPPICLogic rOGarmentValidationLogic = new Garment_BudgetValidationPPICLogic(serviceProviderMock.Object);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(Garment_BudgetValidationPPICLogic)))
                .Returns(rOGarmentValidationLogic);

            return serviceProviderMock;
        }

        [Fact]
        public async void Validate_RO_Garment_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProviderMock = GetServiceProviderMock(dbContext);

            Mock<IHttpClientService> httpClientMock = new Mock<IHttpClientService>();

            httpClientMock.Setup(x => x.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.Created));

            serviceProviderMock.Setup(x => x.GetService(typeof(IHttpClientService)))
                .Returns(httpClientMock.Object);

            var serviceProvider = serviceProviderMock.Object;

            CostCalculationGarmentFacade facade = new CostCalculationGarmentFacade(serviceProvider, dbContext);

            var dataCostCalculationGarment = await DataUtil(facade, serviceProvider, dbContext).GetTestData();
            foreach (var material in dataCostCalculationGarment.CostCalculationGarment_Materials)
            {
                material.IsPRMaster = false;
            }

            Garment_BudgetValidationPPICFacade garmentValidationFacade = new Garment_BudgetValidationPPICFacade(serviceProvider, dbContext);

            var productDict = dataCostCalculationGarment.CostCalculationGarment_Materials.ToDictionary(k => long.Parse(k.ProductId), v => v.ProductCode);

            var result = await garmentValidationFacade.ValidateROGarment(dataCostCalculationGarment, productDict);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async void Validate_RO_Garment_Error()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            CostCalculationGarmentFacade facade = new CostCalculationGarmentFacade(serviceProvider, dbContext);

            var dataCostCalculationGarment = await DataUtil(facade, serviceProvider, dbContext).GetTestData();
            foreach (var material in dataCostCalculationGarment.CostCalculationGarment_Materials)
            {
                material.IsPRMaster = false;
            }

            Garment_BudgetValidationPPICFacade garmentValidationFacade = new Garment_BudgetValidationPPICFacade(serviceProvider, dbContext);

            var productDict = dataCostCalculationGarment.CostCalculationGarment_Materials.ToDictionary(k => long.Parse(k.ProductId), v => v.ProductCode);

            Exception exception = await Assert.ThrowsAsync<Exception>(() => garmentValidationFacade.ValidateROGarment(dataCostCalculationGarment, productDict));

            Assert.NotNull(exception);
        }

        [Fact]
        public async void Validate_RO_Garment_Success_Category_PROCESS()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProviderMock = GetServiceProviderMock(dbContext);

            Mock<IHttpClientService> httpClientMock = new Mock<IHttpClientService>();

            httpClientMock.Setup(x => x.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("{ data: { Items: [] } }", Encoding.UTF8, General.JsonMediaType) });
            httpClientMock.Setup(x => x.PutAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.Created));

            serviceProviderMock.Setup(x => x.GetService(typeof(IHttpClientService)))
                .Returns(httpClientMock.Object);

            var serviceProvider = serviceProviderMock.Object;

            CostCalculationGarmentFacade facade = new CostCalculationGarmentFacade(serviceProvider, dbContext);

            var dataCostCalculationGarment = await DataUtil(facade, serviceProvider, dbContext).GetTestData();
            foreach (var material in dataCostCalculationGarment.CostCalculationGarment_Materials)
            {
                material.CategoryName = "PROCESS";
                material.IsPRMaster = false;
            }

            Garment_BudgetValidationPPICFacade garmentValidationFacade = new Garment_BudgetValidationPPICFacade(serviceProvider, dbContext);

            var productDict = dataCostCalculationGarment.CostCalculationGarment_Materials.ToDictionary(k => long.Parse(k.ProductId), v => v.ProductCode);

            var result = await garmentValidationFacade.ValidateROGarment(dataCostCalculationGarment, productDict);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async void Validate_RO_Garment_Error_Category_PROCESS()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProviderMock = GetServiceProviderMock(dbContext);

            //serviceProviderMock.Setup()

            var serviceProvider = serviceProviderMock.Object;

            CostCalculationGarmentFacade facade = new CostCalculationGarmentFacade(serviceProvider, dbContext);

            var dataCostCalculationGarment = await DataUtil(facade, serviceProvider, dbContext).GetTestData();
            foreach (var material in dataCostCalculationGarment.CostCalculationGarment_Materials)
            {
                material.CategoryName = "PROCESS";
                material.IsPRMaster = false;
            }

            Garment_BudgetValidationPPICFacade garmentValidationFacade = new Garment_BudgetValidationPPICFacade(serviceProvider, dbContext);

            var productDict = dataCostCalculationGarment.CostCalculationGarment_Materials.ToDictionary(k => long.Parse(k.ProductId), v => v.ProductCode);

            Exception exception = await Assert.ThrowsAsync<Exception>(() => garmentValidationFacade.ValidateROGarment(dataCostCalculationGarment, productDict));

            Assert.NotNull(exception);
        }

        [Fact]
        public async void Validate_RO_Garment_Error_Category_Mixed()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            CostCalculationGarmentFacade facade = new CostCalculationGarmentFacade(serviceProvider, dbContext);

            var dataCostCalculationGarment = await DataUtil(facade, serviceProvider, dbContext).GetTestData();
            foreach (var material in dataCostCalculationGarment.CostCalculationGarment_Materials)
            {
                material.IsPRMaster = false;
            }
            dataCostCalculationGarment.CostCalculationGarment_Materials.Add(new CostCalculationGarment_Material
            {
                ProductId = "2",
                CategoryName = "PROCESS",
                IsPRMaster = false
            });

            Garment_BudgetValidationPPICFacade garmentValidationFacade = new Garment_BudgetValidationPPICFacade(serviceProvider, dbContext);

            var productDict = dataCostCalculationGarment.CostCalculationGarment_Materials.ToDictionary(k => long.Parse(k.ProductId), v => v.ProductCode);

            Exception exception = await Assert.ThrowsAsync<Exception>(() => garmentValidationFacade.ValidateROGarment(dataCostCalculationGarment, productDict));

            Assert.NotNull(exception);
        }
    }
}
