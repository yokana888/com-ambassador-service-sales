using Com.Ambassador.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.Utilities
{
    public class NumberToTextIDNTest
    {
        [Fact]
        public void terbilang_With_Belasan_Return_Success()
        {
            var result = NumberToTextIDN.terbilang(16);
            Assert.NotEmpty(result);
            Assert.Equal("Enam belas", result);
        }

        [Fact]
        public void terbilang_With_Puluhan_Return_Success()
        {
            var result = NumberToTextIDN.terbilang(9);
            Assert.NotEmpty(result);
            Assert.Equal("Sembilan", result);
        }

        [Fact]
        public void terbilang_With_Ratusan_Return_Success()
        {
            var result = NumberToTextIDN.terbilang(100);
            Assert.NotEmpty(result);
            Assert.Equal("Seratus", result);
        }

        [Fact]
        public void terbilang_With_Ribuan_Return_Success()
        {
            var result = NumberToTextIDN.terbilang(1000);
            Assert.NotEmpty(result);
            Assert.Equal("Seribu", result);
        }

        [Fact]
        public void terbilangKoma_Return_Success()
        {
            var result = NumberToTextIDN.terbilangKoma(1000);
            Assert.NotEmpty(result);
            Assert.Equal("koma Satu Nol Nol Nol", result);
        }

        [Fact]
        public void TerbilangKoma_MoreThan_4_Decimal()
        {
            float number = 10.00069f;
            var terbilangKoma = NumberToTextIDN.terbilangKoma(number);
            Assert.Equal("koma Nol Nol Nol Tujuh", terbilangKoma);
        }

    }
}
