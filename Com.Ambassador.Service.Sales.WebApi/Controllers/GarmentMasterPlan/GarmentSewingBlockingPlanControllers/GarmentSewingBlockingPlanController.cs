using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.GarmentMasterPlan.GarmentSewingBlockingPlanInterfaces;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentSewingBlockingPlanModel;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentSewingBlockingPlanViewModels;
using Com.Ambassador.Service.Sales.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Ambassador.Service.Sales.WebApi.Controllers.GarmentMasterPlan.GarmentSewingBlockingPlanControllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/sewing-blocking-plans")]
    [Authorize]
    public class GarmentSewingBlockingPlanController : BaseController<GarmentSewingBlockingPlan, GarmentSewingBlockingPlanViewModel, IGarmentSewingBlockingPlan>
    {
        private readonly static string apiVersion = "1.0";

        public GarmentSewingBlockingPlanController(IIdentityService identityService, IValidateService validateService, IGarmentSewingBlockingPlan facade, IMapper mapper, IServiceProvider serviceProvider) : base(identityService, validateService, facade, mapper, apiVersion)
        {
        }
    }
}
