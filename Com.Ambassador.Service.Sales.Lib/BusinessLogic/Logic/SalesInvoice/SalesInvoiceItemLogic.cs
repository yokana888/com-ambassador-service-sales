using Com.Ambassador.Service.Sales.Lib.Models.SalesInvoice;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.NetCore.Lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.SalesInvoice
{
    public class SalesInvoiceItemLogic : BaseLogic<SalesInvoiceItemModel>
    {
        public SalesInvoiceItemLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
        {
        }

        public override ReadResponse<SalesInvoiceItemModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<SalesInvoiceItemModel> Query = DbSet;

            List<string> SearchAttributes = new List<string>()
            {
              ""
            };

            Query = QueryHelper<SalesInvoiceItemModel>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<SalesInvoiceItemModel>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "Id","ProductId","ProductCode","ProductName","QuantityPacking","PackingUom","ItemUom","QuantityItem","Price","Amount","ConvertUnit","ConvertValue",
            };

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<SalesInvoiceItemModel>.Order(Query, OrderDictionary);

            Pageable<SalesInvoiceItemModel> pageable = new Pageable<SalesInvoiceItemModel>(Query, page - 1, size);
            List<SalesInvoiceItemModel> data = pageable.Data.ToList<SalesInvoiceItemModel>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<SalesInvoiceItemModel>(data, totalData, OrderDictionary, SelectedFields);
        }
    }
}
