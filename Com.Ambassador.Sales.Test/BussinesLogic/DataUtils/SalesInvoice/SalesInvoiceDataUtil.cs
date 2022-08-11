using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.SalesInvoice;
using Com.Ambassador.Service.Sales.Lib.Models.SalesInvoice;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.SalesInvoice
{
    public class SalesInvoiceDataUtil : BaseDataUtil<SalesInvoiceFacade, SalesInvoiceModel>
    {
        public SalesInvoiceDataUtil(SalesInvoiceFacade facade) : base(facade)
        {
        }

        public override async Task<SalesInvoiceModel> GetNewData()
        {
            return new SalesInvoiceModel()
            {
                Code = "code",
                AutoIncreament = 1,
                SalesInvoiceNo = "SalesInvoiceNo",
                SalesInvoiceType = "L/C",
                SalesInvoiceCategory = "DYEINGPRINTING",
                SalesInvoiceDate = DateTimeOffset.UtcNow,
                DueDate = DateTimeOffset.UtcNow.AddDays(-2),
                DeliveryOrderNo = "DeliveryOrderNo",
                BuyerId = 1,
                BuyerName = "BuyerName",
                BuyerCode = "BuyerCode",
                BuyerAddress = "BuyerAddress",
                BuyerNPWP = "BuyerNPWP",
                BuyerNIK = "BuyerNIK",
                CurrencyId = 1,
                CurrencyCode = "IDR",
                CurrencySymbol = "Rp",
                CurrencyRate = 14000,
                PaymentType = "Meter",
                VatType = "PPN Kawasan Berikat",
                Remark = "Remark",
                TotalPayment = 100,
                TotalPaid = 0,
                SalesInvoiceDetails = new List<SalesInvoiceDetailModel>()
                {
                    new SalesInvoiceDetailModel()
                    {
                        ShippingOutId = 4,
                        BonNo = "BonNo",
                        SalesInvoiceItems = new List<SalesInvoiceItemModel>()
                        {
                            new SalesInvoiceItemModel()
                            {
                                ProductCode = "ProductCode",
                                ProductName = "ProductName",
                                QuantityPacking = 100,
                                PackingUom = "PackingUom",
                                ItemUom = "MTR",
                                QuantityItem = 1,
                                Price = 1,
                                Amount = 1,
                            },
                        }
                    }
                }
            };
        }

        public async Task<SalesInvoiceModel> GetNewData_2()
        {
            return new SalesInvoiceModel()
            {
                Code = "code",
                AutoIncreament = 1,
                SalesInvoiceNo = "SalesInvoiceNo",
                SalesInvoiceType = "SalesInvoiceType",
                SalesInvoiceCategory = "SPINNING",
                SalesInvoiceDate = DateTimeOffset.UtcNow,
                DueDate = DateTimeOffset.UtcNow.AddDays(-2),
                DeliveryOrderNo = "DeliveryOrderNo",
                BuyerId = 1,
                BuyerName = "BuyerName",
                BuyerCode = "BuyerCode",
                BuyerAddress = "BuyerAddress",
                BuyerNPWP = "BuyerNPWP",
                BuyerNIK = "BuyerNIK",
                CurrencyId = 1,
                CurrencyCode = "IDR",
                CurrencySymbol = "Rp",
                CurrencyRate = 14000,
                PaymentType = "Meter",
                VatType = "PPN Kawasan Berikat",
                Remark = "Remark",
                TotalPayment = 100,
                TotalPaid = 0,

                SalesInvoiceDetails = new List<SalesInvoiceDetailModel>()
                {
                    new SalesInvoiceDetailModel()
                    {
                        ShippingOutId = 5,
                        BonNo = "BonNo",
                        SalesInvoiceItems = new List<SalesInvoiceItemModel>()
                        {
                            new SalesInvoiceItemModel()
                            {
                                ProductCode = "ProductCode",
                                ProductName = "ProductName",
                                PackingUom = "PackingUom",
                                ItemUom = "YARD",
                                QuantityItem = 1,
                                Price = 1,
                                Amount = 1,
                            },
                        }
                    }
                }
            };
        }
    }
}
