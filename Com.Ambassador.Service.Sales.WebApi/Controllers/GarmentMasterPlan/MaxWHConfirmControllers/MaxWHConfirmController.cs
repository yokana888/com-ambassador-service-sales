using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.GarmentMasterPlan.MaxWHConfirmInterfaces;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentMasterPlan.MaxWHConfirmModel;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentMasterPlan.MaxWHConfirmViewModels;
using Com.Ambassador.Service.Sales.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Ambassador.Service.Sales.WebApi.Controllers.GarmentMasterPlan.MaxWHConfirmControllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/max-wh-confirms")]
    [Authorize]
    public class MaxWHConfirmController : BaseController<MaxWHConfirm, MaxWHConfirmViewModel, IMaxWHConfirmFacade>
    {
        private readonly static string apiVersion = "1.0";
        private readonly IIdentityService Service;
        public MaxWHConfirmController(IIdentityService identityService, IValidateService validateService, IMaxWHConfirmFacade facade, IMapper mapper, IServiceProvider serviceProvider) : base(identityService, validateService, facade, mapper, apiVersion)
        {
            Service = identityService;
        }
    }
}
