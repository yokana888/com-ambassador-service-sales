using Com.Ambassador.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Ambassador.Service.Sales.Lib
{
    public class DeliveryNoteProductionViewModel : BaseViewModel, IValidatableObject
    {
        public string Code { get; set; }
        public DateTimeOffset? Date { get; set; }

        public SalesContract SalesContract { get; set; }

        public string Unit { get; set; }
        public string Subject { get; set; }
        public string OtherSubject { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string BallMark { get; set; }
        public string Sample { get; set; }
        public string Remark { get; set; }
        public string YarnSales { get; set; }
        public string MonthandYear { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Date.HasValue)
                yield return new ValidationResult("Tanggal harus diisi!", new List<string> { "Date" });

            if (SalesContract == null || string.IsNullOrWhiteSpace(SalesContract.SalesContractNo))
                yield return new ValidationResult("No. Sales Contract harus diisi", new List<string> { "SalesContract" });

            if (string.IsNullOrWhiteSpace(Unit))
                yield return new ValidationResult("Unit harus diisi", new List<string> { "Unit" });

            if(Subject == "Lainnya")
            {

                if (string.IsNullOrWhiteSpace(OtherSubject))
                    yield return new ValidationResult("Lainnya harus diisi", new List<string> { "OtherSubject" });
            }
            else
            {
                if (string.IsNullOrWhiteSpace(Subject))
                    yield return new ValidationResult("Perihal harus diisi", new List<string> { "Subject" });
            }

            if (string.IsNullOrWhiteSpace(Month))
                yield return new ValidationResult("Bulan harus diisi", new List<string> { "Month" });

            if (string.IsNullOrWhiteSpace(Year))
                yield return new ValidationResult("Tahun harus diisi", new List<string> { "Year" });

            if (string.IsNullOrWhiteSpace(YarnSales))
                yield return new ValidationResult("Penjualan Benang harus diisi", new List<string> { "YarnSales" });

        }
    }
}