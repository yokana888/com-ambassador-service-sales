using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.DOReturn
{
    public class DOReturnViewModel : BaseViewModel, IValidatableObject
    {
        public string Code { get; set; }
        public long? AutoIncreament { get; set; }
        public string DOReturnNo { get; set; }
        public string DOReturnType { get; set; }
        public DateTimeOffset? DOReturnDate { get; set; }
        public BuyerViewModel ReturnFrom { get; set; }
        public string LTKPNo { get; set; }
        public string HeadOfStorage { get; set; }
        public string Remark { get; set; }

        public virtual ICollection<DOReturnDetailViewModel> DOReturnDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(DOReturnType) || DOReturnType == "")
                yield return new ValidationResult("Kode Retur harus diisi", new List<string> { "DOReturnType" });

            if (!DOReturnDate.HasValue || DOReturnDate.Value > DateTimeOffset.Now)
                yield return new ValidationResult("Tgl Retur harus diisi & lebih kecil atau sama dengan hari ini", new List<string> { "DOReturnDate" });

            if (ReturnFrom == null || string.IsNullOrWhiteSpace(ReturnFrom.Name))
                yield return new ValidationResult("Return dari harus diisi", new List<string> { "ReturnFrom" });

            if (string.IsNullOrWhiteSpace(LTKPNo))
                yield return new ValidationResult("No. LTKP harus diisi", new List<string> { "LTKPNo" });

            if (string.IsNullOrWhiteSpace(HeadOfStorage))
                yield return new ValidationResult("Nama Kepala Gudang harus diisi", new List<string> { "HeadOfStorage" });

            int Count = 0;
            string DetailErrors = "[";

            if (DOReturnDetails != null && DOReturnDetails.Count > 0)
            {
                foreach (var detail in DOReturnDetails)
                {
                    DetailErrors += "{";

                    var ErrorCount = 0;

                    if (detail.SalesInvoice == null || string.IsNullOrWhiteSpace(detail.SalesInvoice.SalesInvoiceNo))
                    {
                        Count++;
                        ErrorCount++;
                        DetailErrors += "SalesInvoiceNo : 'No. Ex. Faktur Penjualan kosong / tidak ditemukan',";
                    }

                    //if (detail.DOReturnItems != null && detail.DOReturnItems.Count > 0 && detail.DOReturnItems.All(s => s.SalesInvoiceItem != null) && detail.DOReturnItems.All(s => s.SalesInvoiceItem.QuantityItem <= 0))
                    if (detail.DOReturnItems != null && detail.DOReturnItems.Count > 0 && detail.DOReturnItems.All(s => s.QuantityItem <= 0))
                    {
                        Count++;
                        ErrorCount++;
                        DetailErrors += "TotalNotNull : 'Quantity Item tidak boleh kosong',";
                    }

                    var duplicate = DOReturnDetails.Where(w => w.SalesInvoice != null && detail.SalesInvoice != null && w.SalesInvoice.Id.Equals(detail.SalesInvoice.Id) && w.SalesInvoice.SalesInvoiceNo.Equals(detail.SalesInvoice.SalesInvoiceNo)).ToList();

                    if (duplicate.Count > 1)
                    {
                        Count++;
                        DetailErrors += "SalesInvoiceNo : 'No. Ex. Faktur Penjualan duplikat',";
                    }

                    if (ErrorCount == 0)
                    {
                        if (detail.DOReturnDetailItems == null || detail.DOReturnDetailItems.Count == 0)
                        {
                            Count++;
                            DetailErrors += "DOReturnDetailItem : 'Detail Item Kosong',";
                        }
                        else
                        {
                            DetailErrors += "DOReturnDetailItems: [";

                            foreach (var detailItem in detail.DOReturnDetailItems)
                            {
                                DetailErrors += "{";

                                //if (detailItem.DOSales == null || string.IsNullOrWhiteSpace(detailItem.DOSales.DOSalesNo))
                                if (string.IsNullOrWhiteSpace(detailItem.DOSalesNo))
                                {
                                    Count++;
                                    DetailErrors += "DOSales : 'DO Kosong',";
                                }
                                DetailErrors += "}, ";
                            }
                            DetailErrors += "], ";
                        }

                        if (detail.DOReturnItems == null || detail.DOReturnItems.Count == 0)
                        {
                            Count++;
                            DetailErrors += "DOReturnItem : 'Item Kosong',";
                        }
                        else
                        {
                            DetailErrors += "DOReturnItems: [";

                            foreach (var item in detail.DOReturnItems)
                            {
                                DetailErrors += "{";

                                //if (item.SalesInvoiceItem == null || item.SalesInvoiceItem.QuantityPacking <= 0)
                                if (!item.QuantityPacking.HasValue || item.QuantityPacking <= 0)
                                    {
                                    Count++;
                                    DetailErrors += "QuantityPacking : 'Quantity Packing Panjang tidak boleh lebih kecil atau sama dengan 0',";
                                }
                                //if (item.SalesInvoiceItem == null || item.SalesInvoiceItem.QuantityItem <= 0)
                                if (!item.QuantityItem.HasValue || item.QuantityItem <= 0)
                                {
                                    Count++;
                                    DetailErrors += "QuantityItem : 'Quantity Item Panjang tidak boleh lebih kecil atau sama dengan 0',";
                                }

                                DetailErrors += "}, ";
                            }
                            DetailErrors += "], ";
                        }
                    }
                    else
                    {
                        yield return new ValidationResult("DetailItem kosong", new List<string> { "DOReturnDetailItem" });
                    }

                    DetailErrors += "}, ";
                }
            }
            else
            {
                yield return new ValidationResult("Detail harus diisi", new List<string> { "DOReturnDetail" });
            }

            DetailErrors += "]";

            if (Count > 0)
                yield return new ValidationResult(DetailErrors, new List<string> { "DOReturnDetails" });

        }
    }
}
