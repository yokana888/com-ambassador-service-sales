using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseInterface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Com.Ambassador.Service.Sales.WebApi.Utilities
{
    public abstract class BaseMonitoringController<TViewModel, IFacade> : Controller
        where IFacade : IBaseMonitoringFacade<TViewModel>
    {
        protected IIdentityService IdentityService;
        protected readonly IFacade Facade;
        protected readonly string ApiVersion;

        public BaseMonitoringController(IIdentityService identityService, IFacade facade, string apiVersion)
        {
            IdentityService = identityService;
            Facade = facade;
            ApiVersion = apiVersion;
        }

        [HttpGet]
        public virtual IActionResult Get(int page = 1, int size = 25, string filter = "{}")
        {
            try
            {
                var indexAcceptXls = Request.Headers["Accept"].ToList().IndexOf("application/xls");
                IdentityService.TimezoneOffset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                IdentityService.Username = User.Claims.ToArray().SingleOrDefault(p => p.Type.Equals("username")).Value;
                IdentityService.Token = Request.Headers["Authorization"].FirstOrDefault().Replace("Bearer ", "");

                if (indexAcceptXls > -1)
                {
                    var generatedExcel = Facade.GenerateExcel(filter);

                    return File(generatedExcel.Item1.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", generatedExcel.Item2);
                }
                else
                {
                    Tuple<List<TViewModel>, int> Data = Facade.Read(page, size, filter);

                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.OK_STATUS_CODE, Common.OK_MESSAGE)
                        .Ok(Data.Item1, Data.Item2);
                    return Ok(Result);
                }
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
