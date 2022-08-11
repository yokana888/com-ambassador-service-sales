using Com.Ambassador.Service.Sales.Lib.Models.GarmentOmzetTargetModel;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using Com.Moonlay.NetCore.Lib;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentOmzetTargetLogics
{
    public class GarmentOmzetTargetLogic : BaseLogic<GarmentOmzetTarget>
    {
        private readonly SalesDbContext DbContext;
        public GarmentOmzetTargetLogic(IIdentityService identityService, SalesDbContext dbContext) : base(identityService, dbContext)
        {
            DbContext = dbContext;
        }

        public override ReadResponse<GarmentOmzetTarget> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<GarmentOmzetTarget> Query = DbSet;

            List<string> SearchAttributes = new List<string>()
            {
                "SectionCode", "SectionName", "QuaterCode", "MonthOfPeriod", "YearOfPeriod"
            };

            Query = QueryHelper<GarmentOmzetTarget>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<GarmentOmzetTarget>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "Id", "SectionId", "SectionCode", "SectionName", "QuaterCode", "MonthOfPeriod", "YearOfPeriod", "Amount", "LastModifiedUtc", "CreatedUtc"
            };

            Query = Query
                .Select(ot => new GarmentOmzetTarget
                {
                    Id = ot.Id,
                    SectionId = ot.SectionId,
                    SectionCode = ot.SectionCode,
                    SectionName = ot.SectionName,
                    QuaterCode = ot.QuaterCode,
                    YearOfPeriod = ot.YearOfPeriod,
                    MonthOfPeriod = ot.MonthOfPeriod,
                    Amount = ot.Amount,
                    CreatedUtc = ot.CreatedUtc,
                    LastModifiedUtc = ot.LastModifiedUtc,
                    IsDeleted = ot.IsDeleted
                }).OrderByDescending(s => s.LastModifiedUtc);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<GarmentOmzetTarget>.Order(Query, OrderDictionary);

            Pageable<GarmentOmzetTarget> pageable = new Pageable<GarmentOmzetTarget>(Query, page - 1, size);
            List<GarmentOmzetTarget> data = pageable.Data.ToList<GarmentOmzetTarget>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<GarmentOmzetTarget>(data, totalData, OrderDictionary, SelectedFields);
        }

        public override void Create(GarmentOmzetTarget model)
        {
            EntityExtension.FlagForCreate(model, IdentityService.Username, "sales-service");
            DbSet.Add(model);
        }
    }
}