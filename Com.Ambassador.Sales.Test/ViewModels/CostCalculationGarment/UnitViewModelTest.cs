using Com.Ambassador.Service.Sales.Lib.ViewModels.CostCalculationGarment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.ViewModels.CostCalculationGarment
{
    public class UnitViewModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            UnitViewModel viewModel = new UnitViewModel()
            {
               Name = "Name"
            };

            Assert.Equal("Name", viewModel.Name);
        }

        [Fact]
        public void ValidateDefault()
        {
            UnitViewModel viewModel = new UnitViewModel();
            var result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);
        }
    }
}
