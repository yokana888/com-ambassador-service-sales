using AutoMapper;
using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.WebApi.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace Com.Ambassador.Service.Sales.WebApi.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/sales/delivery-note-production")]
    [Authorize]

    public class DeliveryNoteProductionController : BaseController<DeliveryNoteProductionModel, DeliveryNoteProductionViewModel, IDeliveryNoteProduction>
    {

        private readonly IDeliveryNoteProduction _facade;
        private readonly static string apiVersion = "1.0";
        public DeliveryNoteProductionController(IIdentityService identityService, IValidateService validateService, IDeliveryNoteProduction deliveryNoteProductionFacade, IMapper mapper, IServiceProvider serviceProvider) : base(identityService, validateService, deliveryNoteProductionFacade, mapper, apiVersion)
        {
            _facade = deliveryNoteProductionFacade;
        }

        [HttpGet("pdf/{Id}")]
        public async Task<IActionResult> GetDOSalesPDF([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var indexAcceptPdf = Request.Headers["Accept"].ToList().IndexOf("application/pdf");
                int timeoffsset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                DeliveryNoteProductionModel model = await Facade.ReadByIdAsync(Id);

                if (model == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.NOT_FOUND_STATUS_CODE, Common.NOT_FOUND_MESSAGE)
                        .Fail();
                    return NotFound(Result);
                }
                else
                {
                    DeliveryNoteProductionViewModel viewModel = Mapper.Map<DeliveryNoteProductionViewModel>(model);

                    DeliveryNoteProductionPdfTemplate PdfTemplate = new DeliveryNoteProductionPdfTemplate();
                    MemoryStream stream = PdfTemplate.GeneratePdfTemplate(viewModel, timeoffsset);
                    return new FileStreamResult(stream, "application/pdf")
                    {   
                        FileDownloadName = "SOP " + viewModel.SalesContract.SalesContractNo + ".pdf"
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
