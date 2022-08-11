using AutoMapper;
using Com.Ambassador.Sales.Test.WebApi.Utils;
using Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.DOSalesProfiles;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.DOSales;
using Com.Ambassador.Service.Sales.Lib.Models.DOSales;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.DOSales;
using Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.ViewModels.ProductionOrder;
using Com.Ambassador.Service.Sales.WebApi.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;

namespace Com.Ambassador.Sales.Test.WebApi.Controllers
{
    public class DOSalesControllerTest : BaseControllerTest<DOSalesController, DOSalesModel, DOSalesViewModel, IDOSalesContract>
    {
        [Fact]
        public void Get_DO_Sales_Local_PDF_Success()
        {
            var vm = new DOSalesViewModel()
            {
                DOSalesType = "Lokal",
                DOSalesNo = "DOSalesNo",
                Date = DateTimeOffset.Now,
                HeadOfStorage = "HeadOfStorage",
                DOSalesCategory = "SPINNING",
                Storage = new StorageViewModel() 
                { 
                    name = "name",
                },
                Buyer = new Service.Sales.Lib.ViewModels.IntegrationViewModel.BuyerViewModel()
                {
                    Name = "BuyerName",
                },
                //DestinationBuyerName = "DestinationBuyerName",
                PackingUom = "PCS",
                LengthUom = "MTR",
                Disp = 1,
                Op = 1,
                Sc = 1,
                SalesContract = new FinishingPrintingSalesContractViewModel()
                {
                    SalesContractNo = "SalesContractNo",
                    Buyer = new Service.Sales.Lib.ViewModels.IntegrationViewModel.BuyerViewModel()
                    {
                        Name = "BuyerName",
                    },
                    Material = new Service.Sales.Lib.ViewModels.IntegrationViewModel.ProductViewModel()
                    {
                        Name = "MaterialName",
                    },
                    MaterialConstruction = new Service.Sales.Lib.ViewModels.IntegrationViewModel.MaterialConstructionViewModel()
                    {
                        Name = "MaterialConstructionName",
                    },
                },
                DOSalesDetailItems = new List<DOSalesDetailViewModel>()
                {
                    new DOSalesDetailViewModel()
                    {
                        ProductionOrder = new ProductionOrderViewModel()
                        {
                            OrderNo = "OrderNo",
                            Material = new Service.Sales.Lib.ViewModels.IntegrationViewModel.MaterialViewModel()
                            {
                                Name = "MaterialName",
                            },
                            MaterialConstruction = new Service.Sales.Lib.ViewModels.IntegrationViewModel.MaterialConstructionViewModel()
                            {
                                Name = "MaterialConstructionName",
                            },
                        },
                        UnitOrCode = "UnitCode",
                        Packing = 1,
                        Length = 1,
                        ConvertionValue = 1,
                        NoSOP="NoSOP",
                        ThreadNumber="ThreadNumber",
                        Grade="Grade",
                    }
                }
            };
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(s => s.Map<DOSalesViewModel>(It.IsAny<DOSalesModel>()))
                .Returns(vm);
            var controller = GetController(mocks);
            var response = controller.GetDOSalesPDF(1).Result;

            Assert.NotNull(response);
        }

        [Fact]
        public void Get_DO_Sales_Local_PDF_Weaving_Success()
        {

            var vm = new DOSalesViewModel()
            {
                DOSalesType = "Lokal",
                DOSalesNo = "DOSalesNo",
                Date = DateTimeOffset.Now,
                HeadOfStorage = "HeadOfStorage",
                DOSalesCategory = "WEAVING",
                Storage = new StorageViewModel()
                {
                    name = "name",
                },
                Buyer = new Service.Sales.Lib.ViewModels.IntegrationViewModel.BuyerViewModel()
                {
                    Name = "BuyerName",
                },
                //DestinationBuyerName = "DestinationBuyerName",
                PackingUom = "PCS",
                LengthUom = "MTR",
                Disp = 1,
                Op = 1,
                Sc = 1,
                SalesContract = new FinishingPrintingSalesContractViewModel()
                {
                    SalesContractNo = "SalesContractNo",
                    Buyer = new Service.Sales.Lib.ViewModels.IntegrationViewModel.BuyerViewModel()
                    {
                        Name = "BuyerName",
                    },
                    Material = new Service.Sales.Lib.ViewModels.IntegrationViewModel.ProductViewModel()
                    {
                        Name = "MaterialName",
                    },
                    MaterialConstruction = new Service.Sales.Lib.ViewModels.IntegrationViewModel.MaterialConstructionViewModel()
                    {
                        Name = "MaterialConstructionName",
                    },
                },
                DOSalesDetailItems = new List<DOSalesDetailViewModel>()
                {
                    new DOSalesDetailViewModel()
                    {
                        ProductionOrder = new ProductionOrderViewModel()
                        {
                            OrderNo = "OrderNo",
                            Material = new Service.Sales.Lib.ViewModels.IntegrationViewModel.MaterialViewModel()
                            {
                                Name = "MaterialName",
                            },
                            MaterialConstruction = new Service.Sales.Lib.ViewModels.IntegrationViewModel.MaterialConstructionViewModel()
                            {
                                Name = "MaterialConstructionName",
                            },
                        },
                        UnitOrCode = "UnitCode",
                        Packing = 1,
                        Length = 1,
                        ConvertionValue = 1,
                        NoSOP="NoSOP",
                        ThreadNumber="ThreadNumber",
                        Grade="Grade",
                    }
                }
            };
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(s => s.Map<DOSalesViewModel>(It.IsAny<DOSalesModel>()))
                .Returns(vm);
            var controller = GetController(mocks);
            var response = controller.GetDOSalesPDF(1).Result;

            Assert.NotNull(response);

        }

        [Fact]
        public void Get_DO_Sales_Local_PDF_Dyeing_Success()
        {
            var vm = new DOSalesViewModel()
            {
                DOSalesType = "Lokal",
                DOSalesNo = "DOSalesNo",
                Date = DateTimeOffset.Now,
                HeadOfStorage = "HeadOfStorage",
                DOSalesCategory = "DYEINGPRINTING",
                Storage = new StorageViewModel(){ },
                Buyer = new Service.Sales.Lib.ViewModels.IntegrationViewModel.BuyerViewModel()
                {
                    Name = "BuyerName",
                },
                //DestinationBuyerName = "DestinationBuyerName",
                PackingUom = "PCS",
                LengthUom = "MTR",
                Disp = 1,
                Op = 1,
                Sc = 1,
                SalesContract = new FinishingPrintingSalesContractViewModel()
                {
                    SalesContractNo = "SalesContractNo",
                    Buyer = new Service.Sales.Lib.ViewModels.IntegrationViewModel.BuyerViewModel()
                    {
                        Name = "BuyerName",
                    },
                    Material = new Service.Sales.Lib.ViewModels.IntegrationViewModel.ProductViewModel()
                    {
                        Name = "MaterialName",
                    },
                    MaterialConstruction = new Service.Sales.Lib.ViewModels.IntegrationViewModel.MaterialConstructionViewModel()
                    {
                        Name = "MaterialConstructionName",
                    },
                },
                DOSalesDetailItems = new List<DOSalesDetailViewModel>()
                {
                    new DOSalesDetailViewModel()
                    {
                        ProductionOrder = new ProductionOrderViewModel()
                        {
                            OrderNo = "OrderNo",
                            Material = new Service.Sales.Lib.ViewModels.IntegrationViewModel.MaterialViewModel()
                            {
                                Name = "MaterialName",
                            },
                            MaterialConstruction = new Service.Sales.Lib.ViewModels.IntegrationViewModel.MaterialConstructionViewModel()
                            {
                                Name = "MaterialConstructionName",
                            },
                        },
                        UnitOrCode = "UnitCode",
                        ConstructionName ="ConstructionName    \n",
                        Packing = 1,
                        Length = 1,
                        ConvertionValue = 1,
                        NoSOP="NoSOP",
                        ThreadNumber="ThreadNumber",
                        Grade="Grade",
                    }
                }
            };
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(s => s.Map<DOSalesViewModel>(It.IsAny<DOSalesModel>()))
                .Returns(vm);
            var controller = GetController(mocks);
            var response = controller.GetDOSalesPDF(1).Result;

            Assert.NotNull(response);
        }

        [Fact]
        public void Get_DO_Sales_Local_PDF_Dyeing_Success_YDS()
        {
            var vm = new DOSalesViewModel()
            {
                DOSalesType = "Lokal",
                DOSalesNo = "DOSalesNo",
                Date = DateTimeOffset.Now,
                HeadOfStorage = "HeadOfStorage",
                DOSalesCategory = "DYEINGPRINTING",
                Storage = new StorageViewModel() { },
                Buyer = new Service.Sales.Lib.ViewModels.IntegrationViewModel.BuyerViewModel()
                {
                    Name = "BuyerName",
                },
                //DestinationBuyerName = "DestinationBuyerName",
                PackingUom = "PCS",
                LengthUom = "YDS",
                Disp = 1,
                Op = 1,
                Sc = 1,
                SalesContract = new FinishingPrintingSalesContractViewModel()
                {
                    SalesContractNo = "SalesContractNo",
                    Buyer = new Service.Sales.Lib.ViewModels.IntegrationViewModel.BuyerViewModel()
                    {
                        Name = "BuyerName",
                    },
                    Material = new Service.Sales.Lib.ViewModels.IntegrationViewModel.ProductViewModel()
                    {
                        Name = "MaterialName",
                    },
                    MaterialConstruction = new Service.Sales.Lib.ViewModels.IntegrationViewModel.MaterialConstructionViewModel()
                    {
                        Name = "MaterialConstructionName",
                    },
                },
                DOSalesDetailItems = new List<DOSalesDetailViewModel>()
                {
                    new DOSalesDetailViewModel()
                    {
                        ProductionOrder = new ProductionOrderViewModel()
                        {
                            OrderNo = "OrderNo",
                            Material = new Service.Sales.Lib.ViewModels.IntegrationViewModel.MaterialViewModel()
                            {
                                Name = "MaterialName",
                            },
                            MaterialConstruction = new Service.Sales.Lib.ViewModels.IntegrationViewModel.MaterialConstructionViewModel()
                            {
                                Name = "MaterialConstructionName",
                            },
                        },
                        UnitOrCode = "UnitCode",
                        ConstructionName ="ConstructionName    \n",
                        Packing = 1,
                        Length = 1,
                        ConvertionValue = 1,
                        NoSOP="NoSOP",
                        ThreadNumber="ThreadNumber",
                        Grade="Grade",
                    }
                }
            };
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(s => s.Map<DOSalesViewModel>(It.IsAny<DOSalesModel>()))
                .Returns(vm);
            var controller = GetController(mocks);
            var response = controller.GetDOSalesPDF(1).Result;

            Assert.NotNull(response);
        }

        [Fact]
        public void Get_DO_Sales_Export_PDF_Success()
        {
            var vm = new DOSalesViewModel()
            {
                DOSalesType = "Ekspor",
                DOSalesNo = "DOSalesNo",
                Date = DateTimeOffset.Now,
                DoneBy = "DoneBy",
                PackingUom = "PT",
                WeightUom = "BALE",
                Storage = new StorageViewModel() { },
                SalesContract = new FinishingPrintingSalesContractViewModel()
                {
                    SalesContractNo = "SalesContractNo",
                    Material = new Service.Sales.Lib.ViewModels.IntegrationViewModel.ProductViewModel()
                    {
                        Name = "MaterialName",
                    },
                    MaterialConstruction = new Service.Sales.Lib.ViewModels.IntegrationViewModel.MaterialConstructionViewModel()
                    {
                        Name = "MaterialConstructionName",
                    },
                    Buyer = new Service.Sales.Lib.ViewModels.IntegrationViewModel.BuyerViewModel()
                    {
                        Name = "BuyerName",
                    },
                    Commodity = new Service.Sales.Lib.ViewModels.IntegrationViewModel.CommodityViewModel()
                    {
                        Name = "CommodityName",
                    },
                    MaterialWidth = "MaterialWidth",
                    OrderQuantity = 1,
                    PieceLength = "PieceLength",
                },
                FillEachBale = 1,
                Remark = "Remark",
                DOSalesDetailItems = new List<DOSalesDetailViewModel>()
                {
                    new DOSalesDetailViewModel()
                    {
                        ProductionOrder = new ProductionOrderViewModel()
                        {
                            OrderNo = "OrderNo",
                            Material = new Service.Sales.Lib.ViewModels.IntegrationViewModel.MaterialViewModel()
                            {
                                Name = "MaterialName",
                            },
                            MaterialConstruction = new Service.Sales.Lib.ViewModels.IntegrationViewModel.MaterialConstructionViewModel()
                            {
                                Name = "MaterialConstructionName",
                            },
                        },
                        UnitOrCode = "UnitCode",
                        Packing = 1,
                        Weight = 1,
                        ConvertionValue = 1,
                        NoSOP="NoSOP",
                        ThreadNumber="ThreadNumber",
                        Grade="Grade",

                    }
                }
            };
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(s => s.Map<DOSalesViewModel>(It.IsAny<DOSalesModel>()))
                .Returns(vm);
            var controller = GetController(mocks);
            var response = controller.GetDOSalesPDF(1).Result;

            Assert.NotNull(response);
        }

        [Fact]
        public void Get_DO_Sales_Export_Spinning_PDF_Success()
        {
            var vm = new DOSalesViewModel()
            {
                DOSalesType = "Ekspor",
                DOSalesNo = "DOSalesNo",
                DOSalesCategory = "SPINNING",
                Date = DateTimeOffset.Now,
                DoneBy = "DoneBy",
                PackingUom = "PT",
                WeightUom = "BALE",
                Storage = new StorageViewModel() { },
                SalesContract = new FinishingPrintingSalesContractViewModel()
                {
                    SalesContractNo = "SalesContractNo",
                    Material = new Service.Sales.Lib.ViewModels.IntegrationViewModel.ProductViewModel()
                    {
                        Name = "MaterialName",
                    },
                    MaterialConstruction = new Service.Sales.Lib.ViewModels.IntegrationViewModel.MaterialConstructionViewModel()
                    {
                        Name = "MaterialConstructionName",
                    },
                    Buyer = new Service.Sales.Lib.ViewModels.IntegrationViewModel.BuyerViewModel()
                    {
                        Name = "BuyerName",
                    },
                    Commodity = new Service.Sales.Lib.ViewModels.IntegrationViewModel.CommodityViewModel()
                    {
                        Name = "CommodityName",
                    },
                    MaterialWidth = "MaterialWidth",
                    OrderQuantity = 1,
                    PieceLength = "PieceLength",
                },
                FillEachBale = 1,
                Remark = "Remark",
                DOSalesDetailItems = new List<DOSalesDetailViewModel>()
                {
                    new DOSalesDetailViewModel()
                    {
                        ProductionOrder = new ProductionOrderViewModel()
                        {
                            OrderNo = "OrderNo",
                            Material = new Service.Sales.Lib.ViewModels.IntegrationViewModel.MaterialViewModel()
                            {
                                Name = "MaterialName",
                            },
                            MaterialConstruction = new Service.Sales.Lib.ViewModels.IntegrationViewModel.MaterialConstructionViewModel()
                            {
                                Name = "MaterialConstructionName",
                            },
                        },
                        UnitOrCode = "UnitCode",
                        Packing = 1,
                        Weight = 1,
                        ConvertionValue = 1,
                        NoSOP="NoSOP",
                        ThreadNumber="ThreadNumber",
                        Grade="Grade",

                    }
                }
            };
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(s => s.Map<DOSalesViewModel>(It.IsAny<DOSalesModel>()))
                .Returns(vm);
            var controller = GetController(mocks);
            var response = controller.GetDOSalesPDF(1).Result;

            Assert.NotNull(response);
        }

        [Fact]
        public void Get_DO_Sales_PDF_NotFound()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(default(DOSalesModel));
            var controller = GetController(mocks);
            var response = controller.GetDOSalesPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.NotFound, statusCode);

        }

        [Fact]
        public void Get_DO_Sales_PDF_Exception()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("error"));
            var controller = GetController(mocks);
            var response = controller.GetDOSalesPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);

        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DOSalesMapper>();
                cfg.AddProfile<DOSalesDetailMapper>();
            });
            var mapper = configuration.CreateMapper();

            DOSalesViewModel salesInvoiceViewModel = new DOSalesViewModel { Id = 1 };
            DOSalesModel salesInvoiceModel = mapper.Map<DOSalesModel>(salesInvoiceViewModel);

            Assert.Equal(salesInvoiceViewModel.Id, salesInvoiceModel.Id);

            DOSalesDetailViewModel salesInvoiceDetailViewModel = new DOSalesDetailViewModel { Id = 1 };
            DOSalesDetailModel salesInvoiceDetailModel = mapper.Map<DOSalesDetailModel>(salesInvoiceDetailViewModel);

            Assert.Equal(salesInvoiceDetailViewModel.Id, salesInvoiceDetailModel.Id);
        }

        [Fact]
        public void Validate_Validation()
        {
            List<DOSalesViewModel> viewModels = new List<DOSalesViewModel>
            {
                new DOSalesViewModel{
                    DOSalesType = "",
                    Type = "",
                    FillEachBale = null,
                    SalesContract = new FinishingPrintingSalesContractViewModel()
                    {
                        SalesContractNo = null,
                    },
                    DOSalesDetailItems = new List<DOSalesDetailViewModel>()
                    {
                        new DOSalesDetailViewModel() { },
                    }
                }
            };
            foreach (var viewModel in viewModels)
            {
                var defaultValidationResult = viewModel.Validate(null);
                Assert.True(defaultValidationResult.Count() > 0);
            }
        }

        [Fact]
        public void Validate_Validation_2()
        {
            List<DOSalesViewModel> viewModels = new List<DOSalesViewModel>
            {
                new DOSalesViewModel{
                    DOSalesType = "",
                    Type = "       ",
                    FillEachBale = null,
                    SalesContract = new FinishingPrintingSalesContractViewModel()
                    {
                        SalesContractNo = null,
                    },
                    DOSalesDetailItems = new List<DOSalesDetailViewModel>()
                    {
                        new DOSalesDetailViewModel() { },
                    }
                }
            };
            foreach (var viewModel in viewModels)
            {
                var defaultValidationResult = viewModel.Validate(null);
                Assert.True(defaultValidationResult.Count() > 0);
            }
        }

        [Fact]
        public void Validate_Validation_3()
        {
            List<DOSalesViewModel> viewModels = new List<DOSalesViewModel>
            {
                new DOSalesViewModel{
                    DOSalesType = "",
                    Type = "Type",
                    FillEachBale = null,
                    SalesContract = new FinishingPrintingSalesContractViewModel()
                    {
                        SalesContractNo = null,
                    },
                    DOSalesDetailItems = new List<DOSalesDetailViewModel>()
                    {
                        new DOSalesDetailViewModel() { },
                    }
                }
            };
            foreach (var viewModel in viewModels)
            {
                var defaultValidationResult = viewModel.Validate(null);
                Assert.True(defaultValidationResult.Count() > 0);
            }
        }

        [Fact]
        public void Validate_Validation_For_Local()
        {
            List<DOSalesViewModel> viewModels = new List<DOSalesViewModel>
            {
                new DOSalesViewModel{
                    DOSalesType = "Lokal",
                    Type = null,
                    Disp = -1,
                    Op = -1,
                    DOSalesCategory = "DYEINGPRINTING",
                    Sc = -1,
                    Storage = new StorageViewModel()
                    {
                        _id = 0,
                        name = "",
                        code = "",
                        unit = new UnitViewModel() {},
                    },
                    Commodity = new Service.Sales.Lib.ViewModels.IntegrationViewModel.CommodityViewModel()
                    {
                        Id = 0,
                        Code = "",
                        Name = "",
                        Type = "",
                    },
                    DOSalesDetailItems = new List<DOSalesDetailViewModel>()
                    {
                        new DOSalesDetailViewModel()
                        {
                            Length = -1,
                            ConvertionValue = -1,
                        }
                    }
                },
                new DOSalesViewModel{
                    DOSalesType = "Lokal",
                    Disp = null,
                    Op = null,
                    Sc = null,
                }
            };
            foreach (var viewModel in viewModels)
            {
                var defaultValidationResult = viewModel.Validate(null);
                Assert.True(defaultValidationResult.Count() > 0);
            }
        }

        [Fact]
        public void Validate_Validation_For_Local_2()
        {
            List<DOSalesViewModel> viewModels = new List<DOSalesViewModel>
            {
                new DOSalesViewModel{
                    DOSalesType = "Lokal",
                    Type = null,
                    Disp = -1,
                    Op = -1,
                    DOSalesCategory = "DYEINGPRINTING",
                    Sc = 2,
                    Storage = new StorageViewModel()
                    {
                        _id = 0,
                        name = "",
                        code = "",
                        unit = new UnitViewModel() {},
                    },
                    Commodity = new Service.Sales.Lib.ViewModels.IntegrationViewModel.CommodityViewModel()
                    {
                        Id = 0,
                        Code = "",
                        Name = "",
                        Type = "",
                    },
                    DOSalesDetailItems = new List<DOSalesDetailViewModel>()
                    {
                        new DOSalesDetailViewModel()
                        {
                            Length = -1,
                            ConvertionValue = -1,
                        }
                    }
                },
                new DOSalesViewModel{
                    DOSalesType = "Lokal",
                    Disp = null,
                    Op = null,
                    Sc = null,
                }
            };
            foreach (var viewModel in viewModels)
            {
                var defaultValidationResult = viewModel.Validate(null);
                Assert.True(defaultValidationResult.Count() > 0);
            }
        }

        [Fact]
        public void Validate_Validation_For_Local_Spinning()
        {
            List<DOSalesViewModel> viewModels = new List<DOSalesViewModel>
            {
                new DOSalesViewModel{
                    DOSalesType = "Lokal",
                    Type = null,
                    Disp = -1,
                    Op = -1,
                    DOSalesCategory = "SPINNING",
                    Sc = -1,
                    Storage = new StorageViewModel()
                    {
                        _id = 0,
                        name = "",
                        code = "",
                        unit = new UnitViewModel() {},
                    },
                    Commodity = new Service.Sales.Lib.ViewModels.IntegrationViewModel.CommodityViewModel()
                    {
                        Id = 0,
                        Code = "",
                        Name = "",
                        Type = "",
                    },
                    DOSalesDetailItems = new List<DOSalesDetailViewModel>()
                    {
                        new DOSalesDetailViewModel()
                        {
                            Length = -1,
                            ConvertionValue = -1,
                        }
                    }
                },
                new DOSalesViewModel{
                    DOSalesType = "Lokal",
                    Disp = null,
                    Op = null,
                    Sc = null,
                }
            };
            foreach (var viewModel in viewModels)
            {
                var defaultValidationResult = viewModel.Validate(null);
                Assert.True(defaultValidationResult.Count() > 0);
            }
        }

        [Fact]
        public void Validate_Validation_For_Local_Weaving()
        {
            List<DOSalesViewModel> viewModels = new List<DOSalesViewModel>
            {
                new DOSalesViewModel{
                    DOSalesType = "Lokal",
                    Type = null,
                    Disp = -1,
                    Op = -1,
                    DOSalesCategory = "WEAVING",
                    Sc = -1,
                    Storage = new StorageViewModel()
                    {
                        _id = 0,
                        name = "",
                        code = "",
                        unit = new UnitViewModel() {},
                    },
                    Commodity = new Service.Sales.Lib.ViewModels.IntegrationViewModel.CommodityViewModel()
                    {
                        Id = 0,
                        Code = "",
                        Name = "",
                        Type = "",
                    },
                    DOSalesDetailItems = new List<DOSalesDetailViewModel>()
                    {
                        new DOSalesDetailViewModel()
                        {
                            Length = -1,
                            ConvertionValue = -1,
                        }
                    }
                },
                new DOSalesViewModel{
                    DOSalesType = "Lokal",
                    Disp = null,
                    Op = null,
                    Sc = null,
                }
            };
            foreach (var viewModel in viewModels)
            {
                var defaultValidationResult = viewModel.Validate(null);
                Assert.True(defaultValidationResult.Count() > 0);
            }
        }


        [Fact]
        public void Validate_Validation_For_Export_Spinning()
        {
            List<DOSalesViewModel> viewModels = new List<DOSalesViewModel>
            {
                new DOSalesViewModel{
                    DOSalesType = "Ekspor",
                    DOSalesCategory="SPINNING",
                    FillEachBale = -1,
                    Storage = new StorageViewModel(){ },
                    DOSalesDetailItems = new List<DOSalesDetailViewModel>()
                    {
                        new DOSalesDetailViewModel()
                        {
                            Weight = -1,
                            ConvertionValue = -1,
                        }
                    }
                },
                new DOSalesViewModel{
                    DOSalesType = "Ekspor",
                    FillEachBale = null,
                }
            };
            foreach (var viewModel in viewModels)
            {
                var defaultValidationResult = viewModel.Validate(null);
                Assert.True(defaultValidationResult.Count() > 0);
            }
        }

        [Fact]
        public void Validate_Validation_For_Export_Weaving()
        {
            List<DOSalesViewModel> viewModels = new List<DOSalesViewModel>
            {
                new DOSalesViewModel{
                    DOSalesType = "Ekspor",
                    DOSalesCategory="WEAVING",
                    FillEachBale = -1,
                    Storage = new StorageViewModel(){ },
                    DOSalesDetailItems = new List<DOSalesDetailViewModel>()
                    {
                        new DOSalesDetailViewModel()
                        {
                            Weight = -1,
                            ConvertionValue = -1,
                        }
                    }
                },
                new DOSalesViewModel{
                    DOSalesType = "Ekspor",
                    FillEachBale = null,
                }
            };
            foreach (var viewModel in viewModels)
            {
                var defaultValidationResult = viewModel.Validate(null);
                Assert.True(defaultValidationResult.Count() > 0);
            }
        }

        [Fact]
        public void Validate_Validation_For_Export_Dyeing()
        {
            List<DOSalesViewModel> viewModels = new List<DOSalesViewModel>
            {
                new DOSalesViewModel{
                    DOSalesType = "Ekspor",
                    DOSalesCategory="DYEINGPRINTING",
                    FillEachBale = -1,
                    Storage = new StorageViewModel(){ },
                    DOSalesDetailItems = new List<DOSalesDetailViewModel>()
                    {
                        new DOSalesDetailViewModel()
                        {
                            Weight = -1,
                            ConvertionValue = -1,
                        }
                    }
                },
                new DOSalesViewModel{
                    DOSalesType = "Ekspor",
                    FillEachBale = null,
                }
            };
            foreach (var viewModel in viewModels)
            {
                var defaultValidationResult = viewModel.Validate(null);
                Assert.True(defaultValidationResult.Count() > 0);
            }
        }

        [Fact]
        public void Validate_Null_Model_and_Null_DetailViewModel()
        {
            List<DOSalesViewModel> viewModels = new List<DOSalesViewModel>
            {
                new DOSalesViewModel{}
            };
            foreach (var viewModel in viewModels)
            {
                var defaultValidationResult = viewModel.Validate(null);
                Assert.True(defaultValidationResult.Count() > 0);
            }
        }

        [Fact]
        public void GetDPStock_WithoutException_ReturnOK()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.ReadDPAndStock(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new ReadResponse<DOSalesModel>(new List<DOSalesModel>(), 0, new Dictionary<string, string>(), new List<string>()));
            mocks.Mapper.Setup(f => f.Map<List<DOSalesViewModel>>(It.IsAny<List<DOSalesModel>>())).Returns(this.ViewModels);

            var controller = GetController(mocks);
            var response = controller.GetDPAndStock();
            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void GetDPStock_ReadThrowException_ReturnInternalServerError()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.ReadDPAndStock(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());
            mocks.Mapper.Setup(f => f.Map<List<DOSalesViewModel>>(It.IsAny<List<DOSalesModel>>())).Returns(this.ViewModels);

            var controller = GetController(mocks);
            var response = controller.GetDPAndStock();
            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        [Fact]
        public void GetDPStockMobile_WithoutException_ReturnOK()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.ReadDPAndStock(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new ReadResponse<DOSalesModel>(new List<DOSalesModel>(), 0, new Dictionary<string, string>(), new List<string>()));
            mocks.Mapper.Setup(f => f.Map<List<DOSalesViewModel>>(It.IsAny<List<DOSalesModel>>())).Returns(this.ViewModels);

            var controller = GetController(mocks);
            var response = controller.GetDPAndStockForMobile();
            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void GetDPStockMobile_ReadThrowException_ReturnInternalServerError()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.ReadDPAndStock(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());
            mocks.Mapper.Setup(f => f.Map<List<DOSalesViewModel>>(It.IsAny<List<DOSalesModel>>())).Returns(this.ViewModels);

            var controller = GetController(mocks);
            var response = controller.GetDPAndStockForMobile();
            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }
    }
}
