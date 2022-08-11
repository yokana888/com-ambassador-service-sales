using Com.Ambassador.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.CostCalculationGarment
{
	public class BuyerViewModel : BaseViewModel, IValidatableObject
	{
		public string Code { get; set; }
		public string Name { get; set; }
		public string email { get; set; }
		public string address1 { get; set; }
		public string address2 { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (string.IsNullOrWhiteSpace(this.Name))
				yield return new ValidationResult("Nama Pembeli harus diisi", new List<string> { "Name" });

			if (string.IsNullOrWhiteSpace(this.email))
				yield return new ValidationResult("Email Pembeli harus diisi", new List<string> { "Email" });
			else if (!Helpers.Email.IsValid(this.email))
				yield return new ValidationResult("Format Email tidak benar", new List<string> { "Email" });
		}
	}
}
