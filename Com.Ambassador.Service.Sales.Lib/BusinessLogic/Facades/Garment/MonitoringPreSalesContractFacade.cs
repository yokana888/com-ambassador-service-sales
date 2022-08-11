using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.Garment;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.Garment;
using Com.Ambassador.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.ViewModels.Garment;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel.GarmentPurchaseRequestViewModel;
using Microsoft.Extensions.DependencyInjection;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.Garment
{
    public class MonitoringPreSalesContractFacade : IMonitoringPreSalesContractFacade
    {
        private MonitoringUnpostCostCalculationLogic lgc;
        private MonitoringPreSalesContractLogic logic;
        private IIdentityService identityService;

        public MonitoringPreSalesContractFacade(IServiceProvider serviceProvider)
        {
            lgc = serviceProvider.GetService<MonitoringUnpostCostCalculationLogic>();
            logic = serviceProvider.GetService<MonitoringPreSalesContractLogic>();
            identityService = serviceProvider.GetService<IIdentityService>();
        }

        public Tuple<MemoryStream, string> GenerateExcel(string filter = "{}")
        {
            var data = GetData(filter);

            DataTable result = new DataTable();
            result.Columns.Add(new DataColumn() { ColumnName = "Seksi", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Pre Sales Contract", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Pre Sales Contract", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jenis Sales Contract", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Buyer Agent", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Buyer Brand", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jumlah Order", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "No PR Master", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No RO Master", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jenis PR Master", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Unit (PR Master)", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl PR Master", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Artikel (PR Master)", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Cost Calculation", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No RO JOB", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Artikel (Cost Calculation)", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Unit (Cost Calculation)", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kuantitas", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Confirm Price", DataType = typeof(double) });

            int rowPosition = 2;
            List<(string, Enum, Enum)> mergeCells = new List<(string, Enum, Enum)>() { };

            ExcelPackage package = new ExcelPackage();

            var sheet = package.Workbook.Worksheets.Add("PreSC");
            //sheet.Cells[1, 1, 1, 20].Style.Fill.PatternType = ExcelFillStyle.Solid;
            //sheet.Cells[1, 1, 1, 20].Style.Fill.BackgroundColor.SetColor(Color.Gray);

            if (data != null && data.Count() > 0)
            {
                foreach (var d in data)
                {
                    var firstMergedRowPosition = rowPosition;
                    var lastMergedRowPosition = rowPosition;

                    foreach (var i in Enumerable.Range(0, new List<int> { d.GarmentPurchaseRequests.Count, d.CostCalculations.Count, 1 }.Max()))
                    {
                        MonitoringPreSalesContractGPRViewModel pr = d.GarmentPurchaseRequests.ElementAtOrDefault(i);
                        MonitoringPreSalesContractCCViewModel cc = d.CostCalculations.ElementAtOrDefault(i);

                        if (pr != null && cc != null)
                        {
                            result.Rows.Add(d.Section, d.SCNo, d.SCDate.ToString("dd MMMM yyyy", new CultureInfo("id-ID")), d.SCType, d.BuyerAgent, d.BuyerBrand, d.OrderQuantity, pr.PRNo, pr.RONo, pr.PRType, pr.Unit, pr.Date.ToString("dd MMMM yyyy", new CultureInfo("id-ID")), pr.Article, cc.Date.ToString("dd MMMM yyyy", new CultureInfo("id-ID")), cc.RONo, cc.Article, cc.Unit, cc.Quantity, cc.Uom, cc.ConfirmPrice);
                        }
                        else if (pr != null && cc == null)
                        {
                            result.Rows.Add(d.Section, d.SCNo, d.SCDate.ToString("dd MMMM yyyy", new CultureInfo("id-ID")), d.SCType, d.BuyerAgent, d.BuyerBrand, d.OrderQuantity, pr.PRNo, pr.RONo, pr.PRType, pr.Unit, pr.Date.ToString("dd MMMM yyyy", new CultureInfo("id-ID")), pr.Article, null, null, null, null, null, null, null);
                        }
                        else if (pr == null && cc != null)
                        {
                            result.Rows.Add(d.Section, d.SCNo, d.SCDate.ToString("dd MMMM yyyy", new CultureInfo("id-ID")), d.SCType, d.BuyerAgent, d.BuyerBrand, d.OrderQuantity, null, null, null, null, null, null, cc.Date.ToString("dd MMMM yyyy", new CultureInfo("id-ID")), cc.RONo, cc.Article, cc.Unit, cc.Quantity, cc.Uom, cc.ConfirmPrice);
                        }
                        else if (pr == null && cc == null)
                        {
                            result.Rows.Add(d.Section, d.SCNo, d.SCDate.ToString("dd MMMM yyyy", new CultureInfo("id-ID")), d.SCType, d.BuyerAgent, d.BuyerBrand, d.OrderQuantity, null, null, null, null, null, null, null, null, null, null, null, null, null);
                        }
                        lastMergedRowPosition = rowPosition++;
                    }
                    foreach (var col in new[] { "A", "B", "C", "D", "E", "F", "G" })
                    {
                        if (firstMergedRowPosition != lastMergedRowPosition)
                        {
                            mergeCells.Add(($"{col}{firstMergedRowPosition}:{col}{lastMergedRowPosition}", col == "G" ? ExcelHorizontalAlignment.Right : ExcelHorizontalAlignment.Left, ExcelVerticalAlignment.Center));
                        }
                    }

                    sheet.Cells[firstMergedRowPosition, 1, lastMergedRowPosition, 20].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells[firstMergedRowPosition, 1, lastMergedRowPosition, 20].Style.Fill.BackgroundColor.SetColor(data.IndexOf(d) % 2 != 0 ? Color.White : Color.LightGray);
                }
            }
            else
            {
                result.Rows.Add(null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
            }

            sheet.Cells["A1"].LoadFromDataTable(result, true, TableStyles.Light16);

            foreach ((string cells, Enum hAlign, Enum vAlign) in mergeCells)
            {
                sheet.Cells[cells].Merge = true;
                sheet.Cells[cells].Style.HorizontalAlignment = (ExcelHorizontalAlignment)hAlign;
                sheet.Cells[cells].Style.VerticalAlignment = (ExcelVerticalAlignment)vAlign;
            }
            sheet.Cells[sheet.Dimension.Address].AutoFitColumns();

            MemoryStream stream = new MemoryStream();
            package.SaveAs(stream);

            return new Tuple<MemoryStream, string>(stream, "Monitoring Unpost Cost Calculation");
        }

        public Tuple<List<MonitoringPreSalesContractViewModel>, int> Read(int page = 1, int size = 25, string filter = "{}")
        {
            var data = GetData(filter);

            return Tuple.Create(data, data.Count);
        }

        private List<MonitoringPreSalesContractViewModel> GetData(string filter = "{}")
        {
            var dataSalesContracts = logic.GetQuery(filter).ToList();

            var allSCNo = dataSalesContracts.Select(sc => sc.SCNo).ToHashSet();

            List<CostCalculationGarment> dataCostCalculations = new List<CostCalculationGarment>();
            List<GarmentPurchaseRequestViewModel> dataPurchaseRequests = new List<GarmentPurchaseRequestViewModel>();

            if (allSCNo.Count > 0)
            {
                dataCostCalculations = logic.GetCCQuery(filter, allSCNo).ToList();
                dataPurchaseRequests = logic.GetPurchaseRequest(filter, allSCNo);
            }

            var data = dataSalesContracts.Select(sc => new MonitoringPreSalesContractViewModel
            {
                Section = sc.SectionCode,
                SCNo = sc.SCNo,
                SCDate = sc.SCDate.ToOffset(TimeSpan.FromHours(identityService.TimezoneOffset)).DateTime,
                SCType = sc.SCType,
                BuyerAgent = sc.BuyerAgentName,
                BuyerBrand = sc.BuyerBrandName,
                OrderQuantity = sc.OrderQuantity,
                GarmentPurchaseRequests = dataPurchaseRequests.Where(cc => cc.SCNo == sc.SCNo).Select(pr => new MonitoringPreSalesContractGPRViewModel
                {
                    PRNo = pr.PRNo,
                    RONo = pr.RONo,
                    PRType = pr.PRType,
                    Unit = pr.Unit.Name,
                    Date = pr.CreatedUtc.AddHours(identityService.TimezoneOffset),
                    Article = pr.Article
                }).ToList(),
                CostCalculations = dataCostCalculations.Where(cc => cc.PreSCNo == sc.SCNo).Select(cc => new MonitoringPreSalesContractCCViewModel
                {
                    Date = cc.CreatedUtc.AddHours(identityService.TimezoneOffset),
                    RONo = cc.RO_Number,
                    Article = cc.Article,
                    Unit = cc.UnitName,
                    Quantity = cc.Quantity,
                    Uom = cc.UOMUnit,
                    ConfirmPrice = cc.ConfirmPrice
                }).ToList(),
            }).ToList();

            return data;
        }
    }
}
