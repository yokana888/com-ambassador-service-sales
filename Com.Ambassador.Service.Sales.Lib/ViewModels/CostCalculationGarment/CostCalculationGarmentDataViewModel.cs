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

	public class CostCalculationGarmentForJournal
	{
		public string RONo { get; set; }		
		public double Amount { get; set; }
		public double OTL1 { get; set; }
		public double OTL2 { get; set; }
		public double Risk { get; set; }
		public double AmountCC { get; set; }
	}
}
