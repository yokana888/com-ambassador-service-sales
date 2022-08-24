using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.Garment;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.Garment;
using Com.Ambassador.Service.Sales.Lib.Helpers;
using Com.Ambassador.Service.Sales.Lib.ViewModels.Garment;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.Garment
{
    public class GarmentProductionOrderReportFacade : IGarmentProductionOrderReportFacade
    {
        private GarmentProductionOrderReportLogic logic;

        public GarmentProductionOrderReportFacade(IServiceProvider serviceProvider)
        {
            logic = serviceProvider.GetService<GarmentProductionOrderReportLogic>();
        }

        public Tuple<MemoryStream, string> GenerateExcel(string filter = "{}")
        {
            var Query = logic.GetQuery(filter);
            var data = Query.ToList();

            DataTable result = new DataTable();
            result.Columns.Add(new DataColumn() { ColumnName = "Week", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Seksi", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Komoditi", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Artikel", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No RO", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Cost Calculation", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Quantity", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Confirm Price", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Amount", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Term Pembayaran", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Confirm", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Shipment", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Validasi Kadiv Md", DataType = typeof(string) });

            List<(string, Enum, Enum)> mergeCells = new List<(string, Enum, Enum)>() { };

            int rowPosition = 2;
            if (data != null && data.Count > 0)
            {
                var grandTotalByUom = new List<TotalByUom>();

                foreach (var d in data)
                {
                    var weekFirstMergedRowPosition = rowPosition;
                    var weekLastMergedRowPosition = rowPosition;

                    foreach (var buyer in d.Buyers)
                    {
                        var buyerFirstMergedRowPosition = rowPosition;
                        var buyerLastMergedRowPosition = rowPosition;

                        foreach (var detail in buyer.Details)
                        {
                            result.Rows.Add(d.Week, buyer.Buyer, detail.Section, detail.Commodity, detail.Article, detail.RONo, detail.Date.ToString("dd MMMM yyyy", new CultureInfo("id-ID")), detail.Quantity, detail.Uom, detail.ConfirmPrice, detail.Amount, detail.TermPayment, detail.ConfirmDate.ToString("dd MMMM yyyy", new CultureInfo("id-ID")), detail.ShipmentDate.ToString("dd MMMM yyyy", new CultureInfo("id-ID")), detail.ValidationPPIC);

                            buyerLastMergedRowPosition = rowPosition++;

                            var currentUom = grandTotalByUom.FirstOrDefault(c => c.uom == detail.Uom);
                            if (currentUom == null)
                            {
                                grandTotalByUom.Add(new TotalByUom
                                {
                                    uom = detail.Uom,
                                    quantity = detail.Quantity,
                                    amount = detail.Amount
                                });
                            }
                            else
                            {
                                currentUom.quantity += detail.Quantity;
                                currentUom.amount += detail.Amount;
                            }
                        }

                        result.Rows.Add(null, "SUB TOTAL", null, null, null, null, null, buyer.Quantities, null, null, buyer.Amounts, null, null, null, null);

                        mergeCells.Add(($"B{rowPosition}:G{rowPosition}", ExcelHorizontalAlignment.Right, ExcelVerticalAlignment.Bottom));

                        if (buyerFirstMergedRowPosition != buyerLastMergedRowPosition)
                        {
                            mergeCells.Add(($"B{buyerFirstMergedRowPosition}:B{buyerLastMergedRowPosition}", ExcelHorizontalAlignment.Left, ExcelVerticalAlignment.Top));
                        }

                        weekLastMergedRowPosition = rowPosition++;
                    }

                    if (weekFirstMergedRowPosition != weekLastMergedRowPosition)
                    {
                        mergeCells.Add(($"A{weekFirstMergedRowPosition}:A{weekLastMergedRowPosition}", ExcelHorizontalAlignment.Left, ExcelVerticalAlignment.Top));
                    }
                }

                result.Rows.Add(null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
                result.Rows.Add(null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

                rowPosition++;
                foreach (var i in Enumerable.Range(0, grandTotalByUom.Count))
                {
                    if (i == 0)
                    {
                        result.Rows.Add(null, null, "GRAND TOTAL", grandTotalByUom[i].quantity, grandTotalByUom[i].uom, grandTotalByUom[i].amount, null, null, "GRAND TOTAL", data.Sum(d => d.Buyers.Sum(b => b.Details.Sum(dtl => dtl.Amount))), null, null, null, null, null);
                    }
                    else
                    {
                        result.Rows.Add(null, null, null, grandTotalByUom[i].quantity, grandTotalByUom[i].uom, grandTotalByUom[i].amount, null, null, null, null, null, null, null, null, null);
                    }
                    mergeCells.Add(($"D{++rowPosition}:D{rowPosition}", ExcelHorizontalAlignment.Right, ExcelVerticalAlignment.Bottom));
                }
            }
            else
            {
                result.Rows.Add(null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
            }

            var excel = Excel.CreateExcel(new List<(DataTable, string, List<(string, Enum, Enum)>)>() { (result, "OrderProduksi", mergeCells) }, false);

            return new Tuple<MemoryStream, string>(excel, string.Concat("Laporan Order Produksi", GetSuffixNameFromFilter(filter)));
        }

        private string GetSuffixNameFromFilter(string filterString)
        {
            Dictionary<string, string> filter = JsonConvert.DeserializeObject<Dictionary<string, string>>(filterString);

            return string.Join(null, filter.Select(s => $" - {s.Value}").ToArray());
        }

        public Tuple<List<GarmentProductionOrderReportViewModel>, int> Read(int page = 1, int size = 25, string filter = "{}")
        {
            var Query = logic.GetQuery(filter);
            var data = Query.ToList();

            return Tuple.Create(data, data.Count());
        }

        private class TotalByUom {
            public string uom { get; set; }
            public double quantity { get; set; }
            public double amount { get; set; }
        }
    }
}
