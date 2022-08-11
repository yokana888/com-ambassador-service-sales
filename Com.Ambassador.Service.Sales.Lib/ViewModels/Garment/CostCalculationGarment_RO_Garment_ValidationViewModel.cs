using Com.Ambassador.Service.Sales.Lib.ViewModels.CostCalculationGarment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.Garment
{
    public class CostCalculationGarment_RO_Garment_ValidationViewModel : CostCalculationGarmentViewModel, IValidatableObject
    {
        public new IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (String.IsNullOrWhiteSpace(RO_Number))
            {
                yield return new ValidationResult("No. RO harus diisi.", new List<string> { "RONo" });
            }

            if (CostCalculationGarment_Materials == null || CostCalculationGarment_Materials.Count.Equals(0))
            {
                yield return new ValidationResult("Tidak ada Materials", new List<string> { "MaterialsCount" });
            }
            else
            {
                int ErrorCount = 0;
                string MaterialsError = "[";

                foreach (var material in CostCalculationGarment_Materials)
                {
                    if (material.IsPosted)
                    {
                        ErrorCount++;
                        MaterialsError += "{ No: 'Material sudah dipos' }, ";
                    }
                }

                MaterialsError += "]";

                if (ErrorCount > 0)
                {
                    yield return new ValidationResult(MaterialsError, new List<string> { "Materials" });
                }
            }
        }
    }
}
