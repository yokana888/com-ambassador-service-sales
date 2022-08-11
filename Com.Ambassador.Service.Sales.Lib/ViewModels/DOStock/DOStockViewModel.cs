using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.DOStock
{
    public class DOStockViewModel : BaseViewModel, IValidatableObject
    {
        public DOStockViewModel()
        {
            DOStockItems = new HashSet<DOStockItemViewModel>();
        }
        public string Code { get; set; }
        public long? AutoIncreament { get; set; }
        public string DOStockNo { get; set; }
        public string DOStockType { get; set; }
        public string DOStockCategory { get; set; }
        public string Status { get; set; }
        public bool? Accepted { get; set; }
        public bool? Declined { get; set; }
        public string Type { get; set; }
        public DateTimeOffset? Date { get; set; }
        public BuyerViewModel Buyer { get; set; }
        public string DestinationBuyerName { get; set; }
        public string DestinationBuyerAddress { get; set; }
        public string SalesName { get; set; }
        public string HeadOfStorage { get; set; }

        public string PackingUom { get; set; }
        public string LengthUom { get; set; }
        public int? Disp { get; set; }
        public string Remark { get; set; }

        public ICollection<DOStockItemViewModel> DOStockItems { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(DOStockType))
            {
                yield return new ValidationResult("Jenis DO harus dipilih", new List<string> { "DOStockType" });
            }

            if (string.IsNullOrEmpty(Type))
                yield return new ValidationResult("Seri DO harus dipilih", new List<string> { "Type" });

            if (!Date.HasValue)
                yield return new ValidationResult("Tanggal harus diisi", new List<string> { "Date" });

            if (Buyer == null || Buyer.Id == 0)
                yield return new ValidationResult("Buyer harus diisi", new List<string> { "Buyer" });

            if (string.IsNullOrEmpty(PackingUom))
                yield return new ValidationResult("Satuan Packing harus dipilih", new List<string> { "PackingUom" });

            if (string.IsNullOrEmpty(LengthUom))
                yield return new ValidationResult("Satuan Panjang harus dipilih", new List<string> { "LengthUom" });


            if (string.IsNullOrEmpty(DestinationBuyerName))
                yield return new ValidationResult("Nama Penerima harus diisi", new List<string> { "DestinationBuyerName" });

            if (string.IsNullOrEmpty(DestinationBuyerAddress))
                yield return new ValidationResult("Alamat Tujuan harus diisi", new List<string> { "DestinationBuyerAddress" });

            if (string.IsNullOrEmpty(HeadOfStorage))
                yield return new ValidationResult("Nama Kepala Gudang harus diisi", new List<string> { "HeadOfStorage" });

            if (string.IsNullOrEmpty(SalesName))
                yield return new ValidationResult("Nama Sales harus diisi", new List<string> { "SalesName" });


            if (!Disp.HasValue || Disp <= 0)
                yield return new ValidationResult("Disp harus diisi", new List<string> { "Disp" });

            if (string.IsNullOrEmpty(DOStockType))
                yield return new ValidationResult("Tipe DO harus diisi", new List<string> { "DOStockType" });


            int Count = 0;
            string DetailErrors = "[";

            if (DOStockItems.Count > 0)
            {
                foreach (var item in DOStockItems)
                {
                    DetailErrors += "{";


                    if (item.ProductionOrder == null || item.ProductionOrder.Id == 0)
                    {
                        Count++;
                        DetailErrors += "ProductionOrder : 'No SPP harus diisi',";
                    }

                    if (string.IsNullOrEmpty(item.UnitOrCode))
                    {
                        Count++;
                        DetailErrors += "UnitOrCode : 'Jenis atau Code harus diisi',";
                    }

                    if (item.Packing <= 0)
                    {
                        Count++;
                        DetailErrors += "Packing : 'Jumlah Packing harus lebih besar dari 0',";
                    }

                    if (item.Length <= 0)
                    {
                        Count++;
                        DetailErrors += "Length : 'Panjang harus lebih besar dari 0',";
                    }

                    DetailErrors += "}, ";
                }
            }
            else
            {
                yield return new ValidationResult("Item tidak boleh kosong", new List<string> { "DOStockItem" });
            }

            DetailErrors += "]";

            if (Count > 0)
                yield return new ValidationResult(DetailErrors, new List<string> { "DOStockItems" });
        }
    }
}
