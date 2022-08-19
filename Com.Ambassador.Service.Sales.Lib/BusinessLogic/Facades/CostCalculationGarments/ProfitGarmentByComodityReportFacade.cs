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
    public class ProfitGarmentByComodityReportFacade : IProfitGarmentByComodityReport
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<CostCalculationGarment> DbSet;
        private IdentityService IdentityService;
        private ProfitGarmentByComodityReportLogic ProfitGarmentByComodityReportLogic;

        public ProfitGarmentByComodityReportFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<CostCalculationGarment>();
            this.IdentityService = serviceProvider.GetService<IdentityService>();
            this.ProfitGarmentByComodityReportLogic = serviceProvider.GetService<ProfitGarmentByComodityReportLogic>();
        }

        public Tuple<MemoryStream, string> GenerateExcel(string filter = "{}")
        {

            Dictionary<string, string> FilterDictionary = new Dictionary<string, string>(JsonConvert.DeserializeObject<Dictionary<string, string>>(filter), StringComparer.OrdinalIgnoreCase);

            var Query = ProfitGarmentByComodityReportLogic.GetQuery(filter);
            var data = Query.ToList();
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Komoditi", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Komoditi", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Quantity", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Amount USD", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Profit USD", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Profit IDR ", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Profit FOB", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Term Pembayaran", DataType = typeof(String) });

            Dictionary<string, string> Rowcount = new Dictionary<string, string>();
            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", 0, "", 0, 0, 0, 0, ""); // to allow column name to be generated properly for empty data as template
            else
            {
                Dictionary<string, List<ProfitGarmentByComodityReportViewModel>> dataByUOM = new Dictionary<string, List<ProfitGarmentByComodityReportViewModel>>();

                Dictionary<string, double> subTotalAmount = new Dictionary<string, double>();
                Dictionary<string, double> subTotalProfit1 = new Dictionary<string, double>();
                Dictionary<string, double> subTotalProfit2 = new Dictionary<string, double>();
                Dictionary<string, double> subTotalProfit3 = new Dictionary<string, double>();

                foreach (ProfitGarmentByComodityReportViewModel item in Query.ToList())
                {
                    string BgtUOM = item.UOMUnit;

                    if (!dataByUOM.ContainsKey(BgtUOM)) dataByUOM.Add(BgtUOM, new List<ProfitGarmentByComodityReportViewModel> { });
                    dataByUOM[BgtUOM].Add(new ProfitGarmentByComodityReportViewModel
                    {
                        ComodityCode = item.ComodityCode,
                        ComodityName = item.ComodityName,
                        Quantity = item.Quantity,
                        UOMUnit = item.UOMUnit,
                        Amount = item.Amount,
                        ProfitUSD = item.ProfitUSD,
                        ProfitIDR = item.ProfitIDR,
                        ProfitFOB = item.ProfitFOB,
                        TermPayment = item.TermPayment
                    });

                    if (!subTotalAmount.ContainsKey(BgtUOM))
                    {
                        subTotalAmount.Add(BgtUOM, 0);
                    };

                    subTotalAmount[BgtUOM] += item.Amount;

                    if (!subTotalProfit1.ContainsKey(BgtUOM))
                    {
                        subTotalProfit1.Add(BgtUOM, 0);
                    };

                    subTotalProfit1[BgtUOM] += item.ProfitUSD;

                    if (!subTotalProfit2.ContainsKey(BgtUOM))
                    {
                        subTotalProfit2.Add(BgtUOM, 0);
                    };

                    subTotalProfit2[BgtUOM] += item.ProfitIDR;

                    if (!subTotalProfit3.ContainsKey(BgtUOM))
                    {
                        subTotalProfit3.Add(BgtUOM, 0);
                    };

                    subTotalProfit3[BgtUOM] += item.ProfitFOB;
                }

                double totalAmount = 0;
                double totalAmount1 = 0;
                double totalAmount2 = 0;
                double totalAmount3 = 0;


                int rowPosition = 1;

                foreach (KeyValuePair<string, List<ProfitGarmentByComodityReportViewModel>> UoM in dataByUOM)
                {
                    string U_o_M = "";

                    int index = 0;
                    foreach (ProfitGarmentByComodityReportViewModel item in UoM.Value)
                    {
                        index++;

                        result.Rows.Add(index, item.ComodityCode, item.ComodityName, item.Quantity, item.UOMUnit, item.Amount, item.ProfitUSD, item.ProfitIDR, item.ProfitFOB, item.TermPayment);

                        rowPosition += 1;
                        U_o_M = item.UOMUnit;
                    }
                    result.Rows.Add("", "", "SUB TOTAL", 0, U_o_M, Math.Round(subTotalAmount[UoM.Key], 2), Math.Round(subTotalProfit1[UoM.Key], 2), Math.Round(subTotalProfit1[UoM.Key], 2), Math.Round(subTotalProfit1[UoM.Key], 2), "");

                    rowPosition += 1;
                    totalAmount += subTotalAmount[UoM.Key];
                    totalAmount1 += subTotalProfit1[UoM.Key];
                    totalAmount2 += subTotalProfit1[UoM.Key];
                    totalAmount3 += subTotalProfit1[UoM.Key];
                }
                result.Rows.Add("", "", "T O T A L", 0, "", Math.Round(totalAmount, 2), Math.Round(totalAmount1, 2), Math.Round(totalAmount2, 2), Math.Round(totalAmount3, 2), "");
                rowPosition += 1;
            }

            ExcelPackage package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("Profit Garment By Komoditi");
            sheet.Cells["A1"].LoadFromDataTable(result, true, OfficeOpenXml.Table.TableStyles.Light16);

            sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
            MemoryStream streamExcel = new MemoryStream();
            package.SaveAs(streamExcel);

            string fileName = string.Concat("Profit Garment Per Komoditi", ".xlsx");

            return Tuple.Create(streamExcel, fileName);
        }

        public Tuple<List<ProfitGarmentByComodityReportViewModel>, int> Read(int page = 1, int size = 25, string filter = "{}")
        {
            var Query = ProfitGarmentByComodityReportLogic.GetQuery(filter);
            var data = Query.ToList();

            int TotalData = data.Count();
            return Tuple.Create(data, TotalData);
        }
    }
}
