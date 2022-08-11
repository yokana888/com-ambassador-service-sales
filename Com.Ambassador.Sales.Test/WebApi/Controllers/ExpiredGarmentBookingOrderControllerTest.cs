using Com.Ambassador.Sales.Test.WebApi.Utils;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.GarmentBookingOrderInterface;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentBookingOrderModel;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentBookingOrderViewModels;
using Com.Ambassador.Service.Sales.WebApi.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Ambassador.Sales.Test.WebApi.Controllers
{
    public class ExpiredGarmentBookingOrderControllerTest : BaseControllerTest<ExpiredGarmentBookingOrderController, GarmentBookingOrder, GarmentBookingOrderViewModel, IExpiredGarmentBookingOrder>
    {
        [Fact]
        public void Get_WithoutException_ReturnOK_readExpired()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.ReadExpired(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new ReadResponse<GarmentBookingOrder>(new List<GarmentBookingOrder>(), 0, new Dictionary<string, string>(), new List<string>()));
            mocks.Mapper.Setup(f => f.Map<List<GarmentBookingOrderViewModel>>(It.IsAny<List<GarmentBookingOrder>>())).Returns(this.ViewModels);
            var controller = GetController(mocks);
            var response = controller.Get(1, 25, new List<string>(), "{}", "null", "{}");
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Get_Exception_ReturnInternalServer_readExpired()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.ReadExpired(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new Exception());
            mocks.Mapper.Setup(f => f.Map<List<GarmentBookingOrderViewModel>>(It.IsAny<List<GarmentBookingOrder>>())).Returns(this.ViewModels);
            var controller = GetController(mocks);
            var response = controller.Get(1, 25, new List<string>(), "{}", "null", "{}");
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void ExpiredBOPost_WithoutException_ReturnNoContent()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.BOCancelExpired(It.IsAny<List<GarmentBookingOrder>>(), It.IsAny<string>()))
                .Returns(1);
            mocks.Mapper.Setup(f => f.Map<GarmentBookingOrder>(It.IsAny<GarmentBookingOrderViewModel>())).Returns(this.Model);
            //mocks.Mapper.Setup(f => f.Map<List<GarmentBookingOrderViewModel>>(It.IsAny<List<GarmentBookingOrder>>())).Returns(this.ViewModels);
            var controller = GetController(mocks);
            var response = controller.ExpiredBoPost(new List<GarmentBookingOrderViewModel>());
            Assert.Equal((int)HttpStatusCode.NoContent, GetStatusCode(response));
        }

        [Fact]
        public void ExpiredBOPost_Exception_ReturnInternalServer()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.BOCancelExpired(It.IsAny<List<GarmentBookingOrder>>(), It.IsAny<string>()))
                .Throws(new Exception());
            mocks.Mapper.Setup(f => f.Map<List<GarmentBookingOrderViewModel>>(It.IsAny<List<GarmentBookingOrder>>())).Returns(this.ViewModels);
            //mocks.Mapper.Setup(f => f.Map<List<GarmentBookingOrderViewModel>>(It.IsAny<List<GarmentBookingOrder>>())).Returns(this.ViewModels);
            var controller = GetController(mocks);
            var response = controller.ExpiredBoPost(new List<GarmentBookingOrderViewModel>());
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

    }
}
