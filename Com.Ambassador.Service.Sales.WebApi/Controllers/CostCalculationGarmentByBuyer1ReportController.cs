
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
    [Route("v{version:apiVersion}/cost-calculation-display-by-buyer")]
    [Authorize]
    public class CostCalculationByBuyer1ReportController : BaseMonitoringController<CostCalculationGarmentByBuyer1ReportViewModel, ICostCalculationGarmentByBuyer1Report>
    {
        private readonly static string apiVersion = "1.0";

        public CostCalculationByBuyer1ReportController(IIdentityService identityService, ICostCalculationGarmentByBuyer1Report facade) : base(identityService, facade, apiVersion)
        {
        }
    }
}