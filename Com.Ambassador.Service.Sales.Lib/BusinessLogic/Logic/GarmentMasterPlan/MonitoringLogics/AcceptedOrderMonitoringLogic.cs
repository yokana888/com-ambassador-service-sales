using Com.Ambassador.Service.Sales.Lib.Models.GarmentSewingBlockingPlanModel;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentMasterPlan.MonitoringViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.MonitoringLogics
{
    public class AcceptedOrderMonitoringLogic : BaseMonitoringLogic<AcceptedOrderMonitoringViewModel>
    {
        private IIdentityService identityService;
        private SalesDbContext dbContext;
        private DbSet<GarmentSewingBlockingPlanItem> dbSet;

        public AcceptedOrderMonitoringLogic(IIdentityService identityService, SalesDbContext dbContext)
        {
            this.identityService = identityService;
            this.dbContext = dbContext;
            dbSet = dbContext.Set<GarmentSewingBlockingPlanItem>();
        }

        public override IQueryable<AcceptedOrderMonitoringViewModel> GetQuery(string filter)
        {
            Dictionary<string, string> FilterDictionary = new Dictionary<string, string>(JsonConvert.DeserializeObject<Dictionary<string, string>>(filter), StringComparer.OrdinalIgnoreCase);

            IQueryable<GarmentSewingBlockingPlanItem> Query = dbSet;

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

            var newQ = (from a in Query
                    group a by new { a.WeekNumber, a.UnitCode } into b
                    select new AcceptedOrderMonitoringViewModel
                    {
                        Unit=b.Key.UnitCode,
                        WeekNumber=b.Key.WeekNumber,
                        Quantity=b.Sum(a=>a.OrderQuantity)
                    });

            return newQ;
        }
    }
}
