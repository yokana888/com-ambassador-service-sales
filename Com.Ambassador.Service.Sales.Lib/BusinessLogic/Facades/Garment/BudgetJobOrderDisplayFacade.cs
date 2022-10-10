using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.Garment;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.Garment;
using Com.Ambassador.Service.Sales.Lib.Helpers;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.ViewModels.Garment;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.Garment
{
    public class BudgetJobOrderDisplayFacade : IBudgetJobOrderDisplayFacade
    {
        private readonly SalesDbContext DbContext;
        private IdentityService IdentityService;
        private BudgetJobOrderDisplayLogic budgetJobOrderDisplayLogic;

        public BudgetJobOrderDisplayFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            DbContext = dbContext;
            IdentityService = serviceProvider.GetService<IdentityService>();
            budgetJobOrderDisplayLogic = serviceProvider.GetService<BudgetJobOrderDisplayLogic>();
        }

        public Tuple<MemoryStream, string> GenerateExcel(string filter = "{}")
        {
            Dictionary<string, string> FilterDictionary = new Dictionary<string, string>(JsonConvert.DeserializeObject<Dictionary<string, string>>(filter), StringComparer.OrdinalIgnoreCase);

            var Query = budgetJobOrderDisplayLogic.GetQuery(filter);
            var data = Query.ToList();
            var offset = 7;

            DataTable result = new DataTable();
            result.Columns.Add(new DataColumn() { ColumnName = "Urut", DataType = typeof(int) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nomor RO", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kode Agent", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Agent Buyer", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kode Brand", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Brand Buyer", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tipe Buyer", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Artikel", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Komoditi", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Qty Order", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Shipment", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kode Barang", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Keterangan", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Detil Barang", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Quantity", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Satuan Barang", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Harga", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jumlah", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nomor PO", DataType = typeof(string) });

            foreach (var i in Enumerable.Range(0, (data ?? new List<BudgetJobOrderDisplayViewModel>()).Count()))
            {
                var d = data[i];
                string ShipDate = d.DeliveryDate == new DateTime(1970, 1, 1) ? "-" : d.DeliveryDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                string QtyOrder = string.Format("{0:N2}", d.Quantity);
                string BgtQty = string.Format("{0:N2}", d.BudgetQuantity);
                string BgtPrc = string.Format("{0:N2}", d.Price);
                string BgtAmt = string.Format("{0:N2}", d.BudgetQuantity * d.Price);

                result.Rows.Add(i + 1, d.RO_Number, d.BuyerCode, d.BuyerName, d.BrandCode, d.BrandName, d.Type, d.Article, d.ComodityCode, QtyOrder, d.UOMUnit, ShipDate,
                                d.ProductCode, d.Description, d.ProductRemark, BgtQty, d.UomPriceName, BgtPrc, BgtAmt, d.POSerialNumber);
            }

            var excel = Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, FilterDictionary.GetValueOrDefault("RONo")) }, false);

            return new Tuple<MemoryStream, string>(excel, string.Concat("Display Budget Job Order - ", FilterDictionary.GetValueOrDefault("RONo")));
        }

        public Tuple<List<BudgetJobOrderDisplayViewModel>, int> Read(int page = 1, int size = 25, string filter = "{}")
        {
            var Query = budgetJobOrderDisplayLogic.GetQuery(filter);
            var data = Query.ToList();
            return Tuple.Create(data, data.Count);
        }
    }
}
