using Com.Ambassador.Service.Sales.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.Helpers
{
    public  class TimestampTest
    {

        [Fact]
        public void Generate_Return_Exception()
        {
            DateTime time = DateTime.Now;
            var result = Timestamp.Generate(time);
            Assert.NotEmpty(result);
        }
    }
}
