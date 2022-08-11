using Com.Ambassador.Service.Sales.Lib.ViewModels.ProductionOrder;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.ViewModels.ProductionOrder
{
    public class ProductionOrder_LampStandardViewModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
           
            ProductionOrder_LampStandardViewModel viewModel = new ProductionOrder_LampStandardViewModel()
            {
                Code = "Code",
                Description = "Description",
                LampStandardId =1
            };

            Assert.Equal("Code", viewModel.Code);
            Assert.Equal("Description", viewModel.Description);
            Assert.Equal(1, viewModel.LampStandardId);
        }
    }
}
