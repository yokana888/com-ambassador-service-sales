using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.CostCalculationGarment;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.Services
{
   public class ValidateServiceTest
    {
        [Fact]
        public void Validate_Success()
        {
            Mock<IServiceProvider> serviceProvider = new Mock<IServiceProvider>();
            BudgetExportGarmentReportViewModel viewModel = new BudgetExportGarmentReportViewModel()
            {
                count = 1,
                Article = "Article"
            };

            ValidateService service = new ValidateService(serviceProvider.Object);
            service.Validate(viewModel);

        }

        [Fact]
        public void Validate_Throws_ServiceValidationExeption()
        {
            Mock<IServiceProvider> serviceProvider = new Mock<IServiceProvider>();
            BuyerBrandViewModel viewModel = new BuyerBrandViewModel();

            ValidateService service = new ValidateService(serviceProvider.Object);
            Assert.Throws<ServiceValidationException>(() => service.Validate(viewModel));

        }
    }
}
