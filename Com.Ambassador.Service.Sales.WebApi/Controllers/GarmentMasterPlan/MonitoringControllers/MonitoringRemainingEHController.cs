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
    [Route("v{version:apiVersion}/garment-master-plan/monitoring-remaining-eh")]
    [Authorize]
    public class MonitoringRemainingEHController : BaseMonitoringController<MonitoringRemainingEHViewModel, IMonitoringRemainingEHFacade>
    {
        private readonly static string apiVersion = "1.0";

        public MonitoringRemainingEHController(IIdentityService identityService, IMonitoringRemainingEHFacade facade) : base(identityService, facade, apiVersion)
        {
        }
    }
}
