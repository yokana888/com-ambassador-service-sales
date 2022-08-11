using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.GarmentMasterPlan.MonitoringInterfaces;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.MonitoringLogics;
using Com.Ambassador.Service.Sales.Lib.Helpers;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentMasterPlan.MonitoringViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.GarmentMasterPlan.MonitoringFacades
{
    public class MonitoringRemainingEHFacade : IMonitoringRemainingEHFacade
    {
        private readonly SalesDbContext DbContext;
        //private readonly DbSet<WeeklyPlan> DbSet;
        private IdentityService IdentityService;
        private MonitoringRemainingEHLogic MonitoringRemainingEHLogic;

        public MonitoringRemainingEHFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            DbContext = dbContext;
            //DbSet = DbContext.Set<WeeklyPlan>();
            IdentityService = serviceProvider.GetService<IdentityService>();
            MonitoringRemainingEHLogic = serviceProvider.GetService<MonitoringRemainingEHLogic>();
        }

        public Tuple<MemoryStream, string> GenerateExcel(string filter = "{}")
        {
            var Query = MonitoringRemainingEHLogic.GetQuery(filter);
            var data = Query.ToList();

            DataTable result = new DataTable();

            List<object> rowValuesForEmptyData = new List<object>();
            Dictionary<string, List<UnitDataTable>> rowData = new Dictionary<string, List<UnitDataTable>>();

            result.Columns.Add(new DataColumn() { ColumnName = "Unit", DataType = typeof(string) });
            rowValuesForEmptyData.Add("");
            foreach (var d in data)
            {
                result.Columns.Add(new DataColumn() { ColumnName = d.Unit, DataType = typeof(int) });
                rowValuesForEmptyData.Add(0);

                foreach (var item in d.Items)
                {
                    string week = string.Concat("W", item.WeekNumber);
                    int opUnit = 0;
                    if (d.Unit != "SK")
                    {
                        opUnit = item.Operator;
                    }
                    UnitDataTable unitDataTable = new UnitDataTable
                    {
                        Unit = d.Unit,
                        RemainigEH = item.RemainingEH,
                        Operator = item.Operator,
                        OperatorUnit=opUnit
                        
                    };

                    if (rowData.ContainsKey(week))
                    {
                        var rowValue = rowData[week];
                        var unit = rowValue.FirstOrDefault(a => a.Unit == d.Unit);
                        if (unit == null)
                        {
                            rowValue.Add(unitDataTable);
                        }
                    }
                    else
                    {
                        rowData.Add(week, new List<UnitDataTable> { unitDataTable });
                    }
                }
            }
            if (data.Count > 1)
            {
                result.Columns.Add(new DataColumn() { ColumnName = "Total Remaining EH", DataType = typeof(string) });
                rowValuesForEmptyData.Add("");
            }
            result.Columns.Add(new DataColumn() { ColumnName = "Head Count Unit", DataType = typeof(string) });
            rowValuesForEmptyData.Add("");

            result.Columns.Add(new DataColumn() { ColumnName = "Head Count Total", DataType = typeof(string) });
            rowValuesForEmptyData.Add("");

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
                    foreach (var d in data)
                    {
                        rowValues.Add(rowValue.Value.Where(w => w.Unit == d.Unit).Select(s => s.RemainigEH).FirstOrDefault());
                    }
                    if (data.Count > 1)
                    {
                        rowValues.Add(rowValue.Value.Sum(s => s.RemainigEH));
                    }
                    rowValues.Add(rowValue.Value.Sum(s => s.OperatorUnit));
                    rowValues.Add(rowValue.Value.Sum(s => s.Operator));
                    result.Rows.Add(rowValues.ToArray());
                }
            }

            ExcelPackage package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("RemainingEH");
            sheet.Cells["A1"].LoadFromDataTable(result, true, OfficeOpenXml.Table.TableStyles.Light16);

            for (int y = 1; y <= sheet.Dimension.Rows; y++)
            {
                for (int x = 1; x <= sheet.Dimension.Columns; x++)
                {
                    var cell = sheet.Cells[y, x];

                    cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    cell.Style.Font.Size = 12;
                    cell.Style.Fill.PatternType = ExcelFillStyle.Solid;

                    if (y == 1)
                    {
                        cell.Style.Font.Bold = true;
                        cell.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    }
                    else
                    {
                        if (x > 1 && x <= (sheet.Dimension.Columns - (data.Count > 1 ? 3 : 2)))
                        {
                            cell.Style.Fill.BackgroundColor.SetColor(
                                (int)cell.Value > 0 ? Color.Yellow :
                                (int)cell.Value < 0 ? Color.Red :
                                Color.Green);
                        }
                        else
                        {
                            cell.Style.Fill.BackgroundColor.SetColor(Color.WhiteSmoke);
                        }
                    }
                }
            }

            sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
            MemoryStream streamExcel = new MemoryStream();
            package.SaveAs(streamExcel);

            Dictionary<string, string> FilterDictionary = new Dictionary<string, string>(JsonConvert.DeserializeObject<Dictionary<string, string>>(filter), StringComparer.OrdinalIgnoreCase);
            string fileName = string.Concat("Remaining EH Report ", FilterDictionary["year"], ".xlsx");

            return Tuple.Create(streamExcel, fileName);
        }

        public Tuple<List<MonitoringRemainingEHViewModel>, int> Read(int page = 1, int size = 25, string filter = "{}")
        {
            var Query = MonitoringRemainingEHLogic.GetQuery(filter);
            var data = Query.ToList();
            return Tuple.Create(data, data.Count);
        }

        class UnitDataTable
        {
            public string Unit { get; set; }
            public int RemainigEH { get; set; }
            public int Operator { get; set; }
            public int OperatorUnit { get; set; }
        }
    }
}
