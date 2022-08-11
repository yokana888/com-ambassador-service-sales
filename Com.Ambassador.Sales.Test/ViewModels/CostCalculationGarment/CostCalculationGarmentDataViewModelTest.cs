using Com.Ambassador.Service.Sales.Lib.ViewModels.CostCalculationGarment;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.ViewModels.CostCalculationGarment
{
  public  class CostCalculationGarmentDataViewModelTest
    {

        [Fact]
        public void should_Success_Instantiate()
        {

            CostCalculationGarmentDataProductionReport viewModel = new CostCalculationGarmentDataProductionReport()
            {
                buyerCode = "buyerCode",
                comodityName = "comodityName",
                hours =1,
                qtyOrder =1,
                ro= "ro"
            };

            Assert.Equal("buyerCode", viewModel.buyerCode);
            Assert.Equal("comodityName", viewModel.comodityName);
            Assert.Equal(1, viewModel.hours);
            Assert.Equal(1, viewModel.qtyOrder);
            Assert.Equal("ro", viewModel.ro);

        }
    }
}
