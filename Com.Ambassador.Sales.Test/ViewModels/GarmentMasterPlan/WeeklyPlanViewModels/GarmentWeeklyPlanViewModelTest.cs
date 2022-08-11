using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.Garment.WeeklyPlanInterfaces;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentMasterPlan.WeeklyPlanModels;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.Garment.WeeklyPlanViewModels;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.ViewModels.GarmentMasterPlan.WeeklyPlanViewModels
{
    public class GarmentWeeklyPlanViewModelTest
    {
        [Fact]
        public void ValidateDefault()
        {
            GarmentWeeklyPlanViewModel viewModel = new GarmentWeeklyPlanViewModel();
            var result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public void Validate_When_Id_Empty()
        {
            GarmentWeeklyPlanViewModel viewModel = new GarmentWeeklyPlanViewModel()
            {
                Unit = new UnitViewModel()
                {
                    Id = 1,
                }
            };

            Dictionary<string, string> order = new Dictionary<string, string>();
            var data = new List<GarmentWeeklyPlan>() {
                    new GarmentWeeklyPlan()
            };
            ReadResponse<GarmentWeeklyPlan> response = new ReadResponse<GarmentWeeklyPlan>(data, 1, order, new List<string>());

            Mock<IWeeklyPlanFacade> facade = new Mock<IWeeklyPlanFacade>();

            facade
                .Setup(s => s.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(response);

            Mock<IServiceProvider> servicesProvider = new Mock<IServiceProvider>();
            servicesProvider
                .Setup(s => s.GetService(typeof(IWeeklyPlanFacade)))
                .Returns(facade.Object);

            ValidationContext valContext = new ValidationContext(viewModel, servicesProvider.Object, null);

            var result = viewModel.Validate(valContext);
            Assert.True(result.Count() > 0);
        }
    }
}
