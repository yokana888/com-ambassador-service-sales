using Com.Ambassador.Service.Sales.Lib.Models;
using Com.Ambassador.Service.Sales.Lib.ViewModels.DOSales;
using Com.Ambassador.Service.Sales.WebApi.Helpers;
using Com.Moonlay.NetCore.Lib.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.WebApi.Helpers
{
    public class ResultFormatterTest
    {
        [Fact]
        public void Ok_Return_Success()
        {
            //Setup
            ResultFormatter resultFormatter = new ResultFormatter("1", 200, "ok");
            Efficiency model = new Efficiency();

            //Act
            Dictionary<string, object> result = resultFormatter.Ok();

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Fail_with_Error_Return_Success()
        {
            //Setup
            string ApiVersion = "V1";
            int StatusCode = 200;
            string Message = "OK";
            ResultFormatter formatter = new ResultFormatter(ApiVersion, StatusCode, Message);
           
            var errorData = new
            {
                WarningError = "Format Not Match"
            };

            string error = JsonConvert.SerializeObject(errorData);
            
            //Act
            var result = formatter.Fail(error);

            //Assert
            Assert.True(0 < result.Count());
        }


        [Fact]
        public void Fail_Return_Success()
        {
            //Setup
            string ApiVersion = "V1";
            int StatusCode = 200;
            string Message = "OK";

            UnitViewModel viewModel = new UnitViewModel();
            ResultFormatter formatter = new ResultFormatter(ApiVersion, StatusCode, Message);
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(viewModel);

            var errorData = new
            {
                WarningError = "Format Not Match"
            };

            string error = JsonConvert.SerializeObject(errorData);
            var exception = new ServiceValidationExeption(validationContext, new List<ValidationResult>() { new ValidationResult(error, new List<string>() { "WarningError" }) });

            //Act
            var result = formatter.Fail(exception);

            //Assert
            Assert.True(0 < result.Count());
        }

        [Fact]
        public void Fail_Throws_Exception()
        {
            //Setup
            string ApiVersion = "V1";
            int StatusCode = 200;
            string Message = "OK";

            UnitViewModel viewModel = new UnitViewModel();
            ResultFormatter formatter = new ResultFormatter(ApiVersion, StatusCode, Message);
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(viewModel);
            var exception = new ServiceValidationExeption(validationContext, new List<ValidationResult>() { new ValidationResult("errorMessaage", new List<string>() { "WarningError" }) });

            //Act
            var result = formatter.Fail(exception);

            //Assert
            Assert.True(0 < result.Count());
        }
    }
}
