using Com.Ambassador.Service.Sales.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.ViewModels.DeliveryNoteProduction
{
    public class DeliveryNoteProductionViewModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            DeliveryNoteProductionViewModel viewModel = new DeliveryNoteProductionViewModel()
            {
                BallMark = "BallMark",
                Month = "April",
                MonthandYear = "April",
                OtherSubject = "OtherSubject",
                Remark = "Remark",
                SalesContract = new SalesContract()
               
            };

            Assert.Equal("BallMark", viewModel.BallMark);
        }

        [Fact]
        public void ValidateDefault()
        {
            DeliveryNoteProductionViewModel viewModel = new DeliveryNoteProductionViewModel();
            var result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public void Validate_Subject_Should_Filled()
        {
            DeliveryNoteProductionViewModel viewModel = new DeliveryNoteProductionViewModel()
            {
                Subject = "Lainnya"
            };
            var result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);
        }
        
    }
}
