using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.Utilities.BaseInterface
{
    public interface IBaseMonitoringFacade<TViewModel>
    {
        Tuple<List<TViewModel>, int> Read(int page = 1, int size = 25, string filter = "{}");
        Tuple<MemoryStream, string> GenerateExcel(string filter = "{}");
    }
}
