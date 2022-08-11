using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using Com.Ambassador.Service.Sales.Lib.ViewModels.Spinning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.ViewModels.Spinning
{
    public class SpinningSalesContractViewModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            var date = DateTimeOffset.Now;
            SpinningSalesContractViewModel viewModel = new SpinningSalesContractViewModel()
            {
                Code = "Code",
                SalesContractNo = "SalesContractNo",
                DeliveredTo = "DeliveredTo",
                DeliverySchedule = date,
                DispositionNumber = "DispositionNumber",
                FromStock = true,
                ShippingQuantityTolerance = 1,
                IncomeTax = "IncomeTax",
                TermOfShipment = "TermOfShipment",
                Packing = "Packing",
                Price = 1,
                Comission = "Comission",
                ShipmentDescription = "ShipmentDescription",
                Condition = "Condition",
                Remark = "Remark",
                PieceLength = "PieceLength",
                AutoIncrementNumber = 1
            };

            Assert.Equal("Code", viewModel.Code);
            Assert.Equal("DispositionNumber", viewModel.DispositionNumber);
        }

        [Fact]
        public void ValidateDefault()
        {
            SpinningSalesContractViewModel viewModel = new SpinningSalesContractViewModel();
            var result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public void Validate_When_data_Buyer_Exist()
        {
            SpinningSalesContractViewModel viewModel = new SpinningSalesContractViewModel()
            {
                Buyer = new BuyerViewModel()
                {
                    Id = 1,
                    Type = "ekspor"
                }
            };
            var result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public void Validate_When_data_Agen_Exist()
        {
            SpinningSalesContractViewModel viewModel = new SpinningSalesContractViewModel()
            {
                Agent = new AgentViewModel()
                {
                    Id = 1
                },
                Buyer = new BuyerViewModel()
                {
                    Id = 1,
                    Type = "ekspor"
                }
            };
            var result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);
        }
    }
}
