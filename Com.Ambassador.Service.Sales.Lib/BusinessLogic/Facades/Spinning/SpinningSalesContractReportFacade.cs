using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.Spinning;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.Spinning;
using Com.Ambassador.Service.Sales.Lib.Helpers;
using Com.Ambassador.Service.Sales.Lib.Models.Spinning;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.Spinning;
using Com.Moonlay.NetCore.Lib;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.Spinning
{
    public class SpinningSalesContractReportFacade
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<SpinningSalesContractModel> DbSet;
        private IdentityService IdentityService;
        private SpinningSalesContractLogic SpinningSalesContractLogic;
        public SpinningSalesContractReportFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<SpinningSalesContractModel>();
            this.IdentityService = serviceProvider.GetService<IdentityService>();
            this.SpinningSalesContractLogic = serviceProvider.GetService<SpinningSalesContractLogic>();
        }
        public IQueryable<SpinningSalesContractReportViewModel> GetReportQuery(string no, string buyerCode, string comodityCode, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

            var Query = (from a in DbContext.SpinningSalesContract
                             //Conditions
                         where a.IsDeleted == false
                             && a.SalesContractNo == (string.IsNullOrWhiteSpace(no) ? a.SalesContractNo : no)
                             && a.BuyerCode == (string.IsNullOrWhiteSpace(buyerCode) ? a.BuyerCode : buyerCode)
                             && a.ComodityCode == (string.IsNullOrWhiteSpace(comodityCode) ? a.ComodityCode : comodityCode)
                             && a.CreatedUtc.AddHours(offset).Date >= DateFrom.Date
                             && a.CreatedUtc.AddHours(offset).Date <= DateTo.Date
                         select new SpinningSalesContractReportViewModel
                         {
                             CreatedUtc=a.CreatedUtc,
                             salesContractNo=a.SalesContractNo,
                             comission=a.Comission,
                             comodityName=a.ComodityName,
                             buyerName=a.BuyerName,
                             buyerType=a.BuyerType,
                             agentName=a.AgentName,
                             paymentTo = a.AccountBankName + " - " + a.BankName + " - " + a.AccountBankNumber + " - " + a.AccountCurrencyCode,
                             accountCurrencyCode=a.AccountCurrencyCode,
                             deliverySchedule=a.DeliverySchedule,
                             dispositionNo=a.DispositionNumber,
                             orderQuantity=a.OrderQuantity,
                             price=a.Price,
                             qualityName=a.QualityName,
                             shippingQuantityTolerance=a.ShippingQuantityTolerance,
                             termOfPaymentName=a.TermOfPaymentName,
                             uomUnit=a.UomUnit,
                             LastModifiedUtc = a.LastModifiedUtc
                         });
            return Query;
        }

        public Tuple<List<SpinningSalesContractReportViewModel>, int> GetReport(string no, string buyerCode, string comodityCode, DateTime? dateFrom, DateTime? dateTo, int page, int size, string Order, int offset)
        {
            var Query = GetReportQuery(no,buyerCode,comodityCode, dateFrom, dateTo, offset);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Order);
            if (OrderDictionary.Count.Equals(0))
            {
                Query = Query.OrderByDescending(b => b.LastModifiedUtc);
            }

            Pageable<SpinningSalesContractReportViewModel> pageable = new Pageable<SpinningSalesContractReportViewModel>(Query, page - 1, size);
            List<SpinningSalesContractReportViewModel> Data = pageable.Data.ToList<SpinningSalesContractReportViewModel>();
            int TotalData = pageable.TotalCount;

            return Tuple.Create(Data, TotalData);
        }

        public MemoryStream GenerateExcel(string no, string buyerCode, string comodityCode, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetReportQuery(no, buyerCode, comodityCode, dateFrom, dateTo, offset);
            Query = Query.OrderByDescending(b => b.LastModifiedUtc);
            DataTable result = new DataTable();
            //No	Unit	Budget	Kategori	Tanggal PR	Nomor PR	Kode Barang	Nama Barang	Jumlah	Satuan	Tanggal Diminta Datang	Status	Tanggal Diminta Datang Eksternal


            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nomor Sales Contract", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tgl Sales Contract", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jenis Buyer", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nomor Disposisi", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Komoditas", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jumlah Order", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Toleransi (%)", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kualitas", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Harga", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Mata Uang", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Syarat Pembayaran", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Pembayaran ke Rekening", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jadwal Pengiriman", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Agen", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Komisi", DataType = typeof(String) });
            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "", "", "", "", 0, "", 0, "", 0, "", "", "", "", "", ""); // to allow column name to be generated properly for empty data as template
            else
            {
                int index = 0;
                foreach (var item in Query)
                {
                    index++;
                    DateTimeOffset date = item.deliverySchedule ?? new DateTime(1970, 1, 1);
                    string deliverySchedule = date == new DateTime(1970, 1, 1) ? "-" : date.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    result.Rows.Add(index, item.salesContractNo, item.CreatedUtc.ToString("dd MMM yyyy", new CultureInfo("id-ID")), item.buyerName, item.buyerType,item.dispositionNo, item.comodityName, item.orderQuantity,
                        item.uomUnit, item.shippingQuantityTolerance, item.qualityName, item.price, item.accountCurrencyCode,item.termOfPaymentName, item.paymentTo, deliverySchedule, item.agentName, item.comission);
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Territory") }, true);
        }
    }
}
