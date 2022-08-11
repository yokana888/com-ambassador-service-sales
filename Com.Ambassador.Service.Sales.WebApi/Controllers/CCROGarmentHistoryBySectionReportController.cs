
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.CostCalculationGarmentLogic;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.ViewModels.CostCalculationGarment;
using Com.Ambassador.Service.Sales.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Ambassador.Service.Sales.WebApi.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/cc-ro-garment-history-by-report")]
    [Authorize]
    public class CCROGarmentHistoryBySectionReportController : BaseMonitoringController<CCROGarmentHistoryBySectionReportViewModel, ICCROGarmentHistoryBySectionReport>
    {
        private readonly static string apiVersion = "1.0";

        public CCROGarmentHistoryBySectionReportController(IIdentityService identityService, ICCROGarmentHistoryBySectionReport facade) : base(identityService, facade, apiVersion)
        {
        }
    }
}