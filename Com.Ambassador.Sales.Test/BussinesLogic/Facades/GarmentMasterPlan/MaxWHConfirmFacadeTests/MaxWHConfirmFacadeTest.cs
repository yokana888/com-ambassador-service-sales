using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.GarmentMasterPlan.MaxWHConfirmDataUtils;
using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.GarmentMasterPlan.MaxWHConfirmFacades;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.MaxWHConfirmLogics;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentMasterPlan.MaxWHConfirmViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using AutoMapper;
using Xunit;
using Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.GarmentMasterPlanProfiles;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentMasterPlan.MaxWHConfirmModel;

namespace Com.Ambassador.Sales.Test.BussinesLogic.Facades.GarmentMasterPlan.MaxWHConfirmFacadeTests
{
    public class MaxWHConfirmFacadeTest
    {
        private const string ENTITY = "MaxWHConfirm";

        private const string USERNAME = "Unit Test";
        private IServiceProvider ServiceProvider { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return string.Concat(sf.GetMethod().Name, "_", ENTITY);
        }

        private SalesDbContext DbContext(string testName)
        {
            DbContextOptionsBuilder<SalesDbContext> optionsBuilder = new DbContextOptionsBuilder<SalesDbContext>();
            optionsBuilder
                .UseInMemoryDatabase(testName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            SalesDbContext dbContext = new SalesDbContext(optionsBuilder.Options);

            return dbContext;
        }

        private MaxWHConfirmDataUtil DataUtil(MaxWHConfirmFacade facade, SalesDbContext dbContext)
        {
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            var maxWHConfirmFacade = new MaxWHConfirmFacade(serviceProvider, dbContext);
            var maxWHConfirmDataUtil = new MaxWHConfirmDataUtil(maxWHConfirmFacade);

            return maxWHConfirmDataUtil;
        }

        protected virtual Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(MaxWHConfirmLogic)))
                .Returns(new MaxWHConfirmLogic(identityService, dbContext));

            return serviceProviderMock;
        }

        [Fact]
        public virtual async void Create_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            MaxWHConfirmFacade facade = new MaxWHConfirmFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetNewData();

            var response = await facade.CreateAsync(data);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public  async void DeleteAsync_NotImplementedException()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            MaxWHConfirmFacade facade = new MaxWHConfirmFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();

            //Assert
            await Assert.ThrowsAsync<NotImplementedException>(() => facade.DeleteAsync((int)data.Id));
        }

        [Fact]
        public async void UpdateAsync_NotImplementedException()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            MaxWHConfirmFacade facade = new MaxWHConfirmFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();
            var newData = await DataUtil(facade, dbContext).GetNewData();
            //Assert
            await Assert.ThrowsAsync<NotImplementedException>(() => facade.UpdateAsync((int)data.Id, newData));
        }

        [Fact]
        public void Should_Success_Validate_Data()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            MaxWHConfirmFacade facade = new MaxWHConfirmFacade(serviceProvider, dbContext);

            var data = DataUtil(facade, dbContext).GetNewData();
            MaxWHConfirmViewModel nullViewModel = new MaxWHConfirmViewModel();
            Assert.True(nullViewModel.Validate(null).Count() > 0);

            MaxWHConfirmViewModel vm = new MaxWHConfirmViewModel
            {
                UnitMaxValue=-2,
                SKMaxValue=-2
            };

            Assert.True(vm.Validate(null).Count() > 0);


        }

        [Fact]
        public virtual async void Get_All_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            MaxWHConfirmFacade facade = Activator.CreateInstance(typeof(MaxWHConfirmFacade), serviceProvider, dbContext) as MaxWHConfirmFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.Read(1, 25, "{}", new List<string>(), "", "{}");

            Assert.NotEqual(Response.Data.Count, 0);
        }

        [Fact]
        public virtual async void Get_By_Id_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            MaxWHConfirmFacade facade = Activator.CreateInstance(typeof(MaxWHConfirmFacade), serviceProvider, dbContext) as MaxWHConfirmFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.ReadByIdAsync((int)data.Id);

            Assert.NotEqual(Response.Id, 0);
        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MaxWHConfirmProfile>();
            });
            var mapper = configuration.CreateMapper();

            MaxWHConfirmViewModel vm = new MaxWHConfirmViewModel { Id = 1 };
            MaxWHConfirm model = mapper.Map<MaxWHConfirm>(vm);

            Assert.Equal(vm.Id, model.Id);

        }
    }
}
