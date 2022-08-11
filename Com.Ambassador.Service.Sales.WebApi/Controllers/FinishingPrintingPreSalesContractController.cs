using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrinting;
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
    [Route("v{version:apiVersion}/sales/finishing-printing-pre-sales-contracts")]
    [Authorize]
    public class FinishingPrintingPreSalesContractController : BaseController<FinishingPrintingPreSalesContractModel, FinishingPrintingPreSalesContractViewModel, IFinishingPrintingPreSalesContractFacade>
    {
        private readonly static string apiVersion = "1.0";
        public FinishingPrintingPreSalesContractController(IIdentityService identityService, IValidateService validateService, IFinishingPrintingPreSalesContractFacade facade, IMapper mapper, IServiceProvider serviceProvider) : base(identityService, validateService, facade, mapper, apiVersion)
        {
        }

        [HttpPut("post")]
        public async Task<IActionResult> PreSalesPost([FromBody]List<long> listId)
        {
            try
            {
                ValidateUser();
                int result = await Facade.PreSalesPost(listId);

                return Ok(result);
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        [HttpPut("unpost/{id}")]
        public async Task<IActionResult> PreSalesUnpost([FromRoute]long id)
        {
            try
            {
                ValidateUser();
                int result = await Facade.PreSalesUnpost(id);

                return Ok(result);
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }
    }
}
