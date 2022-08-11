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
    [Route("v{version:apiVersion}/monitoring/pre-sales-contracts")]
    [Authorize]
    public class MonitoringPreSalesContractController : BaseMonitoringController<MonitoringPreSalesContractViewModel, IMonitoringPreSalesContractFacade>
    {
        private readonly static string apiVersion = "1.0";

        public MonitoringPreSalesContractController(IIdentityService identityService, IMonitoringPreSalesContractFacade facade) : base(identityService, facade, apiVersion)
        {
        }
    }
}
