using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.ProductionOrder;
using Com.Ambassador.Service.Sales.Lib.Models.ProductionOrder;
using Com.Ambassador.Service.Sales.Lib.PDFTemplates;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.ViewModels.ProductionOrder;
using Com.Ambassador.Service.Sales.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.FinishingPrintingCostCalculation;
using Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrintingCostCalculation;

namespace Com.Ambassador.Service.Sales.WebApi.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/sales/production-orders/shin")]
    [Authorize]
    public class ShinProductionOrderController : BaseController<ProductionOrderModel, ShinProductionOrderViewModel, IShinProductionOrder>
    {
        private readonly IShinProductionOrder _facade;
        private readonly IShinFinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade;
        private readonly IFinishingPrintingCostCalculationService finishingPrintingCostCalculationService;
        private readonly IFinishingPrintingPreSalesContractFacade fpPreSalesContractFacade;
        private readonly static string apiVersion = "1.0";
        public ShinProductionOrderController(IIdentityService identityService, IValidateService validateService, IShinProductionOrder facade, IMapper mapper, IServiceProvider serviceProvider) : base(identityService, validateService, facade, mapper, apiVersion)
        {
            _facade = facade;
            finishingPrintingSalesContractFacade = serviceProvider.GetService<IShinFinishingPrintingSalesContractFacade>();
            finishingPrintingCostCalculationService = serviceProvider.GetService<IFinishingPrintingCostCalculationService>();
            fpPreSalesContractFacade = serviceProvider.GetService<IFinishingPrintingPreSalesContractFacade>();
        }

            //[HttpGet("filterBySalesContract/{salesContractNo}")]
            //public virtual async Task<IActionResult> ReadBySalesContractNo([FromRoute] string salesContractNo)
            //{
            //    if (!ModelState.IsValid)
            //    {
            //        return BadRequest(ModelState);
            //    }

            //    try
            //    {
            //        List<ProductionOrderModel> model = Facade.ReadBySalesContractNo(salesContractNo);
            //        List<ShinProductionOrderViewModel> viewModel = Mapper.Map<List<ShinProductionOrderViewModel>>(model);
            //        Dictionary<string, object> Result =
            //            new ResultFormatter(ApiVersion, Common.OK_STATUS_CODE, Common.OK_MESSAGE)
            //            .Ok(Mapper, viewModel, 1, viewModel.Count, viewModel.Count, viewModel.Count, new Dictionary<string, string>(), new List<string>());
            //        return Ok(Result);
            //    }
            //    catch (Exception e)
            //    {
            //        Dictionary<string, object> Result =
            //            new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
            //            .Fail();
            //        return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            //    }
            //}

        public override async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                ProductionOrderModel model = await Facade.ReadByIdAsync(id);

                if (model == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.NOT_FOUND_STATUS_CODE, Common.NOT_FOUND_MESSAGE)
                        .Fail();
                    return NotFound(Result);
                }
                else
                {
                    ShinProductionOrderViewModel viewModel = Mapper.Map<ShinProductionOrderViewModel>(model);
                    var fpSCModel = await finishingPrintingSalesContractFacade.ReadParent(viewModel.FinishingPrintingSalesContract.Id);
                    if(fpSCModel != null)
                    {
                        var fpSCVM = Mapper.Map<ShinFinishingPrintingSalesContractViewModel>(fpSCModel);
                        //var fpCCModel = await finishingPrintingCostCalculationService.ReadParent(fpSCVM.CostCalculation.Id);

                        var preSalesContractModel = await fpPreSalesContractFacade.ReadByIdAsync((int)fpSCVM.PreSalesContract.Id);
                        if (preSalesContractModel != null)
                        {
                            fpSCVM.PreSalesContract = Mapper.Map<FinishingPrintingPreSalesContractViewModel>(preSalesContractModel);

                            viewModel.FinishingPrintingSalesContract = fpSCVM;
                        }
                        //if (fpCCModel != null)
                        //{
                        //    var fpCCVM = Mapper.Map<FinishingPrintingCostCalculationViewModel>(fpCCModel);
                        //    var preSalesContractModel = await fpPreSalesContractFacade.ReadByIdAsync((int)fpCCVM.PreSalesContract.Id);
                        //    fpCCVM.PreSalesContract = Mapper.Map<FinishingPrintingPreSalesContractViewModel>(preSalesContractModel);
                        //    fpSCVM.CostCalculation = fpCCVM;

                        //    viewModel.FinishingPrintingSalesContract = fpSCVM;
                        //}
                       
                    }
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.OK_STATUS_CODE, Common.OK_MESSAGE)
                        .Ok<ShinProductionOrderViewModel>(viewModel);
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

        [HttpPut("update-requested-true")]
        public async Task<IActionResult> PutRequestedTrue([FromBody] List<int> ids)
        {

            try
            {
                IdentityService.Username = User.Claims.ToArray().SingleOrDefault(p => p.Type.Equals("username")).Value;
                //ValidateViewModel(viewModel);

                if (ids == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.BAD_REQUEST_STATUS_CODE, Common.BAD_REQUEST_MESSAGE)
                        .Fail();
                    return BadRequest(Result);
                }
                //TModel model = Mapper.Map<TModel>(viewModel);
                await _facade.UpdateRequestedTrue(ids);

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

        [HttpPut("update-requested-false")]
        public async Task<IActionResult> PutRequestedFalse([FromBody] List<int> ids)
        {
            try
            {
                IdentityService.Username = User.Claims.ToArray().SingleOrDefault(p => p.Type.Equals("username")).Value;
                //ValidateViewModel(viewModel);



                if (ids == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.BAD_REQUEST_STATUS_CODE, Common.BAD_REQUEST_MESSAGE)
                        .Fail();
                    return BadRequest(Result);
                }
                //TModel model = Mapper.Map<TModel>(viewModel);
                await _facade.UpdateRequestedFalse(ids);

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

        public class SppParams
        {
            public string context { get; set; }
            public string id { get; set; }
            public double distributedQuantity { get; set; }
        }

        [HttpPut("update-iscompleted-true")]
        public async Task<IActionResult> PutIsCompletedTrue([FromBody] int id)
        {

            try
            {
                IdentityService.Username = User.Claims.ToArray().SingleOrDefault(p => p.Type.Equals("username")).Value;
                //ValidateViewModel(viewModel);



                if (id == 0)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.BAD_REQUEST_STATUS_CODE, Common.BAD_REQUEST_MESSAGE)
                        .Fail();
                    return BadRequest(Result);
                }
                //TModel model = Mapper.Map<TModel>(viewModel);
                await _facade.UpdateIsCompletedTrue(id);

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

        [HttpPut("update-iscompleted-false")]
        public async Task<IActionResult> PutIsCompletedFalse([FromBody] int id)
        {

            try
            {
                IdentityService.Username = User.Claims.ToArray().SingleOrDefault(p => p.Type.Equals("username")).Value;
                //ValidateViewModel(viewModel);



                if (id == 0)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.BAD_REQUEST_STATUS_CODE, Common.BAD_REQUEST_MESSAGE)
                        .Fail();
                    return BadRequest(Result);
                }
                //TModel model = Mapper.Map<TModel>(viewModel);
                await _facade.UpdateIsCompletedFalse(id);

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

        [HttpPut("update-distributed-quantity")]
        public async Task<IActionResult> PutDistributedQuantity([FromBody] List<SppParams> data)
        {

            try
            {
                IdentityService.Username = User.Claims.ToArray().SingleOrDefault(p => p.Type.Equals("username")).Value;
                //ValidateViewModel(viewModel);



                if (data == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.BAD_REQUEST_STATUS_CODE, Common.BAD_REQUEST_MESSAGE)
                        .Fail();
                    return BadRequest(Result);
                }
                //TModel model = Mapper.Map<TModel>(viewModel);
                List<int> id = new List<int>();
                List<double> distributedQuantity = new List<double>();
                foreach (var item in data)
                {
                    id.Add(int.Parse(item.id));
                    distributedQuantity.Add((double)item.distributedQuantity);
                }

                await _facade.UpdateDistributedQuantity(id, distributedQuantity);

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

        [HttpGet("pdf/{Id}")]
        public async Task<IActionResult> GetPDF([FromRoute] int Id)
        {
            try
            {
                var indexAcceptPdf = Request.Headers["Accept"].ToList().IndexOf("application/pdf");
                int timeoffsset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                ProductionOrderModel model = await Facade.ReadByIdAsync(Id);

                if (model == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.NOT_FOUND_STATUS_CODE, Common.NOT_FOUND_MESSAGE)
                        .Fail();
                    return NotFound(Result);
                }
                else
                {
                    ShinProductionOrderViewModel viewModel = Mapper.Map<ShinProductionOrderViewModel>(model);
                    var fpSCModel = await finishingPrintingSalesContractFacade.ReadParent(viewModel.FinishingPrintingSalesContract.Id);
                    if (fpSCModel != null)
                    {
                        var fpSCVM = Mapper.Map<ShinFinishingPrintingSalesContractViewModel>(fpSCModel);
                        //var fpCCModel = await finishingPrintingCostCalculationService.ReadParent(fpSCVM.CostCalculation.Id);
                        var preSalesContractModel = await fpPreSalesContractFacade.ReadByIdAsync((int)fpSCVM.PreSalesContract.Id);

                        if(preSalesContractModel != null)
                        {
                            fpSCVM.PreSalesContract = Mapper.Map<FinishingPrintingPreSalesContractViewModel>(preSalesContractModel);

                            viewModel.FinishingPrintingSalesContract = fpSCVM;
                        }
                        //if (fpCCModel != null)
                        //{
                        //    var fpCCVM = Mapper.Map<FinishingPrintingCostCalculationViewModel>(fpCCModel);
                        //    var preSalesContractModel = await fpPreSalesContractFacade.ReadByIdAsync((int)fpCCVM.PreSalesContract.Id);
                        //    fpCCVM.PreSalesContract = Mapper.Map<FinishingPrintingPreSalesContractViewModel>(preSalesContractModel);
                        //    fpSCVM.CostCalculation = fpCCVM;

                        //    viewModel.FinishingPrintingSalesContract = fpSCVM;
                        //}

                    }

                    ShinProductionOrderPdfTemplate PdfTemplate = new ShinProductionOrderPdfTemplate();
                    MemoryStream stream = PdfTemplate.GeneratePdfTemplate(viewModel, timeoffsset);
                    return new FileStreamResult(stream, "application/pdf")
                    {
                        FileDownloadName = "Production Order" + viewModel.ProductionOrderNo + ".pdf"
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

        [HttpGet("by-year-and-order-type")]
        public IActionResult GetMonthlySummaryByYearAndOrderType([FromQuery] int year = 0, [FromQuery] int orderTypeId = 0)
        {
            if (year == 0)
            {
                year = DateTime.Now.Year;
            }

            try
            {
                var indexAcceptPdf = Request.Headers["Accept"].ToList().IndexOf("application/pdf");
                int timeoffsset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);

                var result = Facade.GetMonthlyOrderQuantityByYearAndOrderType(year, orderTypeId, timeoffsset);

                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.OK_STATUS_CODE, Common.OK_MESSAGE)
                    .Ok(result);
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

        [HttpGet("monthly-by-order-type")]
        public IActionResult GetMonthlyOrderIdsByOrderTypeId([FromQuery] int year = 0, [FromQuery] int month = 0, [FromQuery] int orderTypeId = 0)
        {
            if (year == 0)
            {
                year = DateTime.UtcNow.Year;
            }

            if (month == 0)
            {
                month = DateTime.UtcNow.Month;
            }

            try
            {
                var indexAcceptPdf = Request.Headers["Accept"].ToList().IndexOf("application/pdf");
                int timeoffsset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);

                var result = Facade.GetMonthlyOrderIdsByOrderType(year, month, orderTypeId, timeoffsset);

                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.OK_STATUS_CODE, Common.OK_MESSAGE)
                    .Ok(result);
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

        [HttpPut("update-iscalculated/{id}")]
        public async Task<IActionResult> PutIsCalculated([FromRoute] int id, [FromBody] bool flag)
        {
            try
            {
                IdentityService.Username = User.Claims.ToArray().SingleOrDefault(p => p.Type.Equals("username")).Value;

                if (id == 0)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.BAD_REQUEST_STATUS_CODE, Common.BAD_REQUEST_MESSAGE)
                        .Fail();
                    return BadRequest(Result);
                }
                await _facade.UpdateIsCalculated(id, flag);

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

        [HttpPost("approve/md/{id}")]
        public async Task<IActionResult> ApproveByMD([FromRoute] long id)
        {
            try
            {
                ValidateUser();

                var result = await Facade.ApproveByMD(id);

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

        [HttpPost("approve/sample/{id}")]
        public async Task<IActionResult> ApproveBySample([FromRoute] long id)
        {
            try
            {
                ValidateUser();

                var result = await Facade.ApproveBySample(id);

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
    }
}
