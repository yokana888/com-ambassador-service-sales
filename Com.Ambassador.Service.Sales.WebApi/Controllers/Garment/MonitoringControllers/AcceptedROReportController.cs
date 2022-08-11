using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.Garment;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.ViewModels.Garment;
using Com.Ambassador.Service.Sales.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Com.Ambassador.Service.Sales.WebApi.Controllers.Garment.MonitoringControllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/report/accepted-ros")]
    [Authorize]
    public class AcceptedROReportController : BaseMonitoringController<AcceptedROReportViewModel, IAcceptedROReportFacade>
    {
        private readonly static string apiVersion = "1.0";

        public AcceptedROReportController(IIdentityService identityService, IAcceptedROReportFacade facade) : base(identityService, facade, apiVersion)
        {
        }
    }
}
