using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.DOAval
{
    public class DOAvalViewModel : BaseViewModel, IValidatableObject
    {
        public DOAvalViewModel()
        {
            DOAvalItems = new HashSet<DOAvalItemViewModel>();
        }

        public string Code { get; set; }
        public long? AutoIncreament { get; set; }
        public string DOAvalNo { get; set; }
        public string DOAvalType { get; set; }
        public string DOAvalCategory { get; set; }
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
        public string WeightUom { get; set; }
        public int? Disp { get; set; }
        public string Remark { get; set; }
        public string Construction { get; set; }

        public ICollection<DOAvalItemViewModel> DOAvalItems { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(DOAvalType))
            {
                yield return new ValidationResult("Jenis DO harus dipilih", new List<string> { "DOAvalType" });
            }

            if (string.IsNullOrEmpty(Type))
                yield return new ValidationResult("Seri DO harus dipilih", new List<string> { "Type" });

            if (!Date.HasValue)
                yield return new ValidationResult("Tanggal harus diisi", new List<string> { "Date" });

            if (Buyer == null || Buyer.Id == 0)
                yield return new ValidationResult("Buyer harus diisi", new List<string> { "Buyer" });

            if (string.IsNullOrEmpty(PackingUom))
                yield return new ValidationResult("Satuan Packing harus dipilih", new List<string> { "PackingUom" });

            if (string.IsNullOrEmpty(WeightUom))
                yield return new ValidationResult("Satuan Berat harus dipilih", new List<string> { "WeightUom" });


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


            int Count = 0;
            string DetailErrors = "[";

            if (DOAvalItems.Count > 0)
            {
                foreach (var item in DOAvalItems)
                {
                    DetailErrors += "{";


                    if (string.IsNullOrWhiteSpace(item.AvalType))
                    {
                        Count++;
                        DetailErrors += "AvalType : 'Jenis Aval harus diisi',";
                    }

                    if (item.Packing <= 0)
                    {
                        Count++;
                        DetailErrors += "Packing : 'Jumlah Packing harus lebih besar dari 0',";
                    }

                    if (item.Weight <= 0)
                    {
                        Count++;
                        DetailErrors += "Weight : 'Berat harus lebih besar dari 0',";
                    }

                    DetailErrors += "}, ";
                }
            }
            else
            {
                yield return new ValidationResult("Item tidak boleh kosong", new List<string> { "DOAvalItem" });
            }

            DetailErrors += "]";

            if (Count > 0)
                yield return new ValidationResult(DetailErrors, new List<string> { "DOAvalItems" });
        }
    }
}
