using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.GarmentOmzetTargetFacades;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.GarmentOmzetTargetInterface;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentOmzetTargetModel;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.CostCalculationGarment;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentOmzetTargetViewModels
{
    public class GarmentOmzetTargetViewModel : BaseViewModel, IValidatableObject
    {
        public string YearOfPeriod { get; set; }
        public string QuaterCode { get; set; }
        public string MonthOfPeriod { get; set; }
        public int SectionId { get; set; }
        public string SectionCode { get; set; }
        public string SectionName { get; set; }
        public double Amount { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {               
            if (YearOfPeriod == null)
            {
                yield return new ValidationResult("Tahun harus diisi", new List<string> { "YearOfPeriod" });
            }

            if (MonthOfPeriod == null)
            {
                yield return new ValidationResult("Bulan harus diisi", new List<string> { "MonthOfPeriod" });
            }

            if (QuaterCode == null)
            {
                yield return new ValidationResult("Quater harus diisi", new List<string> { "QuaterCode" });
            }

            if (SectionCode == null)
            {
                yield return new ValidationResult("Seksi harus diisi", new List<string> { "SectionCode" });
            }

            if (Amount <= 0)
            {
                yield return new ValidationResult("Target Omzet harus lebih dari 0", new List<string> { "Amount" });
            }
        }
    }
}