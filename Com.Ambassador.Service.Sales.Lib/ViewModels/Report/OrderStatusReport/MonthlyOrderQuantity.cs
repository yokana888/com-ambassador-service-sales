using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.Report.OrderStatusReport
{
    public class YearlyOrderQuantity
    {
        public List<int> OrderIds { get; set; }
        public int Month { get; set; }
        public double OrderQuantity { get; set; }
    }

    public class MonthlyOrderQuantity
    {
        public int orderId { get; set; }
        public string accountName { get; set; }
        public double afterProductionQuantity { get; set; }
        public string buyerName { get; set; }
        public string colorRequest { get; set; }
        public string constructionComposite { get; set; }
        public DateTimeOffset deliveryDate { get; set; }
        public string designCode { get; set; }
        public double diffOrderShipmentQuantity { get; set; }
        public double inspectingQuantity { get; set; }
        public double notInKanbanQuantity { get; set; }
        public double onProductionQuantity { get; set; }
        public string orderNo { get; set; }
        public double orderQuantity { get; set; }
        public string processType { get; set; }
        public double shipmentQuantity { get; set; }
        public double storageQuantity { get; set; }
        public DateTimeOffset _createdDate { get; set; }
    }
}
