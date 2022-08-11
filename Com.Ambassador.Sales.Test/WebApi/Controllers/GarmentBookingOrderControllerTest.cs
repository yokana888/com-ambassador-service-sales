using Com.Ambassador.Sales.Test.WebApi.Utils;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.GarmentBookingOrderInterface;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentBookingOrderModel;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentBookingOrderViewModels;
using Com.Ambassador.Service.Sales.WebApi.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Com.Ambassador.Sales.Test.WebApi.Controllers
{
    public class GarmentBookingOrderControllerTest : BaseControllerTest<GarmentBookingOrderController, GarmentBookingOrder, GarmentBookingOrderViewModel, IGarmentBookingOrder>
    {

        [Fact]
        public async Task Post_ThrowException_ReturnInternalServerErrors()
        {
            var ViewModel = new GarmentBookingOrderViewModel
            {
                BuyerName = "buyername",
                BuyerCode = "buyercode",
                IsBlockingPlan = true,
                SectionName = "sectionname",
                SectionCode = "sectioncode",
                DeliveryDate = DateTimeOffset.Now.AddDays(20),
                BookingOrderDate = DateTimeOffset.Now
            };
            var mocks = GetMocks();
            mocks.ValidateService
                .Setup(s => s.Validate(It.IsAny<GarmentBookingOrderViewModel>()))
                .Verifiable();
            mocks.Facade
                .Setup(s => s.CreateAsync(It.IsAny<GarmentBookingOrder>()))
                .ThrowsAsync(new Exception());

            var controller = GetController(mocks);
            var response = await controller.Post(ViewModel);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_Cancel_Leftover()
        {
            var ViewModel = new GarmentBookingOrderViewModel
            {
                BuyerName = "buyername",
                BuyerCode = "buyercode",
                IsBlockingPlan = true,
                SectionName = "sectionname",
                SectionCode = "sectioncode",
                DeliveryDate = DateTimeOffset.Now.AddDays(20),
                BookingOrderDate = DateTimeOffset.Now
            };
            var mocks = GetMocks();
            mocks.ValidateService
                .Setup(s => s.Validate(It.IsAny<GarmentBookingOrderViewModel>()))
                .Verifiable();
            mocks.Facade
                .Setup(s => s.CreateAsync(It.IsAny<GarmentBookingOrder>()))
                .ThrowsAsync(new Exception());

            var controller = GetController(mocks);
            var response = await controller.CancelLeftOvers((int)ViewModel.Id, ViewModel);

            Assert.Equal((int)HttpStatusCode.NoContent, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Exception_Cancel_Leftover()
        {
            var ViewModel = new GarmentBookingOrderViewModel
            {
                BuyerName = "buyername",
                BuyerCode = "buyercode",
                IsBlockingPlan = true,
                SectionName = "sectionname",
                SectionCode = "sectioncode",
                DeliveryDate = DateTimeOffset.Now.AddDays(20),
                BookingOrderDate = DateTimeOffset.Now
            };
            var mocks = GetMocks();
            mocks.ValidateService
                .Setup(s => s.Validate(It.IsAny<GarmentBookingOrderViewModel>()))
                .Verifiable();
            mocks.Facade
                .Setup(s => s.CreateAsync(It.IsAny<GarmentBookingOrder>()))
                .ThrowsAsync(new Exception());

            mocks.Facade
                .Setup(s => s.BOCancel(It.IsAny<int>(), It.IsAny<GarmentBookingOrder>()))
                .ThrowsAsync(new Exception());

            var controller = GetController(mocks);
            var response = await controller.CancelLeftOvers((int)ViewModel.Id, ViewModel);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_Delete_Leftover()
        {
            var ViewModel = new GarmentBookingOrderViewModel
            {
                BuyerName = "buyername",
                BuyerCode = "buyercode",
                IsBlockingPlan = true,
                SectionName = "sectionname",
                SectionCode = "sectioncode",
                DeliveryDate = DateTimeOffset.Now.AddDays(20),
                BookingOrderDate = DateTimeOffset.Now
            };
            var mocks = GetMocks();
            mocks.ValidateService
                .Setup(s => s.Validate(It.IsAny<GarmentBookingOrderViewModel>()))
                .Verifiable();
            mocks.Facade
                .Setup(s => s.CreateAsync(It.IsAny<GarmentBookingOrder>()))
                .ThrowsAsync(new Exception());

            var controller = GetController(mocks);
            var response = await controller.DeleteLeftOvers((int)ViewModel.Id, ViewModel);

            Assert.Equal((int)HttpStatusCode.NoContent, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Exception_Delete_Leftover()
        {
            var ViewModel = new GarmentBookingOrderViewModel
            {
                BuyerName = "buyername",
                BuyerCode = "buyercode",
                IsBlockingPlan = true,
                SectionName = "sectionname",
                SectionCode = "sectioncode",
                DeliveryDate = DateTimeOffset.Now.AddDays(20),
                BookingOrderDate = DateTimeOffset.Now
            };
            var mocks = GetMocks();
            mocks.ValidateService
                .Setup(s => s.Validate(It.IsAny<GarmentBookingOrderViewModel>()))
                .Verifiable();
            mocks.Facade
                .Setup(s => s.CreateAsync(It.IsAny<GarmentBookingOrder>()))
                .ThrowsAsync(new Exception());
            mocks.Facade
                .Setup(s => s.BODelete(It.IsAny<int>(), It.IsAny<GarmentBookingOrder>()))
                .ThrowsAsync(new Exception());
            var controller = GetController(mocks);
            var response = await controller.DeleteLeftOvers((int)ViewModel.Id, ViewModel);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Put_ThrowServiceValidationExeption_ReturnBadRequest_DeliveryDate()
        {
            var mocks = this.GetMocks();
            mocks.ValidateService.Setup(s => s.Validate(It.IsAny<GarmentBookingOrderViewModel>())).Throws(this.GetServiceValidationException());
            var ViewModel = new GarmentBookingOrderViewModel
            {
                BuyerName = "buyername",
                BuyerCode = "buyercode",
                IsBlockingPlan = true,
                SectionName = "sectionname",
                SectionCode = "sectioncode",
                DeliveryDate = DateTimeOffset.Now.AddDays(20),
                BookingOrderDate = DateTimeOffset.Now,
                Items = new List<GarmentBookingOrderItemViewModel>
                {
                    new GarmentBookingOrderItemViewModel
                    {
                        DeliveryDate=DateTimeOffset.Now.AddDays(22),
                        ComodityCode="como",
                        ComodityId=It.IsAny<long>(),
                        ComodityName=It.IsAny<string>(),
                        ConfirmQuantity=It.IsAny<double>(),
                    }
                }
            };

            var controller = GetController(mocks);
            var response = await controller.Put((int)ViewModel.Id, ViewModel);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public void Should_Success_GetByNo()
        {

            var mocks = GetMocks();
            mocks.Mapper
                .Setup(s => s.Map<List<GarmentBookingOrderViewModel>>(It.IsAny<List<GarmentBookingOrder>>()))
                .Returns(new List<GarmentBookingOrderViewModel>());
            mocks.Facade
                .Setup(s => s.ReadByBookingOrderNo(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(),
                    It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ReadResponse<GarmentBookingOrder>(new List<GarmentBookingOrder>(), 1,new Dictionary<string, string>(),new List<string>()));

            var controller = GetController(mocks);
            var response = controller.Get(1, 25, new List<string>(), null, null, null);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Fail_GetByNo()
        {

            var mocks = GetMocks();
            mocks.Mapper
                .Setup(s => s.Map<List<GarmentBookingOrderViewModel>>(It.IsAny<List<GarmentBookingOrder>>()))
                .Returns(new List<GarmentBookingOrderViewModel>());
            mocks.Facade
                .Setup(s => s.ReadByBookingOrderNo(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(),
                    It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new Exception());

            var controller = GetController(mocks);
            var response = controller.Get(1, 25, new List<string>(), null, null, null);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
