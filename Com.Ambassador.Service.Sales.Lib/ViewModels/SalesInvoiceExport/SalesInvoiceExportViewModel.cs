using Com.Ambassador.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.SalesInvoiceExport
{
    public class SalesInvoiceExportViewModel : BaseViewModel, IValidatableObject
    {
        public string Code { get; set; }
        public long? AutoIncreament { get; set; }
        public string SalesInvoiceNo { get; set; }
        public string SalesInvoiceCategory { get; set; }
        public string LetterOfCreditNumberType { get; set; }
        public DateTimeOffset? SalesInvoiceDate { get; set; }
        public string FPType { get; set; }
        public string BuyerName { get; set; }
        public string BuyerAddress { get; set; }
        public string Authorized { get; set; }
        public string ShippedPer { get; set; }
        public DateTimeOffset? SailingDate { get; set; }
        public string LetterOfCreditNumber { get; set; }
        public DateTimeOffset? LCDate { get; set; }
        public string IssuedBy { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string HSCode { get; set; }
        public string TermOfPaymentType { get; set; }
        public string TermOfPaymentRemark { get; set; }
        public string ShippingRemark { get; set; }
        public string Remark { get; set; }

        public ICollection<SalesInvoiceExportDetailViewModel> SalesInvoiceExportDetails { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(SalesInvoiceCategory) || SalesInvoiceCategory == "")
                yield return new ValidationResult("Kategori Faktur kosong", new List<string> { "SalesInvoiceCategory" });

            if (string.IsNullOrWhiteSpace(LetterOfCreditNumberType) || LetterOfCreditNumberType == "")
                yield return new ValidationResult("Seri Faktur Penjualan harus dipilih", new List<string> { "LetterOfCreditNumberType" });

            if (!SalesInvoiceDate.HasValue || SalesInvoiceDate.Value > DateTimeOffset.Now)
                yield return new ValidationResult("Tgl Faktur Penjualan harus diisi & lebih kecil atau sama dengan hari ini", new List<string> { "SalesInvoiceDate" });

            if (SalesInvoiceCategory == "DYEINGPRINTING" && (string.IsNullOrWhiteSpace(FPType) || FPType == ""))
                yield return new ValidationResult("Tipe FP harus dipilih", new List<string> { "FPType" });

            if (string.IsNullOrEmpty(BuyerName))
                yield return new ValidationResult("Nama Buyer harus diisi", new List<string> { "BuyerName" });

            if (string.IsNullOrEmpty(BuyerAddress))
                yield return new ValidationResult("Alamat Buyer harus diisi", new List<string> { "BuyerAddress" });

            if (string.IsNullOrWhiteSpace(Authorized) || Authorized == "")
                yield return new ValidationResult("Penanggungjawab harus dipilih", new List<string> { "Authorized" });

            if (string.IsNullOrEmpty(ShippedPer))
                yield return new ValidationResult("Shipped Per harus diisi", new List<string> { "ShippedPer" });

            if (!SailingDate.HasValue)
                yield return new ValidationResult("Tgl Sailing harus diisi", new List<string> { "SailingDate" });

            if (string.IsNullOrEmpty(From))
                yield return new ValidationResult("Asal harus diisi", new List<string> { "From" });

            if (string.IsNullOrEmpty(To))
                yield return new ValidationResult("Tujuan harus diisi", new List<string> { "To" });

            if (LetterOfCreditNumberType == "L/C")
            {
                if (string.IsNullOrEmpty(LetterOfCreditNumber))
                    yield return new ValidationResult("LetterOfCreditNumber harus diisi", new List<string> { "LetterOfCreditNumber" });

                if (!LCDate.HasValue)
                    yield return new ValidationResult("Tgl LC harus diisi", new List<string> { "LCDate" });

                if (string.IsNullOrEmpty(IssuedBy))
                    yield return new ValidationResult("Bank Penerbit harus diisi", new List<string> { "IssuedBy" });
            }

            if (string.IsNullOrEmpty(HSCode))
                yield return new ValidationResult("Kode HS harus diisi", new List<string> { "HSCode" });

            if (string.IsNullOrWhiteSpace(TermOfPaymentType) || TermOfPaymentType == "")
                yield return new ValidationResult("TermOfPayment Type harus dipilih", new List<string> { "TermOfPaymentType" });

            if (string.IsNullOrEmpty(TermOfPaymentRemark))
                yield return new ValidationResult("TermOfPayment Remark harus diisi", new List<string> { "TermOfPaymentRemark" });

            if (string.IsNullOrEmpty(ShippingRemark))
                yield return new ValidationResult("Shipping Remark harus diisi", new List<string> { "ShippingRemark" });

            int Count = 0;
            string DetailErrors = "[";

            if (SalesInvoiceExportDetails != null && SalesInvoiceExportDetails.Count > 0)
            {
                foreach (var detail in SalesInvoiceExportDetails)
                {
                    int ErrorCount = 0;
                    DetailErrors += "{";

                    if (string.IsNullOrWhiteSpace(detail.ContractNo))
                    {
                        Count++;
                        ErrorCount++;
                        DetailErrors += "ContractNo : 'No. Kontrak kosong / tidak ditemukan',";
                    }

                    if (string.IsNullOrWhiteSpace(detail.BonNo))
                    {
                        Count++;
                        ErrorCount++;
                        DetailErrors += "BonNo : 'No. Bon Pengiriman kosong / tidak ditemukan',";
                    }

                    var duplicate = SalesInvoiceExportDetails.Where(w => w.BonNo != null && detail.BonNo != null && w.BonNo.Equals(detail.BonNo)).ToList();
                    if (duplicate.Count > 1)
                    {
                        Count++;
                        DetailErrors += "BonNo : 'No. Bon Pengiriman duplikat',";
                    }

                    if (!detail.GrossWeight.HasValue || detail.GrossWeight <= 0)
                    {
                        Count++;
                        ErrorCount++;
                        DetailErrors += "GrossWeight : 'Berat Kotor harus lebih besar dari 0',";
                    }

                    if (!detail.NetWeight.HasValue || detail.NetWeight <= 0)
                    {
                        Count++;
                        ErrorCount++;
                        DetailErrors += "NetWeight : 'Berat Bersih harus lebih besar dari 0',";
                    }

                    if (!detail.TotalMeas.HasValue || detail.TotalMeas <= 0)
                    {
                        Count++;
                        ErrorCount++;
                        DetailErrors += "TotalMeas : 'Total Meas harus lebih besar dari 0',";
                    }

                    if (string.IsNullOrWhiteSpace(detail.WeightUom))
                    {
                        Count++;
                        DetailErrors += "WeightUom : 'Satuan Berat harus diisi',";
                    }

                    if (string.IsNullOrWhiteSpace(detail.TotalUom))
                    {
                        Count++;
                        DetailErrors += "TotalUom : 'Satuan Total harus diisi',";
                    }
                    if (detail.SalesInvoiceExportItems == null || detail.SalesInvoiceExportItems.Count == 0)
                    {
                        Count++;
                        DetailErrors += "SalesInvoiceItem : 'Item Kosong',";
                    }
                    else
                    {
                        DetailErrors += "SalesInvoiceExportItems: [";

                        foreach (var item in detail.SalesInvoiceExportItems)
                        {
                            DetailErrors += "{";

                            if (string.IsNullOrWhiteSpace(item.ProductCode))
                            {
                                Count++;
                                DetailErrors += "ProductCode : 'Kode produk harus diisi',";
                            }

                            if (string.IsNullOrWhiteSpace(item.ProductName))
                            {
                                Count++;
                                DetailErrors += "ProductName : 'Nama produk harus diisi',";
                            }

                            if (!item.QuantityPacking.HasValue || item.QuantityPacking.Value <= 0)
                            {
                                Count++;
                                DetailErrors += "QuantityPacking : 'Jumlah Packing harus diisi dan lebih besar dari 0',";
                            }

                            if (!item.QuantityItem.HasValue || item.QuantityItem.Value <= 0)
                            {
                                Count++;
                                DetailErrors += "QuantityItem : 'Jumlah Item harus diisi dan lebih besar dari 0',";
                            }

                            if (string.IsNullOrWhiteSpace(item.PackingUom))
                            {
                                Count++;
                                DetailErrors += "PackingUom : 'Satuan Packing harus diisi',";
                            }

                            if (string.IsNullOrWhiteSpace(item.ItemUom))
                            {
                                Count++;
                                DetailErrors += "ItemUom : 'Satuan Item harus diisi',";
                            }

                            if (!item.Price.HasValue || item.Price.Value <= 0)
                            {
                                Count++;
                                DetailErrors += "Price : 'Harga barang harus diisi dan lebih besar dari 0',";
                            }

                            DetailErrors += "}, ";
                        }

                        DetailErrors += "], ";
                    }

                    DetailErrors += "}, ";
                }
            }
            else
            {
                yield return new ValidationResult("Detail harus diisi", new List<string> { "SalesInvoiceDetail" });
            }

            DetailErrors += "]";

            if (Count > 0)
                yield return new ValidationResult(DetailErrors, new List<string> { "SalesInvoiceExportDetails" });
        }
    }
}
