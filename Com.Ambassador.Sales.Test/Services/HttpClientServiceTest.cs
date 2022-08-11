using Com.Ambassador.Service.Sales.Lib.Models.Weaving;
using Com.Ambassador.Service.Sales.Lib.Services;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Ambassador.Sales.Test.Services
{
  public  class HttpClientServiceTest
    {
        [Fact]
        public async Task should_success_GetAsync()
        {
            Mock<IIdentityService> identity = new Mock<IIdentityService>();
            identity.Setup(s => s.Username).Returns("usernameTest");
            HttpClientService httpClient = new HttpClientService(identity.Object);

            var result = await httpClient.GetAsync("https://stackoverflow.com/");
            Assert.NotNull(result);
        }



        [Fact]
        public async Task should_success_PostAsync()
        {
            Mock<IIdentityService> identity = new Mock<IIdentityService>();
            identity.Setup(s => s.Username).Returns("usernameTest");
            HttpClientService httpClient = new HttpClientService(identity.Object);

            WeavingSalesContractModel model = new WeavingSalesContractModel()
            {
                AccountBankName="someone"
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync("https://stackoverflow.com/", stringContent);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task should_success_PutAsync()
        {
            Mock<IIdentityService> identity = new Mock<IIdentityService>();
            identity.Setup(s => s.Username).Returns("usernameTest");
            HttpClientService httpClient = new HttpClientService(identity.Object);

            WeavingSalesContractModel model = new WeavingSalesContractModel()
            {
                AccountBankName = "someone"
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var result = await httpClient.PutAsync("https://stackoverflow.com/", stringContent);
            Assert.NotNull(result);
        }
    }
}
