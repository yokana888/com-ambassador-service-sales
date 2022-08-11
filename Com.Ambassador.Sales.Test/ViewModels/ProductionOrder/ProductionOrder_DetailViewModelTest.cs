using Com.Ambassador.Service.Sales.Lib.ViewModels.ProductionOrder;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.ViewModels.ProductionOrder
{
   public class ProductionOrder_DetailViewModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            var orderid = new List<int>() { 1 };

            ProductionOrder_DetailViewModel viewModel = new ProductionOrder_DetailViewModel()
            {
                ProductionOrderNo= "ProductionOrderNo",
                ColorTemplate= "ColorTemplate"
            };

            Assert.Equal("ProductionOrderNo", viewModel.ProductionOrderNo);
            Assert.Equal("ColorTemplate", viewModel.ColorTemplate);
        }

        [Fact]
        public void Validate_Throws_NotImplementedException()
        {
            ProductionOrder_DetailViewModel viewModel = new ProductionOrder_DetailViewModel();

            Assert.Throws<NotImplementedException>(() => viewModel.Validate(null));
        }
    }
}
