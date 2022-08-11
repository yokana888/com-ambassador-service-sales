using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentMasterPlan.WeeklyPlanModels;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentMasterPlan.MonitoringViewModels;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.MonitoringLogics
{
    public class MonitoringRemainingEHLogic : BaseMonitoringLogic<MonitoringRemainingEHViewModel>
    {
        private IIdentityService identityService;
        private SalesDbContext dbContext;
        private DbSet<GarmentWeeklyPlan> dbSet;

        public MonitoringRemainingEHLogic(IIdentityService identityService, SalesDbContext dbContext)
        {
            this.identityService = identityService;
            this.dbContext = dbContext;
            dbSet = dbContext.Set<GarmentWeeklyPlan>();
        }

        public override IQueryable<MonitoringRemainingEHViewModel> GetQuery(string filter)
        {
            Dictionary<string, string> FilterDictionary = new Dictionary<string, string>(JsonConvert.DeserializeObject<Dictionary<string, string>>(filter), StringComparer.OrdinalIgnoreCase);

            IQueryable<GarmentWeeklyPlan> Query = dbSet.Include(i => i.Items);

            try
            {
                var year = short.Parse(FilterDictionary["year"]);
                Query = dbSet.Where(d => d.Year == year);
            }
            catch (KeyNotFoundException e)
            {
                throw new Exception(string.Concat("[year]", e.Message));
            }

            if (FilterDictionary.TryGetValue("unit", out string unit))
            {
                Query = Query.Where(d => d.UnitCode == unit);
            }

            Query = Query.OrderBy(o => o.UnitCode);

            //foreach (var q in Query)
            //{
            //    var entry = dbContext.Entry(q);
            //    entry.Collection(e => e.Items)
            //        .Query()
            //        .Select(i => new WeeklyPlanItem
            //        {
            //            WeekNumber = i.WeekNumber,
            //            EHTotal = i.EHTotal,
            //            RemainingEH = i.RemainingEH,
            //            UsedEH = i.UsedEH
            //        })
            //        .OrderBy(i => i.WeekNumber.ToString())
            //        .Load();
            //}

            //var asd = from wp in Query
            //          join wpi in dbContext.WeeklyPlanItems on wp.Id equals wpi.WeeklyPlanId
            //          //group new { wp, wpi } by wp.Id into grp
            //          select new
            //          {
            //              wp.Year,
            //              wp.UnitCode,
            //              wpi.WeeklyPlanId,
            //              wpi.WeekNumber,
            //              wpi.EHTotal,
            //              wpi.UsedEH,
            //              wpi.RemainingEH
            //          };

            //var ok = from a in asd
            //         group a by a.UnitCode into grp
            //         select new MonitoringRemainingEHViewModel
            //         {
            //             Unit = grp.Key,
            //             Items = grp.Select(s => new MonitoringRemainingEHItemViewModel
            //             {
            //                 WeekNumber = s.WeekNumber,
            //                 EHTotal = s.EHTotal,
            //                 UsedEH = s.UsedEH,
            //                 RemainingEH = s.RemainingEH
            //             }).ToList()
            //         };

            return Query.Select(d => new MonitoringRemainingEHViewModel
            {
                Unit = d.UnitCode,
                Items = d.Items.OrderBy(i => i.WeekNumber).Select(i => new MonitoringRemainingEHItemViewModel
                {
                    WeekNumber = i.WeekNumber,
                    Operator = i.Operator,
                    RemainingEH = i.RemainingEH,
                }).ToList()
            });
        }
    }
}
