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
    public class BudgetExportGarmentReportFacade : IBudgetExportGarmentReport
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<CostCalculationGarment> DbSet;
        private IdentityService IdentityService;
        private BudgetExportGarmentReportLogic BudgetExportGarmentReportLogic;

        public BudgetExportGarmentReportFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<CostCalculationGarment>();
            this.IdentityService = serviceProvider.GetService<IdentityService>();
            this.BudgetExportGarmentReportLogic = serviceProvider.GetService<BudgetExportGarmentReportLogic>();
        }
        
        public Tuple<MemoryStream, string> GenerateExcel(string filter = "{}")
        {
            var Query = BudgetExportGarmentReportLogic.GetQuery(filter);
            var data = Query.ToList();
            DataTable result = new DataTable();
            var offset = 7;

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "No RO", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Konfeksi", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Seksi", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kode Buyer", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Buyer", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tipe Buyer", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Article", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Shipmnet", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Plan PO", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kategori", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kode Barang", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Deskripsi Barang", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Qty Budget", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Harga Satuan", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Total Budget", DataType = typeof(String) });

            Dictionary<string, string> Rowcount = new Dictionary<string, string>();
            if (Query.ToArray().Count() == 0)
                     result.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""); // to allow column name to be generated properly for empty data as template
            else
            {
                Dictionary<string, List<BudgetExportGarmentReportViewModel>> dataByRO = new Dictionary<string, List<BudgetExportGarmentReportViewModel>>();

                Dictionary<string, double> subTotalAmount = new Dictionary<string, double>();

                foreach (BudgetExportGarmentReportViewModel item in Query.ToList())
                {
                    string RONumber = item.RO_Number;

                    if (!dataByRO.ContainsKey(RONumber)) dataByRO.Add(RONumber, new List<BudgetExportGarmentReportViewModel> { });
                    dataByRO[RONumber].Add(new BudgetExportGarmentReportViewModel
                    {
                        RO_Number = item.RO_Number,
                        UnitName = item.UnitName,
                        Section = item.Section,
                        BuyerCode = item.BuyerCode,
                        BuyerName = item.BuyerName,
                        Article = item.Article,
                        DeliveryDate = item.DeliveryDate,
                        PONumber = item.PONumber,
                        CategoryName = item.CategoryName,
                        ProductCode = item.ProductCode,
                        ProductName = item.ProductName,
                        BudgetQuantity = item.BudgetQuantity,
                        BudgetUOM = item.BudgetUOM,
                        BudgetPrice = item.BudgetPrice,
                        BudgetAmount = item.BudgetAmount,
                        Type = item.Type
                    });

                        if (!subTotalAmount.ContainsKey(RONumber))
                        {
                            subTotalAmount.Add(RONumber, 0);
                        };

                         subTotalAmount[RONumber] += item.BudgetAmount;
                    }                
            
                    double totalAmount = 0;

                    int rowPosition = 1;

                    foreach (KeyValuePair<string, List<BudgetExportGarmentReportViewModel>> RONo in dataByRO)
                    {
                        string RO_No = "";

                        int index = 0;
                        foreach (BudgetExportGarmentReportViewModel item in RONo.Value)
                        {
                            index++;

                            string ShipDate = item.DeliveryDate == new DateTime(1970, 1, 1) ? "-" : item.DeliveryDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                            string BgtQty = string.Format("{0:N2}", item.BudgetQuantity);
                            string BgtPrice = string.Format("{0:N4}", item.BudgetPrice);
                            string BgtAmt = string.Format("{0:N2}", item.BudgetAmount);

                            result.Rows.Add(index, item.RO_Number, item.UnitName, item.Section, item.BuyerCode, item.BuyerName, item.Type, item.Article, ShipDate, 
                                            item.PONumber, item.CategoryName, item.ProductCode, item.ProductName, BgtQty, item.BudgetUOM, BgtPrice, BgtAmt);
                            rowPosition += 1;
                            RO_No = item.RO_Number;
                        }
                        result.Rows.Add("", "", "", "", "", "", "", "", "", "", "SUB TOTAL", "", RO_No, "", "", "", Math.Round(subTotalAmount[RONo.Key], 2));

                        rowPosition += 1;
                        totalAmount += subTotalAmount[RONo.Key];
                    }
                        result.Rows.Add("", "", "", "", "", "", "", "", "", "", "T O T A L", "", "", "", "", "", Math.Round(totalAmount, 2));
                        rowPosition += 1;
            }
            ExcelPackage package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("BUDGET GARMENT");
            sheet.Cells["A1"].LoadFromDataTable(result, true, OfficeOpenXml.Table.TableStyles.Light16);

            sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
            MemoryStream streamExcel = new MemoryStream();
            package.SaveAs(streamExcel);

            string fileName = string.Concat("Budget Garment", ".xlsx");

            return Tuple.Create(streamExcel, fileName);
        }

        public Tuple<List<BudgetExportGarmentReportViewModel>, int> Read(int page = 1, int size = 25, string filter = "{}")
        {
            var Query = BudgetExportGarmentReportLogic.GetQuery(filter);
            var data = Query.ToList();

            int TotalData = data.Count();
            return Tuple.Create(data, TotalData);
        }     
    }
}
