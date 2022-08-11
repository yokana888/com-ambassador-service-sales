using Com.Ambassador.Service.Sales.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.Helpers
{
    public class PercentageTest
    {
        [Fact]
        public void ToFraction_Return_Success()
        {
            dynamic number = 10;
            var result = Percentage.ToFraction(number);
            Assert.Equal(0.1,result);
        }

        [Fact]
        public void ToFraction_Return_Exception()
        {
            dynamic number = "10";
            var result = Percentage.ToFraction(number);
            Assert.Equal(0, result);
        }

        [Fact]
        public void ToPercent_Return_Success()
        {
            dynamic number = 0.1 ;
            var result = Percentage.ToPercent(number);
            Assert.Equal(10, result);
        }

        [Fact]
        public void ToPercent_Return_Exception()
        {
            dynamic number = "0.1";
            var result = Percentage.ToPercent(number);
            Assert.Equal(0, result);
        }
    }
}
