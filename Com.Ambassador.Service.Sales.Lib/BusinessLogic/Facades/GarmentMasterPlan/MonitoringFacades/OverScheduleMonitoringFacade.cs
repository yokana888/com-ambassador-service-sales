using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.GarmentMasterPlan.MonitoringInterfaces;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.MonitoringLogics;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentSewingBlockingPlanModel;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentMasterPlan.MonitoringViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Data;
using System.Globalization;
using Com.Ambassador.Service.Sales.Lib.Helpers;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Newtonsoft.Json;
using Com.Moonlay.NetCore.Lib;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.GarmentMasterPlan.MonitoringFacades
{
    public class OverScheduleMonitoringFacade : IOverScheduleMonitoringFacade
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<GarmentSewingBlockingPlan> DbSet;
        private IdentityService IdentityService;
        private OverScheduleMonitoringLogic OverScheduleMonitoringLogic;

        public OverScheduleMonitoringFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<GarmentSewingBlockingPlan>();
            IdentityService = serviceProvider.GetService<IdentityService>();
            OverScheduleMonitoringLogic = serviceProvider.GetService<OverScheduleMonitoringLogic>();
        }
        public Tuple<MemoryStream, string> GenerateExcel(string filter = "{}")
        {
            var Query = OverScheduleMonitoringLogic.GetQuery(filter);
            var data = Query.ToList();
            DataTable result = new DataTable();
            var offset = 7;
                              
            result.Columns.Add(new DataColumn() { ColumnName = "No. Booking", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Booking", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jumlah Order", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jumlah Confirm", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Pengiriman (booking)", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Komoditi", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "SMV", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Unit", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tahun", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Week", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jumlah", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Keterangan", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Pengiriman", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Used EH", DataType = typeof(string) });

            //List<(string, Enum, Enum)> mergeCells = new List<(string, Enum, Enum)>() { };
            Dictionary<string, string> Rowcount = new Dictionary<string, string>();
            int idx = 1;
            var rCount = 0;
            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", ""); // to allow column name to be generated properly for empty data as template
            else
            {
                //int index = 0;

                foreach (var item in Query.ToList())
                {
                    idx++;
                    if (!Rowcount.ContainsKey(item.bookingCode))
                    {
                        rCount = 0;
                        var index = idx;
                        Rowcount.Add(item.bookingCode, index.ToString());
                    }
                    else
                    {
                        rCount += 1;
                        Rowcount[item.bookingCode] = Rowcount[item.bookingCode] + "-" + rCount.ToString();
                        var val = Rowcount[item.bookingCode].Split("-");
                        if ((val).Length > 0)
                        {
                            Rowcount[item.bookingCode] = val[0] + "-" + rCount.ToString();
                        }
                    }

                    string week= "W"+ item.weekNum + " " + item.startDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMMM yyyy", new CultureInfo("id-ID")) + " s/d " + item.endDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMMM yyyy", new CultureInfo("id-ID")); ;
                    string BookingOrderDate = item.bookingDate == new DateTime(1970, 1, 1) ? "-" : item.bookingDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMMM yyyy", new CultureInfo("id-ID"));
                    string DeliveryDate = item.bookingDeliveryDate == new DateTime(1970, 1, 1) ? "-" : item.bookingDeliveryDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMMM yyyy", new CultureInfo("id-ID"));
                    // string ConfirmDate = confirmDate == new DateTime(1970, 1, 1) ? "-" : confirmDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMMM yyyy", new CultureInfo("id-ID"));
                    string DeliveryDateItems = item.deliveryDate == new DateTime(1970, 1, 1) ? "-" : item.deliveryDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMMM yyyy", new CultureInfo("id-ID"));

                    result.Rows.Add(item.bookingCode, BookingOrderDate, item.buyer, item.bookingOrderQty, item.confirmQty, DeliveryDate, item.comodity, item.smv,
                    item.unit, item.year, week,  item.quantity, item.remark, DeliveryDateItems, item.usedEH);

                }
            }
            ExcelPackage package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("Blocking Plan Sewing");
            sheet.Cells["A1"].LoadFromDataTable(result, true, OfficeOpenXml.Table.TableStyles.Light16);

            foreach (var rowMerge in Rowcount)
            {
                var UnitrowNum = rowMerge.Value.Split("-");
                int rowNum2 = 1;
                int rowNum1 = Convert.ToInt32(UnitrowNum[0]);
                if (UnitrowNum.Length > 1)
                {
                    rowNum2 = Convert.ToInt32(rowNum1) + Convert.ToInt32(UnitrowNum[1]);
                }
                else
                {
                    rowNum2 = Convert.ToInt32(rowNum1);
                }

                sheet.Cells[$"A{rowNum1}:A{rowNum2}"].Merge = true;
                sheet.Cells[$"A{rowNum1}:A{rowNum2}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                sheet.Cells[$"A{rowNum1}:A{rowNum2}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                sheet.Cells[$"B{rowNum1}:B{rowNum2}"].Merge = true;
                sheet.Cells[$"B{rowNum1}:B{rowNum2}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                sheet.Cells[$"B{rowNum1}:B{rowNum2}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                sheet.Cells[$"C{rowNum1}:C{rowNum2}"].Merge = true;
                sheet.Cells[$"C{rowNum1}:C{rowNum2}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                sheet.Cells[$"C{rowNum1}:C{rowNum2}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                sheet.Cells[$"D{rowNum1}:D{rowNum2}"].Merge = true;
                sheet.Cells[$"D{rowNum1}:D{rowNum2}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                sheet.Cells[$"D{rowNum1}:D{rowNum2}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                sheet.Cells[$"E{rowNum1}:E{rowNum2}"].Merge = true;
                sheet.Cells[$"E{rowNum1}:E{rowNum2}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                sheet.Cells[$"E{rowNum1}:E{rowNum2}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                sheet.Cells[$"F{rowNum1}:F{rowNum2}"].Merge = true;
                sheet.Cells[$"F{rowNum1}:F{rowNum2}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                sheet.Cells[$"F{rowNum1}:F{rowNum2}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            }


            sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
            MemoryStream streamExcel = new MemoryStream();
            package.SaveAs(streamExcel);

            //Dictionary<string, string> FilterDictionary = new Dictionary<string, string>(JsonConvert.DeserializeObject<Dictionary<string, string>>(filter), StringComparer.OrdinalIgnoreCase);
            string fileName = string.Concat("Monitoring Keterlambatan Jadwal Pengerjaan ", DateTime.Now.Date , ".xlsx");

            return Tuple.Create(streamExcel, fileName);
        }

        public Tuple<List<OverScheduleMonitoringViewModel>, int> Read(int page = 1, int size = 25, string filter = "{}")
        {
            var Query = OverScheduleMonitoringLogic.GetQuery(filter);
            var data = Query.ToList();
            Pageable<OverScheduleMonitoringViewModel> pageable = new Pageable<OverScheduleMonitoringViewModel>(data, page - 1, size);
            List<OverScheduleMonitoringViewModel> Data_ = pageable.Data.ToList<OverScheduleMonitoringViewModel>();

            int TotalData = pageable.TotalCount;

            return Tuple.Create(Data_, TotalData);
        }
    }
}
