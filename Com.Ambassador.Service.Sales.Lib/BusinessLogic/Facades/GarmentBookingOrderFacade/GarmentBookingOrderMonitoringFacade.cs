using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.GarmentBookingOrderInterface;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentBookingOrderLogics;
using Com.Ambassador.Service.Sales.Lib.Helpers;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentBookingOrderModel;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentBookingOrderViewModels;
using Com.Moonlay.NetCore.Lib;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.GarmentBookingOrderFacade
{
    public class GarmentBookingOrderMonitoringFacade : IGarmentBookingOrderMonitoringInterface
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<GarmentBookingOrder> DbSet;
        private IdentityService IdentityService;

        public GarmentBookingOrderMonitoringFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<GarmentBookingOrder>(); ;
            this.IdentityService = serviceProvider.GetService<IdentityService>();
        }

        public IQueryable<GarmentBookingOrderMonitoringViewModel> GetReportQuery(string section, string no, string buyerCode, string comodityCode, string statusConfirm, string statusBookingOrder, DateTime? dateFrom, DateTime? dateTo, DateTime? dateDeliveryFrom, DateTime? dateDeliveryTo, int offset)
        {
            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

            DateTime DateDeliveryFrom = dateDeliveryFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateDeliveryFrom;
            DateTime DateDeliveryTo = dateDeliveryTo == null ? DateTime.MaxValue : (DateTime)dateDeliveryTo;
            var today = DateTime.Today;
            List<GarmentBookingOrderMonitoringViewModel> listGarmentBookingMonitoring = new List<GarmentBookingOrderMonitoringViewModel>();
            List<GarmentBookingOrderMonitoringViewModel> listAllGarmentBookingMonitoring = new List<GarmentBookingOrderMonitoringViewModel>();
            List<GarmentBookingOrderMonitoringViewModel> listGarmentBookingMonitoringFilter = new List<GarmentBookingOrderMonitoringViewModel>();

            var Query1 = (from a in DbContext.GarmentBookingOrders
                          join b in DbContext.GarmentBookingOrderItems on a.Id equals b.BookingOrderId
                          join c in DbContext.CostCalculationGarments on b.Id equals c.BookingOrderItemId into cc
                          from CCG in cc.DefaultIfEmpty()
                          where a.IsDeleted == false
                             && a.IsCanceled == false
                             && a.OrderQuantity > 0
                             && a.SectionCode == (string.IsNullOrWhiteSpace(section) ? a.SectionCode : section)
                             && a.BookingOrderNo == (string.IsNullOrWhiteSpace(no) ? a.BookingOrderNo : no)
                             && a.BuyerCode == (string.IsNullOrWhiteSpace(buyerCode) ? a.BuyerCode : buyerCode)
                             && b.ComodityCode == (string.IsNullOrWhiteSpace(comodityCode) ? b.ComodityCode : comodityCode)
                             && a.BookingOrderDate.AddHours(offset).Date >= DateFrom.Date
                             && a.BookingOrderDate.AddHours(offset).Date <= DateTo.Date
                             && a.DeliveryDate.AddHours(offset).Date >= DateDeliveryFrom.Date
                             && a.DeliveryDate.AddHours(offset).Date <= DateDeliveryTo.Date
                             && b.IsCanceled==false
                          select new GarmentBookingOrderMonitoringViewModel
                          {
                             CreatedUtc = a.CreatedUtc,
                             BookingOrderNo = a.BookingOrderNo,
                             BookingOrderDate = a.BookingOrderDate,
                             BuyerName = a.BuyerName,
                             OrderQuantity = a.OrderQuantity,
                             DeliveryDate = a.DeliveryDate,
                             ComodityName = b.ComodityName,
                             ConfirmQuantity = b.ConfirmQuantity,
                             CCQuantity = CCG == null ? 0 : CCG.Quantity,
                             RemainingQuantity = 0,
                             DeliveryDateItems = b.DeliveryDate,
                             ConfirmDate = b.ConfirmDate,
                             Remark = b.Remark,
                             StatusConfirm = a.ConfirmedQuantity == 0 ? "Belum Dikonfirmasi" : a.ConfirmedQuantity > 0 ? "Sudah Dikonfirmasi" : "-",
                             StatusBooking = a.IsBlockingPlan == true ? "Sudah Dibuat Master Plan" : a.ConfirmedQuantity == 0 && a.IsBlockingPlan == false ? "Booking" : a.ConfirmedQuantity > 0 && a.IsBlockingPlan == false ? "Confirmed" : "-",
                             OrderLeft = (a.OrderQuantity - a.ConfirmedQuantity) > 0 ? (a.OrderQuantity - a.ConfirmedQuantity).ToString() : "0",
                             DateDiff = ((TimeSpan)(a.DeliveryDate.AddHours(offset) - today)).Days <= 40 && ((TimeSpan)(a.DeliveryDate.AddHours(offset) - today)).Days >= 0 ? ((TimeSpan)(a.DeliveryDate.AddHours(offset) - today)).Days.ToString() : "-",
                             row_count = 1,
                             LastModifiedUtc = a.LastModifiedUtc,
                             NotConfirmedQuantity= a.ExpiredBookingQuantity + a.CanceledQuantity,
                             SurplusQuantity = (a.ConfirmedQuantity - a.OrderQuantity) > 0 ? (a.ConfirmedQuantity - a.OrderQuantity).ToString() : "-"
                          }
                          );

            var Query = (from x in Query1
                         group new { CCQty = x.CCQuantity } by new
                         {
                             x.CreatedUtc,
                             x.BookingOrderNo,
                             x.BookingOrderDate,
                             x.BuyerName,
                             x.OrderQuantity,
                             x.DeliveryDate,
                             x.ComodityName,
                             x.ConfirmQuantity,
                             x.RemainingQuantity,
                             x.DeliveryDateItems,
                             x.ConfirmDate,
                             x.Remark,
                             x.StatusConfirm,
                             x.StatusBooking,
                             x.OrderLeft,
                             x.DateDiff,
                             x.row_count,
                             x.LastModifiedUtc,
                             x.NotConfirmedQuantity,
                             x.SurplusQuantity,
                         } into G

                         select new GarmentBookingOrderMonitoringViewModel
                         {
                             CreatedUtc = G.Key.CreatedUtc,
                             BookingOrderNo = G.Key.BookingOrderNo,
                             BookingOrderDate = G.Key.BookingOrderDate,
                             BuyerName = G.Key.BuyerName,
                             OrderQuantity = G.Key.OrderQuantity,
                             DeliveryDate = G.Key.DeliveryDate,
                             ComodityName = G.Key.ComodityName,
                             ConfirmQuantity = G.Key.ConfirmQuantity,
                             DeliveryDateItems = G.Key.DeliveryDateItems,
                             ConfirmDate = G.Key.ConfirmDate,
                             Remark = G.Key.Remark,
                             StatusConfirm = G.Key.StatusConfirm,
                             StatusBooking = G.Key.StatusBooking,
                             OrderLeft = G.Key.OrderLeft,
                             DateDiff = G.Key.DateDiff,
                             row_count = G.Key.row_count,
                             LastModifiedUtc = G.Key.LastModifiedUtc,
                             NotConfirmedQuantity = G.Key.NotConfirmedQuantity,
                             SurplusQuantity = G.Key.SurplusQuantity,
                             CCQuantity = G.Sum(m => m.CCQty) == 0 ? 0 : G.Sum(m => m.CCQty),
                             RemainingQuantity = G.Sum(m => m.CCQty) == 0 ? Convert.ToInt32((1.05 * G.Key.ConfirmQuantity)) : (G.Key.ConfirmQuantity - Convert.ToInt32((1.05 * G.Key.ConfirmQuantity))) <= 0 ? 0 : Convert.ToInt32((1.05 * G.Key.ConfirmQuantity)) - G.Sum(m => m.CCQty),
                             //StatusConfirm = a.ConfirmedQuantity == 0 ? "Belum Dikonfirmasi" : a.ConfirmedQuantity > 0 ? "Sudah Dikonfirmasi" : "-",

                         }).OrderBy(x => x.BookingOrderNo).ThenBy(x => x.BuyerName);

            foreach (var query in Query)
            {
                if (statusConfirm == "Belum Dikonfirmasi")
                {
                    if (query.StatusConfirm == statusConfirm)
                    {
                        listGarmentBookingMonitoring.Add(query);
                    }
                } 
                else if (statusConfirm == "Sudah Dikonfirmasi")
                {
                    if (query.StatusConfirm == statusConfirm)
                    {
                        listGarmentBookingMonitoring.Add(query);
                    }
                } 
                else
                {
                    listGarmentBookingMonitoring.Add(query);
                }
            }

            if (statusConfirm != "Sudah Dikonfirmasi")
            {
                var query2 = (from a in DbContext.GarmentBookingOrders
                              where a.IsDeleted == false
                              && a.IsCanceled == false
                              && a.OrderQuantity > 0
                              && a.ConfirmedQuantity == 0
                              && a.SectionCode == (string.IsNullOrWhiteSpace(section) ? a.SectionCode : section)
                              && a.BookingOrderNo == (string.IsNullOrWhiteSpace(no) ? a.BookingOrderNo : no)
                              && a.BuyerCode == (string.IsNullOrWhiteSpace(buyerCode) ? a.BuyerCode : buyerCode)
                              && a.BookingOrderDate.AddHours(offset).Date >= DateFrom.Date
                              && a.BookingOrderDate.AddHours(offset).Date <= DateTo.Date
                              && a.DeliveryDate.AddHours(offset).Date >= DateDeliveryFrom.Date
                              && a.DeliveryDate.AddHours(offset).Date <= DateDeliveryTo.Date
                              select new GarmentBookingOrderMonitoringViewModel
                              {
                                  CreatedUtc = a.CreatedUtc,
                                  BookingOrderNo = a.BookingOrderNo,
                                  BookingOrderDate = a.BookingOrderDate,
                                  BuyerName = a.BuyerName,
                                  OrderQuantity = a.OrderQuantity,
                                  DeliveryDate = a.DeliveryDate,
                                  ComodityName = null,
                                  ConfirmQuantity = null,
                                  DeliveryDateItems = null,
                                  ConfirmDate = null,
                                  Remark = null,
                                  StatusConfirm = "Belum Dikonfirmasi",
                                  StatusBooking = a.IsBlockingPlan == true ? "Sudah Dibuat Master Plan" : a.ConfirmedQuantity == 0 && a.IsBlockingPlan == false ? "Booking" : a.ConfirmedQuantity > 0 && a.IsBlockingPlan == false ? "Confirmed" : "-",
                                  OrderLeft = (a.OrderQuantity - a.ConfirmedQuantity)>0 ? (a.OrderQuantity - a.ConfirmedQuantity).ToString() : "0",
                                  //DateDiff = ((TimeSpan)(a.DeliveryDate.AddHours(offset) - today)).Days <= 40 && ((TimeSpan)(a.DeliveryDate.AddHours(offset) - today)).Days >= 0 ? ((TimeSpan)(a.DeliveryDate.AddHours(offset) - today)).Days.ToString() : "-",
                                  row_count = 1,
                                  LastModifiedUtc = a.LastModifiedUtc,
                                  NotConfirmedQuantity = a.ExpiredBookingQuantity + a.CanceledQuantity,
                                  //SurplusQuantity = (a.ConfirmedQuantity - a.OrderQuantity)>0? (a.ConfirmedQuantity - a.OrderQuantity).ToString(): "-"
                              }
                );
                foreach (var query in query2.OrderBy(o => o.BookingOrderNo))
                {
                    if (listGarmentBookingMonitoring.Count > 0)
                    {
                        foreach (var mainQuery in listGarmentBookingMonitoring.OrderBy(o => o.BookingOrderNo))
                        {
                            if (mainQuery.BookingOrderNo != query.BookingOrderNo)
                            {
                                listAllGarmentBookingMonitoring.Add(query);
                            }
                        }
                    }
                    else
                    {
                        listAllGarmentBookingMonitoring.Add(query);
                    }
                }

                foreach (var query in listAllGarmentBookingMonitoring.Distinct())
                {
                    listGarmentBookingMonitoring.Add(query);
                }

                var tempCheck = "";
                foreach (var query in listGarmentBookingMonitoring.OrderBy(o => o.BookingOrderNo))
                {
                    if (tempCheck == query.BookingOrderNo && query.ComodityName==null && query.ConfirmQuantity==null && query.DeliveryDateItems==null && query.ConfirmDate==null && query.Remark==null)
                    {
                        listGarmentBookingMonitoring.Remove(query);
                    }
                    
                    if (tempCheck == "" || tempCheck != query.BookingOrderNo)
                        tempCheck = query.BookingOrderNo;
                }
            }

            foreach (var queryFilter in listGarmentBookingMonitoring)
            {
                if (statusBookingOrder == "Sudah Dibuat Master Plan")
                {
                    if (queryFilter.StatusBooking == statusBookingOrder)
                    {
                        listGarmentBookingMonitoringFilter.Add(queryFilter);
                    }
                } else if (statusBookingOrder == "Booking")
                {
                    if (queryFilter.StatusBooking == statusBookingOrder)
                    {
                        listGarmentBookingMonitoringFilter.Add(queryFilter);
                    }
                } else if (statusBookingOrder == "Confirmed")
                {
                    if (queryFilter.StatusBooking == statusBookingOrder)
                    {
                        listGarmentBookingMonitoringFilter.Add(queryFilter);
                    }
                } else
                {
                    listGarmentBookingMonitoringFilter.Add(queryFilter);
                }
            }

            return listGarmentBookingMonitoringFilter.AsQueryable();
        }

        public Tuple<List<GarmentBookingOrderMonitoringViewModel>, int> Read(string section, string no, string buyerCode, string comodityCode, string statusConfirm, string statusBookingOrder, DateTime? dateFrom, DateTime? dateTo, DateTime? dateDeliveryFrom, DateTime? dateDeliveryTo, int page, int size, string Order, int offset)
        {
            var Query = GetReportQuery(section, no, buyerCode, comodityCode, statusConfirm, statusBookingOrder, dateFrom, dateTo, dateDeliveryFrom, dateDeliveryTo, offset);

            var Data = Query.OrderByDescending(b => b.LastModifiedUtc).ToList();

            Pageable<GarmentBookingOrderMonitoringViewModel> pageable = new Pageable<GarmentBookingOrderMonitoringViewModel>(Data, page - 1, size);
            List<GarmentBookingOrderMonitoringViewModel> Data_ = pageable.Data.ToList<GarmentBookingOrderMonitoringViewModel>();

            int TotalData = pageable.TotalCount;

            return Tuple.Create(Data_, TotalData);
        }

        public MemoryStream GenerateExcel(string section, string no, string buyerCode, string comodityCode, string statusConfirm, string statusBookingOrder, DateTime? dateFrom, DateTime? dateTo, DateTime? dateDeliveryFrom, DateTime? dateDeliveryTo, int offset)
        {
            var Query = GetReportQuery(section, no, buyerCode, comodityCode, statusConfirm, statusBookingOrder, dateFrom, dateTo, dateDeliveryFrom, dateDeliveryTo, offset);
            Query = Query.OrderByDescending(b => b.LastModifiedUtc);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "Kode Booking", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Booking", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jumlah Order", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Pengeriman (booking)", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Komoditi", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jumlah Confirm", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Budget Turun", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Sisa Budget Turun", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Pengiriman (confirm)", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Confirm", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Keterangan", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Status Confirm", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Status Booking Order", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Sisa Order (Belum Confirm)", DataType = typeof(string) });
            //result.Columns.Add(new DataColumn() { ColumnName = "Seilisih Hari (dari Tanggal Pengiriman)", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Not Confirmed Order (MINUS)", DataType = typeof(string) });
            //result.Columns.Add(new DataColumn() { ColumnName = "Over Confirm (SURPLUS)", DataType = typeof(string) });

            List<(string, Enum, Enum)> mergeCells = new List<(string, Enum, Enum)>() { };

            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""); // to allow column name to be generated properly for empty data as template
            else
            {
                int index = 0;
                string temp_No = "";
                int rowPosition = 1;
                int counterTemp = 1;

                foreach (var item in Query.ToList())
                {
                    index++;
                    rowPosition++;
                    DateTimeOffset bookingOrderDate = item.BookingOrderDate ?? new DateTime(1970, 1, 1);
                    DateTimeOffset deliveryDate = item.DeliveryDate ?? new DateTime(1970, 1, 1);
                    DateTimeOffset confirmDate = item.ConfirmDate ?? new DateTime(1970, 1, 1);
                    DateTimeOffset deliveryDateItems = item.DeliveryDateItems ?? new DateTime(1970, 1, 1);

                    string BookingOrderDate = bookingOrderDate == new DateTime(1970, 1, 1) ? "-" : bookingOrderDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMMM yyyy", new CultureInfo("id-ID"));
                    string DeliveryDate = deliveryDate == new DateTime(1970, 1, 1) ? "-" : deliveryDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMMM yyyy", new CultureInfo("id-ID"));
                    string ConfirmDate = confirmDate == new DateTime(1970, 1, 1) ? "-" : confirmDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMMM yyyy", new CultureInfo("id-ID"));
                    string DeliveryDateItems = deliveryDateItems == new DateTime(1970, 1, 1) ? "-" : deliveryDateItems.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMMM yyyy", new CultureInfo("id-ID"));


                    if (temp_No == item.BookingOrderNo)
                    {
                        item.BookingOrderNo = null;
                        BookingOrderDate = null;
                        item.BuyerName = null;
                        item.OrderQuantity = null;
                        DeliveryDate = null;
                        item.StatusConfirm = null;
                        item.StatusBooking = null;
                        item.OrderLeft = null;
                        item.DateDiff = null;

                        counterTemp++;

                        result.Rows.Add(item.BookingOrderNo, BookingOrderDate, item.BuyerName, item.OrderQuantity, DeliveryDate, item.ComodityName, item.ConfirmQuantity, item.CCQuantity, item.RemainingQuantity,
                        DeliveryDateItems, ConfirmDate, item.Remark, item.StatusConfirm, item.StatusBooking, item.OrderLeft, item.NotConfirmedQuantity);


                    } 
                    else
                    {
                        result.Rows.Add(item.BookingOrderNo, BookingOrderDate, item.BuyerName, item.OrderQuantity, DeliveryDate, item.ComodityName, item.ConfirmQuantity, item.CCQuantity, item.RemainingQuantity,
                        DeliveryDateItems, ConfirmDate, item.Remark, item.StatusConfirm, item.StatusBooking, item.OrderLeft, item.NotConfirmedQuantity);

                        if (counterTemp > 1)
                        {
                            mergeCells.Add(($"A{rowPosition - counterTemp}:A{rowPosition - 1}", OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center));
                            mergeCells.Add(($"B{rowPosition - counterTemp}:B{rowPosition - 1}", OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center));
                            mergeCells.Add(($"C{rowPosition - counterTemp}:C{rowPosition - 1}", OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center));
                            mergeCells.Add(($"D{rowPosition - counterTemp}:D{rowPosition - 1}", OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center));
                            mergeCells.Add(($"E{rowPosition - counterTemp}:E{rowPosition - 1}", OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center));
                            //mergeCells.Add(($"K{rowPosition - counterTemp}:K{rowPosition - 1}", OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center));
                            //mergeCells.Add(($"L{rowPosition - counterTemp}:L{rowPosition - 1}", OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center));
                            mergeCells.Add(($"M{rowPosition - counterTemp}:M{rowPosition - 1}", OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center));
                            mergeCells.Add(($"N{rowPosition - counterTemp}:N{rowPosition - 1}", OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center));
                            mergeCells.Add(($"O{rowPosition - counterTemp}:O{rowPosition - 1}", OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center));
                            mergeCells.Add(($"P{rowPosition - counterTemp}:P{rowPosition - 1}", OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center));

                            counterTemp = 1;
                        }
                        temp_No = item.BookingOrderNo;
                    }
                }

                if (counterTemp > 1)
                {
                    mergeCells.Add(($"A{rowPosition + 1 - counterTemp}:A{rowPosition}", OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center));
                    mergeCells.Add(($"B{rowPosition + 1 - counterTemp}:B{rowPosition}", OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center));
                    mergeCells.Add(($"C{rowPosition + 1 - counterTemp}:C{rowPosition}", OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center));
                    mergeCells.Add(($"D{rowPosition + 1 - counterTemp}:D{rowPosition}", OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center));
                    mergeCells.Add(($"E{rowPosition + 1 - counterTemp}:E{rowPosition}", OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center));
                    //mergeCells.Add(($"K{rowPosition + 1 - counterTemp}:K{rowPosition}", OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center));
                    //mergeCells.Add(($"L{rowPosition + 1 - counterTemp}:L{rowPosition}", OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center));
                    mergeCells.Add(($"M{rowPosition + 1 - counterTemp}:M{rowPosition}", OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center));
                    mergeCells.Add(($"N{rowPosition + 1 - counterTemp}:N{rowPosition}", OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center));
                    mergeCells.Add(($"O{rowPosition + 1 - counterTemp}:O{rowPosition}", OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center));
                    mergeCells.Add(($"P{rowPosition + 1 - counterTemp}:P{rowPosition}", OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center));

                    counterTemp = 1;
                }
            }
            return Excel.CreateExcel(new List<(DataTable, string, List<(string, Enum, Enum)>)>() { (result, "Report", mergeCells) }, true);
        }
    }
}
