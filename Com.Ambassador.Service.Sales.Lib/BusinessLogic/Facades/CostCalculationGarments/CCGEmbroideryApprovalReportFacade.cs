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
    public class CCGEmbroideryApprovalReportFacade : ICCGEmbroideryApprovalReport
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<CostCalculationGarment> DbSet;
        private IdentityService IdentityService;
        private CCGEmbroideryApprovalReportLogic CCGEmbroideryApprovalReportLogic;

        public CCGEmbroideryApprovalReportFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<CostCalculationGarment>();
            this.IdentityService = serviceProvider.GetService<IdentityService>();
            this.CCGEmbroideryApprovalReportLogic = serviceProvider.GetService<CCGEmbroideryApprovalReportLogic>();
        }
        
        public Tuple<MemoryStream, string> GenerateExcel(string filter = "{}")
        {
            var Query = CCGEmbroideryApprovalReportLogic.GetQuery(filter);
            var data = Query.ToList();
            DataTable result = new DataTable();
            var offset = 7;

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "No RO", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Konfeksi", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Seksi", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kode Buyer", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Buyer", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Article", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Qty Order", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Satuan Order", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Shipmnet", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Validasi", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Plan PO", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kode Barang", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Deskripsi Barang", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Qty Budget", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Satuan Beli", DataType = typeof(String) });

            Dictionary<string, string> Rowcount = new Dictionary<string, string>();
            if (Query.ToArray().Count() == 0)
                     result.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""); // to allow column name to be generated properly for empty data as template
            else
            {
                int index = 0;
                foreach (CCGEmbroideryApprovalReportViewModel item in data)
                {
                    index++;

                    string ShipDate = item.DeliveryDate == new DateTime(1970, 1, 1) ? "-" : item.DeliveryDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    string VldDate = item.ValidatedDate == new DateTime(1970, 1, 1) ? "-" : item.ValidatedDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));

                    string QtyOrder = string.Format("{0:N2}", item.Quantity);
                    string QtyBudget = string.Format("{0:N2}", item.BudgetQuantity);

                    result.Rows.Add(index, item.RO_Number, item.Section, item.UnitName, item.BuyerCode, item.BuyerName, item.Article, QtyOrder, item.UOMUnit,
                                    ShipDate, VldDate, item.PONumber, item.ProductCode, item.ProductName, QtyBudget, item.BudgetUOM);
                }
            }
            ExcelPackage package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("Validasi Job Order Embro");
            sheet.Cells["A1"].LoadFromDataTable(result, true, OfficeOpenXml.Table.TableStyles.Light16);

            sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
            MemoryStream streamExcel = new MemoryStream();
            package.SaveAs(streamExcel);

            string fileName = string.Concat("Validasi Job Order Embro", ".xlsx");

            return Tuple.Create(streamExcel, fileName);
        }

        public Tuple<List<CCGEmbroideryApprovalReportViewModel>, int> Read(int page = 1, int size = 25, string filter = "{}")
        {
            var Query = CCGEmbroideryApprovalReportLogic.GetQuery(filter);
            var data = Query.ToList();

            int TotalData = data.Count();
            return Tuple.Create(data, TotalData);
        }     
    }
}
