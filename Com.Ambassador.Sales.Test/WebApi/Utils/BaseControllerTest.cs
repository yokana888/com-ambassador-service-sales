using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseInterface;
using Com.Ambassador.Service.Sales.WebApi.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;

namespace Com.Ambassador.Sales.Test.WebApi.Utils
{
    public abstract class BaseControllerTest<TController, TModel, TViewModel, IFacade>
        where TController : BaseController<TModel, TViewModel, IFacade>
        where TModel : BaseModel, new()
        where TViewModel : BaseViewModel, IValidatableObject, new()
        where IFacade : class, IBaseFacade<TModel>
    {
        protected virtual TModel Model
        {
            get { return new TModel(); }
        }

        protected virtual TViewModel ViewModel
        {
            get { return new TViewModel(); }
        }

        protected virtual List<TViewModel> Models
        {
            get { return new List<TViewModel>(); }
        }

        protected virtual List<TViewModel> ViewModels
        {
            get { return new List<TViewModel>(); }
        }

        protected ServiceValidationException GetServiceValidationException()
        {
            Mock<IServiceProvider> serviceProvider = new Mock<IServiceProvider>();
            List<ValidationResult> validationResults = new List<ValidationResult>();
            System.ComponentModel.DataAnnotations.ValidationContext validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(this.ViewModel, serviceProvider.Object, null);
            return new ServiceValidationException(validationContext, validationResults);
        }

        protected (Mock<IIdentityService> IdentityService, Mock<IValidateService> ValidateService, Mock<IFacade> Facade, Mock<IMapper> Mapper, Mock<IServiceProvider> ServiceProvider) GetMocks()
        {
            return (IdentityService: new Mock<IIdentityService>(), ValidateService: new Mock<IValidateService>(), Facade: new Mock<IFacade>(), Mapper: new Mock<IMapper>(), ServiceProvider: new Mock<IServiceProvider>());
        }


        protected virtual TController GetController((Mock<IIdentityService> IdentityService, Mock<IValidateService> ValidateService, Mock<IFacade> Facade, Mock<IMapper> Mapper, Mock<IServiceProvider> ServiceProvider) mocks)
        {
            var user = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            user.Setup(u => u.Claims).Returns(claims);
            mocks.ServiceProvider.Setup(s => s.GetService(typeof(IHttpClientService))).Returns(new HttpClientTestService());
            TController controller = (TController)Activator.CreateInstance(typeof(TController), mocks.IdentityService.Object, mocks.ValidateService.Object, mocks.Facade.Object, mocks.Mapper.Object, mocks.ServiceProvider.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = user.Object
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["Authorization"] = "Bearer unittesttoken";
            controller.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");
            return controller;
        }

        protected int GetStatusCode(IActionResult response)
        {
            return (int)response.GetType().GetProperty("StatusCode").GetValue(response, null);
        }

        private int GetStatusCodeGet((Mock<IIdentityService> IdentityService, Mock<IValidateService> ValidateService, Mock<IFacade> Facade, Mock<IMapper> Mapper, Mock<IServiceProvider> ServiceProvider) mocks)
        {
            TController controller = this.GetController(mocks);
            IActionResult response = controller.Get();

            return this.GetStatusCode(response);
        }

        [Fact]
        public virtual void Get_WithoutException_ReturnOK()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new ReadResponse<TModel>(new List<TModel>(), 0, new Dictionary<string, string>(), new List<string>()));
            mocks.Mapper.Setup(f => f.Map<List<TViewModel>>(It.IsAny<List<TModel>>())).Returns(this.ViewModels);
            
            int statusCode = this.GetStatusCodeGet(mocks);
            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void Get_ReadThrowException_ReturnInternalServerError()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());

            int statusCode = this.GetStatusCodeGet(mocks);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        private async Task<int> GetStatusCodePost((Mock<IIdentityService> IdentityService, Mock<IValidateService> ValidateService, Mock<IFacade> Facade, Mock<IMapper> Mapper, Mock<IServiceProvider> ServiceProvider) mocks)
        {
            TController controller = this.GetController(mocks);
            IActionResult response = await controller.Post(this.ViewModel);

            return this.GetStatusCode(response);
        }

        [Fact]
        public async Task Post_WithoutException_ReturnCreated()
        {
            var mocks = this.GetMocks();
            mocks.ValidateService.Setup(s => s.Validate(It.IsAny<TViewModel>())).Verifiable();
            mocks.Facade.Setup(s => s.CreateAsync(It.IsAny<TModel>())).ReturnsAsync(1);

            int statusCode = await this.GetStatusCodePost(mocks);
            Assert.Equal((int)HttpStatusCode.Created, statusCode);
        }

        [Fact]
        public async Task Post_ThrowServiceValidationExeption_ReturnBadRequest()
        {
            var mocks = this.GetMocks();
            mocks.ValidateService.Setup(s => s.Validate(It.IsAny<TViewModel>())).Throws(this.GetServiceValidationException());

            int statusCode = await this.GetStatusCodePost(mocks);
            Assert.Equal((int)HttpStatusCode.BadRequest, statusCode);
        }

        [Fact]
        public async Task Post_ThrowException_ReturnInternalServerError()
        {
            var mocks = this.GetMocks();
            mocks.ValidateService.Setup(s => s.Validate(It.IsAny<TViewModel>())).Verifiable();
            mocks.Facade.Setup(s => s.CreateAsync(It.IsAny<TModel>())).ThrowsAsync(new Exception());

            int statusCode = await this.GetStatusCodePost(mocks);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        private async Task<int> GetStatusCodeGetById((Mock<IIdentityService> IdentityService, Mock<IValidateService> ValidateService, Mock<IFacade> Facade, Mock<IMapper> Mapper, Mock<IServiceProvider> ServiceProvider) mocks)
        {
            TController controller = this.GetController(mocks);
            IActionResult response = await controller.GetById(1);

            return this.GetStatusCode(response);
        }

        [Fact]
        public virtual async Task GetById_NotNullModel_ReturnOK()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(this.Model);
            mocks.Mapper.Setup(f => f.Map<TViewModel>(It.IsAny<TModel>())).Returns(this.ViewModel);
            
            int statusCode = await this.GetStatusCodeGetById(mocks);
            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public virtual async Task GetById_NullModel_ReturnNotFound()
        {
            var mocks = this.GetMocks();
            mocks.Mapper.Setup(f => f.Map<TViewModel>(It.IsAny<TModel>())).Returns(this.ViewModel);
            mocks.Facade.Setup(f => f.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync((TModel)null);

            int statusCode = await this.GetStatusCodeGetById(mocks);
            Assert.Equal((int)HttpStatusCode.NotFound, statusCode);
        }

        [Fact]
        public virtual async Task GetById_ThrowException_ReturnInternalServerError()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.ReadByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception());

            int statusCode = await this.GetStatusCodeGetById(mocks);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        private async Task<int> GetStatusCodePut((Mock<IIdentityService> IdentityService, Mock<IValidateService> ValidateService, Mock<IFacade> Facade, Mock<IMapper> Mapper, Mock<IServiceProvider> ServiceProvider) mocks, int id, TViewModel viewModel)
        {
            TController controller = this.GetController(mocks);
            IActionResult response = await controller.Put(id, viewModel);

            return this.GetStatusCode(response);
        }

        [Fact]
        public async Task Put_InvalidId_ReturnBadRequest()
        {
            var mocks = this.GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<TViewModel>())).Verifiable();
            var id = 1;
            var viewModel = new TViewModel()
            {
                Id = id + 1
            };

            int statusCode = await this.GetStatusCodePut(mocks, id, viewModel);
            Assert.Equal((int)HttpStatusCode.BadRequest, statusCode);
        }

        [Fact]
        public async Task Put_ValidId_ReturnNoContent()
        {
            var mocks = this.GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<TViewModel>())).Verifiable();
            var id = 1;
            var viewModel = new TViewModel()
            {
                Id = id
            };
            mocks.Mapper.Setup(m => m.Map<TViewModel>(It.IsAny<TModel>())).Returns(viewModel);
            mocks.Facade.Setup(f => f.UpdateAsync(It.IsAny<int>(), It.IsAny<TModel>())).ReturnsAsync(1);

            int statusCode = await this.GetStatusCodePut(mocks, id, viewModel);
            Assert.Equal((int)HttpStatusCode.NoContent, statusCode);
        }

        [Fact]
        public async Task Put_ThrowServiceValidationExeption_ReturnBadRequest()
        {
            var mocks = this.GetMocks();
            mocks.ValidateService.Setup(s => s.Validate(It.IsAny<TViewModel>())).Throws(this.GetServiceValidationException());

            int statusCode = await this.GetStatusCodePut(mocks, 1, this.ViewModel);
            Assert.Equal((int)HttpStatusCode.BadRequest, statusCode);
        }

        [Fact]
        public async Task Put_ThrowException_ReturnInternalServerError()
        {
            var mocks = this.GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<TViewModel>())).Verifiable();
            var id = 1;
            var viewModel = new TViewModel()
            {
                Id = id
            };
            mocks.Mapper.Setup(m => m.Map<TViewModel>(It.IsAny<TModel>())).Returns(viewModel);
            mocks.Facade.Setup(f => f.UpdateAsync(It.IsAny<int>(), It.IsAny<TModel>())).ThrowsAsync(new Exception());

            int statusCode = await this.GetStatusCodePut(mocks, id, viewModel);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        [Fact]
        public async Task Put_ThrowsDbUpdateConcurrencyException()
        {
            var mocks = this.GetMocks();
            mocks.ValidateService.Setup(vs => vs.Validate(It.IsAny<TViewModel>())).Verifiable();
            var id = 1;
            var viewModel = new TViewModel()
            {
                Id = id
            };
            mocks.Mapper.Setup(m => m.Map<TViewModel>(It.IsAny<TModel>())).Returns(viewModel);

            Mock<IUpdateEntry> updateEntry = new Mock<IUpdateEntry>();
            List<IUpdateEntry> listData = new List<IUpdateEntry>()
            {
                updateEntry.Object
            };

            IReadOnlyList<IUpdateEntry> readOnlyData = listData.AsReadOnly();


            mocks.Facade.Setup(f => f.UpdateAsync(It.IsAny<int>(), It.IsAny<TModel>())).ThrowsAsync(new DbUpdateConcurrencyException("errorMessage",readOnlyData));

            int statusCode = await this.GetStatusCodePut(mocks, id, viewModel);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        private async Task<int> GetStatusCodeDelete((Mock<IIdentityService> IdentityService, Mock<IValidateService> ValidateService, Mock<IFacade> Facade, Mock<IMapper> Mapper, Mock<IServiceProvider> ServiceProvider) mocks)
        {
            TController controller = this.GetController(mocks);
            IActionResult response = await controller.Delete(1);
            return this.GetStatusCode(response);
        }

        [Fact]
        public async Task Delete_WithoutException_ReturnNoContent()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.DeleteAsync(It.IsAny<int>())).ReturnsAsync(1);

            int statusCode = await this.GetStatusCodeDelete(mocks);
            Assert.Equal((int)HttpStatusCode.NoContent, statusCode);
        }

        [Fact]
        public async Task Delete_ThrowException_ReturnInternalStatusError()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.DeleteAsync(It.IsAny<int>())).ThrowsAsync(new Exception());

            int statusCode = await this.GetStatusCodeDelete(mocks);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }
    }
}
