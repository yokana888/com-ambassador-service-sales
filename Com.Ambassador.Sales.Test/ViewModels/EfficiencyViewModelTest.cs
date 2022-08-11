using Com.Ambassador.Service.Sales.Lib.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.ViewModels
{
    public class EfficiencyViewModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            EfficiencyViewModel viewModel = new EfficiencyViewModel()
            {
                Name = "Name",
                Code = "Code",
                FinalRange = 1,
                InitialRange = 1,
                Value = 1,
            };

            Assert.Equal("Name", viewModel.Name);
            Assert.Equal("Code", viewModel.Code);
        }

        [Fact]
        public void ValidateDefault()
        {
            EfficiencyViewModel viewModel = new EfficiencyViewModel();
            var result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public void Validate_When_Finalrange_Equal_0()
        {
            EfficiencyViewModel viewModel = new EfficiencyViewModel()
            {
                FinalRange = 0,
            };
            var result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public void Validate_When_InitialRange_Equal_0()
        {
            EfficiencyViewModel viewModel = new EfficiencyViewModel()
            {
                InitialRange = 0,
            };
            var result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public void Validate_When_Finalrange_Equal_InitialRange()
        {
            EfficiencyViewModel viewModel = new EfficiencyViewModel()
            {
                FinalRange = 1,
                InitialRange = 1,
                Value = 1
            };
            var result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);
        }
    }
}
