using Com.Ambassador.Service.Sales.Lib.ViewModels.CostCalculationGarment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.ViewModels.CostCalculationGarment
{
    public class BuyerViewModelTest
    {

        [Fact]
        public void should_Success_Instantiate()
        {
            BuyerViewModel viewModel = new BuyerViewModel()
            {
                Name = "Name",
                Code = "Code",
                email ="adepamungkas@gmail.com",
                address1 ="Jakarta Selatan",
                address2 ="Lampung",
                
            };

            Assert.Equal("Name", viewModel.Name);
            Assert.Equal("Code", viewModel.Code);
            Assert.Equal("adepamungkas@gmail.com", viewModel.email);
            Assert.Equal("Jakarta Selatan", viewModel.address1);
            Assert.Equal("Lampung", viewModel.address2);
        }

        [Fact]
        public void ValidateDefault()
        {
            BuyerViewModel viewModel = new BuyerViewModel();
            var result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);
        }


        [Fact]
        public void Validate_Email_NotValid()
        {
            BuyerViewModel viewModel = new BuyerViewModel()
            {
                email = "adepamungkasgmail.com",
            };
            var result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);
        }
    }
}
