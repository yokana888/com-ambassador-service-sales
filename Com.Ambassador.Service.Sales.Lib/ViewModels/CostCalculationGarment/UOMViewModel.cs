using Com.Ambassador.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.CostCalculationGarment
{
	public class UOMViewModel : BaseViewModel, IValidatableObject
	{
		public string code { get; set; }
		public string Unit { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (string.IsNullOrWhiteSpace(this.code))
				yield return new ValidationResult("Kode harus diisi", new List<string> { "Code" });

			if (string.IsNullOrWhiteSpace(this.Unit))
				yield return new ValidationResult("Nama harus diisi", new List<string> { "Name" });
			else if (int.TryParse(this.Unit, out int n))
				yield return new ValidationResult("Satuan hanya berupa huruf", new List<string> { "Name" });
		}
	}
}
