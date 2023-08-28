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
    public class ProfitGarmentBySectionReportLogic : BaseMonitoringLogic<ProfitGarmentBySectionReportViewModel>
    {
        private IIdentityService identityService;
        private IHttpClientService httpClientService;
        private SalesDbContext dbContext;
        private DbSet<CostCalculationGarment> dbSet;

        private string buyerUri = "master/garment-buyers/all";

        public ProfitGarmentBySectionReportLogic(IIdentityService identityService, SalesDbContext dbContext, IHttpClientService httpClientService)
        {
            this.identityService = identityService;
            this.httpClientService = httpClientService;
            this.dbContext = dbContext;
            dbSet = dbContext.Set<CostCalculationGarment>();
        }

        public override IQueryable<ProfitGarmentBySectionReportViewModel> GetQuery(string filter)
        {
            Filter _filter = JsonConvert.DeserializeObject<Filter>(filter);

            IQueryable<CostCalculationGarment> Query = dbSet;

            if (!string.IsNullOrWhiteSpace(_filter.section))
            {
                Query = Query.Where(cc => cc.Section == _filter.section);
            }
            if (_filter.dateFrom != null)
            {
                var filterDate = _filter.dateFrom.GetValueOrDefault().ToOffset(TimeSpan.FromHours(identityService.TimezoneOffset)).Date;
                Query = Query.Where(cc => cc.DeliveryDate.AddHours(identityService.TimezoneOffset).Date >= filterDate);
            }
            if (_filter.dateTo != null)
            {
                var filterDate = _filter.dateTo.GetValueOrDefault().ToOffset(TimeSpan.FromHours(identityService.TimezoneOffset)).AddDays(1).Date;
                Query = Query.Where(cc => cc.DeliveryDate.AddHours(identityService.TimezoneOffset).Date < filterDate);
            }

            IQueryable<ViewModels.IntegrationViewModel.BuyerViewModel> buyerQ = GetGarmentBuyer().AsQueryable();

            Query = Query.OrderBy(o => o.Section).ThenBy(o => o.BuyerBrandCode);
            var newQ = (from a in Query
                        join b in dbContext.CostCalculationGarment_Materials on a.Id equals b.CostCalculationGarmentId
                        where a.IsApprovedKadivMD == true
                        group new { CMP = b.CM_Price.GetValueOrDefault(), GmtCost = b.Total, Shipfee = b.TotalShippingFee } by new
                        {
                            a.UnitName,
                            a.Section,
                            a.BuyerCode,
                            a.BuyerName,
                            a.BuyerBrandCode,
                            a.BuyerBrandName,
                            a.Commodity,
                            a.CommodityDescription,
                            a.RO_Number,
                            a.Article,
                            a.Quantity,
                            a.UOMUnit,
                            a.DeliveryDate,
                            a.NETFOBP,
                            a.NETFOB,
                            a.ConfirmPrice,
                            a.RateValue,
                            a.FabricAllowance,
                            a.AccessoriesAllowance,
                            a.OTL1CalculatedRate,
                            a.OTL2CalculatedRate,
                            a.Risk,
                            a.CommissionRate,
                            a.CommissionPortion,
                            a.Insurance,
                            a.Freight,
                        } into G

                        select new ProfitGarmentBySectionReportViewModel
                        {
                            UnitName = G.Key.UnitName,
                            Section = G.Key.Section,
                            BuyerCode = G.Key.BuyerCode,
                            BuyerName = G.Key.BuyerName,
                            BrandCode = G.Key.BuyerBrandCode,
                            BrandName = G.Key.BuyerBrandName,
                            Type = buyerQ.Where(x => x.Code == G.Key.BuyerCode).Select(x => x.Type).FirstOrDefault(),
                            RO_Number = G.Key.RO_Number,
                            Comodity = G.Key.Commodity,
                            ComodityDescription = G.Key.CommodityDescription,
                            Profit = G.Key.NETFOBP,
                            Article = G.Key.Article,
                            Quantity = G.Key.Quantity,
                            UOMUnit = G.Key.UOMUnit,
                            DeliveryDate = G.Key.DeliveryDate,
                            ConfirmPrice = G.Key.ConfirmPrice,
                            HPP = G.Key.RateValue > 1 ? Math.Round(((Math.Round(G.Sum(m => m.GmtCost), 2) + G.Key.OTL1CalculatedRate + G.Key.OTL2CalculatedRate + ((G.Key.Risk / 100) * (Math.Round(G.Sum(m => m.GmtCost), 2) + G.Key.OTL1CalculatedRate + G.Key.OTL2CalculatedRate)) + Math.Round(G.Sum(m => m.Shipfee), 2)) / G.Key.RateValue),2) : Math.Round(G.Sum(m => m.GmtCost), 2) + G.Key.OTL1CalculatedRate + G.Key.OTL2CalculatedRate + ((G.Key.Risk / 100) * (Math.Round(G.Sum(m => m.GmtCost), 2) + G.Key.OTL1CalculatedRate + G.Key.OTL2CalculatedRate)) + Math.Round(G.Sum(m => m.Shipfee), 2),
            
                            CurrencyRate = G.Key.RateValue,
                            CMPrice = Math.Round(G.Sum(m => m.CMP), 2) / G.Key.RateValue * 1.05,
                            FOBPrice = ((Math.Round(G.Sum(m => m.CMP), 2) / G.Key.RateValue) * 1.05) + G.Key.ConfirmPrice,
                            FabAllow = G.Key.FabricAllowance,
                            AccAllow = G.Key.AccessoriesAllowance,
                            Amount = G.Key.Quantity * (((Math.Round(G.Sum(m => m.CMP), 2) / G.Key.RateValue) * 1.05) + G.Key.ConfirmPrice),
                            Commision = G.Key.CommissionPortion,
                            CommisionIDR = G.Key.CommissionRate,
                            ProfitIDR = G.Key.RateValue > 1 ? 0 : ((G.Key.ConfirmPrice - G.Key.Insurance - G.Key.Freight) * G.Key.RateValue) - G.Key.CommissionRate - Math.Round(G.Sum(m => m.GmtCost), 2) - G.Key.OTL1CalculatedRate - G.Key.OTL2CalculatedRate - ((G.Key.Risk / 100) * (Math.Round(G.Sum(m => m.GmtCost), 2) + G.Key.OTL1CalculatedRate + G.Key.OTL2CalculatedRate)) - Math.Round(G.Sum(m => m.Shipfee), 2),
                            ProfitUSD = G.Key.RateValue > 1 ? Math.Round(((((G.Key.ConfirmPrice - G.Key.Insurance - G.Key.Freight) * G.Key.RateValue) - G.Key.CommissionRate - Math.Round(G.Sum(m => m.GmtCost), 2) - G.Key.OTL1CalculatedRate - G.Key.OTL2CalculatedRate - ((G.Key.Risk / 100) * (Math.Round(G.Sum(m => m.GmtCost), 2) + G.Key.OTL1CalculatedRate + G.Key.OTL2CalculatedRate)) - Math.Round(G.Sum(m => m.Shipfee), 2)) / G.Key.RateValue), 2) : 0,
                            ProfitFOB = Math.Round(((((((G.Key.ConfirmPrice - G.Key.Insurance - G.Key.Freight) * G.Key.RateValue) - G.Key.CommissionRate - Math.Round(G.Sum(m => m.GmtCost), 2) - G.Key.OTL1CalculatedRate - G.Key.OTL2CalculatedRate - ((G.Key.Risk / 100) * (Math.Round(G.Sum(m => m.GmtCost), 2) + G.Key.OTL1CalculatedRate + G.Key.OTL2CalculatedRate)) - Math.Round(G.Sum(m => m.Shipfee), 2)) / G.Key.RateValue) * 100) / (((Math.Round(G.Sum(m => m.CMP), 2) / G.Key.RateValue) * 1.05) + G.Key.ConfirmPrice)), 2),
                        });

            var termQ = (from a in Query
                         where a.IsApprovedKadivMD == true
                         select new
                         {
                             a.RO_Number,
                             IsFabricCM = a.CostCalculationGarment_Materials.Any(x => x.isFabricCM) ? "CMT" : "FOB"
                         });

            var result = (from a in newQ
                          join b in termQ on a.RO_Number equals b.RO_Number
                          select new ProfitGarmentBySectionReportViewModel
                          {
                              UnitName = a.UnitName,
                              Section = a.Section,
                              BuyerCode = a.BuyerCode,
                              BuyerName = a.BuyerName,
                              BrandCode = a.BrandCode,
                              BrandName = a.BrandName,
                              Type = a.Type,
                              RO_Number = a.RO_Number,
                              Comodity = a.Comodity,
                              ComodityDescription = a.ComodityDescription,
                              Profit = a.Profit,
                              Article = a.Article,
                              Quantity = a.Quantity,
                              UOMUnit = a.UOMUnit,
                              DeliveryDate = a.DeliveryDate,
                              ConfirmPrice = b.IsFabricCM == "FOB" ? a.ConfirmPrice : 0,
                              ConfirmPrice1 = b.IsFabricCM == "CMT" ? a.ConfirmPrice : 0,
                              HPP = a.HPP,
                              CurrencyRate = a.CurrencyRate,
                              CMPrice = a.CMPrice,
                              FOBPrice = a.FOBPrice,
                              FabAllow = a.FabAllow,
                              AccAllow = a.AccAllow,
                              Amount = a.Amount,
                              Commision = a.Commision,
                              CommisionIDR = a.CommisionIDR,
                              ProfitIDR = a.ProfitIDR == 0 ? 0 : a.ProfitIDR,
                              ProfitUSD = a.ProfitUSD == 0 ? 0 : a.ProfitUSD,
                              ProfitFOB = a.ProfitUSD == 0 ? 0 : a.ProfitFOB,
                              TermPayment = b.IsFabricCM,
                          });

            return result;
        }

        private class Filter
        {
            public string section { get; set; }
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
