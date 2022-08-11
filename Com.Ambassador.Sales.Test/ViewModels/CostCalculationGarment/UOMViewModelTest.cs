using Com.Ambassador.Service.Sales.Lib.ViewModels.CostCalculationGarment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.ViewModels.CostCalculationGarment
{
    public class UOMViewModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            UOMViewModel viewModel = new UOMViewModel()
            {
               code = "code"
            };

            Assert.Equal("code", viewModel.code);
        }

        [Fact]
        public void ValidateDefault()
        {
            UOMViewModel viewModel = new UOMViewModel();
            var result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public void ValidateUnit()
        {
            UOMViewModel viewModel = new UOMViewModel()
            {
                Unit = "1"
            };
            var result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);
        }
    }
}
