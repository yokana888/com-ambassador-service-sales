using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.GarmentSalesContractInterface;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentSalesContractModel;
using Com.Ambassador.Service.Sales.Lib.PDFTemplates;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentSalesContractViewModels;
using Com.Ambassador.Service.Sales.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Ambassador.Service.Sales.WebApi.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/merchandiser/garment-sales-contracts")]
    [Authorize]
    public class GarmentSalesContractController : BaseController<GarmentSalesContract, GarmentSalesContractViewModel, IGarmentSalesContract>
    {
        private readonly IHttpClientService HttpClientService;
        private readonly static string apiVersion = "1.0";
        public GarmentSalesContractController(IIdentityService identityService, IValidateService validateService, IGarmentSalesContract facade, IMapper mapper, IServiceProvider serviceProvider) : base(identityService, validateService, facade, mapper, apiVersion)
        {
            HttpClientService = serviceProvider.GetService<IHttpClientService>();
        }

        [HttpGet("pdf/{Id}")]
        public async Task<IActionResult> GetPDF([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var indexAcceptPdf = Request.Headers["Accept"].ToList().IndexOf("application/pdf");
                int timeoffsset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                GarmentSalesContract model = await Facade.ReadByIdAsync(Id);

                if (model == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.NOT_FOUND_STATUS_CODE, Common.NOT_FOUND_MESSAGE)
                        .Fail();
                    return NotFound(Result);
                }
                else
                {
                    GarmentSalesContractViewModel viewModel = Mapper.Map<GarmentSalesContractViewModel>(model);


                    string BuyerUri = "master/garment-buyers";
                    string BuyerBrandUri = "master/garment-buyer-brands";
                    string BankUri = "master/account-banks";

                   
                    /* Get BuyerBrand */
                    var response = HttpClientService.GetAsync($@"{APIEndpoint.Core}{BuyerBrandUri}/" + viewModel.BuyerBrandId).Result.Content.ReadAsStringAsync();
                    Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.Result);
                    var json = result.Single(p => p.Key.Equals("data")).Value;
                    Dictionary<string, object> buyerBrand = JsonConvert.DeserializeObject<Dictionary<string, object>>(json.ToString());

                    Dictionary<string, object> buyers = JsonConvert.DeserializeObject<Dictionary<string, object>>(buyerBrand["Buyers"].ToString());


                    /* Get Buyer */
                    var responseBuyer = HttpClientService.GetAsync($@"{APIEndpoint.Core}{BuyerUri}/" + buyers["Id"]).Result.Content.ReadAsStringAsync();
                    Dictionary<string, object> resultBuyer = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseBuyer.Result);
                    var jsonBuyer = resultBuyer.Single(p => p.Key.Equals("data")).Value;
                    Dictionary<string, object> buyer = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonBuyer.ToString());

                    /* Get AccountBank */
                    var responseBank = HttpClientService.GetAsync($@"{APIEndpoint.Core}{BankUri}/" + viewModel.AccountBank.Id).Result.Content.ReadAsStringAsync();
                    Dictionary<string, object> resultBank = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseBank.Result);
                    var jsonBank = resultBank.Single(p => p.Key.Equals("data")).Value;
                    Dictionary<string, object> bank = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonBank.ToString());

                    string buyerType = buyer["Type"] != null ? buyer["Type"].ToString() : "";
                    if (viewModel.SCType != "Ekspor")
                    {
                        GarmentSalesContractLocalPDFTemplate PdfTemplate = new GarmentSalesContractLocalPDFTemplate();
                        MemoryStream stream = PdfTemplate.GeneratePdfTemplate(viewModel, Facade, timeoffsset, buyer, bank);
                        // model.DocPrinted = true;
                        // await Facade.UpdatePrinted(Id, model);
                        return new FileStreamResult(stream, "application/pdf")
                        {
                            FileDownloadName = "Sales Contract" + viewModel.SalesContractNo + ".pdf"
                        };
                    }
                    else
                    {
                        GarmentSalesContractPDFTemplate PdfTemplate = new GarmentSalesContractPDFTemplate();
                        MemoryStream stream = PdfTemplate.GeneratePdfTemplate(viewModel, Facade, timeoffsset, buyer, bank);
                        // model.DocPrinted = true;
                        // await Facade.UpdatePrinted(Id, model);
                        return new FileStreamResult(stream, "application/pdf")
                        {
                            FileDownloadName = "Sales Contract" + viewModel.SalesContractNo + ".pdf"
                        };
                    }
                    
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
