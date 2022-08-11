using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.SalesInvoice
{
    public class SalesInvoiceReportViewModel
    {
        public SalesInvoiceReportViewModel()
        {
            SalesReceipts = new HashSet<SalesInvoiceReportSalesReceiptViewModel>();
        }

        public long Id { get; set; }
        public string SalesInvoiceNo { get; set; }
        public int Tempo { get; set; }
        public double TotalPayment { get; set; }
        public double Unpaid { get; set; }
        public double TotalPaid { get; set; }
        public string Status { get; set; }
        public string CurrencySymbol { get; set; }

        public ICollection<SalesInvoiceReportSalesReceiptViewModel> SalesReceipts { get; set; }
    }
}
