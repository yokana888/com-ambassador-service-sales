using Com.Ambassador.Service.Sales.Lib.Models.SalesInvoiceExport;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.NetCore.Lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.SalesInvoiceExport
{
    public class SalesInvoiceExportItemLogic : BaseLogic<SalesInvoiceExportItemModel>
    {
        public SalesInvoiceExportItemLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
        {
        }

        public override ReadResponse<SalesInvoiceExportItemModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<SalesInvoiceExportItemModel> Query = DbSet;

            List<string> SearchAttributes = new List<string>()
            {
              ""
            };

            Query = QueryHelper<SalesInvoiceExportItemModel>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<SalesInvoiceExportItemModel>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "Id","ProductId","ProductCode","ProductName","QuantityPacking","PackingUom","ItemUom","QuantityItem","Price","Amount","Description"
            };

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<SalesInvoiceExportItemModel>.Order(Query, OrderDictionary);

            Pageable<SalesInvoiceExportItemModel> pageable = new Pageable<SalesInvoiceExportItemModel>(Query, page - 1, size);
            List<SalesInvoiceExportItemModel> data = pageable.Data.ToList<SalesInvoiceExportItemModel>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<SalesInvoiceExportItemModel>(data, totalData, OrderDictionary, SelectedFields);
        }
    }
}
