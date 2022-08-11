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
    public class AvailableROReportLogic : BaseMonitoringLogic<CostCalculationGarment>
    {
        private SalesDbContext dbContext;
        private readonly IIdentityService identityService;

        public AvailableROReportLogic(SalesDbContext dbContext, IIdentityService identityService)
        {
            this.dbContext = dbContext;
            this.identityService = identityService;
        }

        public override IQueryable<CostCalculationGarment> GetQuery(string filterString)
        {
            Filter filter = JsonConvert.DeserializeObject<Filter>(filterString);

            IQueryable<CostCalculationGarment> Query = dbContext.CostCalculationGarments.Where(cc => cc.IsROAvailable);

            if (!string.IsNullOrWhiteSpace(filter.section))
            {
                Query = Query.Where(cc => cc.Section == filter.section);
            }
            //if (!string.IsNullOrWhiteSpace(filter.roNo))
            //{
            //    Query = Query.Where(cc => cc.RO_Number == filter.roNo);
            //}
            //if (!string.IsNullOrWhiteSpace(filter.buyer))
            //{
            //    Query = Query.Where(cc => cc.BuyerBrandCode == filter.buyer);
            //}
            if (filter.availableDateStart != null)
            {
                var filterDate = filter.availableDateStart.GetValueOrDefault().ToOffset(TimeSpan.FromHours(identityService.TimezoneOffset)).Date;
                Query = Query.Where(cc => cc.ROAvailableDate.AddHours(identityService.TimezoneOffset).Date >= filterDate);
            }
            if (filter.availableDateEnd != null)
            {
                var filterDate = filter.availableDateEnd.GetValueOrDefault().ToOffset(TimeSpan.FromHours(identityService.TimezoneOffset)).AddDays(1).Date;
                Query = Query.Where(cc => cc.ROAvailableDate.AddHours(identityService.TimezoneOffset).Date < filterDate);
            }
            //if (filter.status != null)
            //{
            //    if (filter.status.Equals("OK", StringComparison.OrdinalIgnoreCase))
            //    {
            //        Query = Query.Where(cc => cc.ROAcceptedDate.AddHours(identityService.TimezoneOffset).Date.AddDays(2) >= cc.ROAvailableDate.AddHours(identityService.TimezoneOffset).Date);
            //    }
            //    else if (filter.status.Equals("NOT OK", StringComparison.OrdinalIgnoreCase))
            //    {
            //        Query = Query.Where(cc => cc.ROAcceptedDate.AddHours(identityService.TimezoneOffset).Date.AddDays(2) < cc.ROAvailableDate.AddHours(identityService.TimezoneOffset).Date);
            //    }
            //}

            var result = Query.Select(cc => new CostCalculationGarment
            {
                ROAcceptedDate = cc.ROAcceptedDate,
                ROAvailableDate = cc.ROAvailableDate,
                ValidationSampleDate = cc.ValidationSampleDate,
                ValidationMDDate = cc.ValidationMDDate,
                RO_Number = cc.RO_Number,
                Article = cc.Article,
                BuyerBrandCode = cc.BuyerBrandCode,
                BuyerBrandName = cc.BuyerBrandName,
                DeliveryDate = cc.DeliveryDate,
                Quantity = cc.Quantity,
                UOMUnit = cc.UOMUnit,
                ROAvailableBy = cc.ROAvailableBy
            });

            return result;
        }

        private class Filter
        {
            public string section { get; set; }
            public string roNo { get; set; }
            public string buyer { get; set; }
            public DateTimeOffset? availableDateStart { get; set; }
            public DateTimeOffset? availableDateEnd { get; set; }
            public string status { get; set; }
        }
    }
}
