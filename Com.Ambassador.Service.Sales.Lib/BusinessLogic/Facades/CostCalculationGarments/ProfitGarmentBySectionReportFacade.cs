using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.CostCalculationGarmentLogic;
using Com.Ambassador.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Ambassador.Service.Sales.Lib.ViewModels.CostCalculationGarment;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.CostCalculationGarments;
using Com.Ambassador.Service.Sales.Lib.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Com.Ambassador.Service.Sales.Lib.Helpers;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using Com.Moonlay.NetCore.Lib;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.CostCalculationGarments
{
    public class ProfitGarmentBySectionReportFacade : IProfitGarmentBySectionReport
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<CostCalculationGarment> DbSet;
        private IdentityService IdentityService;
        private ProfitGarmentBySectionReportLogic ProfitGarmentBySectionReportLogic;

        public ProfitGarmentBySectionReportFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<CostCalculationGarment>();
            this.IdentityService = serviceProvider.GetService<IdentityService>();
            this.ProfitGarmentBySectionReportLogic = serviceProvider.GetService<ProfitGarmentBySectionReportLogic>();
        }

        public Tuple<MemoryStream, string> GenerateExcel(string filter = "{}")
        {

            Dictionary<string, string> FilterDictionary = new Dictionary<string, string>(JsonConvert.DeserializeObject<Dictionary<string, string>>(filter), StringComparer.OrdinalIgnoreCase);

            var Query = ProfitGarmentBySectionReportLogic.GetQuery(filter);
            var data = Query.ToList();
            DataTable result = new DataTable();
            var offset = 7;

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "No RO", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Seksi", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Unit", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kode Agent", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Agent", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kode Brand", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Brand", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tipe Buyer", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Article", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Komoditi", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Deskripsi Garment", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Fabric Allowance %", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Acc Allowance %", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Shipment", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Qty Order", DataType = typeof(Double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Confirm Price FOB", DataType = typeof(Double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Confirm Price CMT", DataType = typeof(Double) });
            result.Columns.Add(new DataColumn() { ColumnName = "HPP", DataType = typeof(Double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Fabric Cost", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "FOB Price", DataType = typeof(Double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Amount", DataType = typeof(Double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Rate USD", DataType = typeof(Double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Komisi %", DataType = typeof(Double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Komisi IDR", DataType = typeof(Double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Profit %", DataType = typeof(Double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Profit IDR", DataType = typeof(Double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Profit USD", DataType = typeof(Double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Profit/FOB %", DataType = typeof(Double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Garment Price", DataType = typeof(String) });

            int rowPosition = 1;
            Dictionary<string, string> Rowcount = new Dictionary<string, string>();
            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 0, "", 0, 0, 0, "", 0, 0, 0, 0, 0, 0, 0, 0, 0, ""); // to allow column name to be generated properly for empty data as template
            else
            {
                Dictionary<string, List<ProfitGarmentBySectionReportViewModel>> dataBySection = new Dictionary<string, List<ProfitGarmentBySectionReportViewModel>>();

                Dictionary<string, double> subTotalAmount = new Dictionary<string, double>();
                Dictionary<string, double> subTotalPrftIDR = new Dictionary<string, double>();
                Dictionary<string, double> subTotalPrftUSD = new Dictionary<string, double>();

                var grandTotalByUom = new List<TotalByUom>();

                foreach (ProfitGarmentBySectionReportViewModel item in Query.ToList())
                {
                    string Section = item.Section;

                    if (!dataBySection.ContainsKey(Section)) dataBySection.Add(Section, new List<ProfitGarmentBySectionReportViewModel> { });
                    dataBySection[Section].Add(new ProfitGarmentBySectionReportViewModel
                    {
                        UnitName = item.UnitName,
                        Section = item.Section,
                        BuyerCode = item.BuyerCode,
                        BuyerName = item.BuyerName,
                        BrandCode = item.BrandCode,
                        BrandName = item.BrandName,
                        Type = item.Type,
                        RO_Number = item.RO_Number,
                        Comodity = item.Comodity,
                        ComodityDescription = item.ComodityDescription,
                        CurrencyRate = item.CurrencyRate,
                        Article = item.Article,
                        Quantity = item.Quantity,
                        UOMUnit = item.UOMUnit,
                        DeliveryDate = item.DeliveryDate,
                        ConfirmPrice = item.ConfirmPrice,
                        ConfirmPrice1 = item.ConfirmPrice1,
                        HPP = item.HPP,
                        CMPrice = item.CMPrice,
                        FOBPrice = item.FOBPrice,
                        FabAllow = item.FabAllow,
                        AccAllow = item.AccAllow,
                        Amount = item.Amount,
                        Profit = item.Profit,
                        ProfitIDR = item.ProfitIDR,
                        ProfitUSD = item.ProfitUSD,
                        ProfitFOB = item.ProfitFOB,
                        Commision = item.Commision,
                        CommisionIDR = item.CommisionIDR,
                        TermPayment = item.TermPayment
                    });

                    var currentUom = grandTotalByUom.FirstOrDefault(c => c.uom == item.UOMUnit);
                    if (currentUom == null)
                    {
                        grandTotalByUom.Add(new TotalByUom
                        {
                            uom = item.UOMUnit,
                            quantity = item.Quantity,
                            amount = item.Amount,
                        });
                    }
                    else
                    {
                        currentUom.quantity += item.Quantity;
                        currentUom.amount += item.Amount;
                    }

                    if (!subTotalAmount.ContainsKey(Section))
                    {
                        subTotalAmount.Add(Section, 0);
                    };

                    if (!subTotalPrftIDR.ContainsKey(Section))
                    {
                        subTotalPrftIDR.Add(Section, 0);
                    };

                    if (!subTotalPrftUSD.ContainsKey(Section))
                    {
                        subTotalPrftUSD.Add(Section, 0);
                    };

                    subTotalAmount[Section] += item.Amount;
                    subTotalPrftIDR[Section] += item.ProfitIDR;
                    subTotalPrftUSD[Section] += item.ProfitUSD;

                }

                double totalAmount = 0;
                double totalPrftIDR = 0;
                double totalPrftUSD = 0;

                foreach (KeyValuePair<string, List<ProfitGarmentBySectionReportViewModel>> Seksi in dataBySection)
                {
                    string SECTION = "";

                    int index = 0;
                    foreach (ProfitGarmentBySectionReportViewModel item in Seksi.Value)
                    {
                        index++;

                        string ShipDate = item.DeliveryDate == new DateTime(1970, 1, 1) ? "-" : item.DeliveryDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                        string QtyOrder = string.Format("{0:N2}", item.Quantity);
                        string CfrmPrc = string.Format("{0:N4}", item.ConfirmPrice);
                        string PrftGmt = string.Format("{0:N2}", item.Profit);
                        string PrftIDR = string.Format("{0:N2}", item.ProfitIDR);
                        string PrftUSD = string.Format("{0:N2}", item.ProfitUSD);
                        string PrftFOB = string.Format("{0:N2}", item.ProfitFOB);
                        string CMPrc1 = string.Format("{0:N4}", item.CMPrice);
                        string FOBPrc = string.Format("{0:N4}", item.FOBPrice);
                        string Amnt = string.Format("{0:N2}", item.Amount);
                        string Rate = string.Format("{0:N2}", item.CurrencyRate);
                        string Comm = string.Format("{0:N2}", item.Commision);
                        string CMTPrc = string.Format("{0:N2}", item.ConfirmPrice1);
                        string HP = string.Format("{0:N2}", item.HPP);
                    
                        result.Rows.Add(index, item.RO_Number, item.Section, item.UnitName, item.BuyerCode, item.BuyerName, item.BrandCode, item.BrandName, item.Type,
                                        item.Article, item.Comodity, item.ComodityDescription, item.FabAllow, item.AccAllow, ShipDate, item.Quantity, item.UOMUnit,
                                        item.ConfirmPrice, item.ConfirmPrice1, item.HPP, CMPrc1, item.FOBPrice, item.Amount, item.CurrencyRate, item.Commision, item.CommisionIDR, item.Profit, item.ProfitIDR, item.ProfitUSD, item.ProfitFOB, item.TermPayment);

                        rowPosition += 1;
                        SECTION = item.Section;
                    }
                 
                    result.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "SUB TOTAL  :", "", "SEKSI", 0, SECTION, 0, 0, 0, "", 0, Math.Round(subTotalAmount[Seksi.Key], 2), 0, 0, 0, 0, Math.Round(subTotalPrftIDR[Seksi.Key], 2), Math.Round(subTotalPrftUSD[Seksi.Key], 2), 0, "");

                    rowPosition += 1;
                    totalAmount += subTotalAmount[Seksi.Key];
                    totalPrftIDR += subTotalPrftIDR[Seksi.Key];
                    totalPrftUSD += subTotalPrftUSD[Seksi.Key];
                }
                rowPosition++;
                foreach (var i in Enumerable.Range(0, grandTotalByUom.Count))
                {
                    if (i == 0)
                    {
                          result.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "GRAND TOTAL", "", "", grandTotalByUom[i].quantity, grandTotalByUom[i].uom, grandTotalByUom[i].amount, 0, 0, "GRAND TOTAL AMOUNT", 0, data.Sum(d => d.Amount), 0, 0, 0, 0, 0, 0, 0, "");
                    }
                    else
                    {          
                        result.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", grandTotalByUom[i].quantity, grandTotalByUom[i].uom, grandTotalByUom[i].amount, 0, 0, "", 0, 0, 0, 0, 0, 0, 0, 0, 0, "");
                    }
                }
            }
            //ExcelPackage package = new ExcelPackage();
            //var sheet = package.Workbook.Worksheets.Add("Profit Garment By Seksi");
            //sheet.Cells["A1"].LoadFromDataTable(result, true, OfficeOpenXml.Table.TableStyles.Light16);

            //sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
            //MemoryStream streamExcel = new MemoryStream();
            //package.SaveAs(streamExcel);
            Filter _filter = JsonConvert.DeserializeObject<Filter>(filter);
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Profit Garment By Seksi");
                string startDate = _filter.dateFrom.GetValueOrDefault().Date.ToShortDateString();
                string finishDate= _filter.dateTo.GetValueOrDefault().Date.ToShortDateString();
                worksheet.Cells["A1"].Value = "LAPORAN PROFIT GARMENT PER SEKSI";
                worksheet.Cells["A2"].Value = "TANGGAL AWAL : " + startDate + "  TANGGAL AKHIR : " + finishDate;
                worksheet.Cells["A3"].Value = "SEKSI : " + _filter.section;

                worksheet.Cells["A" + 1 + ":F" + 1 + ""].Merge = true;
                worksheet.Cells["A" + 2 + ":F" + 2 + ""].Merge = true;
                worksheet.Cells["A" + 3 + ":F" + 3 + ""].Merge = true;
                worksheet.Cells["A" + 4 + ":F" + 4 + ""].Merge = true;

                worksheet.Cells["A5"].LoadFromDataTable(result, true);
                worksheet.Cells["A" + 5 + ":AE" + (rowPosition + 5) + ""].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A" + 5 + ":AE" + (rowPosition + 5) + ""].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A" + 5 + ":AE" + (rowPosition + 5) + ""].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A" + 5 + ":AE" + (rowPosition + 5) + ""].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                worksheet.Cells["A" + 1 + ":AE" + (rowPosition + 5) + ""].AutoFitColumns();


                var streamExcel = new MemoryStream();
                package.SaveAs(streamExcel);

                string fileName = string.Concat("Profit Garment Per Seksi", ".xlsx");

                return Tuple.Create(streamExcel, fileName);
            }
        }
        private class Filter
        {
            public string section { get; set; }
            public DateTimeOffset? dateFrom { get; set; }
            public DateTimeOffset? dateTo { get; set; }
        }

        public Tuple<List<ProfitGarmentBySectionReportViewModel>, int> Read(int page = 1, int size = 25, string filter = "{}")
        {
            var Query = ProfitGarmentBySectionReportLogic.GetQuery(filter);
            var data = Query.ToList();

            int TotalData = data.Count();
            return Tuple.Create(data, TotalData);
        }

        private class TotalByUom
        {
            public string uom { get; set; }
            public double quantity { get; set; }
            public double amount { get; set; }
        }
    }
}
