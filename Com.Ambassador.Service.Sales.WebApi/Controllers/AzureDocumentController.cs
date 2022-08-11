using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface;
using Com.Ambassador.Service.Sales.Lib.Services;
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
    [Route("v{version:apiVersion}/azure-documents")]
    [Authorize]
    public class AzureDocumentController : Controller
    {
        private IIdentityService IdentityService;
        private readonly static string apiVersion = "1.0";
        private readonly IAzureDocumentFacade azureDocumentFacade;

        public AzureDocumentController(IIdentityService identityService, IAzureDocumentFacade azureDocumentFacade)
        {
            IdentityService = identityService;
            this.azureDocumentFacade = azureDocumentFacade;
        }

        [HttpGet("{*path}")]
        public async Task<IActionResult> DownloadDocument([FromRoute]string path, [FromQuery]string fileName)
        {
            try
            {
                var result = await azureDocumentFacade.DownloadDocument(path);
                FileStreamResult fileStreamResult = new FileStreamResult(result.File, result.FileType)
                {
                    FileDownloadName = fileName ?? result.FileName
                };

                return fileStreamResult;
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(apiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }
    }
}
