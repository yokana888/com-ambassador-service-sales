using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.DOSales
{
    public class DOSalesViewModel : BaseViewModel, IValidatableObject
    {
        public string Code { get; set; }
        public long? AutoIncreament { get; set; }
        public string DOSalesNo { get; set; }
        public string DOSalesType { get; set; }
        public string DOSalesCategory { get; set; }
        public string Status { get; set; }
        public bool? Accepted { get; set; }
        public bool? Declined { get; set; }
        public string Type { get; set; }
        public DateTimeOffset? Date { get; set; }
        public FinishingPrintingSalesContractViewModel SalesContract { get; set; }
        public MaterialViewModel Material { get; set; }
        public MaterialConstructionViewModel MaterialConstruction { get; set; }
        public CommodityViewModel Commodity { get; set; }
        public BuyerViewModel Buyer { get; set; }
        public string DestinationBuyerName { get; set; }
        public string DestinationBuyerAddress { get; set; }
        public string SalesName { get; set; }
        public string HeadOfStorage { get; set; }

        public StorageViewModel Storage { get; set; }
        
        public string PackingUom { get; set; }
        public string LengthUom { get; set; }
        public string WeightUom { get; set; }
        public string BaleUom { get; set; }
        public int? Disp { get; set; }
        public int? Op { get; set; }
        public int? Sc { get; set; }
        public string DoneBy { get; set; }
        public double? FillEachBale { get; set; }
        public string Remark { get; set; }

        public ICollection<DOSalesDetailViewModel> DOSalesDetailItems { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (string.IsNullOrWhiteSpace(DOSalesType) || DOSalesType == "")
            {
                yield return new ValidationResult("Jenis DO harus dipilih", new List<string> { "DOSalesType" });
            }

            if (string.IsNullOrWhiteSpace(Type) || Type == "")
                yield return new ValidationResult("Seri DO Lokal harus dipilih", new List<string> { "Type" });

            if (!Date.HasValue)
                yield return new ValidationResult("Tanggal DO Lokal harus diisi", new List<string> { "Date" });

            if (SalesContract == null || string.IsNullOrWhiteSpace(SalesContract.SalesContractNo))
                yield return new ValidationResult("No. Sales Contract harus diisi", new List<string> { "SalesContract" });

            if (string.IsNullOrWhiteSpace(PackingUom))
                yield return new ValidationResult("Satuan Packing harus dipilih", new List<string> { "PackingUom" });


            if (DOSalesType == "Lokal")
            {
                if (string.IsNullOrWhiteSpace(DestinationBuyerName))
                    yield return new ValidationResult("Nama Penerima harus diisi", new List<string> { "DestinationBuyerName" });

                if (string.IsNullOrWhiteSpace(DestinationBuyerAddress))
                    yield return new ValidationResult("Alamat Tujuan harus diisi", new List<string> { "DestinationBuyerAddress" });

                if (string.IsNullOrWhiteSpace(HeadOfStorage))
                    yield return new ValidationResult("Nama Kepala Gudang harus diisi", new List<string> { "HeadOfStorage" });

                if (DOSalesCategory != "DYEINGPRINTING" && (Storage == null || Storage._id == 0))
                {
                    yield return new ValidationResult("Nama Gudang harus diisi", new List<string> { "Storage" });
                }

                if (string.IsNullOrWhiteSpace(SalesName))
                    yield return new ValidationResult("Nama Sales harus diisi", new List<string> { "SalesName" });

                if(DOSalesCategory == "SPINNING")
                {
                    if (string.IsNullOrWhiteSpace(WeightUom))
                        yield return new ValidationResult("Satuan Berat harus dipilih", new List<string> { "WeightUom" });
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(LengthUom))
                        yield return new ValidationResult("Satuan Panjang harus dipilih", new List<string> { "LengthUom" });
                }

                

                if (!Disp.HasValue || Disp <= 0)
                    yield return new ValidationResult("Disp harus diisi", new List<string> { "Disp" });

                if (!Op.HasValue || Op <= 0)
                    yield return new ValidationResult("Op harus diisi", new List<string> { "Op" });

                if(DOSalesCategory == "DYEINGPRINTING")
                {
                    if (!Sc.HasValue || Sc <= 0)
                        yield return new ValidationResult("Sc harus diisi", new List<string> { "Sc" });
                }
                
            }
            else if (DOSalesType == "Ekspor")
            {
                if (string.IsNullOrWhiteSpace(DoneBy))
                    yield return new ValidationResult("Dikerjakan oleh harus diisi", new List<string> { "DoneBy" });

                if (!FillEachBale.HasValue || FillEachBale.Value <= 0)
                    yield return new ValidationResult("Isi tiap bale harus lebih besar dari 0", new List<string> { "FillEachBale" });

                //if(DOSalesCategory == "DYEINGPRINTING")
                //{
                //    if (string.IsNullOrWhiteSpace(WeightUom))
                //        yield return new ValidationResult("Satuan Berat harus dipilih", new List<string> { "WeightUom" });
                //}
                //else
                //{
                //    if (string.IsNullOrWhiteSpace(WeightUom))
                //        yield return new ValidationResult("Satuan Berat harus dipilih", new List<string> { "WeightUom" });
                //}

                    
            }

            int Count = 0;
            string DetailErrors = "[";

            if (DOSalesDetailItems != null && DOSalesDetailItems.Count > 0)
            {
                foreach (DOSalesDetailViewModel detail in DOSalesDetailItems)
                {
                    DetailErrors += "{";

                    var rowErrorCount = 0;

                    if(DOSalesCategory == "SPINNING")
                    {

                        if (string.IsNullOrWhiteSpace(detail.NoSOP))
                        {
                            Count++;
                            rowErrorCount++;
                            DetailErrors += "NoSOP : 'No SOP harus diisi',";
                        }

                        if (string.IsNullOrWhiteSpace(detail.ThreadNumber))
                        {
                            Count++;
                            rowErrorCount++;
                            DetailErrors += "ThreadNumber : 'Jenis dan No Benang harus diisi',";
                        }

                        if (detail.Packing <= 0)
                        {
                            Count++;
                            rowErrorCount++;
                            DetailErrors += "Packing : 'Jumlah Pack harus lebih besar dari 0',";
                        }
                        //if (DOSalesType == "Lokal" && detail.Length <= 0)
                        //{
                        //    Count++;
                        //    rowErrorCount++;
                        //    DetailErrors += "Length : 'Panjang harus lebih besar dari 0',";
                        //}
                        if (detail.Weight <= 0)
                        {
                            Count++;
                            rowErrorCount++;
                            DetailErrors += "Weight : 'Berat harus lebih besar dari 0',";
                        }
                        if (detail.ConvertionValue < 0)
                        {
                            Count++;
                            rowErrorCount++;
                            DetailErrors += "ConvertionValue : 'Nilai Berat/Panjang gagal dikonversi ke bentuk satuan lain',";
                        }

                    }
                    else if (DOSalesCategory == "WEAVING")
                    {

                        if (string.IsNullOrWhiteSpace(detail.NoSOP))
                        {
                            Count++;
                            rowErrorCount++;
                            DetailErrors += "NoSOP : 'No SOP harus diisi',";
                        }

                        if (string.IsNullOrWhiteSpace(detail.ThreadNumber))
                        {
                            Count++;
                            rowErrorCount++;
                            DetailErrors += "ThreadNumber : 'Jenis dan No Benang harus diisi',";
                        }

                        if (string.IsNullOrWhiteSpace(detail.Grade))
                        {
                            Count++;
                            rowErrorCount++;
                            DetailErrors += "Grade : 'Grade harus dipilih',";
                        }

                        if (detail.Packing <= 0)
                        {
                            Count++;
                            rowErrorCount++;
                            DetailErrors += "Packing : 'Jumlah Pack harus lebih besar dari 0',";
                        }

                        if (DOSalesType == "Lokal" && detail.Length <= 0)
                        {
                            Count++;
                            rowErrorCount++;
                            DetailErrors += "Length : 'Panjang harus lebih besar dari 0',";
                        }

                        if (DOSalesType == "Ekspor" && detail.Weight <= 0)
                        {
                            Count++;
                            rowErrorCount++;
                            DetailErrors += "Weight : 'Berat harus lebih besar dari 0',";
                        }

                        if (detail.ConvertionValue < 0)
                        {
                            Count++;
                            rowErrorCount++;
                            DetailErrors += "ConvertionValue : 'Nilai Berat/Panjang gagal dikonversi ke bentuk satuan lain',";
                        }

                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(detail.UnitOrCode))
                        {
                            Count++;
                            rowErrorCount++;
                            DetailErrors += "UnitOrCode : 'Unit/Kode harus diisi',";
                        }
                        if (detail.Packing <= 0)
                        {
                            Count++;
                            rowErrorCount++;
                            DetailErrors += "Packing : 'Jumlah Pack harus lebih besar dari 0',";
                        }
                        if (DOSalesType == "Lokal" && detail.Length <= 0)
                        {
                            Count++;
                            rowErrorCount++;
                            DetailErrors += "Length : 'Panjang harus lebih besar dari 0',";
                        }
                        if (DOSalesType == "Ekspor" && detail.Weight < 0)
                        {
                            Count++;
                            rowErrorCount++;
                            DetailErrors += "Weight : 'Berat harus lebih besar sama dengan 0',";
                        }
                        if (detail.ConvertionValue < 0)
                        {
                            Count++;
                            rowErrorCount++;
                            DetailErrors += "ConvertionValue : 'Nilai Berat/Panjang gagal dikonversi ke bentuk satuan lain',";
                        }
                    }

                    
                    DetailErrors += "}, ";
                }
            }
            else
            {
                yield return new ValidationResult("Detail tidak boleh kosong", new List<string> { "DetailItem" });
            }

            DetailErrors += "]";

            if (Count > 0)
                yield return new ValidationResult(DetailErrors, new List<string> { "DOSalesDetailItems" });

        }
    }
}
