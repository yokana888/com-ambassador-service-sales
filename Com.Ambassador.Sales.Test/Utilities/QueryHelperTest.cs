using Com.Ambassador.Service.Sales.Lib.Models.Weaving;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.Utilities
{
  public  class QueryHelperTest
    {
        [Fact]
        public void Filter_Success()
        {
            var query = new List<WeavingSalesContractModel>()
            {
                new WeavingSalesContractModel()
                {
                    BuyerName ="value"
                }
            }.AsQueryable();

            Dictionary<string, object> filterDictionary = new Dictionary<string, object>();
            filterDictionary.Add("BuyerName", "value");

            var result = QueryHelper<WeavingSalesContractModel>.Filter(query, filterDictionary);
            Assert.NotNull(result);
            Assert.True(0 < result.Count());
        }

        [Fact]
        public void Order_Return_Success()
        {
            var query = new List<WeavingSalesContractModel>()
            {
                new WeavingSalesContractModel()
                {
                    BuyerName ="value"
                }
            }.AsQueryable();

            Dictionary<string, string> orderDictionary = new Dictionary<string, string>();
            orderDictionary.Add("BuyerName", "desc");
            var result = QueryHelper<WeavingSalesContractModel>.Order(query, orderDictionary);

            Assert.True(0 < result.Count());
            Assert.NotNull(result);

        }


        [Fact]
        public void Order_with_orderDictionary_Return_Success()
        {
            var query = new List<WeavingSalesContractModel>()
            {
                new WeavingSalesContractModel()
                {
                    BuyerName ="value"
                }
            }.AsQueryable();

            Dictionary<string, string> orderDictionary = new Dictionary<string, string>();
            
            var result = QueryHelper<WeavingSalesContractModel>.Order(query, orderDictionary);

            Assert.True(0 < result.Count());
            Assert.NotNull(result);

        }

        [Fact]
        public void Search_Success()
        {
            var query = new List<WeavingSalesContractModel>()
            {
                new WeavingSalesContractModel()
                {
                    BuyerName ="value"
                }
            }.AsQueryable();

            List<string> searchAttributes = new List<string>()
            {
                "BuyerName"
            };

            var result = QueryHelper<WeavingSalesContractModel>.Search(query, searchAttributes, "value");
            Assert.NotNull(result);
            Assert.True(0 < result.Count());
        }

        [Fact]
        public void Select_Success()
        {
            var query = new List<WeavingSalesContractModel>()
            {
                new WeavingSalesContractModel()
                {
                    BuyerName ="value"
                }
            }.AsQueryable();

           
            IQueryable result = QueryHelper<WeavingSalesContractModel>.Select(query, "");
            Assert.NotNull(result);
          
        }

        
    }
}
