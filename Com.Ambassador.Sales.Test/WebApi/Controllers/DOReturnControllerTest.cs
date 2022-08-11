using AutoMapper;
using Com.Ambassador.Sales.Test.WebApi.Utils;
using Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.DOReturnProfiles;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.DOReturn;
using Com.Ambassador.Service.Sales.Lib.Models.DOReturn;
using Com.Ambassador.Service.Sales.Lib.ViewModels.DOReturn;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using Com.Ambassador.Service.Sales.Lib.ViewModels.SalesInvoice;
using Com.Ambassador.Service.Sales.WebApi.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;

namespace Com.Ambassador.Sales.Test.WebApi.Controllers
{
    public class DOReturnControllerTest : BaseControllerTest<DOReturnController, DOReturnModel, DOReturnViewModel, IDOReturnContract>
    {
        [Fact]
        public void Get_DO_Return_PDF_Success()
        {
            var vm = new DOReturnViewModel()
            {
                DOReturnType = "Type",
                DOReturnNo = "DOReturnNo",
                AutoIncreament = 1,
                DOReturnDate = DateTimeOffset.Now,
                HeadOfStorage = "HeadOfStorage",
                ReturnFrom = new BuyerViewModel()
                {
                    Id = 1,
                    Name = "ReturnFromName",
                },
                LTKPNo = "LKTPNo",
                Remark = "Remark",
                DOReturnDetails = new List<DOReturnDetailViewModel>()
                {
                    new DOReturnDetailViewModel()
                    {
                        SalesInvoice = new SalesInvoiceViewModel() { },
                        DOReturnDetailItems = new List<DOReturnDetailItemViewModel>()
                        {
                            new DOReturnDetailItemViewModel()
                            {
                                //DOSales = new Service.Sales.Lib.ViewModels.DOSales.DOSalesViewModel() { },
                            }
                        },
                        DOReturnItems = new List<DOReturnItemViewModel>()
                        {
                            new DOReturnItemViewModel()
                            {
                                //SalesInvoiceDetail = new SalesInvoiceDetailViewModel()
                                //{
                                //    ShippingOutId = 1,
                                //    BonNo = "BonNo",
                                //},
                                //SalesInvoiceItem = new SalesInvoiceItemViewModel()
                                //{
                                //    ProductId = 1,
                                //    ProductCode = "ProductCode",
                                //    ProductName = "ProductName",
                                //    QuantityPacking = 1,
                                //    PackingUom = "PackingUom",
                                //    ItemUom = "ItemUom",
                                //    QuantityItem = 1,
                                //},

                                ShippingOutId = 1,
                                BonNo = "BonNo",
                                ProductId = 1,
                                ProductCode = "ProductCode",
                                ProductName = "ProductName",
                                QuantityPacking = 1,
                                PackingUom = "PackingUom",
                                ItemUom = "ItemUom",
                                QuantityItem = 1,
                            }
                        },
                    }
                }
            };
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(s => s.Map<DOReturnViewModel>(It.IsAny<DOReturnModel>()))
                .Returns(vm);
            var controller = GetController(mocks);
            var response = controller.GetDOReturnPDF(1).Result;

            Assert.NotNull(response);
        }

        [Fact]
        public void Get_DO_Return_PDF_NotFound()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(default(DOReturnModel));
            var controller = GetController(mocks);
            var response = controller.GetDOReturnPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.NotFound, statusCode);

        }

        [Fact]
        public void GetDOReturnPDF_Return_BadRequest()
        {
            var mocks = GetMocks();
            var controller = GetController(mocks);
            controller.ModelState.AddModelError("key", "test");
            var response = controller.GetDOReturnPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.BadRequest, statusCode);

        }
       

        [Fact]
        public void Get_DO_Return_PDF_Exception()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("error"));
            var controller = GetController(mocks);
            var response = controller.GetDOReturnPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);

        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DOReturnMapper>();
                cfg.AddProfile<DOReturnDetailMapper>();
                cfg.AddProfile<DOReturnDetailItemMapper>();
                cfg.AddProfile<DOReturnItemMapper>();
            });
            var mapper = configuration.CreateMapper();

            DOReturnViewModel salesInvoiceViewModel = new DOReturnViewModel { Id = 1 };
            DOReturnModel salesInvoiceModel = mapper.Map<DOReturnModel>(salesInvoiceViewModel);

            Assert.Equal(salesInvoiceViewModel.Id, salesInvoiceModel.Id);

            DOReturnDetailViewModel salesInvoiceDetailViewModel = new DOReturnDetailViewModel { Id = 1 };
            DOReturnDetailModel salesInvoiceDetailModel = mapper.Map<DOReturnDetailModel>(salesInvoiceDetailViewModel);

            Assert.Equal(salesInvoiceDetailViewModel.Id, salesInvoiceDetailModel.Id);

            DOReturnDetailItemViewModel salesInvoiceDetailItemViewModel = new DOReturnDetailItemViewModel { Id = 1 };
            DOReturnDetailItemModel salesInvoiceDetailItemModel = mapper.Map<DOReturnDetailItemModel>(salesInvoiceDetailItemViewModel);

            Assert.Equal(salesInvoiceDetailItemViewModel.Id, salesInvoiceDetailItemModel.Id);

            DOReturnItemViewModel salesInvoiceItemViewModel = new DOReturnItemViewModel { Id = 1 };
            DOReturnItemModel salesInvoiceItemModel = mapper.Map<DOReturnItemModel>(salesInvoiceItemViewModel);

            Assert.Equal(salesInvoiceItemViewModel.Id, salesInvoiceItemModel.Id);
        }

        [Fact]
        public void Validate_Validation_ViewModel()
        {
            List<DOReturnViewModel> viewModels = new List<DOReturnViewModel>
            {
                new DOReturnViewModel()
                {
                    ReturnFrom = new BuyerViewModel() {},
                    DOReturnDetails = new List<DOReturnDetailViewModel>()
                    {
                        new DOReturnDetailViewModel()
                        {
                            SalesInvoice = new SalesInvoiceViewModel()
                            {
                                Id = 1,
                                SalesInvoiceNo ="SalesInvoiceNo",
                            },
                            DOReturnDetailItems = new List<DOReturnDetailItemViewModel>()
                            {
                                new DOReturnDetailItemViewModel()
                                {
                                    //DOSales = new Service.Sales.Lib.ViewModels.DOSales.DOSalesViewModel(){ },
                                },
                                new DOReturnDetailItemViewModel() {},
                            },
                            DOReturnItems = new List<DOReturnItemViewModel>()
                            {
                                new DOReturnItemViewModel()
                                {
                                    //SalesInvoiceDetail = new SalesInvoiceDetailViewModel(){ },
                                    //SalesInvoiceItem = new SalesInvoiceItemViewModel()
                                    //{
                                    //   QuantityItem = -1,
                                    //   QuantityPacking = -1,
                                    //},
                                    QuantityItem = -1,
                                    QuantityPacking = -1,
                                },
                                 new DOReturnItemViewModel() {},
                            }
                        },
                        new DOReturnDetailViewModel()
                        {
                            SalesInvoice = new SalesInvoiceViewModel()
                            {
                                Id = 1,
                                SalesInvoiceNo ="SalesInvoiceNo",
                            },
                            DOReturnDetailItems = new List<DOReturnDetailItemViewModel>() { },
                            DOReturnItems = new List<DOReturnItemViewModel>() { },
                        },



                        new DOReturnDetailViewModel()
                        {
                            SalesInvoice = new SalesInvoiceViewModel()
                            {
                                Id = 1,
                                SalesInvoiceNo ="SalesInvoiceNo",
                            },
                            DOReturnDetailItems = new List<DOReturnDetailItemViewModel>()
                            {
                                new DOReturnDetailItemViewModel()
                                {
                                    //DOReturnItems = new List<DOReturnItemViewModel>() { },
                                },
                            },
                            DOReturnItems = new List<DOReturnItemViewModel>()
                            {
                                new DOReturnItemViewModel(){ },
                            },
                        },
                    },
                },


                new DOReturnViewModel() {},
                new DOReturnViewModel()
                {
                    ReturnFrom = new BuyerViewModel() {},
                },
                new DOReturnViewModel()
                {
                    ReturnFrom = new BuyerViewModel() {},
                    DOReturnDetails = new List<DOReturnDetailViewModel>()
                    {
                        new DOReturnDetailViewModel() { },
                    },
                },
                new DOReturnViewModel()
                {
                    ReturnFrom = new BuyerViewModel() {},
                    DOReturnDetails = new List<DOReturnDetailViewModel>()
                    {
                        new DOReturnDetailViewModel()
                        {
                            DOReturnDetailItems = new List<DOReturnDetailItemViewModel>()
                            {
                                new DOReturnDetailItemViewModel() { },
                            },
                            DOReturnItems = new List<DOReturnItemViewModel>()
                            {
                                new DOReturnItemViewModel() {},
                            },
                        },
                    },
                },
                new DOReturnViewModel()
                {
                    ReturnFrom = new BuyerViewModel() {},
                    DOReturnDetails = new List<DOReturnDetailViewModel>()
                    {
                        new DOReturnDetailViewModel()
                        {
                            DOReturnDetailItems = new List<DOReturnDetailItemViewModel>()
                            {
                                new DOReturnDetailItemViewModel() {},
                            },
                            DOReturnItems = new List<DOReturnItemViewModel>()
                            {
                                new DOReturnItemViewModel() {},
                            },
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
    }
}
