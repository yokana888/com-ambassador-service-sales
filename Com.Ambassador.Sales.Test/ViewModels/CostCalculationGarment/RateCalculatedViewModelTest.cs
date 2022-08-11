using Com.Ambassador.Service.Sales.Lib.ViewModels.CostCalculationGarment;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.ViewModels.CostCalculationGarment
{
    public class RateCalculatedViewModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            RateCalculatedViewModel viewModel = new RateCalculatedViewModel()
            {
                Name = "Name",
                Code = "Code",
                CalculatedValue =1,
            };

            Assert.Equal("Name", viewModel.Name);
            Assert.Equal(1, viewModel.CalculatedValue);
        }

    }
}
