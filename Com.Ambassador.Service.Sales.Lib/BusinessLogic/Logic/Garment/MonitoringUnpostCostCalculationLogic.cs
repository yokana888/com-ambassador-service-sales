using Com.Ambassador.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using Com.Ambassador.Service.Sales.Lib.ViewModels.Garment;
using Newtonsoft.Json;
using System.Linq;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.Garment
{
    public class MonitoringUnpostCostCalculationLogic : BaseMonitoringLogic<MonitoringUnpostCostCalculationViewModel>
    {
        private SalesDbContext dbContext;

        public MonitoringUnpostCostCalculationLogic(SalesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public override IQueryable<MonitoringUnpostCostCalculationViewModel> GetQuery(string filterString)
        {
            Filter filter = JsonConvert.DeserializeObject<Filter>(filterString);

            IQueryable<CostCalculationGarment> Query = dbContext.CostCalculationGarments;

            if (!string.IsNullOrWhiteSpace(filter.CreatedBy))
            {
                Query = Query.Where(cc => dbContext.CostCalculationGarmentUnpostReasons.Where(rsn => rsn.CreatedBy == filter.CreatedBy).Select(r => r.CostCalculationId).Contains(cc.Id));
            }
            else
            {
                Query = Query.Where(cc => dbContext.CostCalculationGarmentUnpostReasons.Select(r => r.CostCalculationId).Contains(cc.Id));
            }

            if (!string.IsNullOrWhiteSpace(filter.Section))
            {
                Query = Query.Where(w => w.Section == filter.Section);
            }

            if (!string.IsNullOrWhiteSpace(filter.RO_Number))
            {
                Query = Query.Where(d => d.RO_Number == filter.RO_Number);
            }

            if (!string.IsNullOrWhiteSpace(filter.PreSCNo))
            {
                Query = Query.Where(d => d.PreSCNo == filter.PreSCNo);
            }

            if (!string.IsNullOrWhiteSpace(filter.UnitCode))
            {
                Query = Query.Where(d => d.UnitCode == filter.UnitCode);
            }

            var costCalculations = Query.Select(cc => new
            {
                cc.Id,
                cc.Section,
                cc.RO_Number,
                cc.Article,
                cc.PreSCNo,
                cc.UnitName,
                cc.Quantity
            }).ToList();

            var costCalculationsIds = costCalculations.Select(cc => cc.Id).ToHashSet();

            IQueryable<CostCalculationGarmentUnpostReason> ReasonQuery = dbContext.CostCalculationGarmentUnpostReasons;

            if (!string.IsNullOrWhiteSpace(filter.CreatedBy))
            {
                ReasonQuery = ReasonQuery.Where(reason => costCalculationsIds.Contains(reason.CostCalculationId) && reason.CreatedBy == filter.CreatedBy);
            }
            else
            {
                ReasonQuery = ReasonQuery.Where(reason => costCalculationsIds.Contains(reason.CostCalculationId));
            }

            var unpostReasons = ReasonQuery.Select(reason => new
            {
                reason.CostCalculationId,
                reason.CreatedUtc,
                reason.CreatedBy,
                reason.UnpostReason,
            }).ToList();

            var result = costCalculations.Select(s => new MonitoringUnpostCostCalculationViewModel
            {
                Section = s.Section,
                RONo = s.RO_Number,
                Article = s.Article,
                PreSCNo = s.PreSCNo,
                Unit = s.UnitName,
                Quantity = s.Quantity,
                UnpostReasons = unpostReasons.Where(rsn => rsn.CostCalculationId == s.Id).Select(rsn => new MonitoringUnpostCostCalculationReasonsViewModel
                {
                    Creator = rsn.CreatedBy,
                    Date = rsn.CreatedUtc,
                    Reason = rsn.UnpostReason
                }).ToList()
            }).AsQueryable();

            return result;
        }

        private class Filter
        {
            public string Section { get; set; }
            public string RO_Number { get; set; }
            public string PreSCNo { get; set; }
            public string UnitCode { get; set; }
            public string CreatedBy { get; set; }
        }
    }
}
