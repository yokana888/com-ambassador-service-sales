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
    public class ProductionOrder_RunWidthLogic : BaseLogic<ProductionOrder_RunWidthModel>
    {
        public ProductionOrder_RunWidthLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
        {

        }
        public override ReadResponse<ProductionOrder_RunWidthModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<ProductionOrder_RunWidthModel> Query = DbSet;

            List<string> SearchAttributes = new List<string>()
            {
              ""
            };

            Query = QueryHelper<ProductionOrder_RunWidthModel>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<ProductionOrder_RunWidthModel>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "Id", "Code", "Buyer", "DeliverySchedule", "SalesContractNo", "LastModifiedUtc"
            };

            Query = Query
                .Select(field => new ProductionOrder_RunWidthModel
                {
                    Id = field.Id,
                    //Code = field.Code,
                    //SalesContractNo = field.SalesContractNo,
                    //BuyerType = field.BuyerType,
                    //BuyerName = field.BuyerName,
                    //DeliverySchedule = field.DeliverySchedule,
                    LastModifiedUtc = field.LastModifiedUtc
                });

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<ProductionOrder_RunWidthModel>.Order(Query, OrderDictionary);

            Pageable<ProductionOrder_RunWidthModel> pageable = new Pageable<ProductionOrder_RunWidthModel>(Query, page - 1, size);
            List<ProductionOrder_RunWidthModel> data = pageable.Data.ToList<ProductionOrder_RunWidthModel>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<ProductionOrder_RunWidthModel>(data, totalData, OrderDictionary, SelectedFields);
        }

        public HashSet<long> GetIds(long id)
        {
            return new HashSet<long>(DbSet.Where(d => d.ProductionOrderModel.Id == id).Select(d => d.Id));
        }
    }
}
