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
    public class CCROGarmentHistoryBySectionReportFacade : ICCROGarmentHistoryBySectionReport
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<CostCalculationGarment> DbSet;
        private IdentityService IdentityService;
        private CCROGarmentHistoryBySectionReportLogic CCROGarmentHistoryBySectionReportLogic;

        public CCROGarmentHistoryBySectionReportFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<CostCalculationGarment>();
            this.IdentityService = serviceProvider.GetService<IdentityService>();
            this.CCROGarmentHistoryBySectionReportLogic = serviceProvider.GetService<CCROGarmentHistoryBySectionReportLogic>();
        }
        
        public Tuple<MemoryStream, string> GenerateExcel(string filter = "{}")
        {
            var Query = CCROGarmentHistoryBySectionReportLogic.GetQuery(filter);
            var data = Query.ToList();
            DataTable result = new DataTable();
            var offset = 7;

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "No RO", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Seksi", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kode Buyer", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Buyer", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tipe Buyer", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Article", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Shipment", DataType = typeof(String) });

            result.Columns.Add(new DataColumn() { ColumnName = "Validasi CC Kabag Md", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "validasi CC IE", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Validasi CC Purch", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Validasi CC Kadiv Md", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Validasi RO Md", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Validasi RO Sample", DataType = typeof(String) });
            
            Dictionary<string, string> Rowcount = new Dictionary<string, string>();
            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", ""); // to allow column name to be generated properly for empty data as template
            else
            {
                int index = 0;
                foreach (CCROGarmentHistoryBySectionReportViewModel item in data)
                {
                    index++;
                    string ApprovedDate1 = item.DeliveryDate == new DateTime(1970, 1, 1) ? "-" : item.DeliveryDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    string ApprovedDate2 = item.ApprovalMDDate == null ? "-" : item.ApprovalMDDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    string ApprovedDate3 = item.ApprovalIEDate == null ? "-" : item.ApprovalIEDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    string ApprovedDate4 = item.ApprovalPurchDate == null ? "-" : item.ApprovalPurchDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    string ApprovedDate5 = item.ApprovalKadivMDDate == null ? "-" : item.ApprovalKadivMDDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    string ApprovedDate6 = item.ValidatedMDDate == null ? "-" : item.ValidatedMDDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    string ApprovedDate7 = item.ValidatedSampleDate == null ? "-" : item.ValidatedSampleDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));

                    result.Rows.Add(index, item.RO_Number, item.Section, item.BrandCode, item.BrandName, item.Type, item.Article, ApprovedDate1,
                                    ApprovedDate2, ApprovedDate3, ApprovedDate4, ApprovedDate5, ApprovedDate6, ApprovedDate7);
                }
            }  
            ExcelPackage package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("CC RO Garment History");
            sheet.Cells["A1"].LoadFromDataTable(result, true, OfficeOpenXml.Table.TableStyles.Light16);

            sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
            MemoryStream streamExcel = new MemoryStream();
            package.SaveAs(streamExcel);

            string fileName = string.Concat("CC RO Garment History", ".xlsx");

            return Tuple.Create(streamExcel, fileName);
        }

        public Tuple<List<CCROGarmentHistoryBySectionReportViewModel>, int> Read(int page = 1, int size = 25, string filter = "{}")
        {
            var Query = CCROGarmentHistoryBySectionReportLogic.GetQuery(filter);
            var data = Query.ToList();

            int TotalData = data.Count();
            return Tuple.Create(data, TotalData);
        }     
    }
}
