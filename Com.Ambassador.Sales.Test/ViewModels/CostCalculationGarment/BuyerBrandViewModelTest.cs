using Com.Ambassador.Service.Sales.Lib.ViewModels.CostCalculationGarment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.ViewModels.CostCalculationGarment
{
    public class BuyerBrandViewModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            BuyerBrandViewModel viewModel = new BuyerBrandViewModel()
            {
                Name = "Name",
                Code = "Code",
                
            };

            Assert.Equal("Name", viewModel.Name);
        }

        [Fact]
        public void ValidateDefault()
        {
            BuyerBrandViewModel viewModel = new BuyerBrandViewModel();
            var result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);
        }
    }
}
