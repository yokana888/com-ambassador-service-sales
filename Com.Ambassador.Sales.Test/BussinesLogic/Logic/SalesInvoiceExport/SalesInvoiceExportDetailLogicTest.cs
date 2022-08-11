using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.SalesInvoiceExport;
using Com.Ambassador.Service.Sales.Lib.Models.SalesInvoiceExport;
using Com.Ambassador.Service.Sales.Lib.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.BussinesLogic.Logic.SalesInvoiceExport
{
 public   class SalesInvoiceExportDetailLogicTest
    {
        private const string ENTITY = "SalesInvoiceExportDetailLogic";
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
            IIdentityService identityService = new IdentityService { Username = "Username", Token = "Token Test" };
            var serviceProvider = new Mock<IServiceProvider>();

            serviceProvider
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            serviceProvider.Setup(s => s.GetService(typeof(SalesDbContext)))
                .Returns(_dbContext(testname));

            return serviceProvider;
        }

        [Fact]
        public void Read_Return_Success()
        {
            string testName = GetCurrentMethod();
            var dbContext = _dbContext(testName);
            IIdentityService identityService = new IdentityService { Username = "Username" };
            var model = new SalesInvoiceExportDetailModel()
            {
                Description = "Description"

            };

            dbContext.SalesInvoiceExportDetails.Add(model);
            dbContext.SaveChanges();
            SalesInvoiceExportDetailLogic unitUnderTest = new SalesInvoiceExportDetailLogic(GetServiceProvider(testName).Object, identityService, dbContext);

            var result = unitUnderTest.Read(1, 1, "{}", new List<string>() { "" }, null, "{}");
            Assert.True(0 < result.Data.Count);
            Assert.NotEmpty(result.Data);
        }
    }
}
