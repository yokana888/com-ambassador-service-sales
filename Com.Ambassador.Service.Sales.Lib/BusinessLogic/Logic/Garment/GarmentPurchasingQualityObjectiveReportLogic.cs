using Com.Ambassador.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentOmzetTargetModel;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using Com.Ambassador.Service.Sales.Lib.ViewModels.Garment;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.Garment
{
    public class GarmentPurchasingQualityObjectiveReportLogic : BaseMonitoringLogic<GarmentPurchasingQualityObjectiveReportViewModel>
    {
        private SalesDbContext dbContext;
        private IIdentityService identityService;

        public GarmentPurchasingQualityObjectiveReportLogic(SalesDbContext dbContext, IIdentityService identityService)
        {
            this.dbContext = dbContext;
            this.identityService = identityService;
        }

        public override IQueryable<GarmentPurchasingQualityObjectiveReportViewModel> GetQuery(string filterString)
        {
            Filter filter = JsonConvert.DeserializeObject<Filter>(filterString);

            IQueryable<CostCalculationGarment> Query = dbContext.CostCalculationGarments.Where(cc => cc.IsApprovedPPIC);
            if (filter.year > 0 && filter.month > 0)
            {
                var min = new DateTimeOffset(filter.year, filter.month, 1, 0, 0, 0, TimeSpan.FromHours(identityService.TimezoneOffset)).UtcDateTime;
                var max = new DateTimeOffset(filter.month < 12 ? filter.year : filter.year + 1, filter.month % 12 + 1, 1, 0, 0, 0, TimeSpan.FromHours(identityService.TimezoneOffset)).UtcDateTime;
                Query = Query.Where(w => w.DeliveryDate >= min && w.DeliveryDate < max);
            }
            else
            {
                throw new Exception("Invalid Year or Month");
            }

            var selectedQuery = Query.Select(cc => new
            {
                cc.Section,
                cc.Quantity,
                cc.ConfirmPrice
            });

            var result = selectedQuery
                .GroupBy(s => s.Section)
                .Select(grp => new GarmentPurchasingQualityObjectiveReportViewModel
                {
                    Section = grp.Key,
                    Omzet = grp.Sum(cc => cc.Quantity * cc.ConfirmPrice)
                })
                .OrderBy(o => o.Section);

            return result;
        }

        public IQueryable<GarmentOmzetTarget> GetTargetQuery(string filterString)
        {
            Filter filter = JsonConvert.DeserializeObject<Filter>(filterString);

            IQueryable<GarmentOmzetTarget> Query = dbContext.GarmentOmzetTargets;

            var filterYear = filter.year.ToString();
            var filterMonth = new CultureInfo("id-ID").DateTimeFormat.GetMonthName(filter.month);

            Query = Query.Where(w => w.YearOfPeriod == filterYear && w.MonthOfPeriod == filterMonth);

            Query = Query.Select(q => new GarmentOmzetTarget
            {
                SectionCode = q.SectionCode,
                Amount = q.Amount
            });

            return Query;
        }

        private class Filter
        {
            public int year { get; set; }
            public int month { get; set; }
        }
    }
}
