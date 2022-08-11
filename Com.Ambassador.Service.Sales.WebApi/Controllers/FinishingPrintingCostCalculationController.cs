using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.FinishingPrintingCostCalculation;
using Com.Ambassador.Service.Sales.Lib.Models.FinishingPrintingCostCalculation;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrintingCostCalculation;
using Com.Ambassador.Service.Sales.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Ambassador.Service.Sales.WebApi.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/sales/finishing-printing-cost-calculations")]
    [Authorize]
    public class FinishingPrintingCostCalculationController : BaseController<FinishingPrintingCostCalculationModel, FinishingPrintingCostCalculationViewModel, IFinishingPrintingCostCalculationService>
    {
        private readonly static string apiVersion = "1.0";
        private readonly IFinishingPrintingPreSalesContractFacade fpPreSalesContractFacade;
        public FinishingPrintingCostCalculationController(IIdentityService identityService, IValidateService validateService, IFinishingPrintingCostCalculationService facade, IMapper mapper, IServiceProvider serviceProvider) : base(identityService, validateService, facade, mapper, apiVersion)
        {
            fpPreSalesContractFacade = serviceProvider.GetService<IFinishingPrintingPreSalesContractFacade>();
        }

        public override async Task<IActionResult> GetById([FromRoute] int id)
        {
            
            try
            {
                FinishingPrintingCostCalculationModel model = await Facade.ReadByIdAsync(id);

                if (model == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.NOT_FOUND_STATUS_CODE, Common.NOT_FOUND_MESSAGE)
                        .Fail();
                    return NotFound(Result);
                }
                else
                {
                    FinishingPrintingCostCalculationViewModel viewModel = Mapper.Map<FinishingPrintingCostCalculationViewModel>(model);
                    var preSalesContractModel = await fpPreSalesContractFacade.ReadByIdAsync((int)viewModel.PreSalesContract.Id);
                    viewModel.Instruction.Steps = viewModel.Machines.Select(x => x.Step).ToList();
                    viewModel.PreSalesContract = Mapper.Map<FinishingPrintingPreSalesContractViewModel>(preSalesContractModel);
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.OK_STATUS_CODE, Common.OK_MESSAGE)
                        .Ok<FinishingPrintingCostCalculationViewModel>(viewModel);
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

        public override IActionResult Get(int page = 1, int size = 25, string order = "{}", [Bind(Prefix = "Select[]")]List<string> select = null, string keyword = null, string filter = "{}")
        {
            try
            {
                ValidateUser();

                ReadResponse<FinishingPrintingCostCalculationModel> read = Facade.Read(page, size, order, select, keyword, filter);

                List<FinishingPrintingCostCalculationViewModel> DataVM = Mapper.Map<List<FinishingPrintingCostCalculationViewModel>>(read.Data);

                foreach(var vm in DataVM)
                {
                    var preSalesContractModel = fpPreSalesContractFacade.ReadByIdAsync((int)vm.PreSalesContract.Id).Result;
                    vm.PreSalesContract = Mapper.Map<FinishingPrintingPreSalesContractViewModel>(preSalesContractModel);
                }

                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.OK_STATUS_CODE, Common.OK_MESSAGE)
                    .Ok<FinishingPrintingCostCalculationViewModel>(Mapper, DataVM, page, size, read.Count, DataVM.Count, read.Order, read.Selected);
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
        [HttpPut("post")]
        public async Task<IActionResult> CCPost([FromBody]List<long> listId)
        {
            try
            {
                ValidateUser();
                int result = await Facade.CCPost(listId);

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

        [HttpPost("approve/md/{id}")]
        public async Task<IActionResult> CCApproveByMD([FromRoute] long id)
        {
            try
            {
                ValidateUser();

                var result = await Facade.CCApproveMD(id);

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

        [HttpPost("approve/ppic/{id}")]
        public async Task<IActionResult> CCApproveByPPIC([FromRoute] long id)
        {
            try
            {
                ValidateUser();

                var result = await Facade.CCApprovePPIC(id);

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

        [HttpGet("by-presalescontract/{preSalesContractId}")]
        public IActionResult GetByPreSalesContract(long preSalesContractId)
        {
            try
            {
                ValidateUser();

                ReadResponse<FinishingPrintingCostCalculationModel> read = Facade.GetByPreSalesContract(preSalesContractId);

                List<FinishingPrintingCostCalculationViewModel> DataVM = Mapper.Map<List<FinishingPrintingCostCalculationViewModel>>(read.Data);

                foreach (var vm in DataVM)
                {
                    var preSalesContractModel = fpPreSalesContractFacade.ReadByIdAsync((int)vm.PreSalesContract.Id).Result;
                    vm.PreSalesContract = Mapper.Map<FinishingPrintingPreSalesContractViewModel>(preSalesContractModel);
                }

                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.OK_STATUS_CODE, Common.OK_MESSAGE)
                    .Ok<FinishingPrintingCostCalculationViewModel>(Mapper, DataVM, 1, read.Count, read.Count, DataVM.Count, read.Order, read.Selected);
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
