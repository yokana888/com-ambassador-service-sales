using System;
using System.Collections.Generic;
using System.Linq;
using Com.Ambassador.Service.Sales.Lib.Models.DOSales;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.NetCore.Lib;
using Newtonsoft.Json;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.DOSales
{
    public class DOSalesDetailLogic : BaseLogic<DOSalesDetailModel>
    {
        public DOSalesDetailLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
        {
        }

        public override ReadResponse<DOSalesDetailModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<DOSalesDetailModel> Query = DbSet;

            List<string> SearchAttributes = new List<string>()
            {
              ""
            };

            Query = QueryHelper<DOSalesDetailModel>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<DOSalesDetailModel>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "Id","ProductionOrder","ConstructionName","UnitOrCode","Packing","Length","Weight"
            };

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<DOSalesDetailModel>.Order(Query, OrderDictionary);

            Pageable<DOSalesDetailModel> pageable = new Pageable<DOSalesDetailModel>(Query, page - 1, size);
            List<DOSalesDetailModel> data = pageable.Data.ToList<DOSalesDetailModel>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<DOSalesDetailModel>(data, totalData, OrderDictionary, SelectedFields);
        }
        public HashSet<long> GetIds(long id)
        {
            return new HashSet<long>(DbSet.Where(d => d.DOSalesModel.Id == id).Select(d => d.Id));
        }
    }
}
