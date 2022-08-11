using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.DOSales;
using Com.Ambassador.Service.Sales.Lib.Models.DOSales;
using Com.Ambassador.Service.Sales.Lib.PDFTemplates;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.DOSales;
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
    [Route("v{version:apiVersion}/sales/do-sales")]
    [Authorize]

    public class DOSalesController : BaseController<DOSalesModel, DOSalesViewModel, IDOSalesContract>
    {
        private readonly IDOSalesContract _facade;
        private readonly static string apiVersion = "1.0";
        public DOSalesController(IIdentityService identityService, IValidateService validateService, IDOSalesContract doSalesFacade, IMapper mapper, IServiceProvider serviceProvider) : base(identityService, validateService, doSalesFacade, mapper, apiVersion)
        {
            _facade = doSalesFacade;
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
                DOSalesModel model = await Facade.ReadByIdAsync(Id);

                if (model == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.NOT_FOUND_STATUS_CODE, Common.NOT_FOUND_MESSAGE)
                        .Fail();
                    return NotFound(Result);
                }
                else
                {
                    DOSalesViewModel viewModel = Mapper.Map<DOSalesViewModel>(model);

                    DOSalesPdfTemplate PdfTemplate = new DOSalesPdfTemplate();
                    MemoryStream stream = PdfTemplate.GeneratePdfTemplate(viewModel, timeoffsset);
                    return new FileStreamResult(stream, "application/pdf")
                    {
                        FileDownloadName = "DO_Sales/" + viewModel.DOSalesNo + ".pdf"
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

        [HttpGet("stock")]
        public virtual IActionResult GetDPAndStock(int page = 1, int size = 25, string order = "{}", [Bind(Prefix = "Select[]")] List<string> select = null, string keyword = null, string filter = "{}")
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            try
            {
                ValidateUser();

                ReadResponse<DOSalesModel> read = Facade.ReadDPAndStock(page, size, order, select, keyword, filter);

                //Tuple<List<TModel>, int, Dictionary<string, string>, List<string>> Data = Facade.Read(page, size, order, select, keyword, filter);
                List<DOSalesViewModel> DataVM = Mapper.Map<List<DOSalesViewModel>>(read.Data);

                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.OK_STATUS_CODE, Common.OK_MESSAGE)
                    .Ok<DOSalesViewModel>(Mapper, DataVM, page, size, read.Count, DataVM.Count, read.Order, read.Selected);
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

        [HttpGet("stock/mobile")]
        public virtual IActionResult GetDPAndStockForMobile(int page = 1, int size = 25, string order = "{}", [Bind(Prefix = "Select[]")] List<string> select = null, string keyword = null, string filter = "{}")
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            try
            {
                ValidateUser();

                ReadResponse<DOSalesModel> read = Facade.ReadDPAndStock(page, size, order, select, keyword, filter);

                //Tuple<List<TModel>, int, Dictionary<string, string>, List<string>> Data = Facade.Read(page, size, order, select, keyword, filter);
                List<DOSalesViewModel> DataVM = Mapper.Map<List<DOSalesViewModel>>(read.Data);

                //Dictionary<string, object> Result =
                //    new ResultFormatter(ApiVersion, Common.OK_STATUS_CODE, Common.OK_MESSAGE)
                //    .Ok<DOSalesViewModel>(Mapper, DataVM, page, size, read.Count, DataVM.Count, read.Order, read.Selected);
                return Ok(DataVM);

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
