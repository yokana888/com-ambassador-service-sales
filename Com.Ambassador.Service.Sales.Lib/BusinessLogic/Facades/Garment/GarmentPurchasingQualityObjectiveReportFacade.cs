using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.Garment;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.Garment;
using Com.Ambassador.Service.Sales.Lib.ViewModels.Garment;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.Garment
{
    public class GarmentPurchasingQualityObjectiveReportFacade : IGarmentPurchasingQualityObjectiveReportFacade
    {
        private GarmentPurchasingQualityObjectiveReportLogic logic;

        public GarmentPurchasingQualityObjectiveReportFacade(IServiceProvider serviceProvider)
        {
            logic = serviceProvider.GetService<GarmentPurchasingQualityObjectiveReportLogic>();
        }

        public Tuple<MemoryStream, string> GenerateExcel(string filter = "{}")
        {
            var data = GetData(filter);

            ExcelPackage package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("QualityObjective");
            sheet.Cells[1, 1].Value = "LAPORAN SASARAN MUTU PENJUALAN GARMENT";
            sheet.Cells[1, 1, 1, 4].Merge = true;
            sheet.Cells[1, 1, 1, 4].Style.Font.Bold = true;

            data.Add(new GarmentPurchasingQualityObjectiveReportViewModel
            {
                Target = data.Sum(d => d.Target),
                Omzet = data.Sum(d => d.Omzet),
                Achievement = Math.Round(data.Sum(d => d.Omzet) / data.Sum(d => d.Target) * 100, 2, MidpointRounding.AwayFromZero)
            });

            int row = 2;
            foreach (var d in data)
            {
                sheet.Cells[++row, 1].Value = d.Section == null ? "SEMUA SEKSI" : $"SEKSI = {d.Section}";
                sheet.Cells[row, 1, row, 4].Merge = true;
                sheet.Cells[row, 1, row, 4].Style.Font.Bold = true;

                row++;
                sheet.Cells[row, 1].Value = "Target";
                sheet.Cells[row, 2].Value = "Omset";
                sheet.Cells[row, 3].Value = "Persentase Tercapai";
                sheet.Cells[row, 4].Value = "Status";
                sheet.Cells[row, 1, row, 4].Style.Font.Bold = true;
                sheet.Cells[row, 1, row + 1, 4].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                sheet.Cells[row, 1, row + 1, 4].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                sheet.Cells[row, 1, row + 1, 4].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                sheet.Cells[row, 1, row + 1, 4].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                row++;
                sheet.Cells[row, 1].Value = d.Target;
                sheet.Cells[row, 2].Value = d.Omzet;
                sheet.Cells[row, 3].Value = d.Achievement;
                sheet.Cells[row, 1, row, 3].Style.Numberformat.Format = "#,##0.00";
                sheet.Cells[row, 4].Value = d.Achievement > 100 ? "OK" : "NOT OK";

                sheet.InsertRow(++row, 1);
            }
            sheet.Cells[sheet.Dimension.Address].AutoFitColumns();

            MemoryStream stream = new MemoryStream();
            package.SaveAs(stream);

            var filterDict = JsonConvert.DeserializeObject<Dictionary<string, int>>(filter);

            return Tuple.Create(stream, $"Laporan Sasaran Mutu Penjualan Garment - {new CultureInfo("id-ID").DateTimeFormat.GetMonthName(filterDict.GetValueOrDefault("month"))} {filterDict.GetValueOrDefault("year")}");
        }

        public Tuple<List<GarmentPurchasingQualityObjectiveReportViewModel>, int> Read(int page = 1, int size = 25, string filter = "{}")
        {
            var data = GetData(filter);
            return Tuple.Create(data, data.Count);
        }

        private List<GarmentPurchasingQualityObjectiveReportViewModel> GetData(string filter = "{}")
        {
            var data = logic.GetQuery(filter).ToList();

            var targets = logic.GetTargetQuery(filter).ToList();

            foreach (var d in data)
            {
                d.Target = targets.Where(t => t.SectionCode == d.Section).Select(t => t.Amount).FirstOrDefault();
                d.Achievement = d.Omzet / d.Target * 100;
            }

            return data;
        }
    }
}
