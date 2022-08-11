using Com.Ambassador.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentMasterPlan.MaxWHConfirmViewModels
{
    public class MaxWHConfirmViewModel : BaseViewModel, IValidatableObject
    {
        public double UnitMaxValue { get; set; }
        public double SKMaxValue { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (UnitMaxValue <= 0)
            {
                yield return new ValidationResult("Max WH Confirm Unit tidak boleh kosong", new List<string> { "Unitwh" });
            }

            if (SKMaxValue <= 0)
            {
                yield return new ValidationResult("Max WH Confirm SK tidak boleh kosong", new List<string> { "SKwh" });
            }
        }
    }
}
