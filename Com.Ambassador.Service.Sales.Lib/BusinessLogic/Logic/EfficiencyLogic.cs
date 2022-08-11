using Com.Ambassador.Service.Sales.Lib.Models;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.NetCore.Lib;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic
{
    public class EfficiencyLogic : BaseLogic<Efficiency>
    {
        private readonly SalesDbContext DbContext;
        public EfficiencyLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
        {
            this.DbContext = dbContext;
        }
        public override ReadResponse<Efficiency> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<Efficiency> Query = this.DbSet;

            List<string> SearchAttributes = new List<string>()
            {

                    "InitialRange", "FinalRange", "Value"
            };

            Query = QueryHelper<Efficiency>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<Efficiency>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                  "Id", "Code", "InitialRange", "FinalRange", "Value"
            };

            Query = Query
                .Select(b => new Efficiency
                {
                    Id = b.Id,
                    Code = b.Code,
                    InitialRange = b.InitialRange,
                    FinalRange = b.FinalRange,
                    Value = b.Value
                });

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<Efficiency>.Order(Query, OrderDictionary);

            Pageable<Efficiency> pageable = new Pageable<Efficiency>(Query, page - 1, size);
            List<Efficiency> data = pageable.Data.ToList<Efficiency>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<Efficiency>(data, totalData, OrderDictionary, SelectedFields);
        }
       
    }
}
