using Com.Ambassador.Service.Sales.Lib.Models.GarmentSewingBlockingPlanModel;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentMasterPlan.MonitoringViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.MonitoringLogics
{
    public class WeeklyWorkingScheduleMonitoringLogic : BaseMonitoringLogic<WeeklyWorkingScheduleMonitoringViewModel>
    {
        private IIdentityService identityService;
        private SalesDbContext dbContext;
        private DbSet<GarmentSewingBlockingPlanItem> dbSet;

        public WeeklyWorkingScheduleMonitoringLogic(IIdentityService identityService, SalesDbContext dbContext)
        {
            this.identityService = identityService;
            this.dbContext = dbContext;
            dbSet = dbContext.Set<GarmentSewingBlockingPlanItem>();
        }

        public override IQueryable<WeeklyWorkingScheduleMonitoringViewModel> GetQuery(string filter)
        {
            Dictionary<string, string> FilterDictionary = new Dictionary<string, string>(JsonConvert.DeserializeObject<Dictionary<string, string>>(filter), StringComparer.OrdinalIgnoreCase);

            IQueryable<GarmentSewingBlockingPlanItem> Query = dbSet;

            try
            {
                var year = short.Parse(FilterDictionary["year"]);

                Query = dbSet.Where(d => d.Year == year);
            }
            catch (KeyNotFoundException e)
            {
                throw new Exception(string.Concat("[year]", e.Message));
            }

            if (FilterDictionary.TryGetValue("week", out string week))
            {
                Query = Query.Where(d => d.WeekNumber.ToString() == week);
            }

            var dataList = (from a in Query 
                            join b in dbContext.GarmentSewingBlockingPlans on a.BlockingPlanId equals b.Id 
                            join c in dbContext.GarmentBookingOrders on b.BookingOrderId equals c.Id
                            //join d in dbContext.GarmentBookingOrderItems on c.Id equals d.BookingOrderId into h
                            //from booking in h.DefaultIfEmpty()
                     select new WeeklyWorkingScheduleMonitoringViewModel
                     {
                         bookingOrderDate= b.BookingOrderDate,
                         bookingOrderNo=b.BookingOrderNo,
                         buyer= b.BuyerName,
                         confirmQty=c.ConfirmedQuantity,
                         //booking= (from c in dbContext.GarmentBookingOrderItems where c.BookingOrderId== b.BookingOrderId
                         //          select new booking
                         //          {
                         //              confirmComodity=c.ComodityName,
                         //              confirmDeliveryDate=c.DeliveryDate.ToString("dd MMMM yyyy", new CultureInfo("id-ID")),
                         //              confirmQuantity=c.ConfirmQuantity
                         //          }).ToList(),
                         // confirmComodity= booking == null ? "" : booking.ComodityName,
                         deliveryDate = b.DeliveryDate,
                        // confirmDeliveryDate= booking==null? DateTimeOffset.MinValue: booking.DeliveryDate,
                         workingDeliveryDate=a.DeliveryDate,
                        // confirmQuantity= booking == null ? 0 : booking.ConfirmQuantity,
                         isConfirmed=a.IsConfirm,
                         orderQuantity=b.OrderQuantity,
                         quantity=a.OrderQuantity,
                         remark=a.Remark,
                         smv=a.SMVSewing,
                         status=a.IsConfirm ? "Sudah" : "Belum",
                         unit=a.UnitName,
                         week="W"+ a.WeekNumber.ToString() + "-"+ a.StartDate.ToString("dd MMMM yyyy", new CultureInfo("id-ID")) + " s/d " + a.EndDate.ToString("dd MMMM yyyy", new CultureInfo("id-ID")),
                         workingComodity=a.ComodityName.TrimEnd(),
                         year=a.Year
                     }).OrderBy(a=>a.bookingOrderDate).ThenBy(a => a.bookingOrderNo);

            //List<WeeklyWorkingScheduleMonitoringViewModel> reportData = new List<WeeklyWorkingScheduleMonitoringViewModel>();
            //foreach (var data in dataList)
            //{
            //    if (data.booking.Count > 0)
            //    {
            //        var j = 1;
            //        var x = 1;
            //        var y = 1;
            //        foreach(var book in data.booking)
            //        {
            //            if (string.IsNullOrEmpty(data.confirmComodity))
            //            {
            //                data.confirmComodity = j +". "+ book.confirmComodity;
            //            }
            //            else
            //            {
            //                j++;
            //                data.confirmComodity= string.Concat(data.confirmComodity,"; \n", j + ". ", book.confirmComodity);
            //            }

            //            if (string.IsNullOrEmpty(data.confirmDeliveryDate))
            //            {
            //                data.confirmDeliveryDate = x + ". " + book.confirmDeliveryDate;
            //            }
            //            else
            //            {
            //                x++;
            //                data.confirmDeliveryDate = string.Concat(data.confirmDeliveryDate, "; \n", x + ". ", book.confirmDeliveryDate);
            //            }

            //            if (string.IsNullOrEmpty(data.confirmQuantity))
            //            {
            //                data.confirmQuantity = y + ". " + book.confirmQuantity;
            //            }
            //            else
            //            {
            //                y++;
            //                data.confirmQuantity = string.Concat(data.confirmQuantity, "; \n", y + ". ", book.confirmQuantity);
            //            }
            //        }

            //    }
            //    reportData.Add(data);
            //}


            //return reportData.AsQueryable();
            return dataList;
        }
    }
}
