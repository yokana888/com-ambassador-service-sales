using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.GarmentBookingOrderDataUtils;
using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.GarmentMasterPlan.GarmentSewingBlockingPlanDataUtils;
using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.GarmentMasterPlan.MaxWHConfirmDataUtils;
using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.GarmentMasterPlan.WeeklyPlanDataUtils;
using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.GarmentSewingBlockingPlanProfiles;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.GarmentBookingOrderFacade;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.GarmentMasterPlan.GarmentSewingBlockingPlanFacades;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.GarmentMasterPlan.MaxWHConfirmFacades;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.GarmentMasterPlan.WeeklyPlanFacades;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentBookingOrderLogics;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.GarmentSewingBlockingPlanLogics;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.MaxWHConfirmLogics;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.WeeklyPlanLogics;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentSewingBlockingPlanModel;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentSewingBlockingPlanViewModels;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Ambassador.Sales.Test.BussinesLogic.Facades.GarmentMasterPlan.GarmentSewingBlockingPlanTests
{
    public class GarmentSewingBlockingPlanTest 
    {
        private const string ENTITY = "SewingBlockingPlan";

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

        private GarmentSewingBlockingPlanDataUtil DataUtil(GarmentSewingBlockingPlanFacade facade, SalesDbContext dbContext)
        {
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var WeekserviceProvider = GetWeekServiceProviderMock(dbContext).Object;
            var BOserviceProvider = GetBOServiceProviderMock(dbContext).Object;
            var WHServiceProviderMock = GetWHServiceProviderMock(dbContext).Object;

            var weeklyPlanFacade = new WeeklyPlanFacade(WeekserviceProvider, dbContext);
            var weeklyPlanDataUtil = new WeeklyPlanDataUtil(weeklyPlanFacade);

            var bookingOrderFacade = new GarmentBookingOrderFacade(BOserviceProvider, dbContext);
            var garmentBookingOrderDataUtil = new GarmentBookingOrderDataUtil(bookingOrderFacade);

            var maxWHConfirmFacade = new MaxWHConfirmFacade(WHServiceProviderMock, dbContext);
            var maxWHConfirmDataUtil = new MaxWHConfirmDataUtil(maxWHConfirmFacade);

            var garmentSewingBlockingPlanFacade = new GarmentSewingBlockingPlanFacade(serviceProvider, dbContext);
            var garmentSewingBlockingPlanDataUtil = new GarmentSewingBlockingPlanDataUtil(garmentSewingBlockingPlanFacade, weeklyPlanDataUtil, garmentBookingOrderDataUtil, maxWHConfirmDataUtil);

            

            return garmentSewingBlockingPlanDataUtil;
        }

        protected virtual Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(GarmentSewingBlockingPlanLogic)))
                .Returns(new GarmentSewingBlockingPlanLogic( identityService, dbContext) );

            return serviceProviderMock;
        }

        protected virtual Mock<IServiceProvider> GetWeekServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(WeeklyPlanLogic)))
                .Returns(new WeeklyPlanLogic( identityService, dbContext) );

            return serviceProviderMock;
        }

        protected virtual Mock<IServiceProvider> GetWHServiceProviderMock(SalesDbContext dbContext)
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

        protected virtual Mock<IServiceProvider> GetBOServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            var garmentBookingOrderItemLogic = new GarmentBookingOrderItemLogic(identityService, serviceProviderMock.Object, dbContext);
            var garmentBookingOrderLogic = new GarmentBookingOrderLogic(garmentBookingOrderItemLogic, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(GarmentBookingOrderLogic)))
                .Returns(garmentBookingOrderLogic);

            return serviceProviderMock;
        }


        //protected virtual TDataUtil DataUtil(TFacade facade, TDbContext dbContext = null)
        //{
        //    TDataUtil dataUtil = Activator.CreateInstance(typeof(TDataUtil), facade) as TDataUtil;
        //    return dataUtil;
        //}

        [Fact]
        public virtual async void Create_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            GarmentSewingBlockingPlanFacade facade = new GarmentSewingBlockingPlanFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetNewData();

            var response = await facade.CreateAsync(data);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public async Task Should_Success_Validate_Data_Null()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var iserviceProvider = GetServiceProviderMock(dbContext).Object;
            Mock<IServiceProvider> serviceProvider = new Mock<IServiceProvider>();
            serviceProvider.
                Setup(x => x.GetService(typeof(SalesDbContext)))
                .Returns(dbContext);

            GarmentSewingBlockingPlanFacade facade = new GarmentSewingBlockingPlanFacade(serviceProvider.Object, dbContext);

            var data = await DataUtil(facade, dbContext).GetNewData();
            GarmentSewingBlockingPlanViewModel nullViewModel = new GarmentSewingBlockingPlanViewModel();
            //Assert.True(nullViewModel.Validate(null).Count() > 0);

            ValidationContext validationContext = new ValidationContext(nullViewModel, serviceProvider.Object, null);

            var validationResultCreate = nullViewModel.Validate(validationContext).ToList();
            Assert.True(validationResultCreate.Count() > 0);

        }

        [Fact]
        public async Task Should_Success_Validate_Data_Error_Items()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var iserviceProvider = GetServiceProviderMock(dbContext).Object;
            Mock<IServiceProvider> serviceProvider = new Mock<IServiceProvider>();
            serviceProvider.
                Setup(x => x.GetService(typeof(SalesDbContext)))
                .Returns(dbContext);

            GarmentSewingBlockingPlanFacade facade = new GarmentSewingBlockingPlanFacade(serviceProvider.Object, dbContext);

            var data = await DataUtil(facade, dbContext).GetNewData();
            GarmentSewingBlockingPlanViewModel vm = new GarmentSewingBlockingPlanViewModel
            {
                BookingOrderNo = data.BookingOrderNo,
                BookingOrderDate = data.BookingOrderDate,
                DeliveryDate = data.DeliveryDate,
                Items = new List<GarmentSewingBlockingPlanItemViewModel> {
                    new GarmentSewingBlockingPlanItemViewModel
                    {
                        IsConfirm=true,
                        DeliveryDate= DateTimeOffset.UtcNow.Date.AddDays(-2),
                        WeeklyPlanItemId=data.Items.First().WeeklyPlanItemId,
                        whConfirm=63,
                        Unit=new UnitViewModel
                        {
                            Name="unit",
                            Id=1,
                            Code="unit"
                        }
                    },
                    new GarmentSewingBlockingPlanItemViewModel
                    {
                        IsConfirm=true,
                        DeliveryDate= data.DeliveryDate.Date.AddDays(2),
                        WeeklyPlanItemId=data.Items.First().WeeklyPlanItemId,
                        whConfirm=63,
                        Unit=new UnitViewModel
                        {
                            Name="unit",
                            Id=1,
                            Code="unit"
                        }
                    },
                    new GarmentSewingBlockingPlanItemViewModel
                    {
                        IsConfirm=true,
                        DeliveryDate= data.DeliveryDate.Date.AddDays(2),
                        WeeklyPlanItemId=data.Items.First().WeeklyPlanItemId,
                        whConfirm=63,
                        
                    }
                }
            };
            ValidationContext validationContext1 = new ValidationContext(vm, serviceProvider.Object, null);
            var validationResultCreate1 = vm.Validate(validationContext1).ToList();
            Assert.True(validationResultCreate1.Count() > 0);


        }

        [Fact]
        public async Task Should_Success_Validate_Updated_Data()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var iserviceProvider = GetServiceProviderMock(dbContext).Object;
            Mock<IServiceProvider> serviceProvider = new Mock<IServiceProvider>();
            serviceProvider.
                Setup(x => x.GetService(typeof(SalesDbContext)))
                .Returns(dbContext);

            GarmentSewingBlockingPlanFacade facade = new GarmentSewingBlockingPlanFacade(serviceProvider.Object, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();

            GarmentSewingBlockingPlanViewModel vm = new GarmentSewingBlockingPlanViewModel
            {
                BookingOrderNo = data.BookingOrderNo,
                BookingOrderDate = data.BookingOrderDate,
                DeliveryDate = data.DeliveryDate,
                Id = data.Id,
                Items = new List<GarmentSewingBlockingPlanItemViewModel> {
                    new GarmentSewingBlockingPlanItemViewModel
                    {
                        IsConfirm=true,
                        DeliveryDate= DateTimeOffset.UtcNow.Date.AddDays(-2),
                        WeeklyPlanItemId=data.Items.First().WeeklyPlanItemId,
                        whConfirm=63,
                        Id=data.Items.First().Id,
                        Unit=new UnitViewModel
                        {
                            Name="unit",
                            Id=1,
                            Code="unit"
                        }
                    },
                    new GarmentSewingBlockingPlanItemViewModel
                    {
                        IsConfirm=true,
                        DeliveryDate= data.DeliveryDate.Date.AddDays(2),
                        WeeklyPlanItemId=data.Items.First().WeeklyPlanItemId,
                        whConfirm=63,
                        Unit=new UnitViewModel
                        {
                            Name="unit",
                            Id=1,
                            Code="unit"
                        }
                    },
                    new GarmentSewingBlockingPlanItemViewModel
                    {
                        IsConfirm=true,
                        DeliveryDate= data.DeliveryDate.Date.AddDays(2),
                        WeeklyPlanItemId=data.Items.First().WeeklyPlanItemId,
                        whConfirm=63,

                    }
                }
            };
            ValidationContext validationContext1 = new ValidationContext(vm, serviceProvider.Object, null);
            var validationResultCreate1 = vm.Validate(validationContext1).ToList();
            Assert.True(validationResultCreate1.Count() > 0);
        }

        [Fact]
        public virtual async void Get_All_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            GarmentSewingBlockingPlanFacade facade = Activator.CreateInstance(typeof(GarmentSewingBlockingPlanFacade), serviceProvider, dbContext) as GarmentSewingBlockingPlanFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.Read(1, 25, "{}", new List<string>(), "", "{}");

            Assert.NotEqual(Response.Data.Count, 0);
        }

        [Fact]
        public virtual async void Get_By_Id_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            GarmentSewingBlockingPlanFacade facade = Activator.CreateInstance(typeof(GarmentSewingBlockingPlanFacade), serviceProvider, dbContext) as GarmentSewingBlockingPlanFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.ReadByIdAsync((int)data.Id);

            Assert.NotEqual(Response.Id, 0);
        }

        [Fact]
        public virtual async void UpdateAsync_withExistData_Return_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            GarmentSewingBlockingPlanFacade facade = Activator.CreateInstance(typeof(GarmentSewingBlockingPlanFacade), serviceProvider, dbContext) as GarmentSewingBlockingPlanFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();
            data.Status = "Booking Ada Perubahan";
            var response = await facade.UpdateAsync((int)data.Id, data);

            Assert.NotEqual(response, 0);
        }

        

        [Fact]
        public virtual async void Delete_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            GarmentSewingBlockingPlanFacade facade = Activator.CreateInstance(typeof(GarmentSewingBlockingPlanFacade), serviceProvider, dbContext) as GarmentSewingBlockingPlanFacade;
            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = await facade.DeleteAsync((int)data.Id);
            Assert.NotEqual(Response, 0);
        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile<GarmentSewingBlockingPlanMapper>();
            });
            var mapper = configuration.CreateMapper();

            GarmentSewingBlockingPlanViewModel vm = new GarmentSewingBlockingPlanViewModel { Id = 1 };
            GarmentSewingBlockingPlan model = mapper.Map<GarmentSewingBlockingPlan>(vm);

            Assert.Equal(vm.Id, model.Id);

        }
    }
}
