using Com.Ambassador.Service.Sales.Lib.ViewModels.CostCalculationGarment;
using Com.Ambassador.Service.Sales.Lib.ViewModels.Garment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.ViewModels.Garment
{
   public class CostCalculationGarment_RO_Garment_ValidationViewModelTest
    {
        
        [Fact]
        public void ValidateDefault()
        {
            CostCalculationGarment_RO_Garment_ValidationViewModel viewModel = new CostCalculationGarment_RO_Garment_ValidationViewModel();
            var result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public void Validate_MaterialsError()
        {
            CostCalculationGarment_RO_Garment_ValidationViewModel viewModel = new CostCalculationGarment_RO_Garment_ValidationViewModel()
            {
                CostCalculationGarment_Materials =new List<CostCalculationGarment_MaterialViewModel>()
                {
                   new  CostCalculationGarment_MaterialViewModel(){
                       IsPosted =true
                    }
                }
            };
            var result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);
        }

    }
}
