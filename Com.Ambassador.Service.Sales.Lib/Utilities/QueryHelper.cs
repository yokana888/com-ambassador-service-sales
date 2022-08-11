using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.Utilities
{
    public static class QueryHelper<TModel>
           where TModel : BaseModel
    {
        public static IQueryable<TModel> Search(IQueryable<TModel> Query, List<string> SearchAttributes, string Keyword)
        {
            /* Search with Keyword */
            if (Keyword != null)
            {
                string SearchQuery = String.Empty;
                foreach (string Attribute in SearchAttributes)
                {
                    if (Attribute.Contains("."))
                    {
                        var Key = Attribute.Split(".");
                        SearchQuery = string.Concat(SearchQuery, Key[0], $".Any({Key[1]}.Contains(@0)) OR ");
                    }
                    else
                    {
                        SearchQuery = string.Concat(SearchQuery, Attribute, ".Contains(@0) OR ");
                    }
                }

                SearchQuery = SearchQuery.Remove(SearchQuery.Length - 4);

                Query = Query.Where(SearchQuery, Keyword);
            }
            return Query;
        }

        public static IQueryable<TModel> Filter(IQueryable<TModel> Query, Dictionary<string, object> FilterDictionary)
        {
            if (FilterDictionary != null && !FilterDictionary.Count.Equals(0))
            {
                foreach (var f in FilterDictionary)
                {
                    string Key = f.Key;
                    object Value = f.Value;
                    string filterQuery = string.Concat(string.Empty, Key, " == @0");

                    Query = Query.Where(filterQuery, Value);
                }
            }
            return Query;
        }

        public static IQueryable<TModel> Order(IQueryable<TModel> Query, Dictionary<string, string> OrderDictionary)
        {
            /* Default Order */
            if (OrderDictionary.Count.Equals(0))
            {
                OrderDictionary.Add("LastModifiedUtc", "desc");

                Query = Query.OrderByDescending(b => b.LastModifiedUtc);
            }
            /* Custom Order */
            else
            {
                string Key = OrderDictionary.Keys.First();
                string OrderType = OrderDictionary[Key];

                Query = Query.OrderBy(string.Concat(Key, " ", OrderType));
            }
            return Query;
        }

        /// <summary>
        /// new(Id, new(ColumnCode as Code, ColumnName as Name) as Column, Items.Select(new (ItemCode as Code, ItemName as Name) as Item) as Items)
        /// </summary>
        public static IQueryable Select(IQueryable<TModel> Query, string select)
        {
            /* Custom Select */
            if (!string.IsNullOrWhiteSpace(select))
            {
                var SelectedQuery = Query.Select(select);
                return SelectedQuery;
            }

            /* Default Select */
            return Query;
        }
    }
}
