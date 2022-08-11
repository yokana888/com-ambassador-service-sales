using Com.Ambassador.Service.Sales.WebApi.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.WebApi.Utils
{
  public  class APIEndpointTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            APIEndpoint.AzureCore = "AzureCore";
            APIEndpoint.Core = "Core";
            APIEndpoint.StorageAccountKey = "StorageAccountKey";
            APIEndpoint.StorageAccountName = "StorageAccountName";

            Assert.Equal("AzureCore", APIEndpoint.AzureCore);
            Assert.Equal("Core", APIEndpoint.Core);
            Assert.Equal("StorageAccountKey", APIEndpoint.StorageAccountKey);
            Assert.Equal("StorageAccountName", APIEndpoint.StorageAccountName);
        }
    }
}
