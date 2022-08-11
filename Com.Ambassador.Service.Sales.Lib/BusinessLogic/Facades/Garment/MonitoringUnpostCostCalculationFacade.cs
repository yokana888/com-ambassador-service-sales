using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.Garment;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.Garment;
using Com.Ambassador.Service.Sales.Lib.Helpers;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.ViewModels.Garment;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OfficeOpenXml.Style;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.Garment
{
    public class MonitoringUnpostCostCalculationFacade : IMonitoringUnpostCostCalculationFacade
    {
        private MonitoringUnpostCostCalculationLogic logic;
        private IIdentityService identityService;

        public MonitoringUnpostCostCalculationFacade(IServiceProvider serviceProvider)
        {
            logic = serviceProvider.GetService<MonitoringUnpostCostCalculationLogic>();
            identityService = serviceProvider.GetService<IIdentityService>();
        }

        public Tuple<MemoryStream, string> GenerateExcel(string filter = "{}")
        {
            var Query = logic.GetQuery(filter);
            var data = Query.ToList();

            DataTable result = new DataTable();
            result.Columns.Add(new DataColumn() { ColumnName = "Seksi", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No RO", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Artikel", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Sales Contract", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Unit", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kuantitas", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Unpost", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Alasan Unpost", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "User yang Unpost", DataType = typeof(string) });

            int rowPosition = 2;
            List<(string, Enum, Enum)> mergeCells = new List<(string, Enum, Enum)>() { };

            if (data != null && data.Count() > 0)
            {
                foreach (var d in data)
                {
                    var firstMergedRowPosition = rowPosition;
                    var lastMergedRowPosition = rowPosition;

                    foreach (var r in d.UnpostReasons)
                    {
                        result.Rows.Add(d.Section, d.RONo, d.Article, d.PreSCNo, d.Unit, d.Quantity, r.Date.AddHours(identityService.TimezoneOffset).ToString("dd MMMM yyyy", new CultureInfo("id-ID")), r.Reason, r.Creator);
                        lastMergedRowPosition = rowPosition++;
                    }
                    foreach (var col in new[] { "A", "B", "C", "D", "E", "F" })
                    {
                        if (firstMergedRowPosition != lastMergedRowPosition)
                        {
                            mergeCells.Add(($"{col}{firstMergedRowPosition}:{col}{lastMergedRowPosition}", ExcelHorizontalAlignment.Left, ExcelVerticalAlignment.Bottom));
                        }
                    }
                }
            }
            else
            {
                result.Rows.Add(null, null, null, null, null, null, null, null, null);
            }

            var excel = Excel.CreateExcel(new List<(DataTable, string, List<(string, Enum, Enum)>)>() { (result, "Unposted CC", mergeCells) }, false);

            return new Tuple<MemoryStream, string>(excel, string.Concat("Monitoring Unpost Cost Calculation", GetSuffixNameFromFilter(filter)));
        }

        private string GetSuffixNameFromFilter(string filterString)
        {
            Dictionary<string, string> filter = JsonConvert.DeserializeObject<Dictionary<string, string>>(filterString);

            return string.Join(null, filter.Select(s => $" - {s.Value}").ToArray());
        }

        public Tuple<List<MonitoringUnpostCostCalculationViewModel>, int> Read(int page = 1, int size = 25, string filter = "{}")
        {
            var Query = logic.GetQuery(filter);
            var data = Query.ToList();
            return Tuple.Create(data, data.Count);
        }
    }
}
