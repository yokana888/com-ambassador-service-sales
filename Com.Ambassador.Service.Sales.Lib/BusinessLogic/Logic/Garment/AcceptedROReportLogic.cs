using Com.Ambassador.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using Com.Ambassador.Service.Sales.Lib.ViewModels.Garment;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.Garment
{
    public class AcceptedROReportLogic : BaseMonitoringLogic<CostCalculationGarment>
    {
        private SalesDbContext dbContext;
        private readonly IIdentityService identityService;

        public AcceptedROReportLogic(SalesDbContext dbContext, IIdentityService identityService)
        {
            this.dbContext = dbContext;
            this.identityService = identityService;
        }

        public override IQueryable<CostCalculationGarment> GetQuery(string filterString)
        {
            Filter filter = JsonConvert.DeserializeObject<Filter>(filterString);

            IQueryable<CostCalculationGarment> Query = dbContext.CostCalculationGarments.Where(cc => cc.IsValidatedROSample);

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
            if (filter.acceptedDateStart != null)
            {
                var filterDate = filter.acceptedDateStart.GetValueOrDefault().ToOffset(TimeSpan.FromHours(identityService.TimezoneOffset)).Date;
                Query = Query.Where(cc => cc.ValidationSampleDate.AddHours(identityService.TimezoneOffset).Date >= filterDate);
            }
            if (filter.acceptedDateEnd != null)
            {
                var filterDate = filter.acceptedDateEnd.GetValueOrDefault().ToOffset(TimeSpan.FromHours(identityService.TimezoneOffset)).AddDays(1).Date;
                Query = Query.Where(cc => cc.ValidationSampleDate.AddHours(identityService.TimezoneOffset).Date < filterDate);
            }

            var result = Query.Select(cc => new CostCalculationGarment
            {
                ValidationSampleDate = cc.ValidationSampleDate,
                RO_Number = cc.RO_Number,
                Article = cc.Article,
                BuyerBrandName = cc.BuyerBrandName,
                DeliveryDate = cc.DeliveryDate,
                Quantity = cc.Quantity,
                UOMUnit = cc.UOMUnit,
                ValidationSampleBy = cc.ValidationSampleBy
            });

            return result;
        }

        private class Filter
        {
            public string section { get; set; }
            //public string roNo { get; set; }
            //public string buyer { get; set; }
            public DateTimeOffset? acceptedDateStart { get; set; }
            public DateTimeOffset? acceptedDateEnd { get; set; }
        }
    }
}
