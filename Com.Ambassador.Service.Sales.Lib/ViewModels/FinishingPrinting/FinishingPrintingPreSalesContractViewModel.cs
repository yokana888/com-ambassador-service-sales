using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrinting
{
    public class FinishingPrintingPreSalesContractViewModel : BaseViewModel, IValidatableObject
    {
        public string No { get; set; }

        public DateTimeOffset Date { get; set; }

        public string Type { get; set; }

        public BuyerViewModel Buyer { get; set; }

        public UnitViewModel Unit { get; set; }

        public ProcessTypeViewModel ProcessType { get; set; }

        public double OrderQuantity { get; set; }

        public string Remark { get; set; }

        public bool IsPosted { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Date == null || Date == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal Sales Contract harus diisi", new List<string> { "Date" });
            }
            
            if (Buyer == null || Buyer.Id == 0)
            {
                yield return new ValidationResult("Buyer harus diisi", new List<string> { "Buyer" });
            }
            if (Unit == null || Unit.Id == 0)
            {
                yield return new ValidationResult("Unit harus diisi", new List<string> { "Unit" });
            }

            if (ProcessType == null || ProcessType.Id == 0)
            {
                yield return new ValidationResult("Jenis Proses harus diisi", new List<string> { "ProcessType" });
            }

            if (Type == null)
            {
                yield return new ValidationResult("Jenis Sales Contract harus diisi", new List<string> { "Type" });
            }

            if (OrderQuantity <= 0)
            {
                yield return new ValidationResult("Jumlah Order harus lebih dari 0", new List<string> { "OrderQuantity" });
            }
        }
    }
}
