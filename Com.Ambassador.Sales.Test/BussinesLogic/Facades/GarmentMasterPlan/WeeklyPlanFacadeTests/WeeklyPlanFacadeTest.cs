using AutoMapper;
using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.GarmentMasterPlan.WeeklyPlanDataUtils;
using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.GarmentMasterPlanProfiles;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.GarmentMasterPlan.WeeklyPlanFacades;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.WeeklyPlanLogics;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentMasterPlan.WeeklyPlanModels;
using Com.Ambassador.Service.Sales.Lib.ViewModels.Garment.WeeklyPlanViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Ambassador.Sales.Test.BussinesLogic.Facades.GarmentMasterPlan.WeeklyPlanFacadeTests
{
    public class WeeklyPlanFacadeTest : BaseFacadeTest<SalesDbContext, WeeklyPlanFacade, WeeklyPlanLogic, GarmentWeeklyPlan, WeeklyPlanDataUtil>
    {
        private const string ENTITY = "GarmentWeeklyPlan";

        public WeeklyPlanFacadeTest() : base(ENTITY)
        {
        }

        [Fact]
        public  async Task GetYears_Return_Success()
        {
           //Settup
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            WeeklyPlanFacade facade = new WeeklyPlanFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();

            //Act
            List<string> response = facade.GetYears(data.Year.ToString());

            //Assert
            Assert.NotNull(response);
            Assert.NotEqual( 0, response.Count());
        }

        [Fact]
        public async Task GetWeekById_Return_Success()
        {
            //Settup
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            WeeklyPlanFacade facade = new WeeklyPlanFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();

            //Act
            GarmentWeeklyPlanItem response = facade.GetWeekById((int)data.Id);

            //Assert
            Assert.NotNull(response);
            Assert.NotEqual(0, response.Id);
        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<GarmentWeeklyPlanMapper>();
            });
            var mapper = configuration.CreateMapper();

            GarmentWeeklyPlanViewModel vm = new GarmentWeeklyPlanViewModel { Id = 1 };
            GarmentWeeklyPlan model = mapper.Map<GarmentWeeklyPlan>(vm);

            Assert.Equal(vm.Id, model.Id);

        }
    }
}
