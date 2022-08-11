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
    public class DOReturnDetailItemLogic : BaseLogic<DOReturnDetailItemModel>
    {
        public DOReturnDetailItemLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
        {
        }

        public override ReadResponse<DOReturnDetailItemModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<DOReturnDetailItemModel> Query = DbSet;

            List<string> SearchAttributes = new List<string>()
            {
              ""
            };

            Query = QueryHelper<DOReturnDetailItemModel>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<DOReturnDetailItemModel>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "DOSalesId","DOSalesNo",
            };

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<DOReturnDetailItemModel>.Order(Query, OrderDictionary);

            Pageable<DOReturnDetailItemModel> pageable = new Pageable<DOReturnDetailItemModel>(Query, page - 1, size);
            List<DOReturnDetailItemModel> data = pageable.Data.ToList<DOReturnDetailItemModel>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<DOReturnDetailItemModel>(data, totalData, OrderDictionary, SelectedFields);
        }

        public HashSet<long> GetIds(long id)
        {
            return new HashSet<long>(DbSet.Where(d => d.DOReturnDetailModel.Id == id).Select(d => d.Id));
        }
    }
}
