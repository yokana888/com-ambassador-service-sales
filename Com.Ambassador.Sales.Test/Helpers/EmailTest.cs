using Com.Ambassador.Service.Sales.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Com.Ambassador.Sales.Test.Helpers

{
    public class EmailTest
    {
        [Fact]
        public void IsValid_Return_True()
        {
            var result = Email.IsValid("elfatih@gmail.com");
            Assert.True(result);
        }

        [Fact]
        public void IsValid_Return_False()
        {
            var result = Email.IsValid(null);
            Assert.False(result);
        }
    }
}
