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
    public class ProfitGarmentByComodityReportLogic : BaseMonitoringLogic<ProfitGarmentByComodityReportViewModel>
    {
        private IIdentityService identityService;
        private SalesDbContext dbContext;
        private DbSet<CostCalculationGarment> dbSet;

        public ProfitGarmentByComodityReportLogic(IIdentityService identityService, SalesDbContext dbContext)
        {
            this.identityService = identityService;
            this.dbContext = dbContext;
            dbSet = dbContext.Set<CostCalculationGarment>();
        }

        public override IQueryable<ProfitGarmentByComodityReportViewModel> GetQuery(string filter)
        {
            Filter _filter = JsonConvert.DeserializeObject<Filter>(filter);

            IQueryable<CostCalculationGarment> Query = dbSet;

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

            //Query = Query.OrderBy(o => o.ComodityCode);
            //var newQ = (from a in Query
            //            where a.IsApprovedKadivMD == true
            //            group new { Qty = a.Quantity, Amt = a.Quantity * a.ConfirmPrice } by new { a.ComodityCode, a.Commodity, a.UOMUnit } into G

            //            select new ProfitGarmentByComodityReportViewModel
            //            {                
            //                ComodityCode = G.Key.ComodityCode,
            //                ComodityName = G.Key.Commodity,
            //                Quantity = G.Sum(m => m.Qty),
            //                Amount = Math.Round(G.Sum(m => m.Amt), 2),
            //                UOMUnit = G.Key.UOMUnit,
            //            });

            Query = Query.OrderBy(o => o.ComodityCode);
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
                            a.ComodityCode,
                            a.Commodity,
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
                            a.Freight
                        } into G

                        select new ProfitGarmentBySectionReportViewModel
                        {
                            UnitName = G.Key.UnitName,
                            Section = G.Key.Section,
                            BuyerCode = G.Key.BuyerCode,
                            BuyerName = G.Key.BuyerName,
                            BrandCode = G.Key.BuyerBrandCode,
                            BrandName = G.Key.BuyerBrandName,
                            RO_Number = G.Key.RO_Number,
                            Comodity = G.Key.ComodityCode,
                            ComodityDescription = G.Key.Commodity,
                            Profit = G.Key.NETFOBP,
                            Article = G.Key.Article,
                            Quantity = G.Key.Quantity,
                            UOMUnit = G.Key.UOMUnit,
                            DeliveryDate = G.Key.DeliveryDate,
                            ConfirmPrice = G.Key.ConfirmPrice,
                            CurrencyRate = G.Key.RateValue,
                            CMPrice = Math.Round(G.Sum(m => m.CMP), 2) / G.Key.RateValue * 1.05,
                            FOBPrice = ((Math.Round(G.Sum(m => m.CMP), 2) / G.Key.RateValue) * 1.05) + G.Key.ConfirmPrice,
                            FabAllow = G.Key.FabricAllowance,
                            AccAllow = G.Key.AccessoriesAllowance,
                            Amount = G.Key.Quantity * (((Math.Round(G.Sum(m => m.CMP), 2) / G.Key.RateValue) * 1.05) + G.Key.ConfirmPrice),
                            Commision = G.Key.CommissionPortion,
                            ProfitIDR = ((G.Key.ConfirmPrice - G.Key.Insurance - G.Key.Freight) * G.Key.RateValue) - G.Key.CommissionRate - Math.Round(G.Sum(m => m.GmtCost), 2) - G.Key.OTL1CalculatedRate - G.Key.OTL2CalculatedRate - ((G.Key.Risk / 100) * (Math.Round(G.Sum(m => m.GmtCost), 2) + G.Key.OTL1CalculatedRate + G.Key.OTL2CalculatedRate)) - Math.Round(G.Sum(m => m.Shipfee), 2),
                            ProfitUSD = Math.Round(((((G.Key.ConfirmPrice - G.Key.Insurance - G.Key.Freight) * G.Key.RateValue) - G.Key.CommissionRate - Math.Round(G.Sum(m => m.GmtCost), 2) - G.Key.OTL1CalculatedRate - G.Key.OTL2CalculatedRate - ((G.Key.Risk / 100) * (Math.Round(G.Sum(m => m.GmtCost), 2) + G.Key.OTL1CalculatedRate + G.Key.OTL2CalculatedRate)) - Math.Round(G.Sum(m => m.Shipfee), 2)) / G.Key.RateValue), 2),
                            ProfitFOB = Math.Round(((((((G.Key.ConfirmPrice - G.Key.Insurance - G.Key.Freight) * G.Key.RateValue) - G.Key.CommissionRate - Math.Round(G.Sum(m => m.GmtCost), 2) - G.Key.OTL1CalculatedRate - G.Key.OTL2CalculatedRate - ((G.Key.Risk / 100) * (Math.Round(G.Sum(m => m.GmtCost), 2) + G.Key.OTL1CalculatedRate + G.Key.OTL2CalculatedRate)) - Math.Round(G.Sum(m => m.Shipfee), 2)) / G.Key.RateValue) * 100) / (((Math.Round(G.Sum(m => m.CMP), 2) / G.Key.RateValue) * 1.05) + G.Key.ConfirmPrice)), 2),
                        });

            newQ = newQ.OrderBy(o => o.Comodity);

            var termQ = (from a in Query
                         where a.IsApprovedKadivMD == true
                         select new
                         {
                             a.RO_Number,
                             IsFabricCM = a.CostCalculationGarment_Materials.Any(x => x.isFabricCM) ? "CMT" : "FOB"
                         });

            var newQuery = (from c in newQ
                            join d in termQ 
                            on c.RO_Number equals d.RO_Number
                            group new { Qty = c.Quantity, Amt = c.Amount, Prft1 = c.ProfitUSD, Prft2 = c.ProfitIDR, Prft3 = c.ProfitFOB } by new
                            {
                                c.Comodity,
                                c.ComodityDescription,
                                c.UOMUnit,
                                d.IsFabricCM
                            } into GroupData

                            select new ProfitGarmentByComodityReportViewModel
                            {
                                ComodityCode = GroupData.Key.Comodity,
                                ComodityName = GroupData.Key.ComodityDescription,
                                UOMUnit = GroupData.Key.UOMUnit,
                                Quantity = GroupData.Sum(m => m.Qty),
                                Amount = Math.Round(GroupData.Sum(m => m.Amt), 2),
                                TermPayment = GroupData.Key.IsFabricCM,
                                ProfitUSD = Math.Round(GroupData.Sum(m => m.Prft1), 2),
                                ProfitIDR = Math.Round(GroupData.Sum(m => m.Prft2), 2),
                                ProfitFOB = Math.Round(GroupData.Sum(m => m.Prft3), 2),
                            });

            return newQuery;
        }

        private class Filter
        {
            public DateTimeOffset? dateFrom { get; set; }
            public DateTimeOffset? dateTo { get; set; }
        }
    }
}
