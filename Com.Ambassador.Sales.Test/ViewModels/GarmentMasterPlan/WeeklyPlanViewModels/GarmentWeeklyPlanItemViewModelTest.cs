using Com.Ambassador.Service.Sales.Lib.ViewModels.Garment.WeeklyPlanViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.ViewModels.GarmentMasterPlan.WeeklyPlanViewModels
{
    public class GarmentWeeklyPlanItemViewModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            DateTimeOffset date = DateTimeOffset.Now;
            GarmentWeeklyPlanItemViewModel viewModel = new GarmentWeeklyPlanItemViewModel()
            {
                AHTotal = 1,
                Efficiency = 1,
                EHTotal = 1,
                Operator = 1,
                RemainingEH = 1,
                StartDate = date,
                UsedEH = 1,
                WHConfirm = 1,
                WorkingHours = 8,
                EndDate =date
            };

            Assert.Equal(1, viewModel.AHTotal);
            Assert.Equal(1, viewModel.Efficiency);
            Assert.Equal(1, viewModel.EHTotal);
            Assert.Equal(1, viewModel.Operator);
            Assert.Equal(1, viewModel.RemainingEH);
            Assert.Equal(1, viewModel.UsedEH);
            Assert.Equal(1, viewModel.WHConfirm);
            Assert.Equal(8, viewModel.WorkingHours);
            Assert.Equal(date, viewModel.StartDate);
            Assert.Equal(date, viewModel.EndDate);
        }


    }
}
