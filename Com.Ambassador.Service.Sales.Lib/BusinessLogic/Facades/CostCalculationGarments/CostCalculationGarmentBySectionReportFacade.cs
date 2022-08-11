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
    public class CostCalculationGarmentBySectionReportFacade : ICostCalculationGarmentBySectionReport
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<CostCalculationGarment> DbSet;
        private IdentityService IdentityService;
        private CostCalculationBySectionReportLogic CostCalculationBySectionReportLogic;

        public CostCalculationGarmentBySectionReportFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<CostCalculationGarment>();
            this.IdentityService = serviceProvider.GetService<IdentityService>();
            this.CostCalculationBySectionReportLogic = serviceProvider.GetService<CostCalculationBySectionReportLogic>();
        }
        
        public Tuple<MemoryStream, string> GenerateExcel(string filter = "{}")
        {
            var Query = CostCalculationBySectionReportLogic.GetQuery(filter);
            var data = Query.ToList();
            DataTable result = new DataTable();
            var offset = 7;

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "No RO", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Confirm", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Shipment", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Konfeksi", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Seksi", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Penanggung Jawab Seksi", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Article", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Komoditi", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Description", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kode Agennt", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Agent", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kode Brand", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Brand", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jumlah", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Confirm Price", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Amount", DataType = typeof(String) });

            Dictionary<string, string> Rowcount = new Dictionary<string, string>();
            if (Query.ToArray().Count() == 0)
                     result.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""); // to allow column name to be generated properly for empty data as template
            else
            {
                    Dictionary<string, List<CostCalculationGarmentBySectionReportViewModel>> dataByBrand = new Dictionary<string, List<CostCalculationGarmentBySectionReportViewModel>>();
                    Dictionary<string, double> subTotalQty = new Dictionary<string, double>();
                    Dictionary<string, double> subTotalAmount = new Dictionary<string, double>();

                foreach (CostCalculationGarmentBySectionReportViewModel item in Query.ToList())
                {
                    string BrandName = item.BrandName;
                    if (!dataByBrand.ContainsKey(BrandName)) dataByBrand.Add(BrandName, new List<CostCalculationGarmentBySectionReportViewModel> { });
                        dataByBrand[BrandName].Add(new CostCalculationGarmentBySectionReportViewModel
                        {
                            RO_Number = item.RO_Number,
                            ConfirmDate = item.ConfirmDate,
                            DeliveryDate = item.DeliveryDate,
                            UnitName = item.UnitName,
                            Description = item.Description,
                            Section = item.Section,
                            SectionName = item.SectionName,                        
                            Article = item.Article,
                            BuyerCode = item.BuyerCode,
                            BuyerName = item.BuyerName,
                            BrandCode = item.BrandCode,
                            BrandName = item.BrandName,
                            Comodity = item.Comodity,
                            Quantity = item.Quantity,
                            ConfirmPrice = item.ConfirmPrice,
                            UOMUnit = item.UOMUnit,
                            Amount = item.Amount,
                        });

                        if (!subTotalQty.ContainsKey(BrandName))
                        {
                            subTotalQty.Add(BrandName, 0);
                        };

                        if (!subTotalAmount.ContainsKey(BrandName))
                        {
                            subTotalAmount.Add(BrandName, 0);
                        };

                        subTotalQty[BrandName] += item.Quantity;
                        subTotalAmount[BrandName] += item.Amount;
                    }
                
                    double totalQty = 0;
                    double totalAmount = 0;

                    int rowPosition = 1;

                    foreach (KeyValuePair<string, List<CostCalculationGarmentBySectionReportViewModel>> BuyerBrand in dataByBrand)
                    {
                        string BrandCode = "";
                        int index = 0;
                        foreach (CostCalculationGarmentBySectionReportViewModel item in BuyerBrand.Value)
                        {
                            index++;

                            string CfrmDate = item.ConfirmDate == new DateTime(1970, 1, 1) ? "-" : item.ConfirmDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                            string ShipDate = item.DeliveryDate == new DateTime(1970, 1, 1) ? "-" : item.DeliveryDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                            string QtyOrder = string.Format("{0:N2}", item.Quantity);
                            string CnfrmPrice = string.Format("{0:N4}", item.ConfirmPrice);
                            string Amount = string.Format("{0:N2}", item.Amount);

                            result.Rows.Add(index, item.RO_Number, CfrmDate, ShipDate, item.UnitName, item.Section, item.SectionName, item.Article, item.Comodity, item.Description,
                                            item.BuyerCode, item.BuyerName, item.BrandCode, item.BrandName, QtyOrder, item.UOMUnit, CnfrmPrice, Amount);
                            rowPosition += 1;
                            BrandCode = item.BrandName;
                        }
                        result.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "SUB TOTAL", "", BrandCode, Math.Round(subTotalQty[BuyerBrand.Key], 2), "", "", Math.Round(subTotalAmount[BuyerBrand.Key], 2));

                        rowPosition += 1;
                        totalQty += subTotalQty[BuyerBrand.Key];
                        totalAmount += subTotalAmount[BuyerBrand.Key];
                    }
                        result.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "T O T A L", "", Math.Round(totalQty, 2), "", "", Math.Round(totalAmount, 2));
                        rowPosition += 1;
            }
            ExcelPackage package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("COST CALCULATION PER SEKSI");
            sheet.Cells["A1"].LoadFromDataTable(result, true, OfficeOpenXml.Table.TableStyles.Light16);

            sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
            MemoryStream streamExcel = new MemoryStream();
            package.SaveAs(streamExcel);

            string fileName = string.Concat("Cost Calculation Per Seksi", ".xlsx");

            return Tuple.Create(streamExcel, fileName);
        }

        public Tuple<List<CostCalculationGarmentBySectionReportViewModel>, int> Read(int page = 1, int size = 25, string filter = "{}")
        {
            var Query = CostCalculationBySectionReportLogic.GetQuery(filter);
            var data = Query.ToList();

            int TotalData = data.Count();
            return Tuple.Create(data, TotalData);
        }     
    }
}
