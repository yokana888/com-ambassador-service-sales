using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.SalesInvoice;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.SalesInvoice;
using Com.Ambassador.Service.Sales.Lib.Helpers;
using Com.Ambassador.Service.Sales.Lib.Models.SalesInvoice;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.SalesInvoice;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.SalesInvoice
{
    public class SalesInvoiceFacade : ISalesInvoiceContract
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<SalesInvoiceModel> DbSet;
        private IdentityService identityService;
        private readonly IServiceProvider _serviceProvider;
        private SalesInvoiceLogic salesInvoiceLogic;
        public SalesInvoiceFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            this.DbContext = dbContext;
            _serviceProvider = serviceProvider;
            this.DbSet = DbContext.Set<SalesInvoiceModel>();
            this.identityService = serviceProvider.GetService<IdentityService>();
            this.salesInvoiceLogic = serviceProvider.GetService<SalesInvoiceLogic>();
        }

        public async Task<int> CreateAsync(SalesInvoiceModel model)
        {
            int result = 0;
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    int index = 0;
                    do
                    {
                        model.Code = CodeGenerator.Generate();
                    }
                    while (DbSet.Any(d => d.Code.Equals(model.Code)));

                    SalesInvoiceNumberGenerator(model, index);
                    DeliveryOrderNumberGenerator(model);

                    salesInvoiceLogic.Create(model);
                    index++;

                    result = await DbContext.SaveChangesAsync();

                    if (model.SalesInvoiceCategory == "DYEINGPRINTING")
                    {
                        foreach (var detail in model.SalesInvoiceDetails)
                        {
                            var ItemIds = detail.SalesInvoiceItems.Select(s => s.ProductId).ToList();
                            UpdateTrueToShippingOut(detail.ShippingOutId, ItemIds);
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }

            return result;
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    SalesInvoiceModel model = await salesInvoiceLogic.ReadByIdAsync(id);
                    if (model != null)
                    {
                        SalesInvoiceModel salesInvoiceModel = new SalesInvoiceModel();
                        salesInvoiceModel = model;
                        await salesInvoiceLogic.DeleteAsync(id);
                    }

                    if (model.SalesInvoiceCategory == "DYEINGPRINTING")
                    {
                        foreach (var detail in model.SalesInvoiceDetails)
                        {
                            var ItemIds = detail.SalesInvoiceItems.Select(s => s.ProductId).ToList();
                            UpdateFalseToShippingOut(detail.ShippingOutId, ItemIds);
                        }
                    }


                    transaction.Commit();
                }
                catch (Exception e)
                {

                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<SalesInvoiceModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return salesInvoiceLogic.Read(page, size, order, select, keyword, filter);
        }

        public async Task<SalesInvoiceModel> ReadByIdAsync(int id)
        {
            return await salesInvoiceLogic.ReadByIdAsync(id);
        }

        public List<SalesInvoiceModel> ReadByBuyerId(int buyerId)
        {
            return salesInvoiceLogic.ReadByBuyerId(buyerId);
        }

        public async Task<int> UpdateAsync(int id, SalesInvoiceModel model)
        {
            salesInvoiceLogic.UpdateAsync(id, model);

            return await DbContext.SaveChangesAsync();
        }

        private void SalesInvoiceNumberGenerator(SalesInvoiceModel model, int index)
        {
            SalesInvoiceModel lastData = DbSet.IgnoreQueryFilters().Where(w => w.SalesInvoiceType.Equals(model.SalesInvoiceType)).OrderByDescending(o => o.AutoIncreament).FirstOrDefault();

            int YearNow = DateTime.Now.Year;
            var YearNowString = DateTime.Now.ToString("yy");

            if (lastData == null)
            {
                //Using this if SalesInvoiceType not declare index for each Type
                index = 0;

                //if (model.SalesInvoiceType == "BNG")
                //{
                //    index = 28;
                //}
                //else if (model.SalesInvoiceType == "BAB")
                //{
                //    index = 8;
                //}
                //else if (model.SalesInvoiceType == "BNS")
                //{
                //    index = 98;
                //}
                //else if (model.SalesInvoiceType == "RNG")
                //{
                //    index = 14;
                //}
                //else if (model.SalesInvoiceType == "BRG")
                //{
                //    index = 28;
                //}
                //else if (model.SalesInvoiceType == "BAG")
                //{
                //    index = 8;
                //}
                //else if (model.SalesInvoiceType == "BGS")
                //{
                //    index = 98;
                //}
                //else if (model.SalesInvoiceType == "RRG")
                //{
                //    index = 14;
                //}
                //else if (model.SalesInvoiceType == "BLL")
                //{
                //    index = 8;
                //}
                //else if (model.SalesInvoiceType == "BPF")
                //{
                //    index = 98;
                //}
                //else if (model.SalesInvoiceType == "BSF")
                //{
                //    index = 14;
                //}
                //else if (model.SalesInvoiceType == "RPF")
                //{
                //    index = 28;
                //}
                //else if (model.SalesInvoiceType == "BPR")
                //{
                //    index = 8;
                //}
                //else if (model.SalesInvoiceType == "BSR")
                //{
                //    index = 98;
                //}
                //else if (model.SalesInvoiceType == "RPR")
                //{
                //    index = 14;
                //}
                //else if (model.SalesInvoiceType == "BAV")
                //{
                //    index = 8;
                //}
                //else if (model.SalesInvoiceType == "BON")
                //{
                //    index = 98;
                //}
                //else if (model.SalesInvoiceType == "BGM")
                //{
                //    index = 14;
                //}
                //else if (model.SalesInvoiceType == "GPF")
                //{
                //    index = 28;
                //}
                //else if (model.SalesInvoiceType == "RGF")
                //{
                //    index = 8;
                //}
                //else if (model.SalesInvoiceType == "GPR")
                //{
                //    index = 98;
                //}
                //else if (model.SalesInvoiceType == "RGR")
                //{
                //    index = 14;
                //}
                //else if (model.SalesInvoiceType == "RON")
                //{
                //    index = 14;
                //}
                //else
                //{
                //    index = 0;
                //}

                model.AutoIncreament = 1 + index;
                model.SalesInvoiceNo = $"{YearNowString}{model.SalesInvoiceType}{model.AutoIncreament.ToString().PadLeft(6, '0')}";
            }
            else
            {
                if (YearNow > lastData.CreatedUtc.Year)
                {
                    model.AutoIncreament = 1 + index;
                    model.SalesInvoiceNo = $"{YearNowString}{model.SalesInvoiceType}{model.AutoIncreament.ToString().PadLeft(6, '0')}";
                }
                else
                {
                    model.AutoIncreament = lastData.AutoIncreament + (1 + index);
                    model.SalesInvoiceNo = $"{YearNowString}{model.SalesInvoiceType}{model.AutoIncreament.ToString().PadLeft(6, '0')}";
                }
            }
        }

        private void DeliveryOrderNumberGenerator(SalesInvoiceModel model)
        {
            SalesInvoiceModel lastData = DbSet.IgnoreQueryFilters().Where(w => w.SalesInvoiceType.Equals(model.SalesInvoiceType)).OrderByDescending(o => o.AutoIncreament).FirstOrDefault();

            int YearNow = DateTime.Now.Year;
            int MonthNow = DateTime.Now.Month;
            var YearNowString = DateTime.Now.ToString("yy");
            var MonthNowString = DateTime.Now.ToString("MM");
            var formatNo = $"{ model.AutoIncreament}/4.1.0/{MonthNowString}.{YearNowString}";

            if (model.SalesInvoiceType == "BNG")
            {
                model.DeliveryOrderNo = $"B.{formatNo}";
            }
            else if (model.SalesInvoiceType == "BAB")
            {
                model.DeliveryOrderNo = $"BB.{formatNo}";
            }
            else if (model.SalesInvoiceType == "BNS")
            {
                model.DeliveryOrderNo = $"BS.{formatNo}";
            }
            else if (model.SalesInvoiceType == "BRG")
            {
                model.DeliveryOrderNo = $"G.{formatNo}";
            }
            else if (model.SalesInvoiceType == "BAG")
            {
                model.DeliveryOrderNo = $"GG.{formatNo}";
            }
            else if (model.SalesInvoiceType == "BGS")
            {
                model.DeliveryOrderNo = $"GS.{formatNo}";
            }
            else if (model.SalesInvoiceType == "BLL")
            {
                model.DeliveryOrderNo = $"L.{formatNo}";
            }
            else if (model.SalesInvoiceType == "BPF")
            {
                model.DeliveryOrderNo = $"F.{formatNo}";
            }
            else if (model.SalesInvoiceType == "BSF")
            {
                model.DeliveryOrderNo = $"FS.{formatNo}";
            }
            else if (model.SalesInvoiceType == "BPR")
            {
                model.DeliveryOrderNo = $"P.{formatNo}";
            }
            else if (model.SalesInvoiceType == "BSR")
            {
                model.DeliveryOrderNo = $"PS.{formatNo}";
            }
            else if (model.SalesInvoiceType == "BAV")
            {
                model.DeliveryOrderNo = $"V.{formatNo}";
            }
            else if (model.SalesInvoiceType == "BON")
            {
                model.DeliveryOrderNo = $"O.{formatNo}";
            }
            else if (model.SalesInvoiceType == "BGM")
            {
                model.DeliveryOrderNo = $"M.{formatNo}";
            }
            else if (model.SalesInvoiceType == "GPF")
            {
                model.DeliveryOrderNo = $"F.{formatNo}";
            }
            else if (model.SalesInvoiceType == "GPR")
            {
                model.DeliveryOrderNo = $"P.{formatNo}";
            }
            else if (model.SalesInvoiceType == "RON")
            {
                model.DeliveryOrderNo = $"O.{formatNo}";
            }
            else if (model.SalesInvoiceType == "BMK")
            {
                model.DeliveryOrderNo = $"BM.{formatNo}";
            }
            else
            {
                model.DeliveryOrderNo = "";
            }
        }

        public async Task<int> UpdateFromSalesReceiptAsync(int id, SalesInvoiceUpdateModel model)

        {
            int Updated = 0;
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    salesInvoiceLogic.UpdateFromSalesReceiptAsync(id, model);
                    Updated = await DbContext.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }
            return Updated;
        }

        private async Task<List<SalesInvoiceReportViewModel>> GetReportQuery(int buyerId, long salesInvoiceId, bool? isPaidOff, DateTimeOffset? dateFrom, DateTimeOffset? dateTo, int offSet)
        {
            var httpClientService = (IHttpClientService)_serviceProvider.GetService(typeof(IHttpClientService));
            string uri = APIEndpoint.Finance + "/sales-receipts/sales-invoice-report";

            var query = DbSet.AsQueryable();
            if (buyerId != 0)
            {
                query = query.Where(s => s.BuyerId == buyerId);
            }

            if (salesInvoiceId != 0)
            {
                query = query.Where(s => s.Id == salesInvoiceId);
            }

            if (isPaidOff.HasValue)
            {
                query = query.Where(s => s.IsPaidOff == isPaidOff.GetValueOrDefault());
            }

            if (dateFrom.HasValue && dateTo.HasValue)
            {
                query = query.Where(s => dateFrom.Value.Date <= s.DueDate.AddHours(offSet).Date && s.DueDate.AddHours(offSet).Date <= dateTo.Value.Date);
            }
            else if (dateFrom.HasValue && !dateTo.HasValue)
            {
                query = query.Where(s => dateFrom.Value.Date <= s.DueDate.AddHours(offSet).Date);
            }
            else if (!dateFrom.HasValue && dateTo.HasValue)
            {
                query = query.Where(s => s.DueDate.AddHours(offSet).Date <= dateTo.Value.Date);
            }

            var stringContent = JsonConvert.SerializeObject(new
            {
                SalesInvoiceIds = query.Select(s => s.Id)
            });
            var httpContent = new StringContent(stringContent, Encoding.UTF8, General.JsonMediaType);

            var httpResponseMessage = await httpClientService.PostAsync(uri, httpContent);
            httpResponseMessage.EnsureSuccessStatusCode();

            var salesReceiptStringData = await httpResponseMessage.Content.ReadAsStringAsync();
            var salesReceiptData = JsonConvert.DeserializeObject<List<SalesInvoiceReportSalesReceiptViewModel>>(salesReceiptStringData);

            var result = query
                .OrderByDescending(s => s.LastModifiedUtc)
                .Select(s => new SalesInvoiceModel()
                {
                    Id = s.Id,
                    SalesInvoiceNo = s.SalesInvoiceNo,
                    IsPaidOff = s.IsPaidOff,
                    DueDate = s.DueDate,
                    SalesInvoiceDate = s.SalesInvoiceDate,
                    TotalPaid = s.TotalPaid,
                    TotalPayment = s.TotalPayment,
                    CurrencySymbol = s.CurrencySymbol,
                })
                .ToList()
                .Select(s => new SalesInvoiceReportViewModel()
                {
                    Id = s.Id,
                    SalesInvoiceNo = s.SalesInvoiceNo,
                    Status = s.IsPaidOff ? "Lunas" : "Belum Lunas",
                    Tempo = (s.DueDate - s.SalesInvoiceDate).Days + 1,
                    TotalPayment = s.TotalPayment,
                    TotalPaid = s.TotalPaid,
                    Unpaid = (s.TotalPayment - s.TotalPaid < 0) ? 0 : s.TotalPayment - s.TotalPaid,
                    CurrencySymbol = s.CurrencySymbol,
                    SalesReceipts = salesReceiptData.Where(d => d.SalesInvoiceId == s.Id).ToList()
                });

            return result.ToList();
        }

        public async Task<List<SalesInvoiceReportViewModel>> GetReport(int buyerId, long salesInvoiceId, bool? isPaidOff, DateTimeOffset? dateFrom, DateTimeOffset? dateTo, int offSet)
        {
            var data = await GetReportQuery(buyerId, salesInvoiceId, isPaidOff, dateFrom, dateTo, offSet);

            return data;
        }

        public async Task<MemoryStream> GenerateExcel(int buyerId, long salesInvoiceId, bool? isPaidOff, DateTimeOffset? dateFrom, DateTimeOffset? dateTo, int offSet)
        {
            string title = "Laporan Pembayaran Faktur",
                _dateFrom = dateFrom == null ? "-" : dateFrom.Value.ToString("dd MMMM yyyy"),
                _dateTo = dateTo == null ? "-" : dateTo.Value.ToString("dd MMMM yyyy");
            var data = await GetReportQuery(buyerId, salesInvoiceId, isPaidOff, dateFrom, dateTo, offSet);

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn() { ColumnName = "No Faktur", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Total Harga", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Pembayaran Sebelumnya", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jumlah Pembayaran", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Sisa Pembayaran", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Tanggal Pembayaran", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No Kuitansi", DataType = typeof(string) });

            int index = 0;
            if (data.Count == 0)
            {
                dt.Rows.Add("", 0.ToString("#,##0.#0"), 0.ToString("#,##0.#0"), 0.ToString("#,##0.#0"), 0.ToString("#,##0.#0"), "", "");
                index++;
            }
            else
            {
                data = data.OrderBy(s => s.Id).ToList();
                foreach (var item in data)
                {
                    foreach (var detail in item.SalesReceipts.OrderBy(s => s.SalesReceiptDate))
                    {
                        dt.Rows.Add(item.SalesInvoiceNo, string.Format("{0} {1}", item.CurrencySymbol, item.TotalPayment.ToString("#,##0.#0")),
                            string.Format("{0} {1}", detail.CurrencySymbol, detail.TotalPaid.ToString("#,##0.#0")), string.Format("{0} {1}", detail.CurrencySymbol, detail.Nominal.ToString("#,##0.#0")),
                            string.Format("{0} {1}", detail.CurrencySymbol, detail.UnPaid.ToString("#,##0.#0")), detail.SalesReceiptDate.ToOffset(new TimeSpan(offSet, 0, 0)).ToString("d/M/yyyy", new CultureInfo("id-ID")), detail.SalesReceiptNo);
                        index++;
                    }
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Kwitansi") }, title, _dateFrom, _dateTo, true, index);
        }

        private void UpdateTrueToShippingOut(long id, List<int> ItemIds)
        {
            string salesInvoiceUri = APIEndpoint.PackingInventory + "output-shipping/sales-invoice/";

            string Uri = $"{salesInvoiceUri}{id}";

            var data = new
            {
                HasSalesInvoice = true,
                ItemIds = ItemIds,
            };

            IHttpClientService httpClient = (IHttpClientService)this._serviceProvider.GetService(typeof(IHttpClientService));
            var response = httpClient.PutAsync(Uri, new StringContent(JsonConvert.SerializeObject(data).ToString(), Encoding.UTF8, General.JsonMediaType)).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(string.Format("{0}, {1}", response.StatusCode, response.Content));
            }
        }

        private void UpdateFalseToShippingOut(long id, List<int> ItemIds)
        {
            string salesInvoiceUri = APIEndpoint.PackingInventory + "output-shipping/sales-invoice/";

            string Uri = $"{salesInvoiceUri}{id}";

            var data = new
            {
                HasSalesInvoice = true,
                ItemIds = ItemIds,
            };

            IHttpClientService httpClient = (IHttpClientService)this._serviceProvider.GetService(typeof(IHttpClientService));
            var response = httpClient.PutAsync(Uri, new StringContent(JsonConvert.SerializeObject(data).ToString(), Encoding.UTF8, General.JsonMediaType)).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(string.Format("{0}, {1}", response.StatusCode, response.Content));
            }
        }
    }
}
