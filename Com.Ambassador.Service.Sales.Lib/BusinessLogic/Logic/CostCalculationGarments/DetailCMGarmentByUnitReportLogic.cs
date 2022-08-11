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
    public class DetailCMGarmentByUnitReportLogic : BaseMonitoringLogic<DetailCMGarmentByUnitReportViewModel>
    {
        private IIdentityService identityService;
        private SalesDbContext dbContext;
        private DbSet<CostCalculationGarment> dbSet;

        public DetailCMGarmentByUnitReportLogic(IIdentityService identityService, SalesDbContext dbContext)
        {
            this.identityService = identityService;
            this.dbContext = dbContext;
            dbSet = dbContext.Set<CostCalculationGarment>();
        }

        public override IQueryable<DetailCMGarmentByUnitReportViewModel> GetQuery(string filter)
        {
            Dictionary<string, object> FilterDictionary = new Dictionary<string, object>(JsonConvert.DeserializeObject<Dictionary<string, object>>(filter), StringComparer.OrdinalIgnoreCase);

            IQueryable<CostCalculationGarment> Query = dbSet;

            try
            {
                var dateFrom = (DateTime) (FilterDictionary["dateFrom"]);
                var dateTo= (DateTime) (FilterDictionary["dateTo"]);

                Query = dbSet.Where(d => d.DeliveryDate >= dateFrom && 
                                         d.DeliveryDate <= dateTo
                );
            }
            catch (KeyNotFoundException e)
            {
                throw new Exception(e.Message);
            }

            if (FilterDictionary.TryGetValue("unitName", out object unitName))
            {
                Query = Query.Where(d => d.UnitName == unitName.ToString());
            }


            Query = Query.OrderBy(o => o.UnitName).ThenBy(o => o.BuyerBrandName);
            var newQ = (from a in Query
                        join b in dbContext.CostCalculationGarment_Materials on a.Id equals b.CostCalculationGarmentId
                        where b.CategoryName != "PROCESS"
                        group new { BgtAmt = b.Price * b.BudgetQuantity, CMP = b.CM_Price.GetValueOrDefault() } by new { a.UnitName, a.BuyerCode, a.BuyerName, a.BuyerBrandCode, a.BuyerBrandName,
                                    a.RO_Number, a.Article, a.Quantity, a.UOMUnit, a.DeliveryDate, a.OTL1CalculatedRate, a.OTL2CalculatedRate,
                                    a.SMV_Cutting, a.SMV_Sewing, a.SMV_Finishing, a.SMV_Total, a.CommissionRate, a.Insurance, a.Freight, a.ConfirmPrice, a.RateValue} into G
                        select new DetailCMGarmentByUnitReportViewModel
                         {
                            UnitName = G.Key.UnitName,
                            BuyerCode = G.Key.BuyerCode,
                            BuyerName = G.Key.BuyerName,
                            BrandCode = G.Key.BuyerBrandCode,
                            BrandName = G.Key.BuyerBrandName,
                            RO_Number = G.Key.RO_Number,
                            Article = G.Key.Article,
                            Quantity = G.Key.Quantity,
                            UOMUnit = G.Key.UOMUnit,
                            DeliveryDate = G.Key.DeliveryDate,
                            OTL1 = G.Key.OTL1CalculatedRate,
                            OTL2 = G.Key.OTL2CalculatedRate,
                            SMV_Cutting = G.Key.SMV_Cutting,
                            SMV_Sewing = G.Key.SMV_Sewing,
                            SMV_Finishing = G.Key.SMV_Finishing,
                            SMV_Total = G.Key.SMV_Total,
                            Commission = G.Key.CommissionRate,
                            Insurance = G.Key.Insurance,
                            Freight = G.Key.Freight,
                            ConfirmPrice = G.Key.ConfirmPrice,
                            CurrencyRate = G.Key.RateValue,
                            BudgetAmount = Math.Round(G.Sum(m => m.BgtAmt), 2),
                            CMPrice = Math.Round(G.Sum(m => m.CMP), 2),
                            CMIDR = (G.Key.ConfirmPrice * G.Key.RateValue) - G.Key.CommissionRate - (Math.Round(G.Sum(m => m.BgtAmt), 2) / G.Key.Quantity),
                            CM = ((G.Key.ConfirmPrice * G.Key.RateValue) - G.Key.CommissionRate - (Math.Round(G.Sum(m => m.BgtAmt), 2) / G.Key.Quantity)) / G.Key.RateValue,
                            FOB_Price = G.Key.ConfirmPrice + ((Math.Round(G.Sum(m => m.CMP), 2) / G.Key.RateValue) * 1.05) - (G.Key.Insurance + G.Key.Freight),
                        });
            return newQ;
        }
    }
}
