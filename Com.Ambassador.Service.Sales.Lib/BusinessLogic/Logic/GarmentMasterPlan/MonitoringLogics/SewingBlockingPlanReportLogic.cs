using Com.Ambassador.Service.Sales.Lib.Models.GarmentMasterPlan.WeeklyPlanModels;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentSewingBlockingPlanModel;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentBookingOrderViewModels;
using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentMasterPlan.MonitoringViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.MonitoringLogics
{
    public class SewingBlockingPlanReportLogic : BaseMonitoringLogic<SewingBlockingPlanReportViewModel>
    {
        private IIdentityService identityService;
        private SalesDbContext dbContext;
        private DbSet<GarmentSewingBlockingPlan> dbSet;

        public SewingBlockingPlanReportLogic(IIdentityService identityService, SalesDbContext dbContext)
        {
            this.identityService = identityService;
            this.dbContext = dbContext;
            dbSet = dbContext.Set<GarmentSewingBlockingPlan>();
        }
        public override IQueryable<SewingBlockingPlanReportViewModel> GetQuery(string filter)
        {
            Dictionary<string, string> FilterDictionary = new Dictionary<string, string>(JsonConvert.DeserializeObject<Dictionary<string, string>>(filter), StringComparer.OrdinalIgnoreCase);

            IQueryable<GarmentWeeklyPlan> WeekQuery = dbContext.GarmentWeeklyPlans;
            IQueryable<GarmentSewingBlockingPlan> Query = dbSet;
            IQueryable<GarmentSewingBlockingPlanItem> QueryItem = dbContext.GarmentSewingBlockingPlanItems;

            try
            {
                var year = short.Parse(FilterDictionary["year"]);
                WeekQuery = WeekQuery.Where(d => d.Year == year);
                QueryItem = QueryItem.Where(d => d.Year == year);
            }
            catch (KeyNotFoundException e)
            {
                throw new Exception(string.Concat("[year]", e.Message));
            }

            if (FilterDictionary.TryGetValue("unit", out string unit))
            {
                WeekQuery = WeekQuery.Where(d => d.UnitCode == unit);
                QueryItem = QueryItem.Where(d => d.UnitCode == unit);
            }

            WeekQuery = WeekQuery.OrderBy(o => o.UnitCode);
            QueryItem = QueryItem.OrderBy(o => o.UnitCode);


            var JoinQuery = (from a in QueryItem
                             join b in Query on a.BlockingPlanId equals b.Id
                             select new 
                             {
                                 buyer= b.BuyerName + " - " + a.ComodityName,
                                 a.Year, a.IsConfirm, a.OrderQuantity, a.SMVSewing, a.UnitCode, a.UnitName,
                                 a.WeekNumber,a.EHBooking,a.Efficiency,a.EndDate, bookingqty=b.OrderQuantity, a.WeeklyPlanId, b.BookingOrderId, b.BookingOrderDate
                             }).AsQueryable();

            var joinAll = (from a in JoinQuery
                           join b in WeekQuery on a.WeeklyPlanId equals b.Id
                           join d in dbContext.GarmentBookingOrders on a.BookingOrderId equals d.Id
                           select new SewingBlockingPlanReportViewModel
                           {
                               bookingQty = a.OrderQuantity,
                               SMVSewing = a.SMVSewing,
                               isConfirmed = a.IsConfirm,
                               unit = a.UnitCode,
                               buyer = a.buyer,
                               year = a.Year,
                               weekSewingBlocking = a.WeekNumber,
                               UsedEH = a.EHBooking,
                               bookingDate = a.BookingOrderDate,
                               bookingId = a.BookingOrderId,
                               bookingOrderQty = a.bookingqty,
                               bookingOrderItems = dbContext.GarmentBookingOrderItems.Where(a => a.BookingOrderId == d.Id && a.IsCanceled==false)
                                                .Select(data => new BOItemViewModel
                                                {
                                                    ConfirmQuantity = data.ConfirmQuantity,
                                                }).ToList(),
                               items = (from c in dbContext.GarmentWeeklyPlanItems where b.Id== c.WeeklyPlanId orderby c.WeekNumber
                                        select new SewingBlockingPlanReportItemViewModel {
                                            efficiency=c.Efficiency,
                                            EHTotal=c.EHTotal,
                                            remainingEH=c.RemainingEH,
                                            head=c.Operator,
                                            AHTotal=c.AHTotal,
                                            usedTotal=c.UsedEH,
                                            weekEndDate=c.EndDate,
                                            weekNumber=c.WeekNumber,
                                            workingHours=c.WorkingHours,
                                            WHBooking=c.UsedEH !=0 && c.Operator!=0 ? c.UsedEH/(c.Operator*c.Efficiency/100) : 0,
                                        }).ToList()
                        }).AsQueryable();

            return joinAll.OrderBy(c=>c.unit).ThenBy(c=>c.buyer);
        }
    }
}
