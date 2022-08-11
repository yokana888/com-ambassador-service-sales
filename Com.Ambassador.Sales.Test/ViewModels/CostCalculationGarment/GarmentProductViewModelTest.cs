using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.ViewModels.CostCalculationGarment
{
    public class GarmentProductViewModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            Service.Sales.Lib.ViewModels.CostCalculationGarment.GarmentProductViewModel viewModel = new Service.Sales.Lib.ViewModels.CostCalculationGarment.GarmentProductViewModel()
            {
                Name = "Name",
                Code = "Code",
                Composition = "Composition",
                Width = "Width",
                Yarn = "Yarn",
                Const = "Const",
            };

            Assert.Equal("Name", viewModel.Name);
            Assert.Equal("Code", viewModel.Code);
            Assert.Equal("Composition", viewModel.Composition);
            Assert.Equal("Width", viewModel.Width);
            Assert.Equal("Yarn", viewModel.Yarn);
            Assert.Equal("Const", viewModel.Const);
        }
    }
}
