using Com.Ambassador.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.CostCalculationGarment
{
	public class EfficiencyViewModel : BaseViewModel, IValidatableObject
	{
		public string Code { get; set; }
		public string Name { get; set; }
		public int? InitialRange { get; set; }
		public int? FinalRange { get; set; }
		public double? Value { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (this.InitialRange == null)
				yield return new ValidationResult("Range Awal Efisiensi harus diisi", new List<string> { "InitialRange" });
			else if (this.InitialRange <= 0)
				yield return new ValidationResult("Range Awal Efisiensi harus lebih besar dari 0", new List<string> { "InitialRange" });

			if (this.FinalRange == null)
				yield return new ValidationResult("Range Akhir Efisiensi harus diisi", new List<string> { "FinalRange" });
			else if (this.FinalRange <= 0)
				yield return new ValidationResult("Range Akhir Efisiensi harus lebih besar dari 0", new List<string> { "FinalRange" });
			else if (this.FinalRange <= this.InitialRange)
				yield return new ValidationResult("Range Akhir Efisiensi harus lebih besar dari Range Awal", new List<string> { "FinalRange" });

			if (this.Value == null)
				yield return new ValidationResult("Efisiensi harus diisi", new List<string> { "Value" });
			else if (this.Value <= 1)
				yield return new ValidationResult("Efisiensi harus lebih besar dari 1", new List<string> { "Value" });
		}
	}
}
