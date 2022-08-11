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
using OfficeOpenXml;
using Newtonsoft.Json;
using System.Globalization;
using OfficeOpenXml.Style;
using System.Drawing;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.GarmentMasterPlan.MonitoringFacades
{
    public class AcceptedOrderMonitoringFacade : IAcceptedOrderMonitoringFacade
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<GarmentSewingBlockingPlanItem> DbSet;
        private IdentityService IdentityService;
        private AcceptedOrderMonitoringLogic AcceptedOrderMonitoringLogic;

        public AcceptedOrderMonitoringFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<GarmentSewingBlockingPlanItem>();
            IdentityService = serviceProvider.GetService<IdentityService>();
            AcceptedOrderMonitoringLogic = serviceProvider.GetService<AcceptedOrderMonitoringLogic>();
        }
        public Tuple<MemoryStream, string> GenerateExcel(string filter = "{}")
        {
            Dictionary<string, string> FilterDictionary = new Dictionary<string, string>(JsonConvert.DeserializeObject<Dictionary<string, string>>(filter), StringComparer.OrdinalIgnoreCase);
            var Query = AcceptedOrderMonitoringLogic.GetQuery(filter);
            var year = short.Parse(FilterDictionary["year"]);
            var unitFilter = FilterDictionary.ContainsKey("unit") ? FilterDictionary["unit"] : "";
            var weeks = DbContext.GarmentWeeklyPlans.Include(a=>a.Items).Where(a=>a.Year==year);
            var data = Query.ToList();
            var monthTemp = "";
            int total = 0;

            if (!string.IsNullOrWhiteSpace(unitFilter))
            {
                weeks = DbContext.GarmentWeeklyPlans.Include(a => a.Items).Where(a => a.Year == year && a.UnitCode== unitFilter);
            }
            DataTable result = new DataTable();

            List<object> rowValuesForEmptyData = new List<object>();
            Dictionary<string, List<UnitDataTable>> rowData = new Dictionary<string, List<UnitDataTable>>();
            Dictionary<string, string> column = new Dictionary<string, string>();

            result.Columns.Add(new DataColumn() { ColumnName = "Unit", DataType = typeof(string) });
            rowValuesForEmptyData.Add("");
            foreach (var d in weeks.OrderByDescending(a=>a.UnitCode))
            {
                string unitCol = d.UnitCode;
                if (!column.ContainsKey(d.UnitCode))
                {
                    column.Add(unitCol, unitCol);
                    result.Columns.Add(new DataColumn() { ColumnName = d.UnitCode, DataType = typeof(int) });
                }

                rowValuesForEmptyData.Add(0);

                var month = "";
                monthTemp = "";
                foreach (var item in weeks.FirstOrDefault().Items.OrderBy(a=>a.WeekNumber))
                {
                    switch (item.EndDate.Month)
                    {
                        case 1:
                            month = "Januari";
                            break;
                        case 2:
                            month = "Februari";
                            break;
                        case 3:
                            month = "Maret";
                            break;
                        case 4:
                            month = "April";
                            break;
                        case 5:
                            month = "Mei";
                            break;
                        case 6:
                            month = "Juni";
                            break;
                        case 7:
                            month = "Juli";
                            break;
                        case 8:
                            month = "Agustus";
                            break;
                        case 9:
                            month = "September";
                            break;
                        case 10:
                            month = "Oktober";
                            break;
                        case 11:
                            month = "November";
                            break;
                        case 12:
                            month = "Desember";
                            break;
                        default:
                            month = "";
                            break;
                    }

                    string week = string.Concat("W", item.WeekNumber," - ", month);

                    var dataQty = data.FirstOrDefault(a => a.Unit.Equals(d.UnitCode) && a.WeekNumber.Equals(item.WeekNumber));

                    UnitDataTable unitDataTable = new UnitDataTable
                    {
                        Unit = d.UnitCode,
                        qty= dataQty==null? 0 : (int)dataQty.Quantity
                    };

                    //disini
                    if (monthTemp == "")
                    {
                        monthTemp = month;
                    }

                    if (monthTemp == month)
                    {
                        total = dataQty==null? 0 + total : (int)dataQty.Quantity + total;
                    } else if (monthTemp != month)
                    {
                        UnitDataTable unitDataTableTotal = new UnitDataTable
                        {
                            Unit = d.UnitCode,
                            qty = total
                        };
                        string weekTotal = string.Concat("TOTAL ", monthTemp.ToUpper());
                        if (rowData.ContainsKey(weekTotal))
                        {
                            var rowValue = rowData[weekTotal];
                            var unit = rowValue.FirstOrDefault(a => a.Unit == d.UnitCode);
                            if (unit == null)
                            {
                                rowValue.Add(unitDataTableTotal);
                            }
                        }
                        else
                        {
                            rowData.Add(weekTotal, new List<UnitDataTable> { unitDataTableTotal });
                        }
                        total = dataQty == null ? 0 : (int)dataQty.Quantity;
                        monthTemp = month;
                    }


                    if (rowData.ContainsKey(week))
                    {
                        var rowValue = rowData[week];
                        var unit = rowValue.FirstOrDefault(a => a.Unit == d.UnitCode);
                        if (unit == null)
                        {
                            rowValue.Add(unitDataTable);
                        }
                    }
                    else
                    {
                        rowData.Add(week , new List<UnitDataTable> { unitDataTable });
                    }
                }

                UnitDataTable unitDataTableTotal2 = new UnitDataTable
                {
                    Unit = d.UnitCode,
                    qty = total
                };
                string weekTotal2 = string.Concat("TOTAL ", monthTemp.ToUpper());
                if (rowData.ContainsKey(weekTotal2))
                {
                    var rowValue = rowData[weekTotal2];
                    var unit = rowValue.FirstOrDefault(a => a.Unit == d.UnitCode);
                    if (unit == null)
                    {
                        rowValue.Add(unitDataTableTotal2);
                    }
                }
                else
                {
                    rowData.Add(weekTotal2, new List<UnitDataTable> { unitDataTableTotal2 });
                }
                total = 0;

                var sumQty = data.Where(a => a.Unit == d.UnitCode);
                if (!rowData.ContainsKey("TOTAL"))
                {
                    UnitDataTable unitDataTable1 = new UnitDataTable
                    {
                        Unit = d.UnitCode,
                        qty = sumQty==null ? 0 : (int)sumQty.Sum(a => a.Quantity) 
                    };
                    rowData.Add("TOTAL", new List<UnitDataTable> { unitDataTable1 });
                }
                else
                {
                    var sumData = rowData["TOTAL"];
                    UnitDataTable unitDataTable2 = new UnitDataTable
                    {
                        Unit = d.UnitCode,
                        qty = sumQty == null ? 0 : (int)sumQty.Sum(a => a.Quantity)
                    };
                    sumData.Add(unitDataTable2);
                }
            }

            if (data.Count == 0)
            {
                result.Rows.Add(rowValuesForEmptyData.ToArray());
            }
            else
            {
                foreach (var rowValue in rowData)
                {
                    List<object> rowValues = new List<object>();
                    rowValues.Add(rowValue.Key);
                    foreach(var a in rowValue.Value)
                    {
                        rowValues.Add(a.qty);
                    }
                    result.Rows.Add(rowValues.ToArray());
                }
            }

            ExcelPackage package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("Monitoring Order Diterima");
            sheet.Cells["A1"].LoadFromDataTable(result, true, OfficeOpenXml.Table.TableStyles.Light16);

            for (int y = 1; y <= sheet.Dimension.Rows; y++)
            {
                for (int x = 1; x <= sheet.Dimension.Columns; x++)
                {
                    var cell = sheet.Cells[y, x];

                    if (y == 1)
                    {
                        cell.Style.Font.Bold = true;
                    }
                    else
                    {
                        if (sheet.Cells[y, x - x + 1].Value.ToString().Contains("TOTAL"))
                        {
                            cell.Style.Font.Bold = true;
                        } else
                        {
                            cell.Style.Font.Bold = false;
                        }
                    }
                }
            }


            sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
            MemoryStream streamExcel = new MemoryStream();
            package.SaveAs(streamExcel);

            string fileName = string.Concat("Monitoring Order Diterima dan Booking ", FilterDictionary["year"], ".xlsx");

            return Tuple.Create(streamExcel, fileName);
        }

        public Tuple<List<AcceptedOrderMonitoringViewModel>, int> Read(int page = 1, int size = 25, string filter = "{}")
        {
            var Query = AcceptedOrderMonitoringLogic.GetQuery(filter);
            var data = Query.ToList();
            return Tuple.Create(data, data.Count);
        }

        class UnitDataTable
        {
            public string Unit { get; set; }
            public int qty { get; set; }
        }
    }
}
