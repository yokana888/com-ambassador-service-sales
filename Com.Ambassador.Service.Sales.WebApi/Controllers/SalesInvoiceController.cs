using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.SalesInvoice;
using Com.Ambassador.Service.Sales.Lib.Models.SalesInvoice;
using Com.Ambassador.Service.Sales.Lib.PDFTemplates;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.ViewModels.SalesInvoice;
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
    [Route("v{version:apiVersion}/sales/sales-invoices")]
    [Authorize]

    public class SalesInvoiceController : BaseController<SalesInvoiceModel, SalesInvoiceViewModel, ISalesInvoiceContract>
    {
        private readonly ISalesInvoiceContract _facade;
        private readonly static string apiVersion = "1.0";
        public SalesInvoiceController(IIdentityService identityService, IValidateService validateService, ISalesInvoiceContract salesInvoiceFacade, IMapper mapper, IServiceProvider serviceProvider) : base(identityService, validateService, salesInvoiceFacade, mapper, apiVersion)
        {
            _facade = salesInvoiceFacade;
        }

        [HttpGet("filter-by-buyer/{buyerId}")]
        public virtual IActionResult ReadByBuyerId([FromRoute] int buyerId)
        {
            try
            {
                List<SalesInvoiceModel> model = Facade.ReadByBuyerId(buyerId);
                List<SalesInvoiceViewModel> viewModel = Mapper.Map<List<SalesInvoiceViewModel>>(model);
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.OK_STATUS_CODE, Common.OK_MESSAGE)
                    .Ok(Mapper, viewModel, 100, viewModel.Count, viewModel.Count, viewModel.Count, new Dictionary<string, string>(), new List<string>());
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

        [HttpPut("update-from-sales-receipt/{id}")]
        public async Task<IActionResult> UpdateFromSalesReceiptAsync([FromRoute] int id, [FromBody] SalesInvoiceUpdateModel model)
        {
            try
            {
                ValidateUser();

                await Facade.UpdateFromSalesReceiptAsync(id, model);
                return NoContent();
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        [HttpGet("delivery-order-pdf/{Id}")]
        public async Task<IActionResult> GetDeliveryOrderPDF([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var indexAcceptPdf = Request.Headers["Accept"].ToList().IndexOf("application/pdf");
                int timeoffsset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                SalesInvoiceModel model = await Facade.ReadByIdAsync(Id);

                if (model == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.NOT_FOUND_STATUS_CODE, Common.NOT_FOUND_MESSAGE)
                        .Fail();
                    return NotFound(Result);
                }
                else
                {
                    SalesInvoiceViewModel viewModel = Mapper.Map<SalesInvoiceViewModel>(model);

                    DeliveryOrderPdfTemplate PdfTemplate = new DeliveryOrderPdfTemplate();
                    MemoryStream stream = PdfTemplate.GeneratePdfTemplate(viewModel, timeoffsset);
                    return new FileStreamResult(stream, "application/pdf")
                    {
                        FileDownloadName = "Surat_Jalan/" + viewModel.DeliveryOrderNo + ".pdf"
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

        [HttpGet("sales-invoice-valas-pdf/{Id}")]
        public async Task<IActionResult> GetSalesInvoiceValasPDF([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var indexAcceptPdf = Request.Headers["Accept"].ToList().IndexOf("application/pdf");
                int timeoffsset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                SalesInvoiceModel model = await Facade.ReadByIdAsync(Id);

                if (model == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.NOT_FOUND_STATUS_CODE, Common.NOT_FOUND_MESSAGE)
                        .Fail();
                    return NotFound(Result);
                }
                else
                {
                    SalesInvoiceViewModel viewModel = Mapper.Map<SalesInvoiceViewModel>(model);

                    SalesInvoiceValasPdfTemplate PdfTemplate = new SalesInvoiceValasPdfTemplate();
                    MemoryStream stream = PdfTemplate.GeneratePdfTemplate(viewModel, timeoffsset);
                    return new FileStreamResult(stream, "application/pdf")
                    {
                        FileDownloadName = "Faktur_Penjualan_Valas/" + viewModel.SalesInvoiceNo + ".pdf"
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

        [HttpGet("sales-invoice-idr-pdf/{Id}")]
        public async Task<IActionResult> GetSalesInvoiceIDRPDF([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var indexAcceptPdf = Request.Headers["Accept"].ToList().IndexOf("application/pdf");
                int timeoffsset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                SalesInvoiceModel model = await Facade.ReadByIdAsync(Id);

                if (model == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.NOT_FOUND_STATUS_CODE, Common.NOT_FOUND_MESSAGE)
                        .Fail();
                    return NotFound(Result);
                }
                else
                {
                    SalesInvoiceViewModel viewModel = Mapper.Map<SalesInvoiceViewModel>(model);

                    SalesInvoiceIDRPdfTemplate PdfTemplate = new SalesInvoiceIDRPdfTemplate();
                    MemoryStream stream = PdfTemplate.GeneratePdfTemplate(viewModel, timeoffsset);
                    return new FileStreamResult(stream, "application/pdf")
                    {
                        FileDownloadName = "Faktur_Penjualan_IDR/" + viewModel.SalesInvoiceNo + ".pdf"
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
        [HttpGet("reports")]
        public async Task<IActionResult> GetReportAll(int buyerId, int salesInvoiceId, bool? isPaidOff, DateTimeOffset? dateFrom, DateTimeOffset? dateTo, [FromHeader(Name = "x-timezone-offset")] string timezone)
        {
            int offset = Convert.ToInt32(timezone);

            try
            {
                ValidateUser();
                var data = await _facade.GetReport(buyerId, salesInvoiceId, isPaidOff, dateFrom, dateTo, offset);

                return Ok(new
                {
                    apiVersion = ApiVersion,
                    data,
                    message = Common.OK_MESSAGE,
                    statusCode = Common.OK_STATUS_CODE
                });
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        [HttpGet("reports/xls")]
        public async Task<IActionResult> GetXlsAll(int buyerId, int salesInvoiceId, bool? isPaidOff, DateTimeOffset? dateFrom, DateTimeOffset? dateTo, [FromHeader(Name = "x-timezone-offset")] string timezone)
        {

            try
            {
                ValidateUser();
                byte[] xlsInBytes;
                int offset = Convert.ToInt32(timezone);

                var xls = await _facade.GenerateExcel(buyerId, salesInvoiceId, isPaidOff, dateFrom, dateTo, offset);

                string filename = "Laporan Bukti Pembayaran Faktur.xlsx";

                xlsInBytes = xls.ToArray();
                var file = File(xlsInBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
                return file;

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
