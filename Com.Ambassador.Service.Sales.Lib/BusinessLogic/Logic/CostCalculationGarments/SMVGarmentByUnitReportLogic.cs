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
    public class SMVGarmentByUnitReportLogic : BaseMonitoringLogic<SMVGarmentByUnitReportViewModel>
    {
        private IIdentityService identityService;
        private SalesDbContext dbContext;
        private DbSet<CostCalculationGarment> dbSet;

        public SMVGarmentByUnitReportLogic(IIdentityService identityService, SalesDbContext dbContext)
        {
            this.identityService = identityService;
            this.dbContext = dbContext;
            dbSet = dbContext.Set<CostCalculationGarment>();
        }

        public override IQueryable<SMVGarmentByUnitReportViewModel> GetQuery(string filter)
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
                    select new SMVGarmentByUnitReportViewModel
                    {
                        RO_Number = a.RO_Number,
                        ConfirmDate = a.ConfirmDate,
                        DeliveryDate = a.DeliveryDate,
                        UnitName = a.UnitName,
                        Section = a.Section,
                        Article = a.Article,
                        BuyerCode = a.BuyerCode,
                        BuyerName = a.BuyerName,
                        BrandCode = a.BuyerBrandCode,
                        BrandName = a.BuyerBrandName,
                        Comodity = a.ComodityCode,
                        Quantity = a.Quantity,
                        UOMUnit = a.UOMUnit,
                        SMV_Cutting = a.SMV_Cutting,
                        SMV_Sewing = a.SMV_Sewing,
                        SMV_Finishing = a.SMV_Finishing,
                        SMV_Total = a.SMV_Total,
                        StatusValid = a.IsApprovedKadivMD.Equals(true) ? "SUDAH" : "BELUM",
                    });

            return newQ;
        }
    }
}
