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
    public class DetailCMGarmentByUnitReportFacade : IDetailCMGarmentByUnitReport
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<CostCalculationGarment> DbSet;
        private IdentityService IdentityService;
        private DetailCMGarmentByUnitReportLogic DetailCMGarmentByUnitReportLogic;

        public DetailCMGarmentByUnitReportFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<CostCalculationGarment>();
            this.IdentityService = serviceProvider.GetService<IdentityService>();
            this.DetailCMGarmentByUnitReportLogic = serviceProvider.GetService<DetailCMGarmentByUnitReportLogic>();
        }
        
        public Tuple<MemoryStream, string> GenerateExcel(string filter = "{}")
        {

            Dictionary<string, string> FilterDictionary = new Dictionary<string, string>(JsonConvert.DeserializeObject<Dictionary<string, string>>(filter), StringComparer.OrdinalIgnoreCase);

            var Query = DetailCMGarmentByUnitReportLogic.GetQuery(filter);
            var data = Query.ToList();
            DataTable result = new DataTable();
            var offset = 7;

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Unit", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kode Agent", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Agent", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kode Brand", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Brand", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "No RO", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Article", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Shipment", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jumlah", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "FOB Price", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "CM IDR", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "CM USD", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "OTL 1", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "OTL 2", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "SMV Cutting", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "SMV Sewing", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "SMV Finisihing", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "SMV Total", DataType = typeof(String) });

            Dictionary<string, string> Rowcount = new Dictionary<string, string>();
            if (Query.ToArray().Count() == 0)
                     result.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""); // to allow column name to be generated properly for empty data as template
            else
            {
                    int index = 0;
                    foreach (DetailCMGarmentByUnitReportViewModel item in data)
                    {
                        index++;
                        string ShipDate = item.DeliveryDate == new DateTime(1970, 1, 1) ? "-" : item.DeliveryDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                        string QtyOrder = string.Format("{0:N2}", item.Quantity);
                        string SMVC = string.Format("{0:N2}", item.SMV_Cutting);
                        string SMVS = string.Format("{0:N2}", item.SMV_Sewing);
                        string SMVF = string.Format("{0:N2}", item.SMV_Finishing);
                        string SMVT = string.Format("{0:N2}", item.SMV_Total);
                        string OTL_1 = string.Format("{0:N2}", item.OTL1);
                        string OTL_2 = string.Format("{0:N2}", item.OTL2);
                        //var FOB = (item.FOB_Price + ((item.CMPrice / item.CurrencyRate) * 1.05)) - (item.Insurance + item.Freight);
                        string FOBPrc = string.Format("{0:N4}", item.FOB_Price);
                        //var CM = (item.FOB_Price * item.CurrencyRate) - (item.BudgetAmount / item.Quantity) - item.Commission;
                        string CM_IDR = string.Format("{0:N2}", item.CMIDR);
                        string CM_USD = string.Format("{0:N4}", item.CM);


                    result.Rows.Add(index, item.UnitName, item.BuyerCode, item.BuyerName, item.BrandCode, item.BrandName, item.RO_Number, item.Article,
                                        ShipDate, QtyOrder, item.UOMUnit, FOBPrc, CM_IDR, CM_USD, OTL_1, OTL_2, SMVC, SMVS, SMVF, SMVT);
                }
            }
            ExcelPackage package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("Detail CM");
            sheet.Cells["A1"].LoadFromDataTable(result, true, OfficeOpenXml.Table.TableStyles.Light16);

            sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
            MemoryStream streamExcel = new MemoryStream();
            package.SaveAs(streamExcel);

            string fileName = string.Concat("Detail CM Per Unit", ".xlsx");

            return Tuple.Create(streamExcel, fileName);
        }

        public Tuple<List<DetailCMGarmentByUnitReportViewModel>, int> Read(int page = 1, int size = 25, string filter = "{}")
        {
            var Query = DetailCMGarmentByUnitReportLogic.GetQuery(filter);
            var data = Query.ToList();

            int TotalData = data.Count();
            return Tuple.Create(data, TotalData);
        }     
    }
}
