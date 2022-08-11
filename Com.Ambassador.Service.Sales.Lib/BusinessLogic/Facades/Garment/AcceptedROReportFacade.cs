using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.Garment;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.Garment;
using Com.Ambassador.Service.Sales.Lib.Helpers;
using Com.Ambassador.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.ViewModels.Garment;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.Garment
{
    public class AcceptedROReportFacade : IAcceptedROReportFacade
    {
        private AcceptedROReportLogic logic;
        private IIdentityService identityService;

        public AcceptedROReportFacade(IServiceProvider serviceProvider)
        {
            logic = serviceProvider.GetService<AcceptedROReportLogic>();
            identityService = serviceProvider.GetService<IIdentityService>();
        }

        public Tuple<MemoryStream, string> GenerateExcel(string filter = "{}")
        {
            var Query = logic.GetQuery(filter);
            var data = GetData(Query.ToList());

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Tanggal Terima", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "No RO", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Artikel", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Kode Buyer", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Nama Buyer", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Tanggal Shipment", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Quantity", DataType = typeof(double) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "User Penerima", DataType = typeof(string) });

            if (data != null && data.Count > 0)
            {
                foreach (var d in data)
                {
                    dataTable.Rows.Add(d.AcceptedDate.ToString("dd MMMM yyyy", new CultureInfo("id-ID")), d.RONo, d.Article, d.Buyer, d.BuyerName, d.DeliveryDate.ToString("dd MMMM yyyy", new CultureInfo("id-ID")), d.Quantity, d.Uom, d.AcceptedBy);
                }
            }
            else
            {
                dataTable.Rows.Add(null, null, null, null, null, null, null, null, null);
            }

            var excel = Excel.CreateExcel(new List<KeyValuePair<DataTable, string>> { new KeyValuePair<DataTable, string>(dataTable, "AcceptedRO") }, true);

            return Tuple.Create(excel, string.Concat("Laporan Penerimaan RO", GetSuffixNameFromFilter(filter)));
        }

        private string GetSuffixNameFromFilter(string filterString)
        {
            Dictionary<string, object> filter = JsonConvert.DeserializeObject<Dictionary<string, object>>(filterString);

            return string.Join(null, filter.Select(s => string.Concat(" - ", s.Value is string ? s.Value.ToString() : ((DateTime)s.Value).AddHours(identityService.TimezoneOffset).ToString("dd MMMM yyyy") )).ToArray());
        }

        public Tuple<List<AcceptedROReportViewModel>, int> Read(int page = 1, int size = 25, string filter = "{}")
        {
            var Query = logic.GetQuery(filter);
            var data = GetData(Query.ToList());

            return Tuple.Create(data, data.Count);
        }

        private List<AcceptedROReportViewModel> GetData(IEnumerable<CostCalculationGarment> CostCalculationGarments)
        {
            var data = CostCalculationGarments.Select(cc => new AcceptedROReportViewModel
            {
                AcceptedDate = cc.ValidationSampleDate.ToOffset(TimeSpan.FromHours(identityService.TimezoneOffset)).DateTime,
                RONo = cc.RO_Number,
                Article = cc.Article,
                Buyer = cc.BuyerBrandCode,
                BuyerName = cc.BuyerBrandName,
                DeliveryDate = cc.DeliveryDate.ToOffset(TimeSpan.FromHours(identityService.TimezoneOffset)).DateTime,
                Quantity = cc.Quantity,
                Uom = cc.UOMUnit,
                AcceptedBy = cc.ValidationSampleBy
            }).ToList();

            return data;
        }
    }
}
