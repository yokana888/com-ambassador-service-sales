using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Com.Moonlay.NetCore.Lib;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.Helpers;
using OfficeOpenXml;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrinting
{
    public class FinishingPrintingSalesContractReportFacade
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<FinishingPrintingSalesContractModel> DbSet;
        private IdentityService IdentityService;
        private FinishingPrintingSalesContractLogic FinishingPrintingSalesContractLogic;
        public FinishingPrintingSalesContractReportFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<FinishingPrintingSalesContractModel>();
            this.IdentityService = serviceProvider.GetService<IdentityService>();
            this.FinishingPrintingSalesContractLogic = serviceProvider.GetService<FinishingPrintingSalesContractLogic>();
        }
        public IQueryable<FinishingPrintingSalesContractReportViewModel> GetReportQuery(string no, string buyerCode, string orderTypeCode, string comodityCode, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

            var Query = (from a in DbContext.FinishingPrintingSalesContracts
                         join b in DbContext.FinishingPrintingSalesContractDetails on a.Id equals b.FinishingPrintingSalesContract.Id
                         join c in DbContext.ProductionOrder on a.Id equals c.SalesContractId
                         join d in DbContext.ProductionOrder_Details on c.Id equals d.ProductionOrderModel.Id
                         //join c in DbContext.ProductionOrder on a.Id equals c.SalesContractId into d
                         //from po in d.DefaultIfEmpty()
                         //join e in DbContext.ProductionOrder_Details on po.Id equals e.ProductionOrderModel.Id into f
                         //from poDetail in f.DefaultIfEmpty()
                         //Conditions
                         where a.IsDeleted == false
                             && b.IsDeleted == false
                             //&& po.IsDeleted==false
                             //&& poDetail.IsDeleted==false
                             && a.SalesContractNo == (string.IsNullOrWhiteSpace(no) ? a.SalesContractNo : no)
                             && a.BuyerCode == (string.IsNullOrWhiteSpace(buyerCode) ? a.BuyerCode : buyerCode)
                             && a.OrderTypeCode == (string.IsNullOrWhiteSpace(orderTypeCode) ? a.OrderTypeCode : orderTypeCode)
                             && a.CommodityCode == (string.IsNullOrWhiteSpace(comodityCode) ? a.CommodityCode : comodityCode)
                             && a.CreatedUtc.AddHours(offset).Date >= DateFrom.Date
                             && a.CreatedUtc.AddHours(offset).Date <= DateTo.Date
                         select new FinishingPrintingSalesContractReportViewModel
                         {
                             CreatedUtc = a.CreatedUtc,
                             salesContractNo = a.SalesContractNo,
                             comission = a.Commission,
                             comodityName = a.CommodityName,
                             buyerName = a.BuyerName,
                             buyerType = a.BuyerType,
                             agentName = a.AgentName,
                             paymentTo = a.AccountBankName + " - " + a.AccountBankName + " - " + a.AccountBankNumber + " - " + a.AccountBankCurrencyCode,
                             accountCurrencyCode = a.AccountBankCurrencyCode,
                             deliverySchedule = a.DeliverySchedule,
                             dispositionNo = a.DispositionNumber,
                             orderQuantity = a.OrderQuantity,
                             price = b.Price,
                             qualityName = a.QualityName,
                             shippingQuantityTolerance = a.ShippingQuantityTolerance,
                             termOfPaymentName = a.TermOfPaymentName,
                             uomUnit = d.UomUnit,
                             LastModifiedUtc = a.LastModifiedUtc,
                             materialConstructionName = a.MaterialConstructionName,
                             materialName = a.MaterialName,
                             materialWidth = a.MaterialWidth,
                             orderType = a.OrderTypeName,
                             yarnMaterialName = a.YarnMaterialName,
                             color = b.Color,
                             useIncomeTax = a.UseIncomeTax == false ? "Tanpa PPN" : b.UseIncomeTax == true ? "Including PPN" : "Excluding PPN",
                             productionOrderQuantity = d.Quantity,
                             /*productionOrderQuantity = (from d in DbContext.ProductionOrder
                                                        join e in DbContext.ProductionOrder_Details
                                                        on d.Id equals e.ProductionOrderModel.Id
                                                        where d.SalesContractId == a.Id
                                                        && d.IsDeleted==false
                                                        && e.IsDeleted==false
                                                        select e.Quantity).Sum(),*/
                             sppOrderNo = c.OrderNo,
                             sppDate = c.CreatedUtc,
                             status = DbContext.ProductionOrder.Count(c => c.SalesContractId == a.Id && c.IsDeleted == false) > 0 ? "Sudah dibuat SPP" : "Belum dibuat SPP"
                         });
            return Query;
        }

        public Tuple<List<FinishingPrintingSalesContractReportViewModel>, int> GetReport(string no, string buyerCode, string orderTypeCode, string comodityCode, DateTime? dateFrom, DateTime? dateTo, int page, int size, string Order, int offset)
        {
            var Query = GetReportQuery(no, buyerCode, orderTypeCode, comodityCode, dateFrom, dateTo, offset);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Order);
            if (OrderDictionary.Count.Equals(0))
            {
                Query = Query.OrderByDescending(b => b.LastModifiedUtc);
            }

            Pageable<FinishingPrintingSalesContractReportViewModel> pageable = new Pageable<FinishingPrintingSalesContractReportViewModel>(Query, page - 1, size);
            List<FinishingPrintingSalesContractReportViewModel> Data = pageable.Data.ToList<FinishingPrintingSalesContractReportViewModel>();
            int TotalData = pageable.TotalCount;

            return Tuple.Create(Data, TotalData);
        }

        public MemoryStream GenerateExcel(string no, string buyerCode, string orderTypeCode, string comodityCode, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetReportQuery(no, buyerCode, orderTypeCode, comodityCode, dateFrom, dateTo, offset);
            Query = Query.OrderByDescending(b => b.LastModifiedUtc);
            DataTable result = new DataTable();
            //No	Unit	Budget	Kategori	Tanggal PR	Nomor PR	Kode Barang	Nama Barang	Jumlah	Satuan	Tanggal Diminta Datang	Status	Tanggal Diminta Datang Eksternal


            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nomor Sales Contract", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Sales Contract", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jenis Buyer", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nomor Disposisi", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jenis Order", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Komoditas", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Material", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Construction", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Benang", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Lebar Finish", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kualitas", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jumlah Order SC", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nomor Order SPP ", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jumlah sudah dibuatkan SPP", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Turun Order", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Toleransi (%)", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Syarat Pembayaran", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Pembayaran ke Rekening", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jadwal Pengiriman", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Agen", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Komisi", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Warna", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Harga", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Mata Uang", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "PPN", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Status", DataType = typeof(String) });
            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "", "", "", "","","","","","","", 0, 0, "", 0, "",      "", "", "", "", "",0,"","",""); // to allow column name to be generated properly for empty data as template
            else
            {
                int index = 0;
                foreach (var item in Query)
                {
                    index++;
                    DateTimeOffset date = item.deliverySchedule ?? new DateTime(1970, 1, 1);
                    string sppDate = item.sppDate == new DateTime(1970, 1, 1) || item.sppDate.Value.ToString("dd MMM yyyy") == "01 Jan 0001" ? "-" : item.sppDate.Value.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    string deliverySchedule = date == new DateTime(1970, 1, 1) ? "-" : date.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    result.Rows.Add(index, item.salesContractNo, item.CreatedUtc.ToString("dd MMM yyyy", new CultureInfo("id-ID")), item.buyerName, item.buyerType,
                        item.dispositionNo, item.orderType, item.comodityName, item.materialName, item.materialConstructionName, item.yarnMaterialName, item.materialWidth,
                        item.qualityName, item.orderQuantity, item.sppOrderNo, item.productionOrderQuantity, item.uomUnit, sppDate, item.shippingQuantityTolerance, item.termOfPaymentName, item.paymentTo, deliverySchedule,
                        item.agentName, item.comission, item.color, item.price, item.accountCurrencyCode, item.useIncomeTax, item.status);
                }
            }

            ExcelPackage package = new ExcelPackage();
            bool styling = true;
            var Data = Query;

            foreach (KeyValuePair<DataTable, String> item in new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Territory") })
            {
                var sheet = package.Workbook.Worksheets.Add(item.Value);
                sheet.Cells["A1"].LoadFromDataTable(item.Key, true, (styling == true) ? OfficeOpenXml.Table.TableStyles.Light16 : OfficeOpenXml.Table.TableStyles.None);

                Dictionary<string, int> counts = new Dictionary<string, int>();
                Dictionary<string, int> countsType = new Dictionary<string, int>();
                //var docNo = Data.ToArray();
                int value;

                foreach (var a in Data)
                {
                    if (counts.TryGetValue(a.salesContractNo, out value))
                    {
                        counts[a.salesContractNo]++;
                    }
                    else
                    {
                        counts[a.salesContractNo] = 1;
                    }
                }

                int index = 2;
                foreach (KeyValuePair<string, int> b in counts)
                {
                    sheet.Cells["A" + index + ":A" + (index + b.Value - 1)].Merge = true;
                    sheet.Cells["A" + index + ":A" + (index + b.Value - 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                    sheet.Cells["B" + index + ":B" + (index + b.Value - 1)].Merge = true;
                    sheet.Cells["B" + index + ":B" + (index + b.Value - 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                    sheet.Cells["C" + index + ":C" + (index + b.Value - 1)].Merge = true;
                    sheet.Cells["C" + index + ":C" + (index + b.Value - 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                    sheet.Cells["D" + index + ":D" + (index + b.Value - 1)].Merge = true;
                    sheet.Cells["D" + index + ":D" + (index + b.Value - 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                    sheet.Cells["E" + index + ":E" + (index + b.Value - 1)].Merge = true;
                    sheet.Cells["E" + index + ":E" + (index + b.Value - 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                    sheet.Cells["F" + index + ":F" + (index + b.Value - 1)].Merge = true;
                    sheet.Cells["F" + index + ":F" + (index + b.Value - 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                    sheet.Cells["G" + index + ":G" + (index + b.Value - 1)].Merge = true;
                    sheet.Cells["G" + index + ":G" + (index + b.Value - 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                    sheet.Cells["H" + index + ":H" + (index + b.Value - 1)].Merge = true;
                    sheet.Cells["H" + index + ":H" + (index + b.Value - 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                    sheet.Cells["I" + index + ":I" + (index + b.Value - 1)].Merge = true;
                    sheet.Cells["I" + index + ":I" + (index + b.Value - 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                    sheet.Cells["J" + index + ":J" + (index + b.Value - 1)].Merge = true;
                    sheet.Cells["J" + index + ":J" + (index + b.Value - 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                    sheet.Cells["K" + index + ":K" + (index + b.Value - 1)].Merge = true;
                    sheet.Cells["K" + index + ":K" + (index + b.Value - 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                    sheet.Cells["L" + index + ":L" + (index + b.Value - 1)].Merge = true;
                    sheet.Cells["L" + index + ":L" + (index + b.Value - 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                    sheet.Cells["M" + index + ":M" + (index + b.Value - 1)].Merge = true;
                    sheet.Cells["M" + index + ":M" + (index + b.Value - 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                    sheet.Cells["N" + index + ":N" + (index + b.Value - 1)].Merge = true;
                    sheet.Cells["N" + index + ":N" + (index + b.Value - 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                    sheet.Cells["S" + index + ":S" + (index + b.Value - 1)].Merge = true;
                    sheet.Cells["S" + index + ":S" + (index + b.Value - 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                    sheet.Cells["T" + index + ":T" + (index + b.Value - 1)].Merge = true;
                    sheet.Cells["T" + index + ":T" + (index + b.Value - 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                    sheet.Cells["U" + index + ":U" + (index + b.Value - 1)].Merge = true;
                    sheet.Cells["U" + index + ":U" + (index + b.Value - 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                    sheet.Cells["V" + index + ":V" + (index + b.Value - 1)].Merge = true;
                    sheet.Cells["V" + index + ":V" + (index + b.Value - 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                    sheet.Cells["W" + index + ":W" + (index + b.Value - 1)].Merge = true;
                    sheet.Cells["W" + index + ":W" + (index + b.Value - 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                    sheet.Cells["X" + index + ":X" + (index + b.Value - 1)].Merge = true;
                    sheet.Cells["X" + index + ":X" + (index + b.Value - 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                    sheet.Cells["Y" + index + ":Y" + (index + b.Value - 1)].Merge = true;
                    sheet.Cells["Y" + index + ":Y" + (index + b.Value - 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                    sheet.Cells["Z" + index + ":Z" + (index + b.Value - 1)].Merge = true;
                    sheet.Cells["Z" + index + ":Z" + (index + b.Value - 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                    sheet.Cells["AA" + index + ":AA" + (index + b.Value - 1)].Merge = true;
                    sheet.Cells["AA" + index + ":AA" + (index + b.Value - 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                    sheet.Cells["AB" + index + ":AB" + (index + b.Value - 1)].Merge = true;
                    sheet.Cells["AB" + index + ":AB" + (index + b.Value - 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                    sheet.Cells["AC" + index + ":AC" + (index + b.Value - 1)].Merge = true;
                    sheet.Cells["AC" + index + ":AC" + (index + b.Value - 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                    index += b.Value;

                    sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
                }
            }

            MemoryStream stream = new MemoryStream();
            package.SaveAs(stream);
            return stream;
            
            //return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Territory") }, true);
        }
    }
}
