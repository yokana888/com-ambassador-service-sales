using Com.Ambassador.Service.Sales.Lib.Models.ProductionOrder;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.NetCore.Lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.ProductionOrder
{

    public class ProductionOrder_DetailLogic : BaseLogic<ProductionOrder_DetailModel>
    {
        public ProductionOrder_DetailLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
        {

        }
        public override ReadResponse<ProductionOrder_DetailModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<ProductionOrder_DetailModel> Query = DbSet;

            List<string> SearchAttributes = new List<string>()
            {
              ""
            };

            Query = QueryHelper<ProductionOrder_DetailModel>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<ProductionOrder_DetailModel>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "Id", "ColorRequest", "ColorTemplate", "Code", "Buyer", "DeliverySchedule", "SalesContractNo", "LastModifiedUtc"
            };

            Query = Query
                .Select(field => new ProductionOrder_DetailModel
                {
                    Id = field.Id,
                    ColorRequest = field.ColorRequest,
                    ColorTemplate = field.ColorTemplate,
                    //SalesContractNo = field.SalesContractNo,
                    //BuyerType = field.BuyerType,
                    //BuyerName = field.BuyerName,
                    //DeliverySchedule = field.DeliverySchedule,
                    LastModifiedUtc = field.LastModifiedUtc
                });

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<ProductionOrder_DetailModel>.Order(Query, OrderDictionary);

            Pageable<ProductionOrder_DetailModel> pageable = new Pageable<ProductionOrder_DetailModel>(Query, page - 1, size);
            List<ProductionOrder_DetailModel> data = pageable.Data.ToList<ProductionOrder_DetailModel>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<ProductionOrder_DetailModel>(data, totalData, OrderDictionary, SelectedFields);
        }

        public HashSet<long> GetIds(long id)
        {
            return new HashSet<long>(DbSet.Where(d => d.ProductionOrderModel.Id == id).Select(d => d.Id));
        }
    }
}
