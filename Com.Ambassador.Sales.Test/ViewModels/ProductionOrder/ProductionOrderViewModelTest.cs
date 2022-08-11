using Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using Com.Ambassador.Service.Sales.Lib.ViewModels.ProductionOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.ViewModels.ProductionOrder
{
    public class ProductionOrderViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {

            ProductionOrderViewModel viewModel = new ProductionOrderViewModel()
            {
                ArticleFabricEdge = "ArticleFabricEdge",
                MaterialWidth = "MaterialWidth",
                POType = "Test",
                Sample = "Sample",
                Remark = "Remark",
                IsUsed = true,
                IsClosed = true,
                IsRequested = true,
                IsCompleted = true,
                IsCalculated = true,
                AutoIncreament = 1,
                SalesContractNo = "SalesContractNo",
                OrderQuantity = 0,
                LampStandards = new List<ProductionOrder_LampStandardViewModel>(),
                Details = new List<ProductionOrder_DetailViewModel>()
            };

            Assert.Equal(1, viewModel.AutoIncreament);
            Assert.True(viewModel.IsUsed);
            Assert.True(viewModel.IsClosed);
        }

        [Fact]
        public void Validate_DataNull()
        {

            ProductionOrderViewModel viewModel = new ProductionOrderViewModel()
            {
                OrderQuantity = 0,
                LampStandards = new List<ProductionOrder_LampStandardViewModel>(),
                Details = new List<ProductionOrder_DetailViewModel>()
            };
            var result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);
        }


        [Fact]
        public void Validate_DefaultValue()
        {

            ProductionOrderViewModel viewModel = new ProductionOrderViewModel()
            {
                Buyer = new BuyerViewModel()
                {
                    Id = 0
                },
                Uom = new UomViewModel()
                {
                    Id = 0
                },
                FinishingPrintingSalesContract = new FinishingPrintingSalesContractViewModel()
                {
                    Id = 0
                },
                Material = new MaterialViewModel()
                {
                    Id = 1
                },
                ProcessType = new ProcessTypeViewModel()
                {
                    Id = 0
                },
                OrderQuantity = 0,
                YarnMaterial = new YarnMaterialViewModel()
                {
                    Id = 0
                },
                MaterialConstruction = new MaterialConstructionViewModel()
                {
                    Id = 0
                },
                FinishType = new FinishTypeViewModel()
                {
                    Id = 0
                },
                StandardTests = new StandardTestsViewModel()
                {
                    Id = 0
                },
                PackingInstruction = "",
                MaterialOrigin = "",
                FinishWidth = "",
                HandlingStandard = "",
                ShrinkageStandard = "",
                LampStandards = new List<ProductionOrder_LampStandardViewModel>(),
                Details = new List<ProductionOrder_DetailViewModel>()
            };
            var result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);
        }
        [Fact]
        public void Validate_OrderType()
        {
            ProductionOrderViewModel viewModel = new ProductionOrderViewModel()
            {
                OrderType = new OrderTypeViewModel()
                {
                    Id = 1,
                    Name = "printing"
                },
                Details = new List<ProductionOrder_DetailViewModel>(),
                LampStandards = new List<ProductionOrder_LampStandardViewModel>(),

            };

            var result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public void Validate_Run()
        {
            ProductionOrderViewModel viewModel = new ProductionOrderViewModel()
            {
                OrderType = new OrderTypeViewModel()
                {
                    Id = 1,
                    Name = "printing",

                },
                Run = "RUN",
                RunWidth = new List<ProductionOrder_RunWidthViewModel>(),
                ShippingQuantityTolerance = 101,
                Details = new List<ProductionOrder_DetailViewModel>(),
                LampStandards = new List<ProductionOrder_LampStandardViewModel>(),
            };
            var result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public void Validate_RunWidths()
        {
            ProductionOrderViewModel viewModel = new ProductionOrderViewModel()
            {
                OrderType = new OrderTypeViewModel()
                {
                    Id = 1,
                    Name = "printing",

                },
                Run = "RUN",
                RunWidth = new List<ProductionOrder_RunWidthViewModel>()
                {
                    new ProductionOrder_RunWidthViewModel()
                    {
                        Value =-1
                    }
                },
                Details = new List<ProductionOrder_DetailViewModel>()
                {
                    new ProductionOrder_DetailViewModel()
                    {
                        Quantity=-1,
                        Uom =new UomViewModel()
                        {
                            Id=0
                        }
                    }
                },
                LampStandards = new List<ProductionOrder_LampStandardViewModel>()
                {
                    new ProductionOrder_LampStandardViewModel()

                },

            };
            var result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public void Validate_ProcessType()
        {
            ProductionOrderViewModel viewModel = new ProductionOrderViewModel()
            {
                OrderType = new OrderTypeViewModel()
                {
                    Id = 1,
                    Name = "printing"
                },
                ProcessType = new ProcessTypeViewModel()
                {
                    Id = 1,
                    Name = "s"
                },
                Run = "1 run",
                RunWidth = new List<ProductionOrder_RunWidthViewModel>()
                {
                },
                Details = new List<ProductionOrder_DetailViewModel>(),
                LampStandards = new List<ProductionOrder_LampStandardViewModel>(),

            };

            var result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);


            viewModel.ProcessType.SPPCode = "c";
            viewModel.ProcessType.Unit = "printing";
            viewModel.RunWidth.Add(new ProductionOrder_RunWidthViewModel()
            {
                Value = 0
            });
            result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);
            Assert.NotNull(viewModel.ProcessType.Unit);
            Assert.NotNull(viewModel.ProcessType.SPPCode);

            viewModel.Run = null;
            result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);

            viewModel.Run = "1run";
            viewModel.RunWidth = new List<ProductionOrder_RunWidthViewModel>();
            result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);
        }
    }
}
