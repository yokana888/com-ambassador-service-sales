using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.SalesInvoiceExport;
using Com.Ambassador.Service.Sales.Lib.Models.SalesInvoiceExport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.SalesInvoiceExport
{
    public class SalesInvoiceExportDataUtil : BaseDataUtil<SalesInvoiceExportFacade, SalesInvoiceExportModel>
    {
        public SalesInvoiceExportDataUtil(SalesInvoiceExportFacade facade) : base(facade)
        {
        }

        public override async Task<SalesInvoiceExportModel> GetNewData()
        {
            return new SalesInvoiceExportModel()
            {
                Code = "code",
                AutoIncreament = 1,
                SalesInvoiceNo = "SalesInvoiceNo",
                SalesInvoiceCategory = "DYEINGPRINTING",
                LetterOfCreditNumberType = "L/C",
                SalesInvoiceDate = DateTimeOffset.UtcNow,
                FPType = "Printing",
                BuyerName = "BuyerName",
                BuyerAddress = "BuyerAddress",
                Authorized = "Amumpuni",
                ShippedPer = "ShippedPer",
                SailingDate = DateTimeOffset.UtcNow,
                LetterOfCreditNumber = "LetterOfCreditNumber",
                LCDate = DateTimeOffset.UtcNow,
                IssuedBy = "IssuedBy",
                From = "From",
                To = "To",
                HSCode = "HSCode",
                TermOfPaymentType = "TermOfPaymentType",
                TermOfPaymentRemark = "TermOfPaymentRemark",
                ShippingRemark = "ShippingRemark",
                Remark = "Remark",
                SalesInvoiceExportDetails = new List<SalesInvoiceExportDetailModel>()
                {
                    new SalesInvoiceExportDetailModel()
                    {
                        BonId = 4,
                        BonNo = "BonNo",
                        ContractNo = "ContractNo",
                        Description = "Description",
                        GrossWeight = 100,
                        NetWeight = 100,
                        TotalMeas = 100,
                        WeightUom = "KG",
                        TotalUom = "CBM",
                        SalesInvoiceExportItems = new List<SalesInvoiceExportItemModel>()
                        {
                            new SalesInvoiceExportItemModel()
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
    }
}
