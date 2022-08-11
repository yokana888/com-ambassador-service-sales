using AutoMapper;
using Com.Ambassador.Sales.Test.WebApi.Utils;
using Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.SalesInvoiceExportProfiles;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.SalesInvoiceExport;
using Com.Ambassador.Service.Sales.Lib.Models.SalesInvoiceExport;
using Com.Ambassador.Service.Sales.Lib.ViewModels.SalesInvoiceExport;
using Com.Ambassador.Service.Sales.WebApi.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.WebApi.Controllers
{
    public class SalesInvoiceExportControllerTest : BaseControllerTest<SalesInvoiceExportController, SalesInvoiceExportModel, SalesInvoiceExportViewModel, ISalesInvoiceExportContract>
    {
        [Fact]
        public void Get_Sales_Invoice_Export_Valas_PDF_SalesInvoiceCategory_DyeingPrinting_And_SalesInvoiceType_LC()
        {
            var vm = new SalesInvoiceExportViewModel()
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
                SalesInvoiceExportDetails = new List<SalesInvoiceExportDetailViewModel>()
                {
                    new SalesInvoiceExportDetailViewModel()
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
                        SalesInvoiceExportItems = new List<SalesInvoiceExportItemViewModel>()
                        {
                            new SalesInvoiceExportItemViewModel()
                            {
                                ProductCode = "ProductCode",
                                ProductName = "ProductName",
                                QuantityPacking = 100,
                                PackingUom = "PackingUom",
                                ItemUom = "MTR",
                                QuantityItem = 1,
                                Price = 1,
                                Amount = 1,
                            }
                        }
                    }
                }

            };
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(s => s.Map<SalesInvoiceExportViewModel>(It.IsAny<SalesInvoiceExportModel>()))
                .Returns(vm);
            var controller = GetController(mocks);
            var response = controller.GetSalesInvoiceExportValasPDF(1).Result;

            Assert.NotNull(response);
        }

        [Fact]
        public void Get_Sales_Invoice_Export_IDR_PDF_SalesInvoiceCategory_DyeingPrinting_And_SalesInvoiceType_LC()
        {
            var vm = new SalesInvoiceExportViewModel()
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
                SalesInvoiceExportDetails = new List<SalesInvoiceExportDetailViewModel>()
                {
                    new SalesInvoiceExportDetailViewModel()
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
                        SalesInvoiceExportItems = new List<SalesInvoiceExportItemViewModel>()
                        {
                            new SalesInvoiceExportItemViewModel()
                            {
                                ProductCode = "ProductCode",
                                ProductName = "ProductName",
                                QuantityPacking = 100,
                                PackingUom = "PackingUom",
                                ItemUom = "MTR",
                                QuantityItem = 1,
                                Price = 1,
                                Amount = 1,
                            }
                        }
                    }
                }

            };
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(s => s.Map<SalesInvoiceExportViewModel>(It.IsAny<SalesInvoiceExportModel>()))
                .Returns(vm);
            var controller = GetController(mocks);
            var response = controller.GetSalesInvoiceExportIDRPDF(1).Result;

            Assert.NotNull(response);
        }

        [Fact]
        public void Get_Sales_Invoice_Export_Valas_PDF_SalesInvoiceCategory_SpinningWeaving_And_SalesInvoiceType_PP()
        {
            var vm = new SalesInvoiceExportViewModel()
            {
                Code = "code",
                AutoIncreament = 1,
                SalesInvoiceNo = "SalesInvoiceNo",
                SalesInvoiceCategory = "SPINNING",
                LetterOfCreditNumberType = "P.P",
                SalesInvoiceDate = DateTimeOffset.UtcNow,
                BuyerName = "BuyerName",
                BuyerAddress = "BuyerAddress",
                Authorized = "Amumpuni",
                ShippedPer = "ShippedPer",
                SailingDate = DateTimeOffset.UtcNow,
                LetterOfCreditNumber = "LetterOfCreditNumber",
                HSCode = "HSCode",
                TermOfPaymentType = "TermOfPaymentType",
                TermOfPaymentRemark = "TermOfPaymentRemark",
                ShippingRemark = "ShippingRemark",
                Remark = "Remark",
                SalesInvoiceExportDetails = new List<SalesInvoiceExportDetailViewModel>()
                {
                    new SalesInvoiceExportDetailViewModel()
                    {
                        BonId = 4,
                        BonNo = "BonNo",
                        ContractNo = "ContractNo",
                        GrossWeight = 100,
                        NetWeight = 100,
                        TotalMeas = 100,
                        WeightUom = "KG",
                        TotalUom = "CBM",
                        SalesInvoiceExportItems = new List<SalesInvoiceExportItemViewModel>()
                        {
                            new SalesInvoiceExportItemViewModel()
                            {
                                ProductCode = "ProductCode",
                                ProductName = "ProductName",
                                QuantityPacking = 100,
                                PackingUom = "PackingUom",
                                ItemUom = "MTR",
                                QuantityItem = 1,
                                Price = 1,
                                Amount = 1,
                            }
                        }
                    }
                }

            };
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(s => s.Map<SalesInvoiceExportViewModel>(It.IsAny<SalesInvoiceExportModel>()))
                .Returns(vm);
            var controller = GetController(mocks);
            var response = controller.GetSalesInvoiceExportValasPDF(1).Result;

            Assert.NotNull(response);
        }

        [Fact]
        public void Get_Sales_Invoice_Export_IDR_PDF_SalesInvoiceCategory_SpinningWeaving_And_SalesInvoiceType_PP()
        {
            var vm = new SalesInvoiceExportViewModel()
            {
                Code = "code",
                AutoIncreament = 1,
                SalesInvoiceNo = "SalesInvoiceNo",
                SalesInvoiceCategory = "SPINNING",
                LetterOfCreditNumberType = "P.P",
                SalesInvoiceDate = DateTimeOffset.UtcNow,
                BuyerName = "BuyerName",
                BuyerAddress = "BuyerAddress",
                Authorized = "Amumpuni",
                ShippedPer = "ShippedPer",
                SailingDate = DateTimeOffset.UtcNow,
                LetterOfCreditNumber = "LetterOfCreditNumber",
                HSCode = "HSCode",
                TermOfPaymentType = "TermOfPaymentType",
                TermOfPaymentRemark = "TermOfPaymentRemark",
                ShippingRemark = "ShippingRemark",
                Remark = "Remark",
                SalesInvoiceExportDetails = new List<SalesInvoiceExportDetailViewModel>()
                {
                    new SalesInvoiceExportDetailViewModel()
                    {
                        BonId = 4,
                        BonNo = "BonNo",
                        ContractNo = "ContractNo",
                        GrossWeight = 100,
                        NetWeight = 100,
                        TotalMeas = 100,
                        WeightUom = "KG",
                        TotalUom = "CBM",
                        SalesInvoiceExportItems = new List<SalesInvoiceExportItemViewModel>()
                        {
                            new SalesInvoiceExportItemViewModel()
                            {
                                ProductCode = "ProductCode",
                                ProductName = "ProductName",
                                QuantityPacking = 100,
                                PackingUom = "PackingUom",
                                ItemUom = "MTR",
                                QuantityItem = 1,
                                Price = 1,
                                Amount = 1,
                            }
                        }
                    }
                }

            };
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(s => s.Map<SalesInvoiceExportViewModel>(It.IsAny<SalesInvoiceExportModel>()))
                .Returns(vm);
            var controller = GetController(mocks);
            var response = controller.GetSalesInvoiceExportIDRPDF(1).Result;

            Assert.NotNull(response);
        }

        [Fact]
        public void Get_Sales_Invoice_Export_Valas_PDF_NotFound()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(default(SalesInvoiceExportModel));
            var controller = GetController(mocks);
            var response = controller.GetSalesInvoiceExportValasPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.NotFound, statusCode);

        }

        [Fact]
        public void Get_Sales_Invoice_Export_IDR_PDF_NotFound()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(default(SalesInvoiceExportModel));
            var controller = GetController(mocks);
            var response = controller.GetSalesInvoiceExportIDRPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.NotFound, statusCode);

        }

        [Fact]
        public void Get_Sales_Invoice_Export_Valas_PDF_Exception()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("error"));
            var controller = GetController(mocks);
            var response = controller.GetSalesInvoiceExportValasPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);

        }

        [Fact]
        public void Get_Sales_Invoice_Export_IDR_PDF_Exception()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("error"));
            var controller = GetController(mocks);
            var response = controller.GetSalesInvoiceExportIDRPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);

        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<SalesInvoiceExportMapper>();
                cfg.AddProfile<SalesInvoiceExportDetailMapper>();
                cfg.AddProfile<SalesInvoiceExportItemMapper>();
            });
            var mapper = configuration.CreateMapper();

            SalesInvoiceExportViewModel salesInvoiceViewModel = new SalesInvoiceExportViewModel { Id = 1 };
            SalesInvoiceExportModel salesInvoiceModel = mapper.Map<SalesInvoiceExportModel>(salesInvoiceViewModel);

            Assert.Equal(salesInvoiceViewModel.Id, salesInvoiceModel.Id);

            SalesInvoiceExportDetailViewModel salesInvoiceDetailViewModel = new SalesInvoiceExportDetailViewModel { Id = 1 };
            SalesInvoiceExportDetailModel salesInvoiceDetailModel = mapper.Map<SalesInvoiceExportDetailModel>(salesInvoiceDetailViewModel);

            Assert.Equal(salesInvoiceDetailViewModel.Id, salesInvoiceDetailModel.Id);

            SalesInvoiceExportItemViewModel salesInvoiceItemViewModel = new SalesInvoiceExportItemViewModel { Id = 1 };
            SalesInvoiceExportItemModel salesInvoiceItemModel = mapper.Map<SalesInvoiceExportItemModel>(salesInvoiceItemViewModel);

            Assert.Equal(salesInvoiceItemViewModel.Id, salesInvoiceItemModel.Id);
        }

        [Fact]
        public void Validate_Validation_ViewModel()
        {
            List<SalesInvoiceExportViewModel> viewModels = new List<SalesInvoiceExportViewModel>
            {
                new SalesInvoiceExportViewModel(){},
                new SalesInvoiceExportViewModel()
                {
                    SalesInvoiceExportDetails = new List<SalesInvoiceExportDetailViewModel>()
                    {
                        new SalesInvoiceExportDetailViewModel() {},
                        new SalesInvoiceExportDetailViewModel() {
                            SalesInvoiceExportItems = new List<SalesInvoiceExportItemViewModel>()
                            {
                                new SalesInvoiceExportItemViewModel() {},
                            }
                        },
                    }
                },
            };
            foreach (var viewModel in viewModels)
            {
                var defaultValidationResult = viewModel.Validate(null);
                Assert.True(defaultValidationResult.Count() > 0);
            }
        }

        [Fact]
        public void Validate_Validation()
        {
            List<SalesInvoiceExportViewModel> viewModels = new List<SalesInvoiceExportViewModel>
            {
                new SalesInvoiceExportViewModel
                {
                    SalesInvoiceCategory = "",
                    LetterOfCreditNumberType = "",
                    SalesInvoiceDate = DateTimeOffset.Now.AddDays(10),
                    Authorized = "",
                    TermOfPaymentType = "",
                    SalesInvoiceExportDetails = new List<SalesInvoiceExportDetailViewModel>()
                    {
                        new SalesInvoiceExportDetailViewModel()
                        {
                            GrossWeight = -100,
                            NetWeight = -100,
                            TotalMeas = -100,
                        },
                    },
                },
                new SalesInvoiceExportViewModel
                {
                    SalesInvoiceCategory = null,
                    LetterOfCreditNumberType = null,
                    SalesInvoiceDate = null,
                    BuyerName = null,
                    BuyerAddress = null,
                    Authorized = null,
                    ShippedPer = null,
                    SailingDate = null,
                    LetterOfCreditNumber = null,
                    HSCode = null,
                    TermOfPaymentType = null,
                    TermOfPaymentRemark = null,
                    ShippingRemark = null,

                    SalesInvoiceExportDetails = new List<SalesInvoiceExportDetailViewModel>()
                    {
                        new SalesInvoiceExportDetailViewModel()
                        {
                            BonNo  = null,
                    ContractNo = null,
                            Description = null,
                            GrossWeight = null,
                            NetWeight = null,
                            TotalMeas = null,
                        },
                    },
                },
                new SalesInvoiceExportViewModel
                {
                    SalesInvoiceCategory = "DYEINGPRINTING",
                    FPType = "",
                },
                new SalesInvoiceExportViewModel
                {
                    SalesInvoiceCategory = "DYEINGPRINTING",
                    FPType = null,
                    LetterOfCreditNumberType = "L/C",
                    LCDate = null,
                    From = null,
                    To = null,
                },
                new SalesInvoiceExportViewModel
                {
                    SalesInvoiceExportDetails = new List<SalesInvoiceExportDetailViewModel>()
                    {
                        new SalesInvoiceExportDetailViewModel()
                        {
                            BonId = 4,
                            BonNo = "BonNo",
                            GrossWeight = 100,
                            NetWeight = 100,
                            TotalMeas = 100,
                            WeightUom = "KG",
                            TotalUom = "CBM",
                            SalesInvoiceExportItems = new List<SalesInvoiceExportItemViewModel>()
                            {
                                new SalesInvoiceExportItemViewModel()
                                {
                                    QuantityPacking = -100,
                                    QuantityItem = -100,
                                    Price = -100,
                                },
                                new SalesInvoiceExportItemViewModel()
                                {
                                    ProductCode = null,
                                    ProductName = null,
                                    QuantityPacking = null,
                                    QuantityItem = null,
                                    PackingUom = null,
                                    ItemUom = null,
                                    Price = null,
                                },
                            },
                        },
                        new SalesInvoiceExportDetailViewModel()
                        {
                            BonId = 4,
                            BonNo = "BonNo",
                            GrossWeight = 100,
                            NetWeight = 100,
                            TotalMeas = 100,
                            WeightUom = "KG",
                            TotalUom = "CBM",
                            SalesInvoiceExportItems = new List<SalesInvoiceExportItemViewModel>() { },
                        },
                    },
                },
            };
            foreach (var viewModel in viewModels)
            {
                var defaultValidationResult = viewModel.Validate(null);
                Assert.True(defaultValidationResult.Count() > 0);
            }
        }

        [Fact]
        public void Validate_Duplicate_DetailViewModel()
        {
            List<SalesInvoiceExportViewModel> viewModels = new List<SalesInvoiceExportViewModel>
            {
                new SalesInvoiceExportViewModel {
                    SalesInvoiceCategory = "DYEINGPRINTING",
                    SalesInvoiceExportDetails = new List<SalesInvoiceExportDetailViewModel>()
                    {
                        new SalesInvoiceExportDetailViewModel()
                        {
                            BonId = 1,
                            BonNo ="BonNo",
                            SalesInvoiceExportItems = new List<SalesInvoiceExportItemViewModel>()
                            {
                                new SalesInvoiceExportItemViewModel()
                                {
                                    Id = 2,
                                    ProductCode = "ProductCode",
                                    QuantityPacking = 100,
                                    PackingUom = "PackingUom",
                                    QuantityItem = 10,
                                    ItemUom = "MTR",
                                    ProductName = "ProductName",
                                    Price = 100,
                                    Amount = 100,
                                },
                                new SalesInvoiceExportItemViewModel()
                                {
                                    Id = 2,
                                    ProductCode = "ProductCode",
                                    QuantityPacking = 100,
                                    PackingUom = "PackingUom",
                                    QuantityItem = 10,
                                    ItemUom = "MTR",
                                    ProductName = "ProductName",
                                    Price = 100,
                                    Amount = 100,
                                },
                            }
                        },
                        new SalesInvoiceExportDetailViewModel()
                        {
                            BonId = 1,
                            BonNo ="BonNo",
                        }
                    }
                }
            };
            foreach (var viewModel in viewModels)
            {
                var defaultValidationResult = viewModel.Validate(null);
                Assert.True(defaultValidationResult.Count() > 0);
            }
        }
    }
}
