using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using Com.Ambassador.Service.Sales.Lib.ViewModels.Weaving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.ViewModels.Weaving
{
    public class WeavingSalesContractViewModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            WeavingSalesContractViewModel viewModel = new WeavingSalesContractViewModel()
            {
               Code = "Code",
               SalesContractNo = "SalesContractNo",
                DispositionNumber = "DispositionNumber",
                FromStock =true,
                MaterialWidth = "MaterialWidth",
                ShippingQuantityTolerance = 1,
                IncomeTax = "IncomeTax",
                TermOfShipment = "TermOfShipment",
                TransportFee = "TransportFee",
                Packing = "Packing",
                DeliveredTo = "DeliveredTo",
                Price =1,
                Comission = "Comission",
                ShipmentDescription = "ShipmentDescription",
                Condition = "Condition",
                Remark = "Remark",
                PieceLength = "PieceLength",
                AutoIncrementNumber =1
            };

            Assert.Equal("Code", viewModel.Code);
            Assert.Equal("SalesContractNo", viewModel.SalesContractNo);
        }

        [Fact]
        public void ValidateDefault()
        {
            WeavingSalesContractViewModel viewModel = new WeavingSalesContractViewModel()
            {
                Product = new ProductViewModel()
                {
                    Id = 0
                }
            };
            var result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public void Validate_When_Buyer_Data_Exist()
        {
            WeavingSalesContractViewModel viewModel = new WeavingSalesContractViewModel()
            {
                Agent =new AgentViewModel()
                {
                    Id =0
                },
                Buyer =new BuyerViewModel()
                {
                    Id =1,
                    Type = "ekspor",
                    
                },
                Product = new ProductViewModel()
                {
                    Id = 0
                }
            };
            var result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public void Validate_When_Data_Agent_Exist()
        {
            WeavingSalesContractViewModel viewModel = new WeavingSalesContractViewModel()
            {
                Agent = new AgentViewModel()
                {
                    Id = 1
                },
                Buyer = new BuyerViewModel()
                {
                    Id = 1,
                    Type = "ekspor",

                },
                Product = new ProductViewModel()
                {
                    Id = 0
                }
            };
            var result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);
        }
    }
}
