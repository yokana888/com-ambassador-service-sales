using Com.Ambassador.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.Garment
{
    public class LatestAvailableROGarmentReportLogic : BaseMonitoringLogic<CostCalculationGarment>
    {
        private SalesDbContext dbContext;
        private readonly IIdentityService identityService;

        public LatestAvailableROGarmentReportLogic(SalesDbContext dbContext, IIdentityService identityService)
        {
            this.dbContext = dbContext;
            this.identityService = identityService;
        }

        public override IQueryable<CostCalculationGarment> GetQuery(string filterString)
        {
            Filter filter = JsonConvert.DeserializeObject<Filter>(filterString);

            IQueryable<CostCalculationGarment> Query = dbContext.CostCalculationGarments.Where(cc => cc.IsValidatedROMD);

            if (!string.IsNullOrWhiteSpace(filter.section))
            {
                Query = Query.Where(cc => cc.Section == filter.section);
            }
    
            if (filter.dateStart != null)
            {
                var filterDate = filter.dateStart.GetValueOrDefault().ToOffset(TimeSpan.FromHours(identityService.TimezoneOffset)).Date;
                Query = Query.Where(cc => cc.DeliveryDate.AddHours(identityService.TimezoneOffset).Date >= filterDate);
            }
            if (filter.dateEnd != null)
            {
                var filterDate = filter.dateEnd.GetValueOrDefault().ToOffset(TimeSpan.FromHours(identityService.TimezoneOffset)).AddDays(1).Date;
                Query = Query.Where(cc => cc.DeliveryDate.AddHours(identityService.TimezoneOffset).Date < filterDate);
            }

            var result = Query.Select(cc => new CostCalculationGarment
            {
                ValidationMDDate = cc.ValidationMDDate,
                DeliveryDate = cc.DeliveryDate,
                RO_Number = cc.RO_Number,
                Article = cc.Article,
                BuyerBrandCode = cc.BuyerBrandCode,
                BuyerBrandName = cc.BuyerBrandName,
                BuyerCode = cc.BuyerCode,
                Quantity = cc.Quantity,
                UOMUnit = cc.UOMUnit,
                LeadTime = cc.LeadTime,
                SizeRange = cc.SizeRange,
                RO_GarmentId = cc.RO_GarmentId,
            });

            return result;
        }

        private class Filter
        {
            public string section { get; set; }
            public string roNo { get; set; }
            public string buyer { get; set; }
            public DateTimeOffset? dateStart { get; set; }
            public DateTimeOffset? dateEnd { get; set; }
            public string status { get; set; }
        }
    }
}
