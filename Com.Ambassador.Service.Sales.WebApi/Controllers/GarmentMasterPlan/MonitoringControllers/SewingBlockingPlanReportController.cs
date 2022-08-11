using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.GarmentMasterPlan.MonitoringInterfaces;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentMasterPlan.MonitoringViewModels;
using Com.Ambassador.Service.Sales.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Ambassador.Service.Sales.WebApi.Controllers.GarmentMasterPlan.MonitoringControllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/garment-master-plan/sewing-blocking-plan-monitoring")]
    [Authorize]
    public class SewingBlockingPlanReportController : BaseMonitoringController<SewingBlockingPlanReportViewModel, ISewingBlockingPlanReportFacade>
    {
        private readonly static string apiVersion = "1.0";

        public SewingBlockingPlanReportController(IIdentityService identityService, ISewingBlockingPlanReportFacade facade) : base(identityService, facade, apiVersion)
        {
        }
    }
}
