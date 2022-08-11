using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.Garment;
using Com.Ambassador.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.CostCalculationGarment;
using Com.Ambassador.Service.Sales.Lib.ViewModels.Garment;
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
    [Route("v{version:apiVersion}/merchandiser/budget-validations/ppic")]
    [Authorize]
    public class Garment_BudgetValidationPPICController : Controller
    {
        private readonly string ApiVersion = "1.0.0";
        private IGarment_BudgetValidationPPIC facade;
        protected IIdentityService IdentityService;
        protected readonly IValidateService ValidateService;
        protected readonly IMapper Mapper;

        public Garment_BudgetValidationPPICController(IIdentityService identityService, IValidateService validateService, IGarment_BudgetValidationPPIC facade, IMapper mapper)
        {
            this.facade = facade;
            this.IdentityService = identityService;
            this.ValidateService = validateService;
            this.Mapper = mapper;
        }

        private void ValidateUser()
        {
            IdentityService.Username = User.Claims.ToArray().SingleOrDefault(p => p.Type.Equals("username")).Value;
            IdentityService.Token = Request.Headers["Authorization"].FirstOrDefault().Replace("Bearer ", "");
        }

        private void ValidateViewModel(CostCalculationGarment_RO_Garment_ValidationViewModel viewModel)
        {
            ValidateService.Validate(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CostCalculationGarment_RO_Garment_ValidationViewModel viewModel)
        {
            try
            {
                ValidateUser();
                ValidateViewModel(viewModel);

                var model = Mapper.Map<CostCalculationGarment>(viewModel);

                var productDicts = new Dictionary<long, string>();
                foreach (var material in viewModel.CostCalculationGarment_Materials)
                {
                    if (productDicts.GetValueOrDefault(material.Product.Id) == null)
                    {
                        productDicts.Add(material.Product.Id, material.Product.Name);
                    }
                }

                await facade.ValidateROGarment(model, productDicts);
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.OK_STATUS_CODE, Common.OK_MESSAGE)
                    .Ok();
                return Ok(Result);
            }
            catch (ServiceValidationException e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.BAD_REQUEST_STATUS_CODE, Common.BAD_REQUEST_MESSAGE)
                    .Fail(e);
                return BadRequest(Result);
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