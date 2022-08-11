using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.CostCalculationGarment
{
    public class RateViewModel : BaseViewModel, IValidatableObject
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public double? Value { get; set; }
        public UnitViewModel Unit { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(this.Name))
            {
                yield return new ValidationResult("Nama ongkos harus diisi", new List<string> { "Name" });
            }
            else if (Unit != null)
            {
                if (Unit.Id < 1)
                {
                    yield return new ValidationResult("Data Unit tidak benar", new List<string> { "Unit" });
                }
                else
                {
                    var rateFacade = (IRate)validationContext.GetService(typeof(IRate));
                    var rate = rateFacade.Read(1, 1, "{}", null, null, $"{{ Name: \"{Name}\", UnitId: \"{Unit.Id}\" }}");
                    if (rate.Data.Count(data => data.Id != Id) > 0)
                    {
                        yield return new ValidationResult($"Nama ongkos '{Name}' dan Unit '{Unit.Name}' sudah ada.", new List<string> { "Unit" });
                    }
                }
            }

            if (this.Value == null)
                yield return new ValidationResult("Tarif ongkos harus diisi", new List<string> { "Value" });
            else if (this.Value <= 0)
                yield return new ValidationResult("Tarif ongkos harus lebih besar dari 0", new List<string> { "Value" });
        }
    }
}
