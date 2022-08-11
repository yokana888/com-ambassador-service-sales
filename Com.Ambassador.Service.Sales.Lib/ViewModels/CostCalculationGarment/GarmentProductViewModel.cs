using Com.Ambassador.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.CostCalculationGarment
{
	public class GarmentProductViewModel : BaseViewModel, IValidatableObject
	{
		public string Code { get; set; }
		public string Name { get; set; }
        public string Composition { get; set; }
		public string Const { get; set; }
		public string Yarn { get; set; }
		public string Width { get; set; }
		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			throw new NotImplementedException();
		}
	}
}
