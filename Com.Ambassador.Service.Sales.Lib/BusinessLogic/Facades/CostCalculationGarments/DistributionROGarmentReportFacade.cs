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
    public class DistributionROGarmentReportFacade :  IDistributionROGarmentReport
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<CostCalculationGarment> DbSet;
        private IdentityService IdentityService;
        private DistributionROGarmentReportLogic DistributionROGarmentReportLogic;

        public DistributionROGarmentReportFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<CostCalculationGarment>();
            this.IdentityService = serviceProvider.GetService<IdentityService>();
            this.DistributionROGarmentReportLogic = serviceProvider.GetService<DistributionROGarmentReportLogic>();
        }
        
        public Tuple<MemoryStream, string> GenerateExcel(string filter = "{}")
        {

            Dictionary<string, string> FilterDictionary = new Dictionary<string, string>(JsonConvert.DeserializeObject<Dictionary<string, string>>(filter), StringComparer.OrdinalIgnoreCase);

            var Query = DistributionROGarmentReportLogic.GetQuery(filter);
            var data = Query.ToList();
            DataTable result = new DataTable();
            var offset = 7;

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Distribusi", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nomor R/O", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kode Agent", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Agent", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kode Brand", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Brand", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Article", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "CUTTING", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "SPL", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "QC SPL", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "GD FABRIC", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "GD ACC SEW", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "QC BULK (SEW)", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "ADM SEW", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "QC FINAL", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "QA IN LINE", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "QC BULK (FNS)", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "QA FINAL", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "FNSH", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "GD PACK", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "MD", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "NOHI/NISSENKEN", DataType = typeof(String) });


            Dictionary<string, string> Rowcount = new Dictionary<string, string>();
            if (Query.ToArray().Count() == 0)
                     result.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""); // to allow column name to be generated properly for empty data as template
            else
            {
                int index = 0;
                foreach (DistributionROGarmentReportViewModel item in data)
                {
                            index++;
                            string DistDate = item.DistributionDate == new DateTime(1970, 1, 1) ? "-" : item.DistributionDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));

                            result.Rows.Add(index, DistDate, item.RO_Number, item.BuyerCode, item.BuyerName, item.BrandCode, item.BrandName, item.Article,
                                            "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                }
            }
            ExcelPackage package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("DISTRIBUSI RO GARMENT");
            sheet.Cells["A1"].LoadFromDataTable(result, true, OfficeOpenXml.Table.TableStyles.Light16);

            sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
            MemoryStream streamExcel = new MemoryStream();
            package.SaveAs(streamExcel);

            string fileName = string.Concat("Distribusi RO Garment", ".xlsx");

            return Tuple.Create(streamExcel, fileName);

        }

        public Tuple<List<DistributionROGarmentReportViewModel>, int> Read(int page = 1, int size = 25, string filter = "{}")
        {
            var Query = DistributionROGarmentReportLogic.GetQuery(filter);
            var data = Query.ToList();

            int TotalData = data.Count();
            return Tuple.Create(data, TotalData);
        }     
    }
}
