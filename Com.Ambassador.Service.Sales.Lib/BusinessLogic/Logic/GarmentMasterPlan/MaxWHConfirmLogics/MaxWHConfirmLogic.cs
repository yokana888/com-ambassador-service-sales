using Com.Ambassador.Service.Sales.Lib.Models.GarmentMasterPlan.MaxWHConfirmModel;
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

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.MaxWHConfirmLogics
{
    public class MaxWHConfirmLogic : BaseLogic<MaxWHConfirm>
    {
        private readonly SalesDbContext DbContext;
        public MaxWHConfirmLogic(IIdentityService IdentityService, SalesDbContext dbContext) : base(IdentityService, dbContext)
        {
            DbContext = dbContext;
        }

        public override ReadResponse<MaxWHConfirm> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<MaxWHConfirm> Query = this.DbSet;

            if (keyword != null)
            {
                Query = Query.Where(w => w.UnitMaxValue.ToString().Contains(keyword) || w.SKMaxValue.ToString().Contains(keyword));
            }

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<MaxWHConfirm>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = select ?? new List<string>()
            {
                "Id","MaxValue"
            };

            Query = Query
                .Select(field => new MaxWHConfirm
                {
                    Id = field.Id,
                    CreatedUtc=field.CreatedUtc,
                    UnitMaxValue=field.UnitMaxValue,
                    SKMaxValue=field.SKMaxValue,
                    LastModifiedUtc = field.LastModifiedUtc
                });

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<MaxWHConfirm>.Order(Query, OrderDictionary);

            Pageable<MaxWHConfirm> pageable = new Pageable<MaxWHConfirm>(Query, page - 1, size);
            List<MaxWHConfirm> data = pageable.Data.ToList<MaxWHConfirm>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<MaxWHConfirm>(data, totalData, OrderDictionary, SelectedFields);
        }

        public override void Create(MaxWHConfirm model)
        {
            base.Create(model);
        }

        public override async Task<MaxWHConfirm> ReadByIdAsync(long id)
        {
            var model = await DbSet.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
            return model;
        }
    }
}
