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

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.Weaving
{
    public class WeavingSalesContractLogic : BaseLogic<WeavingSalesContractModel>
    {
        public WeavingSalesContractLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService,serviceProvider, dbContext)
        {
        }

        public override ReadResponse<WeavingSalesContractModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<WeavingSalesContractModel> Query = this.DbSet;

            List<string> SearchAttributes = new List<string>()
            {
                "SalesContractNo","BuyerName"
            };

            Query = QueryHelper<WeavingSalesContractModel>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<WeavingSalesContractModel>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "Id", "SalesContractNo","Buyer","DeliverySchedule"
            };

            Query = Query
                .Select(field => new WeavingSalesContractModel
                {
                    Id = field.Id,
                    Code = field.Code,
                    SalesContractNo = field.SalesContractNo,
                    BuyerCode = field.BuyerCode,
                    BuyerId = field.BuyerId,
                    BuyerName = field.BuyerName,
                    BuyerType = field.BuyerType,
                    DeliverySchedule = field.DeliverySchedule,
                    LastModifiedUtc = field.LastModifiedUtc
                });

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<WeavingSalesContractModel>.Order(Query, OrderDictionary);

            Pageable<WeavingSalesContractModel> pageable = new Pageable<WeavingSalesContractModel>(Query, page - 1, size);
            List<WeavingSalesContractModel> data = pageable.Data.ToList<WeavingSalesContractModel>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<WeavingSalesContractModel>(data, totalData, OrderDictionary, SelectedFields);
        }
    }
}
