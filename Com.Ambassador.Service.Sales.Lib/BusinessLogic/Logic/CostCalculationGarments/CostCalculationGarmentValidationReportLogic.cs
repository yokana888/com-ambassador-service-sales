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
    public class CostCalculationGarmentValidationReportLogic : BaseMonitoringLogic<CostCalculationGarmentValidationReportViewModel>
    {
        private IIdentityService identityService;
        private SalesDbContext dbContext;
        private DbSet<CostCalculationGarment> dbSet;

        public CostCalculationGarmentValidationReportLogic(IIdentityService identityService, SalesDbContext dbContext)
        {
            this.identityService = identityService;
            this.dbContext = dbContext;
            dbSet = dbContext.Set<CostCalculationGarment>();
        }

        public override IQueryable<CostCalculationGarmentValidationReportViewModel> GetQuery(string filter)
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

            Query = Query.OrderBy(o => o.UnitName).ThenBy(o => o.RO_Number);

            var newQ = (from a in Query
                    select new CostCalculationGarmentValidationReportViewModel
                    {
                        RO_Number = a.RO_Number,
                        StaffName = a.CreatedBy, 
                        ConfirmDate = a.ConfirmDate,
                        DeliveryDate = a.DeliveryDate,
                        UnitName = a.UnitCode + "-" + a.UnitName,
                        Section = a.Section + "-" + a.SectionName,
                        Article = a.Article,
                        BuyerCode = a.BuyerCode,
                        BuyerName = a.BuyerName,
                        BrandCode = a.BuyerBrandCode,
                        BrandName = a.BuyerBrandName,
                        Comodity = a.ComodityCode,
                        Quantity = a.Quantity,
                        UOMUnit = a.UOMUnit,
                        ValidatedMD = a.IsApprovedMD.Equals(true) ? "SUDAH" : "BELUM",
                        ValidatedIE = a.IsApprovedIE.Equals(true) ? "SUDAH" : "BELUM",
                        ValidatedPurch = a.IsApprovedPurchasing.Equals(true) ? "SUDAH" : "BELUM",
                        ValidatedKadiv = a.IsApprovedKadivMD.Equals(true) ? "SUDAH" : "BELUM",
                        ValidatedDate = a.ApprovedKadivMDDate,
                    });

            return newQ;
        }
    }
}
