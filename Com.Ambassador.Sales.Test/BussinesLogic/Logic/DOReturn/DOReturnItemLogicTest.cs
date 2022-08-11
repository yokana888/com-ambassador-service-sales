using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.DOReturn;
using Com.Ambassador.Service.Sales.Lib.Models.DOReturn;
using Com.Ambassador.Service.Sales.Lib.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Xunit;

namespace Com.Ambassador.Sales.Test.BussinesLogic.Logic.DOReturn
{
    public class DOReturnItemLogicTest
    {
        private const string ENTITY = "DOReturnItemLogic";

        [MethodImpl(MethodImplOptions.NoInlining)]
        public string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return string.Concat(sf.GetMethod().Name, "_", ENTITY);
        }

        private SalesDbContext _dbContext(string testName)
        {
            DbContextOptionsBuilder<SalesDbContext> optionsBuilder = new DbContextOptionsBuilder<SalesDbContext>();
            optionsBuilder
                .UseInMemoryDatabase(testName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            SalesDbContext dbContext = new SalesDbContext(optionsBuilder.Options);

            return dbContext;
        }

        public Mock<IServiceProvider> GetServiceProvider(string testname)
        {
            IIdentityService identityService = new IdentityService { Username = "Username" };
            var serviceProvider = new Mock<IServiceProvider>();

            serviceProvider
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            serviceProvider.Setup(s => s.GetService(typeof(SalesDbContext)))
                .Returns(_dbContext(testname));

            return serviceProvider;
        }

        [Fact]
        public void Read_With_EmptyKeyword_Return_Success()
        {
            string testName = GetCurrentMethod();
            var dbContext = _dbContext(testName);
            IIdentityService identityService = new IdentityService { Username = "Username" };
            DOReturnItemLogic unitUnderTest = new DOReturnItemLogic(GetServiceProvider(testName).Object, identityService, dbContext);
            dbContext.DOReturnItems.Add(new DOReturnItemModel()
            {
                ProductName = "ProductName",
                Active = true,
                CreatedBy = "someone",
                ProductCode = "ProductCode",
                UId = "1",
                CreatedUtc = DateTime.UtcNow,
                LastModifiedBy = "someone",
                PackingUom = "",
                CreatedAgent = "CreatedAgent",
                DeletedAgent = "DeletedAgent",
                IsDeleted = false,
                LastModifiedUtc = DateTime.UtcNow,
                LastModifiedAgent = "LastModifiedAgent"

            }); 

            var result = unitUnderTest.Read(1, 1, "{}", new List<string>() { "" }, null, "{}");
            Assert.NotNull(result);
        }
    }
}
