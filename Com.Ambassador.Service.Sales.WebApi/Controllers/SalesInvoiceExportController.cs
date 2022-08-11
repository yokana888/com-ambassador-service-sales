using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.SalesInvoiceExport;
using Com.Ambassador.Service.Sales.Lib.Models.SalesInvoiceExport;
using Com.Ambassador.Service.Sales.Lib.PDFTemplates;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.ViewModels.SalesInvoiceExport;
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
    [Route("v{version:apiVersion}/sales/sales-invoices-export")]
    [Authorize]

    public class SalesInvoiceExportController : BaseController<SalesInvoiceExportModel, SalesInvoiceExportViewModel, ISalesInvoiceExportContract>
    {
        private readonly ISalesInvoiceExportContract _facade;
        private readonly static string apiVersion = "1.0";
        public SalesInvoiceExportController(IIdentityService identityService, IValidateService validateService, ISalesInvoiceExportContract salesInvoiceExportFacade, IMapper mapper, IServiceProvider serviceProvider) : base(identityService, validateService, salesInvoiceExportFacade, mapper, apiVersion)
        {
            _facade = salesInvoiceExportFacade;
        }

        [HttpGet("sales-invoice-export-valas-pdf/{Id}")]
        public async Task<IActionResult> GetSalesInvoiceExportValasPDF([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var indexAcceptPdf = Request.Headers["Accept"].ToList().IndexOf("application/pdf");
                int timeoffsset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                SalesInvoiceExportModel model = await Facade.ReadByIdAsync(Id);

                if (model == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.NOT_FOUND_STATUS_CODE, Common.NOT_FOUND_MESSAGE)
                        .Fail();
                    return NotFound(Result);
                }
                else
                {
                    SalesInvoiceExportViewModel viewModel = Mapper.Map<SalesInvoiceExportViewModel>(model);

                    SalesInvoiceExportValasPdfTemplate PdfTemplate = new SalesInvoiceExportValasPdfTemplate();
                    MemoryStream stream = PdfTemplate.GeneratePdfTemplate(viewModel, timeoffsset);
                    return new FileStreamResult(stream, "application/pdf")
                    {
                        FileDownloadName = "Faktur_Penjualan_Export_Valas/" + viewModel.SalesInvoiceNo + ".pdf"
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

        [HttpGet("sales-invoice-export-idr-pdf/{Id}")]
        public async Task<IActionResult> GetSalesInvoiceExportIDRPDF([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var indexAcceptPdf = Request.Headers["Accept"].ToList().IndexOf("application/pdf");
                int timeoffsset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                SalesInvoiceExportModel model = await Facade.ReadByIdAsync(Id);

                if (model == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.NOT_FOUND_STATUS_CODE, Common.NOT_FOUND_MESSAGE)
                        .Fail();
                    return NotFound(Result);
                }
                else
                {
                    SalesInvoiceExportViewModel viewModel = Mapper.Map<SalesInvoiceExportViewModel>(model);

                    SalesInvoiceExportIDRPdfTemplate PdfTemplate = new SalesInvoiceExportIDRPdfTemplate();
                    MemoryStream stream = PdfTemplate.GeneratePdfTemplate(viewModel, timeoffsset);
                    return new FileStreamResult(stream, "application/pdf")
                    {
                        FileDownloadName = "Faktur_Penjualan_Export_IDR/" + viewModel.SalesInvoiceNo + ".pdf"
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
