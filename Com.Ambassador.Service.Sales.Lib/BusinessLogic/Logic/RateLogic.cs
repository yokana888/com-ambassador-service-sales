using Com.Ambassador.Service.Sales.Lib.Models;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.NetCore.Lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic
{
   public  class RateLogic : BaseLogic<Rate>
    {
        private readonly SalesDbContext DbContext;
        public RateLogic(IIdentityService identityService, SalesDbContext dbContext) : base(identityService, dbContext)
        {
            this.DbContext = dbContext;
        }
        public override ReadResponse<Rate> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<Rate> Query = this.DbSet;

            List<string> SearchAttributes = new List<string>()
            {
                "Name"
            };

            Query = QueryHelper<Rate>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<Rate>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = select != null && select.Count > 0 ? select : new List<string>()
            {
                "Id", "Code", "Name", "Value", "Unit"
            };

            Query = Query
                .Select(b => new Rate
                {
                    Id = b.Id,
                    Code = b.Code,
                    Name = b.Name,
                    Value = b.Value,
                    UnitId = b.UnitId,
                    UnitCode = b.UnitCode,
                    UnitName = b.UnitName,
                    LastModifiedUtc = b.LastModifiedUtc
                });

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<Rate>.Order(Query, OrderDictionary);

            Pageable<Rate> pageable = new Pageable<Rate>(Query, page - 1, size);
            List<Rate> data = pageable.Data.ToList<Rate>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<Rate>(data, totalData, OrderDictionary, SelectedFields);
        }
    }
}
