using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.LocalMerchandiserInterfaces;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel.LocalMerchandiserViewModels;
using Com.Ambassador.Service.Sales.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Com.Ambassador.Service.Sales.WebApi.Controllers.LocalMerchandiserControllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/local-merchandiser/horders")]
    [Authorize]
    public class HOrderController : Controller
    {
        private readonly static string ApiVersion = "1.0";
        private readonly IHOrderFacade facade;

        public HOrderController(IHOrderFacade facade)
        {
            this.facade = facade;
        }

        [HttpGet("kode-by-no")]
        public IActionResult ReadKodeByNo(string no = null)
        {
            try
            {
                List<string> data = facade.GetKodeByNo(no);

                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.OK_STATUS_CODE, Common.OK_MESSAGE)
                    .Ok(data);

                return Ok(Result);
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        [HttpGet("data-production-report-by-no/{ro}")]
        public IActionResult GetDataForProductionReportByNo(string ro)
        {
            try
            {
                List<HOrderDataForProductionReportViewModel> data = facade.GetDataForProductionReportByNo(ro);

                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.OK_STATUS_CODE, Common.OK_MESSAGE)
                    .Ok(data);

                return Ok(Result);
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
