using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.Garment.GarmentMerchandiser;
using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.GarmentPreSalesContractDataUtils;
using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.CostCalculationGarments;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.GarmentPreSalesContractFacades;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.CostCalculationGarments;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentPreSalesContractLogics;
using Com.Ambassador.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Ambassador.Service.Sales.Lib.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Ambassador.Sales.Test.BussinesLogic.Facades.Garment.GarmentMerchandiser
{
    public class CostCalculationGarmentFacadeTest : BaseFacadeTest<SalesDbContext, CostCalculationGarmentFacade, CostCalculationGarmentLogic, CostCalculationGarment, CostCalculationGarmentDataUtil>
    {
        private const string ENTITY = "CostCalculationGarment";

        public CostCalculationGarmentFacadeTest() : base(ENTITY)
        {
        }

        protected override CostCalculationGarmentDataUtil DataUtil(CostCalculationGarmentFacade facade, SalesDbContext dbContext = null)
        {
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            GarmentPreSalesContractFacade garmentPreSalesContractFacade = new GarmentPreSalesContractFacade(serviceProvider, dbContext);
            GarmentPreSalesContractDataUtil garmentPreSalesContractDataUtil = new GarmentPreSalesContractDataUtil(garmentPreSalesContractFacade);

            CostCalculationGarmentDataUtil costCalculationGarmentDataUtil = new CostCalculationGarmentDataUtil(facade, garmentPreSalesContractDataUtil);
            return costCalculationGarmentDataUtil;
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
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
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);
            serviceProviderMock
                .Setup(x => x.GetService(typeof(CostCalculationGarmentMaterialLogic)))
                .Returns(costCalculationGarmentMaterialLogic);
            serviceProviderMock
                .Setup(x => x.GetService(typeof(CostCalculationGarmentLogic)))
                .Returns(costCalculationGarmentLogic);
            serviceProviderMock
                .Setup(x => x.GetService(typeof(GarmentPreSalesContractLogic)))
                .Returns(garmentPreSalesContractLogic);
            serviceProviderMock
                .Setup(x => x.GetService(typeof(IAzureImageFacade)))
                .Returns(azureImageFacadeMock.Object);

            return serviceProviderMock;
        }

        [Fact]
        public virtual async void Get_ROAcceptance_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            CostCalculationGarmentFacade facade = Activator.CreateInstance(typeof(CostCalculationGarmentFacade), serviceProvider, dbContext) as CostCalculationGarmentFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.ReadForROAcceptance(1, 25, "{}", new List<string>(), "", "{}");

            Assert.NotEqual(Response.Data.Count, 0);
        }

        [Fact]
        public async void ROAcceptance_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            CostCalculationGarmentFacade facade = new CostCalculationGarmentFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();
            List<long> listData = new List<long> { data.Id };
            var Response = await facade.AcceptanceCC(listData, "test");
            Assert.NotEqual(Response, 0);
        }

        [Fact]
        public virtual async void Get_ROAvailable_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            CostCalculationGarmentFacade facade = Activator.CreateInstance(typeof(CostCalculationGarmentFacade), serviceProvider, dbContext) as CostCalculationGarmentFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.ReadForROAvailable(1, 25, "{}", new List<string>(), "", "{}");

            Assert.NotEqual(Response.Data.Count, 0);
        }

        [Fact]
        public async void ROAvailable_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            CostCalculationGarmentFacade facade = new CostCalculationGarmentFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();
            List<long> listData = new List<long> { data.Id };
            var Response = await facade.AvailableCC(listData, "test");
            Assert.NotEqual(Response, 0);
        }

        [Fact]
        public virtual async void Get_RODistribute_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            CostCalculationGarmentFacade facade = Activator.CreateInstance(typeof(CostCalculationGarmentFacade), serviceProvider, dbContext) as CostCalculationGarmentFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.ReadForRODistribution(1, 25, "{}", new List<string>(), "", "{}");

            Assert.NotEqual(Response.Data.Count, 0);
        }

        [Fact]
        public async void RODistribute_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            CostCalculationGarmentFacade facade = new CostCalculationGarmentFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();
            List<long> listData = new List<long> { data.Id };
            var Response = await facade.DistributeCC(listData, "test");
            Assert.NotEqual(Response, 0);
        }

        [Fact]
        public async Task PostCC_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            CostCalculationGarmentFacade facade = new CostCalculationGarmentFacade(serviceProvider, dbContext);
            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = await facade.PostCC(new List<long> { data.Id });
            Assert.NotEqual(Response, 0);

            var ResultData = await facade.ReadByIdAsync((int)data.Id);
            Assert.Equal(ResultData.IsPosted, true);
        }

        [Fact]
        public async Task UnpostCC_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            CostCalculationGarmentFacade facade = new CostCalculationGarmentFacade(serviceProvider, dbContext);
            var data = await DataUtil(facade, dbContext).GetTestData();

            var reason = "Alasan kenapa melakukan unpost.";
            var Response = await facade.UnpostCC(data.Id, reason);
            Assert.NotEqual(Response, 0);

            var ResultData = await facade.ReadByIdAsync((int)data.Id);
            Assert.Equal(ResultData.IsPosted || ResultData.IsApprovedMD || ResultData.IsApprovedPurchasing || ResultData.IsApprovedIE || ResultData.IsApprovedKadivMD || ResultData.IsApprovedPPIC, false);
        }

        [Fact]
        public async Task UnpostCC_Error()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            CostCalculationGarmentFacade facade = new CostCalculationGarmentFacade(serviceProvider, dbContext);

            await Assert.ThrowsAnyAsync<Exception>(async () => await facade.UnpostCC(0, string.Empty));
        }

        [Fact]
        public async Task Patch_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            CostCalculationGarmentFacade facade = new CostCalculationGarmentFacade(serviceProvider, dbContext);
            var data = await DataUtil(facade, dbContext).GetTestData();

            JsonPatchDocument<CostCalculationGarment> jsonPatch = new JsonPatchDocument<CostCalculationGarment>();
            jsonPatch.Replace(m => m.IsPosted, true);

            int Response = await facade.Patch(data.Id, jsonPatch);
            Assert.NotEqual(Response, 0);

            var ResultData = await facade.ReadByIdAsync((int)data.Id);
            Assert.Equal(ResultData.IsPosted, true);
        }

        [Fact]
        public async Task Patch_Error()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            CostCalculationGarmentFacade facade = new CostCalculationGarmentFacade(serviceProvider, dbContext);
            var data = await DataUtil(facade, dbContext).GetTestData();

            JsonPatchDocument<CostCalculationGarment> jsonPatch = new JsonPatchDocument<CostCalculationGarment>();
            jsonPatch.Replace(m => m.Id, 0);

            var Response = await Assert.ThrowsAnyAsync<Exception>(async () => await facade.Patch(data.Id, jsonPatch));
            Assert.NotEqual(Response.Message, null);
        }

        [Fact]
        public async Task Get_Unpost_Reason_Creators_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            CostCalculationGarmentFacade facade = new CostCalculationGarmentFacade(serviceProvider, dbContext);
            var data = await DataUtil(facade, dbContext).GetTestData();
            await facade.PostCC(new List<long> { data.Id });
            await facade.UnpostCC(data.Id, "Alasan Unpost");

            var Response = facade.ReadUnpostReasonCreators(data.CreatedBy, 1, 25);

            Assert.NotEmpty(Response);
        }

		[Fact]
		public virtual async void CostCalculationGarmentDataProductionReport_Success()
		{
			var dbContext = DbContext(GetCurrentMethod());
			var serviceProvider = GetServiceProviderMock(dbContext).Object;

			CostCalculationGarmentFacade facade = Activator.CreateInstance(typeof(CostCalculationGarmentFacade), serviceProvider, dbContext) as CostCalculationGarmentFacade;

			var data = await DataUtil(facade, dbContext).GetTestData();

			var Response = facade.GetComodityQtyOrderHoursBuyerByRo(data.RO_Number);

			Assert.NotNull(Response);
		}

        [Fact]
        public virtual async void Get_Dynamic_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            CostCalculationGarmentFacade facade = new CostCalculationGarmentFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.ReadDynamic(1, 25, "{}", "new(Id)", null, "{}", "[]");

            Assert.NotEqual(Response.Data.Count, 0);
        }

        [Fact]
        public virtual async void Get_Material_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            CostCalculationGarmentFacade facade = new CostCalculationGarmentFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.ReadMaterials(1, 25, "{}", "new(Id)", null, "{}", "[]");

            Assert.NotEqual(Response.Data.Count, 0);
        }

        [Fact]
        public virtual async void Get_Materials_By_PRMasterItemIds_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            CostCalculationGarmentFacade facade = new CostCalculationGarmentFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();

            var prMasterItemIds = JsonConvert.SerializeObject(data.CostCalculationGarment_Materials.Select(s => s.PRMasterItemId));

            var Response = facade.ReadMaterialsByPRMasterItemIds(1, 25, "{}", "new(Id)", null, "{}", "[]", prMasterItemIds);

            Assert.NotEqual(Response.Data.Count, 0);
        }
    }
}
