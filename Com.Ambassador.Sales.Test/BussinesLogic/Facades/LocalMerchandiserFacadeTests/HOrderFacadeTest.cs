using Com.Ambassador.Service.Sales.Lib;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.LocalMerchandiserFacades;
using Com.Ambassador.Service.Sales.WebApi.Controllers.LocalMerchandiserControllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using Xunit;

namespace Com.Ambassador.Sales.Test.BussinesLogic.Facades.LocalMerchandiserFacadeTests
{
    public class HOrderFacadeTest
    {
        [Fact]
        public void Get_Kode_By_No_Success()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Kode", typeof(string));
            dataTable.Rows.Add("KODE");

            Mock<ILocalMerchandiserDbContext> mockDbContext = new Mock<ILocalMerchandiserDbContext>();
            mockDbContext.Setup(s => s.ExecuteReader(It.IsAny<string>(), It.IsAny<List<SqlParameter>>()))
                .Returns(dataTable.CreateDataReader());

            HOrderFacade facade = new HOrderFacade(mockDbContext.Object);

            var result = facade.GetKodeByNo();
            Assert.NotEmpty(result);
        }

        [Fact]
        public void Get_Kode_By_No_Error()
        {
            Mock<ILocalMerchandiserDbContext> mockDbContext = new Mock<ILocalMerchandiserDbContext>();
            mockDbContext.Setup(s => s.ExecuteReader(It.IsAny<string>(), It.IsAny<List<SqlParameter>>()))
                .Throws(new Exception("Error ExecuteReader"));

            HOrderFacade facade = new HOrderFacade(mockDbContext.Object);

            var result = Assert.ThrowsAny<Exception>(() => facade.GetKodeByNo());
            Assert.NotNull(result);
        }

        [Fact]
        public void Get_Data_For_ProductionReport_By_No_Success()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("No", typeof(string));
            dataTable.Columns.Add("Codeby", typeof(string));
            dataTable.Columns.Add("Sh_Cut", typeof(decimal));
            dataTable.Columns.Add("Kode", typeof(string));
            dataTable.Columns.Add("Qty", typeof(decimal));
            dataTable.Rows.Add("No", "Codeby", 5, "Kode", 10);

            Mock<ILocalMerchandiserDbContext> mockDbContext = new Mock<ILocalMerchandiserDbContext>();
            mockDbContext.Setup(s => s.ExecuteReader(It.IsAny<string>(), It.IsAny<List<SqlParameter>>()))
                .Returns(dataTable.CreateDataReader());

            HOrderFacade facade = new HOrderFacade(mockDbContext.Object);

            var result = facade.GetDataForProductionReportByNo("No");
            Assert.NotEmpty(result);
        }

        [Fact]
        public void Get_Data_For_ProductionReport_By_No_Error()
        {
            Mock<ILocalMerchandiserDbContext> mockDbContext = new Mock<ILocalMerchandiserDbContext>();
            mockDbContext.Setup(s => s.ExecuteReader(It.IsAny<string>(), It.IsAny<List<SqlParameter>>()))
                .Throws(new Exception("Error ExecuteReader"));

            HOrderFacade facade = new HOrderFacade(mockDbContext.Object);

            var result = Assert.ThrowsAny<Exception>(() => facade.GetDataForProductionReportByNo("No"));
            Assert.NotNull(result);
        }

        [Fact]
        public void Create_Connection_Error()
        {
            var result = Assert.ThrowsAny<Exception>(() => new LocalMerchandiserDbContext(""));
            Assert.NotNull(result);
        }
    }
}
