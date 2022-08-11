using AutoMapper;
using Com.Ambassador.Sales.Test.BussinesLogic.DataUtils.DOStock;
using Com.Ambassador.Sales.Test.BussinesLogic.Utils;
using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.AutoMapperProfiles.DOStockProfiles;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.DOStock;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.DOStock;
using Com.Ambassador.Service.Sales.Lib.Models.DOSales;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.DOStock;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Ambassador.Sales.Test.BussinesLogic.Facades.DOStock
{
    public class DOStockFacadeTest : BaseFacadeTest<SalesDbContext, DOStockFacade, DOStockLogic, DOSalesModel, DOStockDataUtil>
    {
        private const string ENTITY = "DOStock";
        public DOStockFacadeTest() : base(ENTITY)
        {
        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DOStockMapper>();
            });
            var mapper = configuration.CreateMapper();

            DOStockViewModel vm = new DOStockViewModel
            {
                Id = 1,
                DOStockItems = new List<DOStockItemViewModel>()
                {
                    new DOStockItemViewModel()
                }
            };
            DOSalesModel model = mapper.Map<DOSalesModel>(vm);

            Assert.Equal(vm.Id, model.Id);

            var vm2 = mapper.Map<DOStockViewModel>(model);

            Assert.Equal(vm2.Id, model.Id);
        }

        [Fact]
        public virtual void ValidateVM()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            DOStockFacade facade = new DOStockFacade(serviceProvider, dbContext);

            var data = new DOStockViewModel();
            var validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(data));

            data.DOStockItems = new List<DOStockItemViewModel>()
            {
                new DOStockItemViewModel()
            };

            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(data));
        }

        
    }
}
