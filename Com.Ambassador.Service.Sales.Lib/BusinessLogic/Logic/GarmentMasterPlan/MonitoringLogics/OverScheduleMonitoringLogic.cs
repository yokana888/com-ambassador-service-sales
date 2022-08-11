using Com.Ambassador.Service.Sales.Lib.Models.GarmentSewingBlockingPlanModel;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentMasterPlan.MonitoringViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.MonitoringLogics
{
    public class OverScheduleMonitoringLogic : BaseMonitoringLogic<OverScheduleMonitoringViewModel>
    {
        private IIdentityService identityService;
        private SalesDbContext dbContext;
        private DbSet<GarmentSewingBlockingPlan> dbSet;

        public OverScheduleMonitoringLogic(IIdentityService identityService, SalesDbContext dbContext)
        {
            this.identityService = identityService;
            this.dbContext = dbContext;
            dbSet = dbContext.Set<GarmentSewingBlockingPlan>();
        }

        public override IQueryable<OverScheduleMonitoringViewModel> GetQuery(string filter)
        {
            Dictionary<string, string> FilterDictionary = new Dictionary<string, string>(JsonConvert.DeserializeObject<Dictionary<string, string>>(filter), StringComparer.OrdinalIgnoreCase);

            string section = FilterDictionary.TryGetValue("section", out section) ? section : "";
            string bookingCode = FilterDictionary.TryGetValue("bookingCode", out bookingCode) ? bookingCode : "";
            string buyer = FilterDictionary.TryGetValue("buyer", out buyer) ? buyer : "";
            string comodity = FilterDictionary.TryGetValue("comodity", out comodity) ? comodity : "";
            DateTime DateFrom = FilterDictionary.TryGetValue("dateFrom", out string dateFrom)? string.IsNullOrWhiteSpace(dateFrom) ? DateTime.MinValue : DateTime.Parse(dateFrom) : DateTime.MinValue;
            DateTime DateTo = FilterDictionary.TryGetValue("dateTo", out string dateTo) ? string.IsNullOrWhiteSpace(dateTo) ? DateTime.MaxValue : DateTime.Parse(dateTo) : DateTime.MaxValue;

            DateTime DateDeliveryFrom = FilterDictionary.TryGetValue("dateDeliveryFrom", out string dateDeliveryFrom) ? string.IsNullOrWhiteSpace(dateDeliveryFrom) ? DateTime.MinValue : DateTime.Parse(dateDeliveryFrom) : DateTime.MinValue;
            DateTime DateDeliveryTo = FilterDictionary.TryGetValue("dateDeliveryTo", out string dateDeliveryTo) ? string.IsNullOrWhiteSpace(dateDeliveryTo) ? DateTime.MaxValue : DateTime.Parse(dateDeliveryTo) : DateTime.MaxValue;

            IQueryable<GarmentSewingBlockingPlan> Query = dbSet;
            IQueryable<GarmentSewingBlockingPlanItem> QueryItem = dbContext.GarmentSewingBlockingPlanItems.Where(a=>a.StartDate.Date> a.DeliveryDate.Date);

            var reportQuery =(from a in QueryItem
                              join b in Query on a.BlockingPlanId equals b.Id
                              join c in dbContext.GarmentBookingOrders on b.BookingOrderId equals c.Id
                              where c.SectionCode == (string.IsNullOrWhiteSpace(section) ? c.SectionCode : section)
                              && b.BookingOrderNo == (string.IsNullOrWhiteSpace(bookingCode) ? b.BookingOrderNo : bookingCode)
                              && b.BuyerCode == (string.IsNullOrWhiteSpace(buyer) ? b.BuyerCode : buyer)
                              && a.ComodityCode == (string.IsNullOrWhiteSpace(comodity) ? a.ComodityCode : comodity)
                              && b.BookingOrderDate.AddHours(7).Date >= DateFrom.Date
                              && b.BookingOrderDate.AddHours(7).Date <= DateTo.Date
                              && a.DeliveryDate.AddHours(7).Date >= DateDeliveryFrom.Date
                              && a.DeliveryDate.AddHours(7).Date <= DateDeliveryTo.Date
                              select new OverScheduleMonitoringViewModel
                              {
                                  bookingCode=b.BookingOrderNo,
                                  bookingDate=b.BookingOrderDate,
                                  bookingDeliveryDate=b.DeliveryDate,
                                  bookingOrderQty=b.OrderQuantity,
                                  confirmQty=c.ConfirmedQuantity,
                                  buyer=b.BuyerName,
                                  comodity=a.ComodityName,
                                  deliveryDate=a.DeliveryDate,
                                  endDate= a.EndDate,
                                  startDate=a.StartDate,
                                  quantity=a.OrderQuantity,
                                  weekNum=a.WeekNumber,
                                  remark=a.Remark,
                                  smv=a.SMVSewing,
                                  unit=a.UnitCode,
                                  usedEH=a.EHBooking,
                                  year=a.Year
                              }).OrderBy(a=>a.bookingDate);

            return reportQuery;
        }
    }
}
