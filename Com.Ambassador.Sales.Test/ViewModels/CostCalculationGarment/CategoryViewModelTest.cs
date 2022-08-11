using Com.Ambassador.Service.Sales.Lib.ViewModels.CostCalculationGarment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.ViewModels.CostCalculationGarment
{
    public class CategoryViewModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            CategoryViewModel viewModel = new CategoryViewModel()
            {
                name = "name",
                SubCategory = "SubCategory",
                code = "code",
            };

            Assert.Equal("SubCategory", viewModel.SubCategory);
        }

        [Fact]
        public void ValidateDefault()
        {
            CategoryViewModel viewModel = new CategoryViewModel();
            var result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);
        }
    }
}
