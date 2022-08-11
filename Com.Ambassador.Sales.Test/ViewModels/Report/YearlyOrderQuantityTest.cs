using Com.Ambassador.Service.Sales.Lib.ViewModels.Report.OrderStatusReport;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.ViewModels.Report
{
    public class YearlyOrderQuantityTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            var orderid = new List<int>() { 1 };

            YearlyOrderQuantity viewModel = new YearlyOrderQuantity()
            {
                OrderIds= orderid,
                Month =1,
                OrderQuantity =1
            };

            Assert.Equal(1, viewModel.Month);
            Assert.Equal(orderid, viewModel.OrderIds);
            Assert.Equal(1, viewModel.OrderQuantity);
        }

        [Fact]
        public void should_Success_Instantiate_MonthlyOrderQuantity()
        {
            var orderid = new List<int>() { 1 };
            DateTimeOffset date = DateTimeOffset.Now;
            MonthlyOrderQuantity viewModel = new MonthlyOrderQuantity()
            {
               accountName = "accountName",
               afterProductionQuantity =1,
               buyerName = "buyerName",
               colorRequest = "colorRequest",
               constructionComposite = "constructionComposite",
               deliveryDate = date,
               designCode = "designCode",
               diffOrderShipmentQuantity =1,
               inspectingQuantity =1,
               notInKanbanQuantity =1,
               onProductionQuantity =1,
               orderId =1,
               orderNo = "orderNo",
               orderQuantity =1,
               processType = "processType",
               shipmentQuantity =1,
               storageQuantity =1,
               _createdDate =date

            };

            Assert.Equal("accountName", viewModel.accountName);
            Assert.Equal(1, viewModel.afterProductionQuantity);
            Assert.Equal("buyerName", viewModel.buyerName);
            Assert.Equal(date, viewModel.deliveryDate);
            Assert.Equal("buyerName", viewModel.buyerName);
            Assert.Equal("colorRequest", viewModel.colorRequest);
            Assert.Equal("constructionComposite", viewModel.constructionComposite);
            Assert.Equal("designCode", viewModel.designCode);
            Assert.Equal(1, viewModel.diffOrderShipmentQuantity);
            Assert.Equal(1, viewModel.inspectingQuantity);
            Assert.Equal(1, viewModel.notInKanbanQuantity);
            Assert.Equal(1, viewModel.onProductionQuantity);
            Assert.Equal("orderNo", viewModel.orderNo);
            Assert.Equal(1, viewModel.orderQuantity);
            Assert.Equal("processType", viewModel.processType);
            Assert.Equal(1, viewModel.shipmentQuantity);
            Assert.Equal(1, viewModel.storageQuantity);
            Assert.Equal(date, viewModel._createdDate);
        }

        
    }
}
