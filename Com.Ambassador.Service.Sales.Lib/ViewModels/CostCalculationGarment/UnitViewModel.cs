using Com.Ambassador.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.CostCalculationGarment
{
	public class UnitViewModel : BaseViewModel, IValidatableObject
	{
		public string Code { get; set; }
		public string Name { get; set; }
		 

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (string.IsNullOrWhiteSpace(this.Name))
				yield return new ValidationResult("Nama Unit harus diisi", new List<string> { "Name" });
		}
	}
}
