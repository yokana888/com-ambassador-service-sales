using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.LocalMerchandiserInterfaces;
using Com.Ambassador.Service.Sales.Lib.ViewModels.CostCalculationGarment;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel.LocalMerchandiserViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.LocalMerchandiserFacades
{
    public class HOrderFacade : IHOrderFacade
    {
        private readonly ILocalMerchandiserDbContext dbContext;

        public HOrderFacade(ILocalMerchandiserDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<string> GetKodeByNo(string no = null)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("no", no));

            var reader = dbContext.ExecuteReader("SELECT Kode FROM HOrder WHERE No=@no", parameters);

            List<string> data = new List<string>();

            while (reader.Read())
            {
                data.Add(reader.GetString(0));
            }

            return data;
        }

        public List<HOrderDataForProductionReportViewModel> GetDataForProductionReportByNo(string ro)
        {
            var listRO = ro.Split(",").Where(w => !string.IsNullOrWhiteSpace(w)).Distinct().ToArray();

            string cmdText = "SELECT No, Codeby, Sh_Cut, Kode, Qty FROM HOrder WHERE No in ({0})";

            List<SqlParameter> parameters = new List<SqlParameter>();

            for (int i = 0; i < listRO.Length; i++)
            {
                parameters.Add(new SqlParameter("@ro" + i, listRO[i]));
            }

            string inClause = string.Join(", ", parameters.Select(s => s.ParameterName));

            List<HOrderDataForProductionReportViewModel> data = new List<HOrderDataForProductionReportViewModel>();

            if (parameters.Count > 0)
            {
                var reader = dbContext.ExecuteReader(string.Format(cmdText, inClause), parameters);

                while (reader.Read())
                {
                    data.Add(new HOrderDataForProductionReportViewModel
                    {
                        No = reader.GetString(0),
                        Codeby = reader.GetString(1),
                        Sh_Cut = reader.GetDecimal(2),
                        Kode = reader.GetString(3),
                        Qty = reader.GetDecimal(4),
                    });
                }
            }

            return data;
        }
    }
}
