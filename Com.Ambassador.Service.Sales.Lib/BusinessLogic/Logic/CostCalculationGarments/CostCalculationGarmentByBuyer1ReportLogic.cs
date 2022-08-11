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
    public class CostCalculationByBuyer1ReportLogic : BaseMonitoringLogic<CostCalculationGarmentByBuyer1ReportViewModel>
    {
        private IIdentityService identityService;
        private SalesDbContext dbContext;
        private DbSet<CostCalculationGarment> dbSet;

        public CostCalculationByBuyer1ReportLogic(IIdentityService identityService, SalesDbContext dbContext)
        {
            this.identityService = identityService;
            this.dbContext = dbContext;
            dbSet = dbContext.Set<CostCalculationGarment>();
        }

        public override IQueryable<CostCalculationGarmentByBuyer1ReportViewModel> GetQuery(string filter)
        {
            Dictionary<string, string> FilterDictionary = new Dictionary<string, string>(JsonConvert.DeserializeObject<Dictionary<string, string>>(filter), StringComparer.OrdinalIgnoreCase);

            IQueryable<CostCalculationGarment> Query = dbSet;

            try
            {
                var year = int.Parse(FilterDictionary["year"]);
                Query = dbSet.Where(d => d.ConfirmDate.Year == year 
                );
            }
            catch (KeyNotFoundException e)
            {
                throw new Exception(e.Message);
            }

            if (FilterDictionary.TryGetValue("buyerAgent", out string buyerAgent))
            {
                Query = Query.Where(d => d.BuyerCode == buyerAgent.ToString());
            }

            if (FilterDictionary.TryGetValue("buyerBrand", out string buyerBrand))
            {
                Query = Query.Where(d => d.BuyerBrandCode == buyerBrand.ToString());
            }

            Query = Query.OrderBy(o => o.BuyerBrandName).ThenBy(o => o.RO_Number);

            var newQ = (from a in Query
                    select new CostCalculationGarmentByBuyer1ReportViewModel
                    {
                        RO_Number = a.RO_Number,
                        DeliveryDate = a.DeliveryDate,
                        Description = a.CommodityDescription,
                        Article = a.Article,
                        BuyerCode = a.BuyerCode,
                        BuyerName = a.BuyerName,
                        BrandCode = a.BuyerBrandCode,
                        BrandName = a.BuyerBrandName,
                        Commission = a.CommissionPortion,
                        Quantity = a.Quantity,
                        ConfirmPrice = a.ConfirmPrice,
                        UOMUnit = a.UOMUnit,
                        Amount = a.Quantity * a.ConfirmPrice,
                    });

            return newQ;
        }
    }
}
