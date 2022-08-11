using Com.Ambassador.Service.Sales.Lib.Models.GarmentMasterPlan.WeeklyPlanModels;
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

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.WeeklyPlanLogics
{
    public class WeeklyPlanLogic : BaseLogic<GarmentWeeklyPlan>
    {
        private readonly SalesDbContext DbContext;
        public WeeklyPlanLogic(IIdentityService IdentityService, SalesDbContext dbContext) : base(IdentityService, dbContext)
        {
            DbContext = dbContext;
        }

        public override ReadResponse<GarmentWeeklyPlan> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<GarmentWeeklyPlan> Query = this.DbSet;

            if (keyword != null)
            {
                Query = Query.Where(w => w.Year.ToString().Contains(keyword) || w.UnitCode.Contains(keyword) || w.UnitName.Contains(keyword) || w.Items.Any(i=>i.WeekNumber.ToString().Contains(keyword)));
            }

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<GarmentWeeklyPlan>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = select ?? new List<string>()
            {
                "Id", "Year", "Unit","Items"
            };

            Query = Query
                .Select(field => new GarmentWeeklyPlan
                {
                    Id = field.Id,
                    Year = field.Year,
                    UnitId = field.UnitId,
                    UnitCode = field.UnitCode,
                    UnitName = field.UnitName,
                    LastModifiedUtc = field.LastModifiedUtc,
                    Items = field.Items.OrderBy(w => w.WeekNumber).ToList()
                });

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<GarmentWeeklyPlan>.Order(Query, OrderDictionary);

            Pageable<GarmentWeeklyPlan> pageable = new Pageable<GarmentWeeklyPlan>(Query, page - 1, size);
            List<GarmentWeeklyPlan> data = pageable.Data.ToList<GarmentWeeklyPlan>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<GarmentWeeklyPlan>(data, totalData, OrderDictionary, SelectedFields);
        }

        public override void Create(GarmentWeeklyPlan model)
        {
            foreach (var item in model.Items)
            {
                EntityExtension.FlagForCreate(item, IdentityService.Username, "sales-service");
            }
            base.Create(model);
        }

        public override async Task<GarmentWeeklyPlan> ReadByIdAsync(long id)
        {
            var model = await DbSet.AsNoTracking().Include(d => d.Items).FirstOrDefaultAsync(d => d.Id == id);
            model.Items = model.Items.OrderBy(i => i.WeekNumber).ToList();
            return model;
        }

        public override async Task DeleteAsync(long id)
        {
            var model = await DbSet.Include(d => d.Items).FirstOrDefaultAsync(d => d.Id == id);
            EntityExtension.FlagForDelete(model, IdentityService.Username, "sales-service", true);
            foreach (var item in model.Items)
            {
                EntityExtension.FlagForDelete(item, IdentityService.Username, "sales-service", true);
            }
        }

        public override void UpdateAsync(long id, GarmentWeeklyPlan newModel)
        {
            var model = DbSet.Include(d => d.Items).FirstOrDefault(d => d.Id == id);
            EntityExtension.FlagForUpdate(model, IdentityService.Username, "sales-service");
            foreach (var item in model.Items)
            {
                var newItem = newModel.Items.Single(i => i.Id == item.Id);
                item.Efficiency = newItem.Efficiency;
                item.Operator = newItem.Operator;
                item.WorkingHours = newItem.WorkingHours;
                item.AHTotal = newItem.AHTotal;
                item.EHTotal = newItem.EHTotal;
                item.RemainingEH = newItem.RemainingEH;
                EntityExtension.FlagForUpdate(item, IdentityService.Username, "sales-service");
            }
        }

        internal List<string> GetYears(string keyword)
        {
            return DbSet.Where(d => d.Year.ToString().Contains(keyword))
                .Select(d => d.Year.ToString())
                .Distinct()
                .OrderBy(year => year).ToList();
        }

        internal GarmentWeeklyPlanItem GetWeekById(long id)
        {
            return DbContext.GarmentWeeklyPlanItems.FirstOrDefault(d => d.Id.Equals(id));
        }
    }
}
