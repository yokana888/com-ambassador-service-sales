using Com.Ambassador.Service.Sales.Lib.Helpers;
using Com.Ambassador.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentPreSalesContractModel;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel.GarmentPurchaseRequestViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.Garment
{
    public class MonitoringPreSalesContractLogic : BaseMonitoringLogic<GarmentPreSalesContract>
    {
        private IServiceProvider serviceProvider;
        private SalesDbContext dbContext;
        private readonly IIdentityService identityService;

        public MonitoringPreSalesContractLogic(IServiceProvider serviceProvider, SalesDbContext dbContext, IIdentityService identityService)
        {
            this.serviceProvider = serviceProvider;
            this.dbContext = dbContext;
            this.identityService = identityService;
        }

        public override IQueryable<GarmentPreSalesContract> GetQuery(string filterString)
        {
            Filter filter = JsonConvert.DeserializeObject<Filter>(filterString);

            IQueryable<GarmentPreSalesContract> Query = dbContext.GarmentPreSalesContracts;

            if (!string.IsNullOrWhiteSpace(filter.section))
            {
                Query = Query.Where(sc => sc.SectionCode == filter.section);
            }
            if (!string.IsNullOrWhiteSpace(filter.preSCNo))
            {
                Query = Query.Where(sc => sc.SCNo == filter.preSCNo);
            }
            if (!string.IsNullOrWhiteSpace(filter.preSCType))
            {
                Query = Query.Where(sc => sc.SCType == filter.preSCType);
            }
            if (!string.IsNullOrWhiteSpace(filter.buyerAgent))
            {
                Query = Query.Where(sc => sc.BuyerAgentCode == filter.buyerAgent);
            }
            if (!string.IsNullOrWhiteSpace(filter.buyerBrand))
            {
                Query = Query.Where(sc => sc.BuyerBrandCode == filter.buyerBrand);
            }
            if (filter.dateStart != null)
            {
                var filterDate = filter.dateStart.GetValueOrDefault().ToOffset(TimeSpan.FromHours(identityService.TimezoneOffset)).Date;
                Query = Query.Where(sc => sc.SCDate.AddHours(identityService.TimezoneOffset).Date >= filterDate);
            }
            if (filter.dateEnd != null)
            {
                var filterDate = filter.dateEnd.GetValueOrDefault().ToOffset(TimeSpan.FromHours(identityService.TimezoneOffset)).AddDays(1).Date;
                Query = Query.Where(sc => sc.SCDate.AddHours(identityService.TimezoneOffset).Date < filterDate);
            }

            if (!string.IsNullOrWhiteSpace(filter.prNoMaster) || !string.IsNullOrWhiteSpace(filter.roNoMaster) || !string.IsNullOrWhiteSpace(filter.unitMaster))
            {
                IHttpClientService httpClientService = (IHttpClientService)serviceProvider.GetService(typeof(IHttpClientService));
                UriBuilder uriBuilder = new UriBuilder(string.Concat(APIEndpoint.AzurePurchasing, "garment-purchase-requests/dynamic"));

                var filterDict = new Dictionary<string, object>()
                {
                    { "PRType == \"MASTER\" || PRType == \"SAMPLE\"", true }
                };
                if (!string.IsNullOrWhiteSpace(filter.prNoMaster))
                {
                    filterDict.Add("PRNo", filter.prNoMaster);
                }
                if (!string.IsNullOrWhiteSpace(filter.roNoMaster))
                {
                    filterDict.Add("RONo", filter.roNoMaster);
                }
                if (!string.IsNullOrWhiteSpace(filter.unitMaster))
                {
                    filterDict.Add("UnitCode", filter.unitMaster);
                }
                var filterDictString = JsonConvert.SerializeObject(filterDict);
                var selectDictString = JsonConvert.SerializeObject(new Dictionary<string, object>()
                {
                    { "SCNo", 1 },
                });

                var queryDict = new Dictionary<string, object>()
                {
                    { "select", HttpUtility.UrlEncode(selectDictString) },
                    { "filter", HttpUtility.UrlEncode(filterDictString) },
                    { "size", int.MaxValue }
                };
                uriBuilder.Query = string.Join("&", queryDict.Select(dict => dict.Key + "=" + dict.Value));

                var responseMessage = httpClientService.GetAsync(uriBuilder.Uri.AbsoluteUri).Result;
                if (!responseMessage.IsSuccessStatusCode)
                {
                    throw new Exception("Get SCNo from Garment Purchase Request Failed");
                }
                var contentString = responseMessage.Content.ReadAsStringAsync().Result;
                var content = JsonConvert.DeserializeObject<Dictionary<string, object>>(contentString);
                var contentData = content.SingleOrDefault(p => p.Key.Equals("data")).Value;

                var dataGarmentPurchaseRequest = JsonConvert.DeserializeObject<List<GarmentPurchaseRequestViewModel>>(contentData.ToString());

                var scNos = dataGarmentPurchaseRequest.Select(pr => pr.SCNo).ToHashSet();

                Query = Query.Where(sc => scNos.Contains(sc.SCNo));
            }

            if (!string.IsNullOrWhiteSpace(filter.roNoJob) || !string.IsNullOrWhiteSpace(filter.unitJob))
            {
                IQueryable<CostCalculationGarment> ccQuery = dbContext.CostCalculationGarments;
                if (!string.IsNullOrWhiteSpace(filter.roNoJob))
                {
                    ccQuery = ccQuery.Where(cc => cc.RO_Number == filter.roNoJob);
                }
                if (!string.IsNullOrWhiteSpace(filter.unitJob))
                {
                    ccQuery = ccQuery.Where(cc => cc.UnitCode == filter.unitJob);
                }
                var preSCIds = ccQuery.Select(cc => cc.PreSCId);

                Query = Query.Where(sc => preSCIds.Contains(sc.Id));
            }

            Query = Query.Select(sc => new GarmentPreSalesContract
            {
                SectionCode = sc.SectionCode,
                SCNo = sc.SCNo,
                SCDate = sc.SCDate,
                SCType = sc.SCType,
                BuyerAgentName = sc.BuyerAgentName,
                BuyerBrandName = sc.BuyerBrandName,
                BuyerAgentCode = sc.BuyerAgentCode,
                OrderQuantity = sc.OrderQuantity
            });

            return Query;
        }

        public IQueryable<CostCalculationGarment> GetCCQuery(string filterString, HashSet<string> preSCNos)
        {
            Filter filter = JsonConvert.DeserializeObject<Filter>(filterString);

            IQueryable<CostCalculationGarment> Query = dbContext.CostCalculationGarments;

            Query = Query.Where(cc => preSCNos.Contains(cc.PreSCNo));

            if (!string.IsNullOrWhiteSpace(filter.roNoJob))
            {
                Query = Query.Where(cc => cc.RO_Number == filter.roNoJob);
            }
            if (!string.IsNullOrWhiteSpace(filter.unitJob))
            {
                Query = Query.Where(cc => cc.UnitCode == filter.unitJob);
            }

            Query = Query.Select(cc => new CostCalculationGarment
            {
                PreSCNo = cc.PreSCNo,
                CreatedUtc = cc.CreatedUtc,
                RO_Number = cc.RO_Number,
                Article = cc.Article,
                UnitName = cc.UnitName,
                Quantity = cc.Quantity,
                UOMUnit = cc.UOMUnit,
                ConfirmPrice = cc.ConfirmPrice
            });

            return Query;
        }

        internal List<GarmentPurchaseRequestViewModel> GetPurchaseRequest(string filterString, HashSet<string> allSCNo)
        {
            Filter filter = JsonConvert.DeserializeObject<Filter>(filterString);

            IHttpClientService httpClientService = (IHttpClientService)serviceProvider.GetService(typeof(IHttpClientService));
            UriBuilder uriBuilder = new UriBuilder(string.Concat(APIEndpoint.AzurePurchasing, "garment-purchase-requests/dynamic"));

            var scNoFilter = "1==0";
            if (allSCNo.Count > 0)
            {
                scNoFilter = string.Join(" || ", allSCNo.Select(scNo => $"SCNo==\"{scNo}\""));
            }
            var filterDict = new Dictionary<string, object>()
            {
                { scNoFilter, true },
                { "PRType == \"MASTER\" || PRType == \"SAMPLE\"", true }
            };
            if (!string.IsNullOrWhiteSpace(filter.prNoMaster))
            {
                filterDict.Add("PRNo", filter.prNoMaster);
            }
            if (!string.IsNullOrWhiteSpace(filter.roNoMaster))
            {
                filterDict.Add("RONo", filter.roNoMaster);
            }
            if (!string.IsNullOrWhiteSpace(filter.unitMaster))
            {
                filterDict.Add("UnitCode", filter.unitMaster);
            }
            var filterDictString = JsonConvert.SerializeObject(filterDict);
            var selectDictString = JsonConvert.SerializeObject(new Dictionary<string, object>()
            {
                { "SCNo", 1 },
                { "PRNo", 1 },
                { "RONo", 1 },
                { "PRType", 1 },
                { "Unit", "new(UnitName as Name)" },
                { "CreatedUtc", 1 },
                { "Article", 1 },
            });

            var queryDict = new Dictionary<string, object>()
            {
                { "select", HttpUtility.UrlEncode(selectDictString) },
                { "filter", HttpUtility.UrlEncode(filterDictString) },
                { "size", int.MaxValue }
            };
            uriBuilder.Query = string.Join("&", queryDict.Select(dict => dict.Key + "=" + dict.Value));

            var responseMessage = httpClientService.GetAsync(uriBuilder.Uri.AbsoluteUri).Result;
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Get Garment Purchase Request by SCNo Failed");
            }
            var contentString = responseMessage.Content.ReadAsStringAsync().Result;
            var content = JsonConvert.DeserializeObject<Dictionary<string, object>>(contentString);
            var contentData = content.SingleOrDefault(p => p.Key.Equals("data")).Value;
            var data = JsonConvert.DeserializeObject<List<GarmentPurchaseRequestViewModel>>(contentData.ToString());

            return data;
        }

        private class Filter
        {
            public string section { get; set; }
            public string preSCNo { get; set; }
            public string preSCType { get; set; }
            public string buyerAgent { get; set; }
            public string buyerBrand { get; set; }

            public string prNoMaster { get; set; }
            public string roNoMaster { get; set; }
            public string unitMaster { get; set; }

            public string roNoJob { get; set; }
            public string unitJob { get; set; }

            public DateTimeOffset? dateStart { get; set; }
            public DateTimeOffset? dateEnd { get; set; }
        }
    }
}
