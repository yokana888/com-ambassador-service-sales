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
    public class BudgetExportGarmentReportLogic : BaseMonitoringLogic<BudgetExportGarmentReportViewModel>
    {
        private IIdentityService identityService;
        private SalesDbContext dbContext;
        private DbSet<CostCalculationGarment> dbSet;

        public BudgetExportGarmentReportLogic(IIdentityService identityService, SalesDbContext dbContext)
        {
            this.identityService = identityService;
            this.dbContext = dbContext;
            dbSet = dbContext.Set<CostCalculationGarment>();
        }

        public override IQueryable<BudgetExportGarmentReportViewModel> GetQuery(string filter)
        {
            Filter _filter = JsonConvert.DeserializeObject<Filter>(filter);

            IQueryable<CostCalculationGarment> Query = dbSet;

            if (!string.IsNullOrWhiteSpace(_filter.section))
            {
                Query = Query.Where(cc => cc.Section == _filter.section);
            }

            if (!string.IsNullOrWhiteSpace(_filter.unitName))
            {
                Query = Query.Where(cc => cc.UnitName == _filter.unitName);
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

            Query = Query.OrderBy(o => o.RO_Number).ThenBy(o => o.BuyerBrandCode);

            var newQ = (from a in Query
                        join b in dbContext.CostCalculationGarment_Materials on a.Id equals b.CostCalculationGarmentId 
                        where b.CategoryName != "PROCESS" && a.IsApprovedKadivMD == true

                        select new BudgetExportGarmentReportViewModel
                        {
                            RO_Number = a.RO_Number,
                            UnitName = a.UnitCode + "-" + a.UnitName,
                            Section = a.Section,
                            BuyerCode = a.BuyerBrandCode,
                            BuyerName = a.BuyerBrandName,
                            Article = a.Article,
                            DeliveryDate = a.DeliveryDate,
                            PONumber = b.PO_SerialNumber,
                            CategoryName = b.CategoryName,
                            ProductCode = b.ProductCode,
                            ProductName = b.Description,
                            BudgetQuantity = b.BudgetQuantity,
                            BudgetUOM = b.UOMPriceName,
                            BudgetPrice = b.Price,
                            BudgetAmount = b.Price * b.BudgetQuantity, 
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
