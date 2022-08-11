using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.GarmentBookingOrderFacade;
using Com.Ambassador.Service.Sales.WebApi.Helpers;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.GarmentBookingOrderInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.WebApi.Utilities;

namespace Com.Ambassador.Service.Sales.WebApi.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/sales/garment-booking-orders-monitoring")]
    [Authorize]
    public class GarmentBookingOrderMonitoringController : Controller
    {
        private readonly static string apiVersion = "1.0";
        private readonly IGarmentBookingOrderMonitoringInterface facades;
        private readonly IIdentityService Service;
        public GarmentBookingOrderMonitoringController(IIdentityService identityService, IGarmentBookingOrderMonitoringInterface facade)
        {
            Service = identityService;
            facades = facade;
        }

        [HttpGet]
        public IActionResult GetReportAll(string section, string no, string buyerCode, string comodityCode, string statusConfirm, string statusBookingOrder, DateTime? dateFrom, DateTime? dateTo, DateTime? dateDeliveryFrom, DateTime? dateDeliveryTo, int page, int size, string Order = "{}")
        {
            int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
            string accept = Request.Headers["Accept"];

            try
            {

                var data = facades.Read(section, no, buyerCode, comodityCode, statusConfirm, statusBookingOrder, dateFrom, dateTo,  dateDeliveryFrom, dateDeliveryTo, page, size, Order, offset);

                return Ok(new
                {
                    apiVersion = apiVersion,
                    data = data.Item1,
                    info = new { total = data.Item2 },
                    message = Common.OK_MESSAGE,
                    statusCode = Common.OK_STATUS_CODE
                });
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new Utilities.ResultFormatter(apiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        [HttpGet("download")]
        public IActionResult GetXlsAll(string section, string no, string buyerCode, string comodityCode, string statusConfirm, string statusBookingOrder, DateTime? dateFrom, DateTime? dateTo, DateTime? dateDeliveryFrom, DateTime? dateDeliveryTo)
        {

            try
            {
                byte[] xlsInBytes;
                int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : Convert.ToDateTime(dateFrom);
                DateTime DateTo = dateTo == null ? DateTime.Now : Convert.ToDateTime(dateTo);
                DateTime DateDeliveryFrom = dateDeliveryFrom == null ? new DateTime(1970, 1, 1) : Convert.ToDateTime(dateDeliveryFrom);
                DateTime DateDeliveryTo = dateDeliveryTo == null ? DateTime.MaxValue : Convert.ToDateTime(dateDeliveryTo);

                var xls = facades.GenerateExcel(section, no, buyerCode, comodityCode, statusConfirm, statusBookingOrder, dateFrom, dateTo, dateDeliveryFrom, dateDeliveryTo, offset);

                string filename = String.Format("Laporan Booking Order - {0}.xlsx", DateTime.UtcNow.ToString("ddMMyyyy"));

                xlsInBytes = xls.ToArray();
                var file = File(xlsInBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
                return file;

            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new Utilities.ResultFormatter(apiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }
    }
}
