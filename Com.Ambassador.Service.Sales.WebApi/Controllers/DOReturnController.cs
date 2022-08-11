using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.DOReturn;
using Com.Ambassador.Service.Sales.Lib.Models.DOReturn;
using Com.Ambassador.Service.Sales.Lib.PDFTemplates;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.ViewModels.DOReturn;
using Com.Ambassador.Service.Sales.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Ambassador.Service.Sales.WebApi.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/sales/do-return")]
    [Authorize]

    public class DOReturnController : BaseController<DOReturnModel, DOReturnViewModel, IDOReturnContract>
    {
        private readonly IDOReturnContract _facade;
        private readonly static string apiVersion = "1.0";
        public DOReturnController(IIdentityService identityService, IValidateService validateService, IDOReturnContract doReturnFacade, IMapper mapper, IServiceProvider serviceProvider) : base(identityService, validateService, doReturnFacade, mapper, apiVersion)
        {
            _facade = doReturnFacade;
        }

        [HttpGet("pdf/{Id}")]
        public async Task<IActionResult> GetDOReturnPDF([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var indexAcceptPdf = Request.Headers["Accept"].ToList().IndexOf("application/pdf");
                int timeoffsset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                DOReturnModel model = await Facade.ReadByIdAsync(Id);

                if (model == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.NOT_FOUND_STATUS_CODE, Common.NOT_FOUND_MESSAGE)
                        .Fail();
                    return NotFound(Result);
                }
                else
                {
                    DOReturnViewModel viewModel = Mapper.Map<DOReturnViewModel>(model);

                    DOReturnPdfTemplate PdfTemplate = new DOReturnPdfTemplate();
                    MemoryStream stream = PdfTemplate.GeneratePdfTemplate(viewModel, timeoffsset);
                    return new FileStreamResult(stream, "application/pdf")
                    {
                        FileDownloadName = "DO_Retur/" + viewModel.DOReturnNo + ".pdf"
                    };
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
