using Com.Ambassador.Service.Sales.Lib.Helpers;
using Com.Ambassador.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using Com.Ambassador.Service.Sales.Lib.ViewModels.CostCalculationGarment;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.CostCalculationGarments
{
    public class CCROGarmentHistoryBySectionReportLogic : BaseMonitoringLogic<CCROGarmentHistoryBySectionReportViewModel>
    {
        private IIdentityService identityService;
        private IHttpClientService httpClientService;
        private SalesDbContext dbContext;
        private DbSet<CostCalculationGarment> dbSet;

        private string buyerUri = "master/garment-buyers/all";

        public CCROGarmentHistoryBySectionReportLogic(IIdentityService identityService, SalesDbContext dbContext, IHttpClientService httpClientService)
        {
            this.identityService = identityService;
            this.httpClientService = httpClientService;
            this.dbContext = dbContext;
            dbSet = dbContext.Set<CostCalculationGarment>();
        }

        public override IQueryable<CCROGarmentHistoryBySectionReportViewModel> GetQuery(string filter)
        {
            Filter _filter = JsonConvert.DeserializeObject<Filter>(filter);

            Dictionary<string, string> FilterDictionary = new Dictionary<string, string>(JsonConvert.DeserializeObject<Dictionary<string, string>>(filter), StringComparer.OrdinalIgnoreCase);

            IQueryable<CostCalculationGarment> Query = dbSet;

            if (!string.IsNullOrWhiteSpace(_filter.roNo))
            {
                Query = Query.Where(cc => cc.RO_Number == _filter.roNo);
            }

            if (!string.IsNullOrWhiteSpace(_filter.section))
            {
                Query = Query.Where(cc => cc.Section == _filter.section);
            }

            if (_filter.dateFrom != null)
            {
                var filterDate = _filter.dateFrom.GetValueOrDefault().ToOffset(TimeSpan.FromHours(identityService.TimezoneOffset)).Date;
                Query = Query.Where(cc => cc.ValidationMDDate.AddHours(identityService.TimezoneOffset).Date >= filterDate);
            }

            if (_filter.dateTo != null)
            {
                var filterDate = _filter.dateTo.GetValueOrDefault().ToOffset(TimeSpan.FromHours(identityService.TimezoneOffset)).AddDays(1).Date;
                Query = Query.Where(cc => cc.ValidationMDDate.AddHours(identityService.TimezoneOffset).Date < filterDate);
            }

            IQueryable<ViewModels.IntegrationViewModel.BuyerViewModel> buyerQ = GetGarmentBuyer().AsQueryable();

            Query = Query.OrderBy(o => o.Section).ThenBy(o => o.BuyerBrandName);

            var newQ = (from a in Query where a.IsValidatedROMD == true 
                    select new CCROGarmentHistoryBySectionReportViewModel
                    {
                        RO_Number = a.RO_Number,
                        DeliveryDate = a.DeliveryDate,
                        Section = a.Section,
                        Article = a.Article,
                        BrandCode = a.BuyerBrandCode,
                        BrandName = a.BuyerBrandName,
                        ApprovalMDDate = a.ApprovedMDDate,
                        ApprovalIEDate = a.ApprovedIEDate,
                        ApprovalPurchDate = a.ApprovedPurchasingDate,
                        ApprovalKadivMDDate = a.ApprovedKadivMDDate,
                        ValidatedMDDate = a.ValidationMDDate,
                        ValidatedSampleDate = a.ValidationSampleDate,
                        Type = buyerQ.Where(x => x.Code == a.BuyerCode).Select(x => x.Type).FirstOrDefault()
                    });

            return newQ;
        }

        private class Filter
        {
            public string section { get; set; }
            public string roNo { get; set; }
            public DateTimeOffset? dateFrom { get; set; }
            public DateTimeOffset? dateTo { get; set; }
        }

        public List<ViewModels.IntegrationViewModel.BuyerViewModel> GetGarmentBuyer()
        {
            var response = httpClientService.GetAsync($@"{APIEndpoint.Core}{buyerUri}").Result.Content.ReadAsStringAsync();

            if (response != null)
            {
                Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.Result);
                var json = result.Single(p => p.Key.Equals("data")).Value;
                List<ViewModels.IntegrationViewModel.BuyerViewModel> buyerList = JsonConvert.DeserializeObject<List<ViewModels.IntegrationViewModel.BuyerViewModel>>(json.ToString());

                return buyerList;
            }
            else
            {
                return null;
            }
        }
    }
}
