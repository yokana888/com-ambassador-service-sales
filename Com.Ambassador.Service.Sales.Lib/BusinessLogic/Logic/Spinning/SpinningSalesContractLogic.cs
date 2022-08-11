using Com.Ambassador.Service.Sales.Lib.Models.Spinning;
using Com.Ambassador.Service.Sales.Lib.Models.Weaving;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.NetCore.Lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.Spinning
{
    public class SpinningSalesContractLogic : BaseLogic<SpinningSalesContractModel>
    {
        public SpinningSalesContractLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService,serviceProvider, dbContext)
        {
        }
        public override ReadResponse<SpinningSalesContractModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<SpinningSalesContractModel> Query = this.DbSet;

            List<string> SearchAttributes = new List<string>()
            {
                "SalesContractNo","BuyerName"
            };

            Query = QueryHelper<SpinningSalesContractModel>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<SpinningSalesContractModel>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "Id", "SalesContractNo","Buyer","DeliverySchedule","Comodity","ComodityDescription","OrderQuantity","UomUnit","DeliveredTo"
            };

            Query = Query
                .Select(field => new SpinningSalesContractModel
                {
                    Id = field.Id,
                    Code = field.Code,
                    SalesContractNo = field.SalesContractNo,
                    BuyerCode = field.BuyerCode,
                    BuyerId = field.BuyerId,
                    BuyerName = field.BuyerName,
                    BuyerType = field.BuyerType,
                    DeliverySchedule = field.DeliverySchedule,
                    LastModifiedUtc = field.LastModifiedUtc,
                    ComodityName = field.ComodityName,
                    ComodityDescription = field.ComodityDescription,
                    OrderQuantity = field.OrderQuantity,
                    UomUnit = field.UomUnit,
                    DeliveredTo = field.DeliveredTo
                });

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<SpinningSalesContractModel>.Order(Query, OrderDictionary);

            Pageable<SpinningSalesContractModel> pageable = new Pageable<SpinningSalesContractModel>(Query, page - 1, size);
            List<SpinningSalesContractModel> data = pageable.Data.ToList<SpinningSalesContractModel>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<SpinningSalesContractModel>(data, totalData, OrderDictionary, SelectedFields);
        }
    }
}
