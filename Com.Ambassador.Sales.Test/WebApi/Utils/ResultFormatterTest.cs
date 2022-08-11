using Com.Ambassador.Service.Sales.Lib.ViewModels.DOSales;
using Com.Ambassador.Service.Sales.WebApi.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Xunit;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using System.Linq;
using Moq;
using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.Models;

namespace Com.Ambassador.Sales.Test.WebApi.Utils
{
    public class ResultFormatterTest
    {
        [Fact]
        public void Ok_Return_Success()
        {
            //Setup
            ResultFormatter resultFormatter = new ResultFormatter("1", 200, "ok");
            var mapper = new Mock<IMapper>();
            List<Efficiency> data = new List<Efficiency>();
            Dictionary<string, string> orderDictionary = new Dictionary<string, string>();
            orderDictionary.Add("Name", "desc");

            //Act
            var result = resultFormatter.Ok<Efficiency>(mapper.Object, data, 1, 1, 1, 1, orderDictionary, new List<string>() { "Name" });

            //Assert
            Assert.NotNull(result);
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
            var exception = new ServiceValidationException(validationContext, new List<ValidationResult>() { new ValidationResult(error, new List<string>() { "WarningError" }) });

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
            var exception = new ServiceValidationException(validationContext, new List<ValidationResult>() { new ValidationResult("errorMessaage", new List<string>() { "WarningError" }) });

            //Act
            var result = formatter.Fail(exception);

            //Assert
            Assert.True(0 < result.Count());
        }
    }
}
