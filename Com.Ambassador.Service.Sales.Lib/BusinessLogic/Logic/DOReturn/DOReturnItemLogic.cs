using Com.Ambassador.Service.Sales.Lib.Models.DOReturn;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.NetCore.Lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.DOReturn
{
    public class DOReturnItemLogic : BaseLogic<DOReturnItemModel>
    {
        public DOReturnItemLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
        {
        }

        public override ReadResponse<DOReturnItemModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<DOReturnItemModel> Query = DbSet;

            List<string> SearchAttributes = new List<string>()
            {
              ""
            };

            Query = QueryHelper<DOReturnItemModel>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<DOReturnItemModel>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "ShippingOutId","BonNo",
                "Id","ProductId","ProductCode","ProductName","QuantityPacking","PackingUom","ItemUom","QuantityItem",
            };

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<DOReturnItemModel>.Order(Query, OrderDictionary);

            Pageable<DOReturnItemModel> pageable = new Pageable<DOReturnItemModel>(Query, page - 1, size);
            List<DOReturnItemModel> data = pageable.Data.ToList<DOReturnItemModel>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<DOReturnItemModel>(data, totalData, OrderDictionary, SelectedFields);
        }

        public HashSet<long> GetIds(long id)
        {
            return new HashSet<long>(DbSet.Where(d => d.DOReturnDetailModel.Id == id).Select(d => d.Id));
        }
    }
}
