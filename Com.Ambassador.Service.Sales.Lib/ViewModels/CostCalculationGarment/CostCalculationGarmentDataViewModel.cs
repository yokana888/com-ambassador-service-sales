using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.CostCalculationGarment
{
	public class CostCalculationGarmentDataProductionReport
	{
		public string comodityName { get; set; }
		public string ro { get; set; }
		public string buyerCode { get; set; }
		public double hours { get; set; }
		public double qtyOrder { get; set; }
	}
}
