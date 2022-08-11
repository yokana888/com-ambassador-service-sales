using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.ROGarmentLogics;
using Com.Ambassador.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Ambassador.Service.Sales.Lib.Models.ROGarments;
using Com.Ambassador.Service.Sales.Lib.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Ambassador.Sales.Test.BussinesLogic.Logic.ROGarmentLogics
{
    public class ROGarmentLogicTest
    {
        private const string ENTITY = "RO_Garment_SizeBreakdowns";
        [MethodImpl(MethodImplOptions.NoInlining)]
        public string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return string.Concat(sf.GetMethod().Name, "_", ENTITY);
        }

        private SalesDbContext _dbContext(string testName)
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            DbContextOptionsBuilder<SalesDbContext> optionsBuilder = new DbContextOptionsBuilder<SalesDbContext>();
            optionsBuilder
                .UseInMemoryDatabase(testName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .UseInternalServiceProvider(serviceProvider);


            SalesDbContext dbContext = new SalesDbContext(optionsBuilder.Options);

            return dbContext;
        }

        public Mock<IServiceProvider> GetServiceProvider(string testname)
        {
            IIdentityService identityService = new IdentityService { Username = "Username", Token = "Token Test" };
            var serviceProvider = new Mock<IServiceProvider>();

            serviceProvider
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            serviceProvider.Setup(s => s.GetService(typeof(SalesDbContext)))
                .Returns(_dbContext(testname));

            ROGarmentSizeBreakdownLogic rOGarmentSizeBreakdown = new ROGarmentSizeBreakdownLogic(serviceProvider.Object, identityService, _dbContext(testname));
           
            serviceProvider.Setup(s => s.GetService(typeof(ROGarmentSizeBreakdownLogic)))
               .Returns(rOGarmentSizeBreakdown);

            return serviceProvider;
        }

        [Fact]
        public void UpdateAsync_Return_Success()
        {
            string testName = GetCurrentMethod();
            var dbContext = _dbContext(testName);
            IIdentityService identityService = new IdentityService { Username = "Username" };
            var model = new RO_Garment()
            {
                Code = "Code",
                RO_Garment_SizeBreakdowns=new List<RO_Garment_SizeBreakdown>()
                {
                    new RO_Garment_SizeBreakdown()
                    {
                        Id=1,
                        ColorName ="red"
                    },
                  
                },
                CostCalculationGarment =new CostCalculationGarment(),
            };

            dbContext.RO_Garments.Add(model);
            dbContext.SaveChanges();
            ROGarmentLogic unitUnderTest = new ROGarmentLogic(GetServiceProvider(testName).Object, identityService, dbContext);

            unitUnderTest.UpdateAsync(model.Id, model);
        }

        


    }
}
