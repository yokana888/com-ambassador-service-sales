using Com.Ambassador.Service.Sales.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.Helpers
{
    public class NumberTest
    {
        [Fact]
        public void ToRupiah_Return_Success()
        {
            dynamic number = 10;
            var result = Number.ToRupiah(number);
            Assert.Equal("Rp10,00", result);
        }

        [Fact]
        public void ToRupiah_Return_Exception()
        {
            dynamic number = "10";
            var result = Number.ToRupiah(number);
            Assert.Equal("10", result);
        }

        [Fact]
        public void ToRupiahWithoutSymbol_Return_Success()
        {
            dynamic number = 10;
            var result = Number.ToRupiahWithoutSymbol(number);
            Assert.Equal("10,00", result);
        }

        [Fact]
        public void ToRupiahWithoutSymbol_Return_Exception()
        {
            dynamic number = "10";
            var result = Number.ToRupiahWithoutSymbol(number);
            Assert.Equal("10", result);
        }

        [Fact]
        public void ToDollar_Return_Success()
        {
            dynamic number = 10;
            var result = Number.ToDollar(number);
            Assert.Equal("$10.00", result);
        }

        [Fact]
        public void ToDollar_Return_Exception()
        {
            dynamic number = "10";
            var result = Number.ToDollar(number);
            Assert.Equal("10", result);
        }


    }
}
