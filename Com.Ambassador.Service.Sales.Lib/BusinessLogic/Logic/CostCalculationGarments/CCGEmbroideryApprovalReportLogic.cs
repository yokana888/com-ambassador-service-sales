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
    public class CCGEmbroideryApprovalReportLogic : BaseMonitoringLogic<CCGEmbroideryApprovalReportViewModel>
    {
        private IIdentityService identityService;
        private SalesDbContext dbContext;
        private DbSet<CostCalculationGarment> dbSet;

        public CCGEmbroideryApprovalReportLogic(IIdentityService identityService, SalesDbContext dbContext)
        {
            this.identityService = identityService;
            this.dbContext = dbContext;
            dbSet = dbContext.Set<CostCalculationGarment>();
        }

        public override IQueryable<CCGEmbroideryApprovalReportViewModel> GetQuery(string filter)
        {
            Dictionary<string, object> FilterDictionary = new Dictionary<string, object>(JsonConvert.DeserializeObject<Dictionary<string, object>>(filter), StringComparer.OrdinalIgnoreCase);

            IQueryable<CostCalculationGarment> Query = dbSet;

            try
            {
                var dateFrom = (DateTime)(FilterDictionary["dateFrom"]);
                var dateTo = (DateTime)(FilterDictionary["dateTo"]);

                Query = dbSet.Where(d => d.DeliveryDate >= dateFrom &&
                                         d.DeliveryDate <= dateTo
                );
            }
            catch (KeyNotFoundException e)
            {
                throw new Exception(e.Message);
            }

            Query = Query.OrderBy(o => o.RO_Number).ThenBy(o => o.BuyerBrandCode);

            var newQ = (from a in Query
                        join b in dbContext.CostCalculationGarment_Materials on a.Id equals b.CostCalculationGarmentId 
                        where (b.CategoryCode == "EMB" || b.CategoryCode == "WSH") && a.IsApprovedKadivMD == true

                        select new CCGEmbroideryApprovalReportViewModel
                        {
                            RO_Number = a.RO_Number,
                            UnitName = a.UnitCode + "-" + a.UnitName,
                            Section = a.Section,
                            BuyerCode = a.BuyerBrandCode,
                            BuyerName = a.BuyerBrandName,
                            Article = a.Article,
                            Quantity = a.Quantity,
                            UOMUnit = a.UOMUnit, 
                            DeliveryDate = a.DeliveryDate,
                            ValidatedDate = a.ApprovedKadivMDDate,
                            PONumber = b.PO_SerialNumber,
                            ProductCode = b.ProductCode,
                            ProductName = b.Description,
                            BudgetQuantity = b.BudgetQuantity,
                            BudgetUOM = b.UOMPriceName,
                        });
            return newQ;
        }
        private class Filter
        {
            public string unitName { get; set; }
            public string section { get; set; }
            public DateTimeOffset? dateFrom { get; set; }
            public DateTimeOffset? dateTo { get; set; }
        }
    }
}
